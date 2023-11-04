using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PoliceNS.PoliceStateNS;
using Gun;
// 한석호 작성
public class ChasePoliceCar : Police, ISetTransform, IUpdateCheckList
{
    public static bool isStop = false;  // 추격 경찰차 행동 제어 여부

    [Range (0f,10f)]
    public float Speed;

    [SerializeField] private GameObject[] colArr;
    [SerializeField] private Animator ani;

    private const int RIGHTUP = 0; // 앞
    private const int RIGHTDOWN = 1;    // 앞
    private const int LEFTUP = 2;   // 뒤
    private const int LEFTDOWN = 3; // 뒤
    private const int FRONT = 4;
    private const int BACK = 5;

    /// <summary>
    /// 추격 경찰차 상태. SPUERCHASE는 거리 상관없이 무조건 플레이어 쫓아오는 상태
    /// </summary>
    private enum MoveRoute { NONE, GO, GORIGHT, GOLEFT, BACK, BACKRIGHT, BACKLEFT, STOP};
    private Transform playerTrans;
    private Transform myTrans;
    private Rigidbody2D rigid;

    //private PoliceState chaserPoliceState = PoliceState.SPUERCHASE;
    private MoveRoute oldRoute = MoveRoute.NONE;    // 이전 프레임의 이동 방향
    /// <summary>
    /// 플레이어를 찾았는지 여부를 알기위한 인터페이스
    /// </summary>
    private IGetBool iGetBool;
    private ICheckCol[] iCheckColArr;
    private PoliceState temState;
    private PoliceGunShooting GunMethod;

    private Vector3 outVec = new Vector3(100, 30, 0);
    private Vector3 ranTarget = Vector3.one;
    private Color redEmi = new Color(255f/255f, 35/255f, 51f/255f, 79f/255f);
    private Color yellowEmi = new Color(255f/255f, 214f/255f, 35f/255f, 79f/255f);
    private Color greenEmi = new Color(35f/255f, 255f/255f, 78f/255f, 79f/255f);

    private MeshRenderer mesh;

    private List<int> colList = new List<int>();    // 감지된 콜라이더 리스트
    private float time; // 현재 상태가 발동되고 있는 시간
    private float oldAngle = -999f; // 이전 프레임에서 추격경찰차와 플레이어의 각도 차이
    private float autoAndStopTime = 0;   // 자동주행과 정지상태인 시간
    private bool isRigid = false;   // 리지드바디 제어 변수

