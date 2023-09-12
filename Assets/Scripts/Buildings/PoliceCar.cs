using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PolicePathNS;

// 한석호 작성

public class PoliceCar : MonoBehaviour, IPoliceCar, IPoliceCarControl
{
    [SerializeField] private GameObject checkColObj;

    private static List<int> policeCarCodeList = new List<int>();

    private List<PolicePath> policePathList = new List<PolicePath>();

    private Transform trans;

    private Vector3 temRotate;
    private Vector3 temPosition;

    private float speed;    // 자동차의 속도
    private float dis;
    private float rotate;   // 플레이어는 해당 값만큼 z축 방향을 돌려야 합니다.
    private int index;
    private int policeCarCode;  // 자동차 고유번호
    private bool nextBehaviour;
    private bool isBehaviour;
    private bool isLock = false;
    private bool isLeft = false;
    // Start is called before the first frame update
    void Awake()
    {
        trans = this.transform;
        checkColObj.GetComponent<PoliceCarCollisionCheck>().SetIPoliceCarIsBehaviour(this);
        InitValue();
    }

    private void InitValue()
    {
        isLeft = Random.Range(0, 2) == 0 ? true : false;
        //isLeft = false;
        trans.eulerAngles = new Vector3(0,0, isLeft ? 0 : 180);
        speed = Random.Range(1,10);
        //speed = 9;
        dis = 0;
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

    public void InitPoliceCarPath(List<PolicePath> policePathList)
    {
        this.policePathList.Clear();
        for (int i = 0; i < policePathList.Count; i++)
        {
            this.policePathList.Add(policePathList[i]);
        }
        if (isLeft) { index = 0; }
        else { index = this.policePathList.Count - 1; }
    }

    // 경찰차에게 어떤 행동을 하게 할지 명령을 내립니다.
    private void PoliceCarBehaviour(int choice, float value)
    {
        switch(choice)
        {
            case 1:
                Straight(value);
                break;
            case 2:
                Turn(value);
                TurnStraight(value);
                break;
        }
    }
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
            this.rotate = rotate * (isLeft ? 1 : -1);
            // 방향을 바꾸기전 경찰차의 z축 rotation값을 temRotate에 저장합니다.
            temRotate = trans.eulerAngles;
        }
        if (rotate * (isLeft ? 1 : -1) > 0)
        {
            this.rotate += (-1) * speed;
            trans.Rotate(new Vector3(0, 0, speed));
            if (this.rotate < 0)
            {
                this.rotate = 0f;
            }
        }
        else if (rotate * (isLeft ? 1 : -1) < 0)
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
            trans.eulerAngles = temRotate + new Vector3(0,0, rotate * (isLeft ? 1 : -1));
            // 다음 명령을 부를 수 있도록 nextBehaviour값을 true로 바꿉니다.
            nextBehaviour = true;
        }
    }
    void FixedUpdate()
    {
        if (policePathList.Count != 0 && isBehaviour)
        {
            PoliceCarBehaviour(policePathList[index].Behaviour, policePathList[index].Value);
        }

        if (nextBehaviour && isBehaviour)
        {
            trans.position = new Vector3((float)System.Math.Round(trans.position.x,1), (float)System.Math.Round(trans.position.y, 1));

            if (isLeft)
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
    public void SetIsBehaviour(bool bo)
    {
        isBehaviour = bo;
    }

    public int GetPoliceCarCode()
    {
        return policeCarCode;
    }
}
