using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PoliceNS.PolicePathNS;
using PoliceNS.PoliceStateNS;

// 한석호 작성

public class PoliceCar : MonoBehaviour, IPoliceCar, IMovingPoliceCarControl, IInspectingPoliceCarControl, IEndInspecting
{
    [Range(0f, 100f)] public float PoliceHp;

    [SerializeField] private GameObject checkColObj;
    [SerializeField] private GameObject stopCheckColObj;
    public static bool IsInspecting { get; private set; }

    private static List<int> policeCarCodeList = new List<int>();

    private PoliceState policeState;

    private IPoliceSmokeEffect iPoliceSmokeEffect;

    // 경로를 차례대로 들고 있다.
    private List<PolicePath> policePathList = new List<PolicePath>();

    private PlayerMove playerMove;
    private Transform trans;
    private Coroutine smokeEffectCoroutine;

    private Vector3 temRotate;
    private Vector3 temPosition;

    private float speed;    // 자동차의 속도
    private float rotate;   // 플레이어는 해당 값만큼 z축 방향을 돌려야 합니다.
    private int hp;
    private int index;
    private int policeCarCode;  // 자동차 고유번호
    private bool nextBehaviour;
    private bool isBehaviour;   // 주변에 차가 있는지 여부에 따라 행동을 제어할 수 있게 해준다.
    private bool isLock = false;
    private bool isRight = false;   // 경찰차의 방향이 왼쪽인지 오른쪽인지를 확인해준다. 
    private bool isExplosion = false;
    void Awake()
    {
        PoliceHp = 100;

        trans = this.transform;
        if (checkColObj != null)
        {
            checkColObj.GetComponent<PoliceCarCollisionCheck>().SetIPoliceCarIsBehaviour(this);
        }
        if (stopCheckColObj != null)
        {
            stopCheckColObj.GetComponent<StopPoliceCarCollisionCheck>().SetIInspectingPoliceCarControl(this);
            stopCheckColObj.GetComponent<StopPoliceCarCollisionCheck>().SetIPoliceCarIsBehaviour(this);
            stopCheckColObj.GetComponent<StopPoliceCarCollisionCheck>().SetIEndInspecting(this);

        }
        InitValue();
    }

    private void Start()
    {
        smokeEffectCoroutine = StartCoroutine(PoliceSmokeCoroutine());
    }

    public void SetPlayerMove(PlayerMove playerMove)
	{
        this.playerMove = playerMove;
	}

    private void InitValue()
    {
        InitState(true);

        isRight = Random.Range(0, 2) == 0 ? true : false;
        //isLeft = false;
        trans.eulerAngles = new Vector3(0,0, isRight ? 0 : 180);
        speed = Random.Range(1,10); // 속도를 랜덤으로 줌
        hp = 100;   // 자동차의 기본 체력은 100
        rotate = 0;
        index = 0;
        while(true)
        {
            policeCarCode = Random.Range(0, 1000);
            if (policeCarCodeList.FindIndex(a => a.Equals(policeCarCode)) == -1)
            {
                policeCarCodeList.Add(policeCarCode);
                break;
            }
        }
        
        isBehaviour = true;
    }
    // 상태를 초기화해준다.
    private void InitState(bool bo)
    {
        if (bo)
        {
            // 경찰차의 상태를 랜덤으로 정해준다.
            policeState = (PoliceState)Random.Range(1, 3);
        }
        // 경찰차에 상태에 따라 켜줘야할 콜라이더가 다르다.
        if (policeState == PoliceState.MOVING)
        {
            checkColObj.SetActive(true);    // 켜준다.
            stopCheckColObj.SetActive(false);   // 꺼준다.
        }
        else if (policeState == PoliceState.STOP)
        {
            stopCheckColObj.SetActive(true);    // 켜준다.
            checkColObj.SetActive(false);   // 꺼준다.
        }
        else if (policeState == PoliceState.INSPECTING)
        {
            IsInspecting = true;
            if (playerMove != null)
            {
                playerMove.Stop = true;
            }
            stopCheckColObj.SetActive(false);
            checkColObj.SetActive(false);
        }
        else if (policeState == PoliceState.DESTROY)
        {
            stopCheckColObj.SetActive(false);
            checkColObj.SetActive(false);
        }
    }

    // 경찰차의 경로를 정해주는 함수이다.
    public void InitPoliceCarPath(List<PolicePath> policePathList)
    {
        // 경로를 깔끔하게 다 지워준다.
        this.policePathList.Clear();
        for (int i = 0; i < policePathList.Count; i++)
        {
            this.policePathList.Add(policePathList[i]);
        }
        // 경찰차가 오른쪽을 바라보고 있다면 0부터 경로를 시작하고, 왼쪽을 바라보고 있다면 끝번호부터 경로를 시작한다.
        if (isRight) { index = 0; }
        else { index = this.policePathList.Count - 1; }
    }