    protected override void Awake()
    {
        base.Awake();
        PoliceHp = 500;
        policeState = PoliceState.SPUERCHASE;
        temState = PoliceState.NONE;
        Speed = Random.Range(3f, 10f);
        ranTarget = new Vector3(Random.Range(0, 70), Random.Range(0, 70), 0);
        myTrans = this.transform;
        rigid = this.GetComponent<Rigidbody2D>();
        mesh = this.transform.GetChild(0).GetComponent<MeshRenderer>();
        iGetBool = this.GetComponent<IGetBool>();
        iCheckColArr = new ICheckCol[colArr.Length];
        for (int i = 0; i < colArr.Length; i++)
		{
            iCheckColArr[i] = colArr[i].GetComponent<ICheckCol>();
            iCheckColArr[i].InitNumber(i, this);
		}
        GunMethod = new PoliceGunShooting(transform, "Police");
        GunMethod.ani = ani;
        GunMethod.ShootAudio = GetComponent<AudioSource>();
    }
    /// <summary>
    /// 무조건적으로 플레이어를 따라오는 상태이다.
    /// </summary>
    private void SpuerChase()
    {
        //Debug.DrawLine(myTrans.position, myTrans.position + myTrans.right * 100f);
        //Debug.DrawLine(myTrans.position, myTrans.position + (playerTrans.position - myTrans.position).normalized * 100f);
        
        // 주변에 방해물이 없을 시, 혹은 플레이어를 발견했을 시
        if (!CheckObstacle() || iGetBool.GetBool())
        {
            MoveToTarget((myTrans.position + (playerTrans.position - myTrans.position).normalized) - myTrans.position, playerTrans.position - myTrans.position);
        }
        // 주변에 방해물이 있을 시
        else
        {
            // 자동주행을 한다.
            Move();
        }
    }
    /// <summary>
    /// 자유롭게 맵을 주행한다.
    /// </summary>
    private void AutoMove()
    {

        if (Vector3.SqrMagnitude(ranTarget - myTrans.position) <= 1f + autoAndStopTime)
		{
            ranTarget = new Vector3(Random.Range(0, 70), Random.Range(0, 70));
            autoAndStopTime = 0;
		}

        // 주변에 방해물이 없을 시, 혹은 플레이어를 발견했을 시
        if (!CheckObstacle() || autoAndStopTime >= 5f)
        {
            MoveToTarget((ranTarget - myTrans.position).normalized, ranTarget - myTrans.position);
        }
        // 주변에 방해물이 있을 시
        else
        {
            // 자동주행을 한다.
            Move();
        }
    }
    /// <summary>
    /// 추격 경찰차는 맵 밖으로 나간다.
    /// </summary>
    private void OutMap()
	{
        //Debug.DrawLine(myTrans.position, myTrans.position + myTrans.right * 100f);
        //Debug.DrawLine(myTrans.position, outVec);

        // 주변에 방해물이 없을 시, 혹은 플레이어를 발견했을 시, 또는 OUTMAP상태가 된지 10초가 지나고 방해물이 있을 시
        if (!CheckObstacle() || iGetBool.GetBool() || (time >= 10f && CheckObstacle()))
        {
            MoveToTarget((outVec - myTrans.position).normalized, outVec - myTrans.position);
            if (time > 15f)
			{
                time = 0f;
			}
        }
        // 주변에 방해물이 있을 시
        else
        {
            // 자동주행을 한다.
            Move();
        }
    }
    /// <summary>
    /// 타겟을 향해 바라보고 이동한다.
    /// </summary>
    /// <param name="target"></param>
    /// <param name="fixTarget"></param>
    private void MoveToTarget(Vector3 target, Vector3 fixTarget)
	{
        // 경찰차를 target 방향쪽으로 회전시킴
        // 경찰차가 바라보는 방향의 벡터
        Vector3 from = (myTrans.position + myTrans.right) - myTrans.position;
        // 경찰차에서 플레이어 쪽 방향의 벡터
        Vector3 to = target;
        // Spin이 false를 리턴하면 경찰차가 타겟을 바라보게 함
        if (!SpinToTarget(GetAngle(from, to)))
        {
            // 경찰차에서 타겟쪽을 바라보는 벡터
            Vector3 ve = fixTarget;
            // 경찰차 기준 타겟의 각도를 구함
            float angle = Mathf.Atan2(ve.y, ve.x) * Mathf.Rad2Deg;
            // 타겟을 바라보게 각도를 바꿈
            myTrans.rotation = Quaternion.AngleAxis(angle, myTrans.forward);
        }
        // 경찰차가 바라보는 쪽으로 전진시킨다.
        Straight(1);
    }
    /// <summary>
    /// 이동을 해야할 시, 어떤 방향으로 이동할지 정한다.
    /// </summary>
    /// <returns></returns>
    private MoveRoute FindRoute()
	{
        // FRONT,RIGHTUP, RIGHTDOWN,BACK 모두 탐지되는 경우
        if (colList.FindIndex(a => a.Equals(FRONT)) != -1 && colList.FindIndex(a => a.Equals(BACK)) != -1 &&
                colList.FindIndex(a => a.Equals(RIGHTDOWN)) != -1 && colList.FindIndex(a => a.Equals(RIGHTUP)) != -1)
		{
            // 정지한다.
            if (autoAndStopTime < 1f)
            {
                oldRoute = MoveRoute.STOP;
            }
            else
			{
                oldRoute = MoveRoute.BACK;
			}
            return oldRoute;
		}
        // FRONT, RIGHTUP, RIGHTDOWN 모두 탐지되는 경우
        else if (colList.FindIndex(a => a.Equals(FRONT)) != -1 &&
                colList.FindIndex(a => a.Equals(RIGHTDOWN)) != -1 && colList.FindIndex(a => a.Equals(RIGHTUP)) != -1)
		{
            // 후진을 한다.
            // LEFTDOWN, LEFTUP도 막힌 경우
            if (colList.FindIndex(a => a.Equals(LEFTDOWN)) != -1 && colList.FindIndex(a => a.Equals(LEFTUP)) != -1)
			{
                // 뒤로 쭉 빠진다.
                oldRoute = MoveRoute.BACK;
                return oldRoute;
            }
            // LEFTDOWN만 막힌경우
            else if (colList.FindIndex(a => a.Equals(LEFTDOWN)) != -1)
			{
                if (oldRoute == MoveRoute.BACKLEFT || oldRoute == MoveRoute.BACK)
				{
                    return oldRoute;
				}

                // 일정확률로 BACKLEFT한다. 높은 확률로 BACK한다.
                oldRoute = Random.Range(0, 100) > 70 ? MoveRoute.BACKLEFT : MoveRoute.BACK;
                return oldRoute;
			}
            // LEFTUP만 막힌경우
            else if (colList.FindIndex(a => a.Equals(LEFTUP)) != -1)
			{
                if (oldRoute == MoveRoute.BACKRIGHT || oldRoute == MoveRoute.BACK)
				{
                    return oldRoute;
				}

                // 일정확률로 BACKRIGHT한다. 높은 확률로 BACK 한다.
                oldRoute = Random.Range(0, 100) > 70 ? MoveRoute.BACKRIGHT : MoveRoute.BACK;
                return oldRoute;
			}
            // 뒤가 안막힌 경우
            else
			{
                // 뒤로 쭉 빠진다.
                oldRoute = MoveRoute.BACK;
                return oldRoute;
			}
		}
        // 그 외에 경우
        else
		{
            // RIGHTDOWN, FRONT가 막힌경우
            if (colList.FindIndex(a => a.Equals(RIGHTDOWN)) != -1 && colList.FindIndex(a => a.Equals(FRONT)) != -1)
			{
                // 좌회전
                oldRoute = MoveRoute.GOLEFT;
                return oldRoute;
			}
            // RIGHTUP, FRONT가 막힌경우
            else if (colList.FindIndex(a => a.Equals(RIGHTUP)) != -1 && colList.FindIndex(a => a.Equals(FRONT)) != -1)
			{
                // 우회전
                oldRoute = MoveRoute.GORIGHT;
                return oldRoute;
			}
            // FRONT만 막힌경우
            else if (colList.FindIndex(a => a.Equals(FRONT)) != -1)
            {
                if (oldRoute == MoveRoute.GOLEFT || oldRoute == MoveRoute.GORIGHT)
                {
                    return oldRoute;
                }

                // 우회전 혹은 좌회전
                oldRoute = Random.Range(0, 100) > 50 ? MoveRoute.GOLEFT : MoveRoute.GORIGHT;
                return oldRoute;
			}
            // 그 외
            else
			{
                // 정면
                oldRoute = MoveRoute.GO;
                return oldRoute;
			}
		}
	}

