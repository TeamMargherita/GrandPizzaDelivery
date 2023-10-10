using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 한석호 작성

public class FieldOfViewPoliceCar : MonoBehaviour, IGetBool
{
	public float ViewRadius;	// 시야 반지름 길이
	[Range(0,360)]
	public float ViewAngle; // 시야 각도 크기

	public LayerMask TargetMask;
	public LayerMask ObstacleMask;

	[HideInInspector]
	public List<Transform> visibleTargets = new List<Transform>();

	private Coroutine findTargetsWithDelayCoroutine;

	public float MeshResolution;
	public float EdgeDstThreshold;
	public int EdgeResolveIterations;

	public MeshFilter ViewMeshFilter;
	
	private Mesh viewMesh;

	private bool isFindPlayer = false;

	private void Start()
	{
		viewMesh = new Mesh();
		viewMesh.name = "View Mesh";
		ViewMeshFilter.mesh = viewMesh;

		findTargetsWithDelayCoroutine = StartCoroutine(FindTargetsWithDelay());
	}
	private void LateUpdate()
	{
		DrawFieldOfView();
	}
	private IEnumerator FindTargetsWithDelay()
	{
		while(true)
		{
			yield return Constant.OneTime;
			yield return Constant.OneTime;
			isFindPlayer = FindVisibleTargets();
		}
	}
	/// <summary>
	/// 목표물을 찾는 함수.목표물엔 TargetLayer가 설정되어있다.
	/// </summary>
	private bool FindVisibleTargets()
	{
		//visibleTargets.Clear();
		Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, ViewRadius, TargetMask);

