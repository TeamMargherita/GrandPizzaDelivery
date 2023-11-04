using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PoliceNS.PolicePathNS;
using PoliceNS.PoliceStateNS;

// 한석호 작성

public class PoliceCar : Police, IPoliceCar, IMovingPoliceCarControl, IInspectingPoliceCarControl, IEndConversation, IPriorityCode
{
    [SerializeField] private GameObject checkColObj;    // 이동 시 충돌을 방지하기 위한 콜라이더
    [SerializeField] private GameObject stopCheckColObj;    // 정지 시, 불심검문을 위한 콜라이더
    public static bool IsInspecting { get; private set; }   // 플레이어가 불심검문 중인지 확인하는 정적 변수

    private static List<int> policeCarCodeList = new List<int>();  // 경찰차 고유번호 리스트

    // 경로를 차례대로 들고 있다.
    private List<PolicePath> policePathList = new List<PolicePath>();

    private GameObject banana;
    private PlayerMove playerMove;
    private Transform trans;
    private Coroutine shootBananaCoroutine;
    private Rigidbody2D rigid2D;
    
    private Vector3 temRotate;
    private Vector3 temPosition;

    public float Speed;    // 자동차의 속도
    private float rotate;   // 플레이어는 해당 값만큼 z축 방향을 돌려야 합니다.
    private int hp; // 경찰차의 체력
    private int index;  // 경로 리스트의 인덱스. 오름차순으로 받을지 내림차순으로 받을지에 따라 더해주는 값의 부호가 다르다.
    private int policeCarCode;  // 경찰차 고유번호. 경찰차들끼리 우선순위를 정하는데 사용한다.
    private bool nextBehaviour;
    private bool isBehaviour;   // 주변에 차가 있는지 여부에 따라 행동을 제어할 수 있게 해준다.
    private bool isLock = false;
    private bool isRight = false;   // 경찰차의 방향이 왼쪽인지 오른쪽인지를 확인해준다.
    private bool isStop = false;    // 경찰차 멈춤 여부
    protected override void Awake()
    {
        base.Awake();
        PoliceHp = 100; // 경찰차 초기 체력 설정

        trans = this.transform; // transform 캐싱
        if (checkColObj != null)
        {
            // 필요한 인터페이스들을 넣어줌
            checkColObj.GetComponent<PoliceCarCollisionCheck>().SetIPoliceCarIsBehaviour(this);
            checkColObj.GetComponent<PoliceCarCollisionCheck>().SetIPriority(this);
        }
        if (stopCheckColObj != null)
        {
            // 필요한 인터페이스들을 넣어줌
            stopCheckColObj.GetComponent<StopPoliceCarCollisionCheck>().SetIInspectingPoliceCarControl(this);
            stopCheckColObj.GetComponent<StopPoliceCarCollisionCheck>().SetIPoliceCarIsBehaviour(this);
            stopCheckColObj.GetComponent<StopPoliceCarCollisionCheck>().SetIEndInspecting(this);
            stopCheckColObj.GetComponent<StopPoliceCarCollisionCheck>().SetIPriority(this);
        }
        if (this.GetComponent<Rigidbody2D>() != null)
		{
            // rigidbody2d 캐싱
            rigid2D = this.GetComponent<Rigidbody2D>();
        }

        InitValue();    // 초기에 설정되어야 하는 것
    }

    protected override void Start()
    {
        base.Start();
        shootBananaCoroutine = StartCoroutine(ShootBananaTermCoroutine());
    }

    public void SetPlayerMove(PlayerMove playerMove)
	{
        this.playerMove = playerMove;
	}
    public void SetBanana(GameObject banana)
	{
        this.banana = banana; 
	}