    // 경찰차에게 어떤 행동을 하게 할지 명령을 내립니다.
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
                Turn(value);
                TurnStraight(value);
                break;
        }
    }
    // 자동차가 회전 후 회전한 방향으로 일정거리 직진하게끔 해줍니다.
    private void TurnStraight(float value)
    {
        int n = 1;
        if (value > 0) { n = -1; }
        trans.position += transform.right * ((Mathf.PI * speed) / (2 * value * (-1) * n));
    }
    // 바라보는 방향으로 직진합니다.
    private void Straight(float dis)
    {
        if (!isLock)
        {
            temPosition = trans.position + (transform.right * dis);
            isLock = true;
        }
        
        if (Vector3.SqrMagnitude(trans.position - temPosition)
            <= Vector3.SqrMagnitude((trans.position + transform.right * speed * Time.deltaTime) - temPosition))
        {
            trans.position = temPosition;
            
            //Debug.Log($"{trans.position.x}  {trans.position.y}");
            nextBehaviour = true;
            isLock = false;
        }
        else
        {
            trans.position += transform.right * speed * Time.deltaTime;
        }
    }
    // 회전합니다. 해당 함수는 Mathf.Abs(rotate)값만큼 호출되고 다음 명령으로 넘어가게 합니다.
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
        if (rotate * (isRight ? 1 : -1) > 0)
        {
            this.rotate += (-1) * speed;
            trans.Rotate(new Vector3(0, 0, speed));
            if (this.rotate < 0)
            {
                this.rotate = 0f;
            }
        }
        else if (rotate * (isRight ? 1 : -1) < 0)
        {
            this.rotate += speed;
            trans.Rotate(new Vector3(0, 0, (-1) * speed));
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

    private IEnumerator PoliceSmokeCoroutine()
    {
        var time = new WaitForSeconds(0.01f);
        int r = 0;
        while(true)
        {
            r = Random.Range(5, 15);

            if (PoliceHp < 70f)
            {
                iPoliceSmokeEffect.InsPoliceSmokeEfectObj(this.transform);
            }

            if (PoliceHp <= 0f && policeState != PoliceState.DESTROY)
            {
                this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                // 파괴 상태로 변경
                policeState = PoliceState.DESTROY;
                // 상태에 따른 콜라이더 초기화
                InitState(false);
                // 파괴
                //Destroy(this.gameObject, 10f);
                //StopCoroutine(smokeEffectCoroutine);
                Invoke("AddForceCar", 9f);
            }
            
            // 경찰차 체력이 0이 되면 rigidbody-constrait을 해제하고 10초 후 제거하도록함.
             

            for (int i = 0; i < r; i++)
            {
                yield return time;
            }
        }
    }

    private void AddForceCar()
    {
        // 폭발 이미지 넣기
        isExplosion = true;

        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(0, 10f), Random.Range(0, 10f)), ForceMode2D.Impulse);
        Invoke("DestroyCar", 5f);
    }

    private void DestroyCar()
    {
        StopCoroutine(smokeEffectCoroutine);
        Destroy(this.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 태그가 플레이어면 조건 참
        if (collision.gameObject.tag.Equals("Player"))
        {
            // 크리티컬 1.5배
            PoliceHp -= Mathf.Abs(collision.gameObject.GetComponent<PlayerMove>().Speed) * 7f * Random.Range(1.0f, 1.5f);

            if (PoliceHp < 0f) { PoliceHp = 0f; }
            
        }
    }


    void FixedUpdate()
    {
        if (policeState == PoliceState.DESTROY && isExplosion)
        {

        }

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

            if (nextBehaviour && isBehaviour)
            {
                trans.position = new Vector3((float)System.Math.Round(trans.position.x, 1), (float)System.Math.Round(trans.position.y, 1));

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
                nextBehaviour = false;
            }
        }
        else if (policeState == PoliceState.STOP)
        {
            if (Random.Range(0,10000) < 10) { InitState(true); }
        }
    }
    public void SetIsBehaviour(bool bo)
    {
        isBehaviour = bo;
    }
    public int GetPoliceCarCode()
    {
        return policeCarCode;
    }
    public void SetPoliceState(PoliceState policeState)
    {
        this.policeState = policeState;
        InitState(false);
    }
    public void EndInspecting()
	{
        playerMove.Stop = false;
        IsInspecting = false;
        this.policeState = PoliceState.MOVING;
        InitState(false);
	}
    public void SetIInspectingPanelControl(IInspectingPanelControl iInspectingPanelControl)
    {
        stopCheckColObj.GetComponent<StopPoliceCarCollisionCheck>().SetIInspectingPanelControl(iInspectingPanelControl);
    }
    public void SetPoliceSmokeEffect(IPoliceSmokeEffect iPoliceSmokeEffect)
	{
        this.iPoliceSmokeEffect = iPoliceSmokeEffect;
    }
}