		for (int i = 0; i < targetsInViewRadius.Length; i++)
		{
			Transform target = targetsInViewRadius[i].transform;	// 목표물의 transform
			Vector3 dirToTarget = (target.position - transform.position).normalized;	// 해당 물체 기준 목표물이 있는 방향
			if (Vector3.Angle(transform.right, dirToTarget) < ViewAngle / 2)
			{
				// 해당 물체와 목표물과의 거리
				float dstToTarget = Vector3.Distance(transform.position, target.position);
				
				// 물체를 향해 레이를 발사했을 때 방해물 레이어(ObstacleMask)가 달린 오브젝트가 없는 경우에만 visibleTargets에 추가함 
				//Debug.DrawRay(transform.position, dirToTarget * dstToTarget,Color.green);
				if (!Physics2D.Raycast (transform.position, dirToTarget, dstToTarget, ObstacleMask))
				{
					//visibleTargets.Add(target);
					return true;
				}
			}
		}
		return false;
	}
	/// <summary>
	/// FieldOfView를 그려준다.
	/// </summary>
	private void DrawFieldOfView()
	{
		// 라인 개수
		int stepCount = Mathf.RoundToInt(ViewAngle * MeshResolution);
		// 라인 개수에 따른 일정한 각도
		float stepAngleSize = ViewAngle / stepCount;
		// 삼각형을 만들 정점들을 저장
		List<Vector3> viewPoints = new List<Vector3>();
		ViewCastInfo oldViewCast = new ViewCastInfo();
		for (int i = 0; i < stepCount; i++)
		{
			// transform.eulerAngles.z는 현재 차가 바라보는 각도. ViewAngle/2를 빼서 시야각 끝 부분을 표시.
			// 거기서부터 stepAngleSize * i를 더해주면 끝부분부터 일정한 간격으로 각도를 나눌 수 있음.
			float angle = -transform.eulerAngles.z - ViewAngle / 2 + stepAngleSize * i;
			//Debug.DrawLine(transform.position, transform.position + DirFromAngle(angle, true) * ViewRadius, Color.red);
			ViewCastInfo newViewCast = ViewCast(angle);

			if (i > 0)
			{
				bool edgeDstThresholdExceeded = Mathf.Abs(oldViewCast.Dst - newViewCast.Dst) > EdgeDstThreshold;
				if (oldViewCast.Hit != newViewCast.Hit || (oldViewCast.Hit && newViewCast.Hit && edgeDstThresholdExceeded))
				{
					// 모서리 위치에 대한 정보를 받아온다.
					EdgeInfo edge = FindEdge(oldViewCast, newViewCast);
					// viewPoints에 정보를 담는다. 삼각형을 만드는데 쓰일 것이다.
					if (edge.PointA != Vector3.zero)
					{
						viewPoints.Add(edge.PointA);
					}
					if (edge.PointB != Vector3.zero)
					{
						viewPoints.Add(edge.PointB);
					}
				}
			}

			viewPoints.Add(newViewCast.Point);
			oldViewCast = newViewCast;
		}

		int vertexCount = viewPoints.Count + 1;
		Vector3[] vertices = new Vector3[vertexCount];
		int[] triangles = new int[(vertexCount - 2) * 3];
		// vertices[0]은 삼각형들의 공통점.
		vertices[0] = Vector3.zero;
		for (int i = 0; i < vertexCount - 1; i++)
		{
			// 정점 별 위치를 저장
			vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);
			// 삼각형 정점을 저장. 삼각형은 정점이 세개니까 세개를 저장
			if (i < vertexCount - 2)
			{
				triangles[i * 3] = 0;
				triangles[i * 3 + 1] = i + 1;
				triangles[i * 3 + 2] = i + 2;
			}
		}

		viewMesh.Clear();
		viewMesh.vertices = vertices;
		viewMesh.triangles = triangles;
		viewMesh.RecalculateNormals();
	}
	/// <summary>
	/// 모서리를 찾는 함수
	/// </summary>
	/// <param name="minViewCast"> 장애물과 부딪혀서 얻게 된 레이캐스트 정보</param>
	/// <param name="maxViewCast"> 장애물과 부딪히지 않은 레이캐스트 정보</param>
	/// <returns></returns>
	private EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
	{
		float minAngle = minViewCast.Angle;
		float maxAngle = maxViewCast.Angle;
		Vector3 minPoint = Vector3.zero;
		Vector3 maxPoint = Vector3.zero;

		// minViewCast 와 maxViewCast 사이 어딘가에 존재할 모서리를 찾는다. 범위를 이진탐색으로 좁혀가며 찾는방법이다.
		for (int i = 0; i< EdgeResolveIterations; i++)
		{
			// 두 각도 사이의 중간을 잡는다.
			float angle = (minAngle + maxAngle) / 2;
			// 중간 각도에 대한 ViewCastInfo를 생성한다.
			ViewCastInfo newViewCast = ViewCast(angle);

			bool edgeDstThresholdExceeded = Mathf.Abs(minViewCast.Dst - newViewCast.Dst) > EdgeDstThreshold;

			// 여전히 해당 각도에 레이를 쐈을 시와 minViewCast의 방해물 정보가 같다면 min 값을 바꿔준다.
			// 방해물이 없다면 max 값을 바꿔준다.
			if (newViewCast.Hit == minViewCast.Hit && !edgeDstThresholdExceeded)
			{
				minAngle = angle;
				minPoint = newViewCast.Point;
			}
			else
			{
				maxAngle = angle;
				maxPoint = newViewCast.Point;
			}
		}
		// 좁힌 값을 반환한다.
		return new EdgeInfo(minPoint, maxPoint);
	}

	/// <summary>
	/// 받아온 각도로 레이를 쏴서 방해물이 있을 때와 없을 때를 구분하여 ViewCastInfo 구조체를 반환한다.
	/// </summary>
	/// <param name="globalAngle">받아온 각도</param>
	/// <returns></returns>
	private ViewCastInfo ViewCast(float globalAngle)
	{
		Vector3 dir = DirFromAngle(globalAngle, true);
		RaycastHit2D hit = Physics2D.Raycast(transform.position,dir,ViewRadius,ObstacleMask);
		
		// ObstacleMask가 붙은 오브젝트에 레이가 닿았을 때 조건 만족
		if (hit)
		{
			return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
		}
		else
		{
			return new ViewCastInfo(false, transform.position + dir * ViewRadius, ViewRadius, globalAngle);
		}
	}

	/// <summary>
	/// 각도에 따른 바라보는 방향을 반환함
	/// </summary>
	/// <param name="angleInDegrees"></param>
	/// <param name="angleIsGlobal"></param>
	/// <returns></returns>
	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
	{
		if (!angleIsGlobal)
		{
			// 오브젝트 각도에 따라 각도의 값을 수정해줌
			angleInDegrees -= transform.eulerAngles.z;
		}
		return new Vector3(Mathf.Sin((90 + angleInDegrees) * Mathf.Deg2Rad), Mathf.Cos((90 + angleInDegrees) * Mathf.Deg2Rad), 0);
	}
	/// <summary>
	/// 플레이어를 찾았는지 여부를 반환한다.
	/// </summary>
	/// <returns></returns>
	public bool GetBool()
	{
		return isFindPlayer;
	}

	public struct ViewCastInfo
	{
		public bool Hit;
		public Vector3 Point;
		public float Dst;
		public float Angle;

		public ViewCastInfo(bool hit, Vector3 point, float dst, float angle)
		{
			Hit = hit;
			Point = point;
			Dst = dst;
			Angle = angle;
		}
	}

	public struct EdgeInfo
	{
		public Vector3 PointA;
		public Vector3 PointB;

		public EdgeInfo(Vector3 pointA, Vector3 pointB)
		{
			PointA = pointA;
			PointB = pointB;
		}
	}


}