    private void InitValue()
    {
        InitState(true);

        isRight = Random.Range(0, 2) == 0 ? true : false;
        trans.eulerAngles = new Vector3(0,0, isRight ? 0 : 180);
        Speed = Random.Range(1,10); // 속도를 랜덤으로 줌
        hp = 100;   // 자동차의 기본 체력은 100
        rotate = 0;
        index = 0;
        while(true)
        {
            policeCarCode = Random.Range(0, 1000) + 100000000;
            if (policeCarCodeList.FindIndex(a => a.Equals(policeCarCode)) == -1)
            {
                policeCarCodeList.Add(policeCarCode);
                break;
            }
        }
        
        isBehaviour = true;
    }
    /// <summary>
    /// 상태에 따라 콜라이더를 꺼주거나 켜준다. 
    /// </summary>
    /// <param name="bo">true면 경찰차의 상태를 랜덤으로 정해주며, false면 경찰차의 상태를 정해주진 않는다.</param>    
    protected override void InitState(bool bo)
    {
        if (bo)
        {
            // 밤일 때만
            if (GameManager.Instance.isDarkDelivery)
            {
                // 경찰차의 상태를 랜덤으로 정해준다.
                policeState = (PoliceState)Random.Range(1, 3);
            }
            else
            {
                policeState = PoliceState.MOVING;
            }
        }
        // 경찰차에 상태에 따라 켜줘야할 콜라이더가 다르다.
        if (policeState == PoliceState.MOVING)  // 경찰차가 이동 상태일 때
        {
            checkColObj.SetActive(true);    // 켜준다.
            stopCheckColObj.SetActive(false);   // 꺼준다.
        }
        else if (policeState == PoliceState.STOP)   // 경찰차가 정지 상태일 때(불심검문)
        {
            stopCheckColObj.SetActive(true);    // 켜준다.
            checkColObj.SetActive(false);   // 꺼준다.
        }
        else if (policeState == PoliceState.INSPECTING) // 경찰차가 불심검문 중인 상태일 때
        {
            IsInspecting = true;    // true면 플레이어가 불심검문 중임을 나타냄
            if (playerMove != null)
            {
                playerMove.Stop = true; // 플레이어의 이동을 제어한다.
            }
            stopCheckColObj.SetActive(false);   // 꺼준다.
            checkColObj.SetActive(false);   // 꺼준다.
        }
        else if (policeState == PoliceState.DESTROY)    // 경찰차가 파괴됨 상태일 때
        {
            stopCheckColObj.SetActive(false);   // 꺼준다.
            checkColObj.SetActive(false);   // 꺼준다.
        }
    }
    /// <summary>
    /// 경찰차의 경로를 정해주는 함수이다.
    /// </summary>
    public void InitPoliceCarPath(List<PolicePath> policePathList)
    {
        // 경로를 깔끔하게 다 지워준다.
        this.policePathList.Clear();
        // 경로를 차례대로 넣어준다.
        for (int i = 0; i < policePathList.Count; i++)
        {
            this.policePathList.Add(policePathList[i]);
        }
        // 경찰차가 오른쪽을 바라보고 있다면 0부터 경로를 시작하고, 왼쪽을 바라보고 있다면 끝번호부터 경로를 시작한다.
        if (isRight) { index = 0; }
        else { index = this.policePathList.Count - 1; }
    }