    /// <summary>
    /// 두 점 사이의 각도를 구한다.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    private float GetAngle(Vector3 from, Vector3 to)
	{
        // from 과 to 사이의 각도를 구한다. transform.forward는 다른 벡터가 회전하는 기준이 되는 벡터
        return Vector3.SignedAngle(from, to, transform.forward);
	}
    /// <summary>
    /// 방해물이 존재하는지 확인 하는 함수
    /// </summary>
    private bool CheckObstacle()
	{
        if (colList.Count > 0)
		{
            return true;
		}
        return false;
	}
    /// <summary>
    /// MoveRoute에 따라 이동시키는 함수를 호출한다.
    /// </summary>
    private void Move()
	{
        switch (FindRoute())
        {
            case MoveRoute.STOP:
                //Debug.Log("정지");
                // 정지상태로 변경 후 해당 함수 종료
                ResetState(PoliceState.STOP);
                return;
                break;
            case MoveRoute.GO:
                //Debug.Log("정면");
                Straight(1);
                break;
            case MoveRoute.GORIGHT:
                //Debug.Log("정면우회전");
                Spin(-2f);
                Straight(0.1f);
                break;
            case MoveRoute.GOLEFT:
                //Debug.Log("정면좌회전");
                Spin(2f);
                Straight(0.1f);
                break;
            case MoveRoute.BACK:
                //Debug.Log("후진");
                Back(1);
                break;
            case MoveRoute.BACKRIGHT:
                //Debug.Log("후진우회전");
                Spin(2f);
                Back(0.1f);
                break;
            case MoveRoute.BACKLEFT:
                //Debug.Log("후진좌회전");
                Spin(-2f);
                Back(0.1f);
                break;
        }
    }
    /// <summary>
    /// 경찰차를 회전시킵니다. angle의 부호에 따라 회전방향이 다릅니다.
    /// angle의 부호가 바뀌는 순간이 오면(angle이 0 근처일떄) 목표를 바라보는 것으로 간주하고 false를 리턴. 회전을 멈춤
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    private bool SpinToTarget(float angle)
	{
        // Mathf.Abs(angle) <= 5에서 5는 180에서 -180으로 넘어가는 경우를 제외하기 위해 대강 정해준 수 
        if ((oldAngle >= 0 && angle <= 0 && oldAngle != -999 && Mathf.Abs(angle) <= 5) || (oldAngle <= 0 && angle >= 0 && oldAngle != -999 && Mathf.Abs(angle) <= 5))
		{
            //Debug.Log(angle + " " + oldAngle);
            oldAngle = angle;
            return false;
        }
        else
		{
            myTrans.localEulerAngles += new Vector3(0, 0, (angle >= 0 ? 1 : -1) * Time.deltaTime * Speed * 15f);
        }
        oldAngle = angle;
        return true;
    }
    /// <summary>
    /// angle 만큼 경찰차를 회전시킨다.
    /// </summary>
    /// <param name="angle"></param>
    private void Spin(float angle)
	{
        oldAngle = -999;
        myTrans.localEulerAngles += new Vector3(0,0,angle);
	}
    /// <summary>
    /// 경찰차를 바라보는 방향으로 이동시킨다.
    /// </summary>
    private void Straight(float k)
	{
        myTrans.localPosition += myTrans.right * Time.deltaTime * Speed * k;
	}
    /// <summary>
    /// 경찰차를 바라보는 방향으로 후진시킨다.
    /// </summary>
    /// <param name="k"></param>
    private void Back(float k)
	{
        myTrans.localPosition -= myTrans.right * Time.deltaTime * Speed * k;
        //Debug.Log("발동??");
    }
    /// <summary>
    /// 플레이어의 트랜스폼을 가져옴. 위치를 가져와 추격하기 위함
    /// </summary>
    /// <param name="trans"></param>
    public void SetTransform(Transform trans)
	{
        playerTrans = trans;
        GunMethod.PlayerTransfrom = playerTrans;
    }
    /// <summary>
    /// 상태를 갱신하고 시간을 초기화시킴.
    /// </summary>
    /// <param name="state"></param>
    private void ResetState(PoliceState state)
	{
        policeState = state;
        time = 0f;
	}
    /// <summary>
    /// 경찰차의 움직임을 제어한다.
    /// </summary>
    /// <returns></returns>
    private bool ControlMove()
	{
        if (isStop)
        {
            if (isRigid)
            {
                rigid.constraints = RigidbodyConstraints2D.FreezeAll;
                isRigid = false;
            }
        }
        else
        {
            if (!isRigid)
            {
                rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
                isRigid = true;
            }
        }

        return isStop;
    }
    /// <summary>
    /// 경찰차 상태에 따른 시야의 색을 바꿔준다.
    /// </summary>
    /// <param name="state"></param>
    private void ChangeFOVColor(PoliceState state)
	{
        if (temState != policeState)
		{
            temState = policeState;

            if (temState == PoliceState.SPUERCHASE)
			{
                mesh.material.SetColor(Shader.PropertyToID("_Color"), redEmi);
			}
            else if (temState == PoliceState.AUTOMOVE)
            {
                mesh.material.SetColor(Shader.PropertyToID("_Color"), yellowEmi);
            }
            else if (temState == PoliceState.OUTMAP || temState == PoliceState.DESTROY)
            {
                mesh.material.SetColor(Shader.PropertyToID("_Color"), greenEmi);
            }
        }
        else
		{
            return;
		}
	}
    /// <summary>
    /// 경찰차 일시 정지. 풀리면 이동한다.
    /// </summary>
    /// <param name="bo"></param>
    public override void PausePoliceCar(bool bo)
    {
        if (bo)
        {
            if (temState == PoliceState.AUTOMOVE && temState == PoliceState.OUTMAP)
            {
                policeState = PoliceState.NONE;
            }
            else
            {
                policeState = temState;
            }
        }
        else
        {
            policeState = temState;
        }
    }

    
    private void FixedUpdate()
	{
        // 특정상황에서 모든 추격 경찰차 정지
        if (ControlMove()) { return; }
        if (policeState == PoliceState.DESTROY ||  policeState == PoliceState.NONE) { return; }

        time += Time.deltaTime;
        ChangeFOVColor(policeState);

        // 플레이어를 발견한다면 슈팅 자세
        GunMethod.ShootingStance = iGetBool.GetBool();
        GunMethod.Fire(1f, 10);

        switch (policeState)
        {
            // 플레이어와의 거리 상관없이 무조건 쫓아오는 상태임.
            case PoliceState.SPUERCHASE:
                // 플레이어를 무조건 쫓아옴
                SpuerChase();
                // 만약 플레이어를 발견한다면 시간을 0초로 돌려서 SUPERCHASE 상태를 갱신.
                if (iGetBool.GetBool())
                {
                    ResetState(PoliceState.SPUERCHASE);
                }
                // 만약 20초가 지나면 자동주행모드로 바꿈
                if (time >= 20f)
                {
                    ResetState(PoliceState.AUTOMOVE);
                }
                break;
            // 맵을 랜덤으로 돌아다니는 상태임
            case PoliceState.AUTOMOVE:
                AutoMove();
                // 만약 플레이어를 발견한다면 시간을 0초로 돌리고 SUPERCHASE 상태로 전환
                if (iGetBool.GetBool())
				{
                    ResetState(PoliceState.SPUERCHASE);
				}
                // 자동주행 모두가 30초동안 이루어지면 맵 밖으로 나가는 상태가 됨.
                else if (time >= 30f)
				{
                    ResetState(PoliceState.OUTMAP);
				}
                break;
            case PoliceState.STOP:
                if (time >= 2f)
				{
                    ResetState(PoliceState.AUTOMOVE);
				}
                break;
            case PoliceState.OUTMAP:
                OutMap();
                // 만약 플레이어를 발견한다면 시간을 0초로 돌리고 SUPERCHASE 상태로 전환
                if (iGetBool.GetBool())
                {
                    ResetState(PoliceState.SPUERCHASE);
                }

                if (Vector3.SqrMagnitude(myTrans.position - outVec) <= 2)
				{
                    Destroy(this.gameObject);
				}
                break;
        }
        if (policeState == PoliceState.STOP || policeState == PoliceState.AUTOMOVE)
		{
            autoAndStopTime += Time.deltaTime;
        }
    }

	public void UpdateCheck(int num, bool isAdd)
	{
		if (isAdd)
		{
            if (colList.FindIndex(a => a.Equals(num)) == -1)
			{
                colList.Add(num);
			}
		}
        else
		{
            colList.Remove(num);
		}
	}
}
