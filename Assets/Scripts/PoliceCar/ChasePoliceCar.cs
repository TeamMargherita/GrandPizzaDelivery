using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 한석호 작성
public class ChasePoliceCar : MonoBehaviour, ISetTransform
{
    [Range (0f,10f)]
    public float Speed;
    /// <summary>
    /// 추격 경찰차 상태. SPUERCHASE는 거리 상관없이 무조건 플레이어 쫓아오는 상태
    /// </summary>
    private enum ChaserPoliceState { NONE, SPUERCHASE, AUTOMOVE};

    private Transform playerTrans;
    private Transform myTrans;
    private Coroutine stateCoroutine;

    private RaycastHit2D hit;
    
    private ChaserPoliceState chaserPoliceState = ChaserPoliceState.SPUERCHASE;
    
    /// <summary>
    /// 플레이어를 찾았는지 여부를 알기위한 인터페이스
    /// </summary>
    private IGetBool iGetBool;  
    private float time; // 현재 상태가 발동되고 있는 시간
    private float oldAngle = -999f; // 이전 프레임에서 추격경찰차와 플레이어의 각도 차이
	// Start is called before the first frame update
	private void Awake()
	{
        Speed = Random.Range(3f, 10f);
        myTrans = this.transform;
        iGetBool = this.GetComponent<IGetBool>();
	}
	void Start()
    {
        //stateCoroutine = StartCoroutine(stateMachine());
    }
    /// <summary>
    /// 추격 경찰차의 상태 변경을 담당
    /// </summary>
    /// <returns></returns>
    private IEnumerator stateMachine()
	{
        while(true)
		{
            // 플레이어와의 거리 상관없이 무조건 쫓아오는 상태임.
            if (chaserPoliceState == ChaserPoliceState.SPUERCHASE)
			{
                // 만약 


                yield return Constant.OneTime;
			}
		}
	}
    /// <summary>
    /// 무조건적으로 플레이어를 따라오는 상태이다.
    /// </summary>
    private void SpuerChase()
	{
        Debug.DrawLine(myTrans.position, myTrans.position + myTrans.right * 100f);
        Debug.DrawLine(myTrans.position, myTrans.position+ (playerTrans.position - myTrans.position).normalized * 100f);
        // 1.우선 경찰차를 플레이어 방향쪽으로 회전시킴
        // 경찰차가 바라보는 방향의 벡터
        Vector3 from = (myTrans.position + myTrans.right) - myTrans.position;
        // 경찰차에서 플레이어 쪽 방향의 벡터
        Vector3 to = (myTrans.position + (playerTrans.position - myTrans.position).normalized) - myTrans.position;
        // Spin이 false를 리턴하면 경찰차가 플레이어를 바라보게 함
        if (!Spin(GetAngle(from, to)))
		{
            // 경찰차에서 플레이어쪽을 바라보는 벡터
            Vector3 ve = playerTrans.position - myTrans.position;
            // 경찰차 기준 플레이어의 각도를 구함
            float angle = Mathf.Atan2(ve.y, ve.x) * Mathf.Rad2Deg;
            // 플레이어를 바라보게 각도를 바꿈
            myTrans.rotation = Quaternion.AngleAxis(angle, myTrans.forward);
        }
        // 2.경찰차가 바라보는 쪽으로 전진시킨다.
        Straight();
    }
    /// <summary>
    /// 두 점 사이의 각도를 구한다.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    private float GetAngle(Vector3 from, Vector3 to)
	{
        Vector3 v = from - (myTrans.position + new Vector3(1,0));
        return Vector3.SignedAngle(from, to, Vector3.forward);
	}
    private void AutoMove()
	{

	}
    /// <summary>
    /// 경찰차를 회전시킵니다. angle의 부호에 따라 회전방향이 다릅니다.
    /// angle의 부호가 바뀌는 순간이 오면(angle이 0 근처일떄) 목표를 바라보는 것으로 간주하고 false를 리턴. 회전을 멈춤
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    private bool Spin(float angle)
	{
        if ((oldAngle >= 0 && angle <= 0 && oldAngle != -999 && angle != 180) || (oldAngle <= 0 && angle >= 0 && oldAngle != -999 && angle != 180))
		{
            oldAngle = angle;
            return false;
        }
        else
		{
            myTrans.localEulerAngles += new Vector3(0, 0, (angle >= 0 ? 1 : -1) * Time.deltaTime * Speed * 5f);
        }
        oldAngle = angle;
        return true;
    }
    /// <summary>
    /// 경찰차를 바라보는 방향으로 이동시킨다.
    /// </summary>
    private void Straight()
	{
        myTrans.localPosition += myTrans.right * Time.deltaTime * Speed;
	}
    /// <summary>
    /// 플레이어의 트랜스폼을 가져옴. 위치를 가져와 추격하기 위함
    /// </summary>
    /// <param name="trans"></param>
    public void SetTransform(Transform trans)
	{
        playerTrans = trans;
	}
    /// <summary>
    /// 상태를 갱신하고 시간을 초기화시킴.
    /// </summary>
    /// <param name="state"></param>
    private void ResetState(ChaserPoliceState state)
	{
        chaserPoliceState = state;
        time = 0f;
	}

	private void FixedUpdate()
	{
        time += Time.deltaTime;

        switch (chaserPoliceState)
        {
            // 플레이어와의 거리 상관없이 무조건 쫓아오는 상태임.
            case ChaserPoliceState.SPUERCHASE:
                // 플레이어를 무조건 쫓아옴
                SpuerChase();
                // 만약 플레이어를 발견한다면 시간을 0초로 돌려서 SUPERCHASE 상태를 갱신.
                if (iGetBool.GetBool())
                {
                    ResetState(ChaserPoliceState.SPUERCHASE);
                }
                // 만약 10초가 지나면 자동주행모드로 바꿈
                if (time >= 1200f)
                {
                    ResetState(ChaserPoliceState.AUTOMOVE);
                }
                break;
            // 맵을 랜덤으로 돌아다니는 상태임
            case ChaserPoliceState.AUTOMOVE:
                AutoMove();
                // 만약 플레이어를 발견한다면 시간을 0초로 돌리고 SUPERCHASE 상태로 전환
                if (iGetBool.GetBool())
				{
                    ResetState(ChaserPoliceState.SPUERCHASE);
				}
                break;
        }
	}
}