    /// <summary>
    /// 경찰차에게 어떤 행동을 하게 할지 명령을 내립니다.
    /// </summary>
    private void PoliceCarBehaviour(int choice, float value)
    {
        switch(choice)
        {
            case 1:
                Straight(value);
                // 직진 중 일정확률로 불심검문(STOP) 상태로 바뀝니다.
                if (Random.Range(0,1000) < 2) { InitState(true); }
                break;
            case 2:
                // 좌회전, 우회전을 한다.
                Turn(value);
                TurnStraight(value);
                break;
        }
    }
    private float nn = 0f;
    /// <summary>
    /// 자동차가 회전 후 회전한 방향으로 일정거리 직진하게끔 해줍니다.
    /// </summary>
    private void TurnStraight(float value)
    {
        
        if (nn + Speed < Mathf.Abs(value))
        {
            trans.position += transform.right * ((Mathf.PI * Speed * Time.timeScale) / (2 * Mathf.Abs(value)));
            nn += Speed;
        }
        else
        {
            trans.position += transform.right * ((Mathf.PI * (Mathf.Abs(value) - nn) * Time.timeScale) / (2 * Mathf.Abs(value)));
            nn = 0f;
        }
    }
    /// <summary>
    /// 바라보는 방향으로 직진합니다.
    /// </summary>
    private void Straight(float dis)
    {
        if (!isLock)
        {
            // temPosition은 경찰차가 목표로 하고 있는 지점이다.
            // isLock으로 목표지점을 한번만 정해준다.
            temPosition = trans.position + (transform.right * dis);
            isLock = true;
        }
        
        // 경찰차의 위치가 목표지점에 가장 가까워졌을 시, 경찰차의 위치를 목표지점으로 바꿔 위치의 오차를 없앤다.
        if (Vector3.SqrMagnitude(trans.position - temPosition)
            <= Vector3.SqrMagnitude((trans.position + transform.right * Speed * Time.deltaTime) - temPosition))
        {
            trans.position = temPosition;
            
            nextBehaviour = true;   // 목표지점에 도달했으므로 다음 명령을 받을 준비가 된다.
            isLock = false; // 목표지점에 도달했으므로 temPosition을 다음 호출 시 다시 받아야한다.
        }
        else
        {
            // 경찰차를 목표지점 방향으로 일정거리 이동시킨다.
            trans.position += transform.right * Speed * Time.deltaTime;
        }
    }
    /// <summary>
    /// 회전합니다. 해당 함수는 Mathf.Abs(rotate)값만큼 호출되고 다음 명령으로 넘어가게 합니다.
    /// </summary>
    private void Turn(float rotate)
    {
        // this.rotate의 값을 설정해줍니다.
        if (this.rotate == 0f)
        {
            // 방향의 값은 경찰차가 어느 방향을 바라보고 가는지에 따라 다릅니다.
            this.rotate = rotate * (isRight ? 1 : -1);
            // 방향을 바꾸기전 경찰차의 z축 rotation값을 temRotate에 저장합니다.
            temRotate = trans.eulerAngles;
        }
        // rotate의 값이 음수인지 양수인지 여부와, 경찰차가 오른쪽으로 가는지 아닌지에 따라 
        // 총 4가지의 상황이 생기며, 그에 따라 다르게 회전을 시켜줄 필요가 있다.
        if (rotate * (isRight ? 1 : -1) > 0)
        {
            this.rotate += (-1) * Speed * Time.timeScale;
            trans.Rotate(new Vector3(0, 0, Speed * Time.timeScale));
            if (this.rotate < 0)
            {
                this.rotate = 0f;
            }
        }
        else if (rotate * (isRight ? 1 : -1) < 0)
        {
            this.rotate += Speed * Time.timeScale;
            trans.Rotate(new Vector3(0, 0, (-1) * Speed * Time.timeScale));
            if (this.rotate > 0)
            {
                this.rotate = 0f;
            }
        }

        // this.rotate값이 0이 되면 회전이 끝났음을 의미합니다.
        if (this.rotate == 0f)
        {
            // 경찰차의 각도에 오차가 있을 것에 대비하여 회전이 끝난 후 정확한 방향값을 다시 넣어줍니다.
            trans.eulerAngles = temRotate + new Vector3(0,0, rotate * (isRight ? 1 : -1));
            // 다음 명령을 부를 수 있도록 nextBehaviour값을 true로 바꿉니다.
            nextBehaviour = true;
        }


    }
    /// <summary>
    /// 일정 시간마다 바나나를 던지게 하는 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShootBananaTermCoroutine()
	{
        var time = new WaitForSeconds(1f);
        int r = 0;
        while(true)
		{
            r = Random.Range(50, 150);

            for (int i = 0; i < r; i++)
			{
                yield return time;
			}

            ShootBanana();
		}
	}
    private void ShootBanana()
	{
        GameObject ba = Instantiate(banana);
        ba.transform.position = this.transform.position;
        ba.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(0, 5f), Random.Range(0, 5f)), ForceMode2D.Impulse);
	}

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        // 플레이어와 충돌하면 경찰차는 대미지를 입음.
        // 태그가 플레이어면 조건 참
        if (collision.gameObject.tag.Equals("Player"))
        {
            // 크리티컬 1.5배
            PoliceHp -= Mathf.Abs(collision.gameObject.GetComponent<PlayerMove>().Speed) * 7f * Random.Range(1.0f, 1.5f);

            if (PoliceHp < 0f) { PoliceHp = 0f; }


            if (damagedCoroutine != null)
            {
                StopCoroutine(damagedCoroutine);
            }
            damagedCoroutine = StartCoroutine(Damaged());
        }
        // 경찰차끼리 충돌해도 대미지를 입음.
        else if (collision.gameObject.GetComponent<IPoliceCar>() != null)
        {
            // 경찰차가 전부 자기 경로를 찾아 달리고 있는 경우일 때 부딪힌다면 피해를 입지 않음. 그렇지 않을 경우에만 피해를 입음.
            if (!(collision.gameObject.GetComponent<IPoliceCar>().GetPoliceState() == PoliceState.MOVING
                && collision.gameObject.GetComponent<IPoliceCar>().GetPoliceState() == policeState))
            {
                // 부딪힌 경찰차가 MOVING상태가 아니라면 경찰차의 velocity를 따른다.
                if (collision.gameObject.GetComponent<IPoliceCar>().GetPoliceState() != PoliceState.MOVING)
                {
                    PoliceHp -= Mathf.Sqrt(Mathf.Pow(collision.gameObject.GetComponent<IPoliceCar>().GetRigidBody2D().velocity.x, 2)
                        + Mathf.Pow(collision.gameObject.GetComponent<IPoliceCar>().GetRigidBody2D().velocity.x, 2)) * 7f;

                }
                // 부딪힌 경찰차가 MOVING상태라면 경찰차의 speed를 따른다.
                else
                {
                    PoliceHp -= collision.gameObject.GetComponent<IPoliceCar>().GetSpeed() * 7f;
                }


                if (damagedCoroutine != null)
                {
                    StopCoroutine(damagedCoroutine);
                }
                damagedCoroutine = StartCoroutine(Damaged());
            }
        }

    }
    public void SetIsStop(bool bo)
    {
        isStop = bo;
    }
    protected override void DestroyPolice()
	{
        iStop.RemovePoliceList(this);
	}
    /// <summary>
    /// 경찰차 일시 정지. 풀리면 이동한다.
    /// </summary>
    /// <param name="bo"></param>
    public override void PausePoliceCar(bool bo)
    {
        if (bo)
        {
            policeState = PoliceState.NONE;
        }
        else
        {
            policeState = PoliceState.MOVING;
        }
    }
    void FixedUpdate()
    {
        if (isStop) { return; }
        if (policeState == PoliceState.DESTROY) { return; }

        // 경찰차의 상태가 MOVING이여야만 조건을 만족한다.
        if (policeState == PoliceState.MOVING)
        {
            // 주변에 차가 없다는 것을 감지하고, 저장된 경로가 0이 아니라면 조건을 만족한다.
            if (policePathList.Count != 0 && isBehaviour)
            {
                // 경찰차에게 어떤 행동을 하게 할지 명령을 내립니다.
                PoliceCarBehaviour(policePathList[index].Behaviour, policePathList[index].Value);
            }
            // 다음 명령을 받을 준비가 되어있고, 경찰차 주변에 부딪힐 차량이 없는 경우
            // index를 바꾸고 다음 명령을 호출하도록 한다.
            if (nextBehaviour && isBehaviour)
            {
                // 소수점이하의 오차를 없애기 위해 현재 포지션값을 반올림하여 정수로 바꾼다.
                trans.position = new Vector3((float)System.Math.Round(trans.position.x, 1), (float)System.Math.Round(trans.position.y, 1));
                // 오른쪽으로 경찰차가 움직이면, 경로 리스트의 인덱스를 1 올리고, 그 반대면 1 내린다.
                if (isRight)
                {
                    if (policePathList.Count - 1 == index)
                    {
                        index = 0;
                    }
                    else
                    {
                        index++;
                    }
                }
                else
                {
                    if (index == 0)
                    {
                        index = policePathList.Count - 1;
                    }
                    else
                    {
                        index--;
                    }
                }
                // 경로 리스트 인덱스를 바꿨으니 다음 명령을 받을 수 없는 상태로 바꾼다.
                nextBehaviour = false;
            }
        }
        // 일정확률로 불심검문 상태에서 다시 새로운 상태로 초기화시켜준다.
        else if (policeState == PoliceState.STOP)
        {
            if (Random.Range(0,10000) < 10) { InitState(true); }
        }
    }
    // 충돌하게 되는지 여부에 따라 값을 바꿔준다.
    public void SetIsBehaviour(bool bo)
    {
        isBehaviour = bo;
    }
    /// <summary>
    /// 경찰차의 고유 번호를 가져온다. 고유번호는 경찰차들끼리 우선순위를 정하는데 사용한다.
    /// </summary>
    public int GetPriorityCode()
    {
        return policeCarCode;
    }
    /// <summary>
    /// 경찰차의 상태를 바꾼다. 상태를 초기화하지는 않고,
    /// 필요없는 콜라이더는 InitState함수로 꺼주고, 필요한 건 켜준다.
    /// </summary>
    /// <param name="policeState"></param>
    public void SetPoliceState(PoliceState policeState)
    {
        this.policeState = policeState;
        InitState(false);
    }
    /// <summary>
    /// 불심검문중 상태가 끝나고 경찰차의 상태를 이동 상태로 바꿔준다. 그 외에
    /// 플레이어 제어권이나, 불심검문중인 상태가 아님을 나타내기 위해 변수값을 바꿔준다.
    /// </summary>
    public void EndConversation()
	{
        playerMove.Stop = false;
        IsInspecting = false;
        this.policeState = PoliceState.MOVING;
        InitState(false);
	}
    public void SetIInspectingPanelControl(IConversationPanelControl iInspectingPanelControl)
    {
        stopCheckColObj.GetComponent<StopPoliceCarCollisionCheck>().SetIInspectingPanelControl(iInspectingPanelControl);
    }
    public Rigidbody2D GetRigidBody2D()
	{
        return rigid2D;
	}
    public float GetSpeed()
	{
        return Speed;
	}
    public PoliceState GetPoliceState()
	{
        return policeState;
	}
}
