using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PoliceNS.PoliceStateNS;
using Gun;
// �Ѽ�ȣ �ۼ�
public class ChasePoliceCar : Police, ISetTransform, IUpdateCheckList
{
    public static bool isStop = false;  // �߰� ������ �ൿ ���� ����

    [Range (0f,10f)]
    public float Speed;

    [SerializeField] private GameObject[] colArr;
    [SerializeField] private Animator ani;
    [SerializeField] private AudioSource PoliceAudio;

    private const int RIGHTUP = 0; // ��
    private const int RIGHTDOWN = 1;    // ��
    private const int LEFTUP = 2;   // ��
    private const int LEFTDOWN = 3; // ��
    private const int FRONT = 4;
    private const int BACK = 5;

    /// <summary>
    /// �߰� ������ ����. SPUERCHASE�� �Ÿ� ������� ������ �÷��̾� �Ѿƿ��� ����
    /// </summary>
    private enum MoveRoute { NONE, GO, GORIGHT, GOLEFT, BACK, BACKRIGHT, BACKLEFT, STOP};
    private Transform playerTrans;
    private Transform myTrans;
    private Rigidbody2D rigid;

    //private PoliceState chaserPoliceState = PoliceState.SPUERCHASE;
    private MoveRoute oldRoute = MoveRoute.NONE;    // ���� �������� �̵� ����
    /// <summary>
    /// �÷��̾ ã�Ҵ��� ���θ� �˱����� �������̽�
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

    private List<int> colList = new List<int>();    // ������ �ݶ��̴� ����Ʈ
    private float time; // ���� ���°� �ߵ��ǰ� �ִ� �ð�
    private float oldAngle = -999f; // ���� �����ӿ��� �߰ݰ������� �÷��̾��� ���� ����
    private float autoAndStopTime = 0;   // �ڵ������ ���������� �ð�
    private bool isRigid = false;   // ������ٵ� ���� ����

    protected override void Awake()
    {
        base.Awake();
        policeType = PoliceType.CHASER;
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
    /// ������������ �÷��̾ ������� �����̴�.
    /// </summary>
    private void SpuerChase()
    {
        //Debug.DrawLine(myTrans.position, myTrans.position + myTrans.right * 100f);
        //Debug.DrawLine(myTrans.position, myTrans.position + (playerTrans.position - myTrans.position).normalized * 100f);
        
        // �ֺ��� ���ع��� ���� ��, Ȥ�� �÷��̾ �߰����� ��
        if (!CheckObstacle() || iGetBool.GetBool())
        {
            MoveToTarget((myTrans.position + (playerTrans.position - myTrans.position).normalized) - myTrans.position, playerTrans.position - myTrans.position);
        }
        // �ֺ��� ���ع��� ���� ��
        else
        {
            // �ڵ������� �Ѵ�.
            Move();
        }
    }
    /// <summary>
    /// �����Ӱ� ���� �����Ѵ�.
    /// </summary>
    private void AutoMove()
    {

        if (Vector3.SqrMagnitude(ranTarget - myTrans.position) <= 1f + autoAndStopTime)
		{
            ranTarget = new Vector3(Random.Range(0, 70), Random.Range(0, 70));
            autoAndStopTime = 0;
		}

        // �ֺ��� ���ع��� ���� ��, Ȥ�� �÷��̾ �߰����� ��
        if (!CheckObstacle() || autoAndStopTime >= 5f)
        {
            MoveToTarget((ranTarget - myTrans.position).normalized, ranTarget - myTrans.position);
        }
        // �ֺ��� ���ع��� ���� ��
        else
        {
            // �ڵ������� �Ѵ�.
            Move();
        }
    }
    /// <summary>
    /// �߰� �������� �� ������ ������.
    /// </summary>
    private void OutMap()
	{
        //Debug.DrawLine(myTrans.position, myTrans.position + myTrans.right * 100f);
        //Debug.DrawLine(myTrans.position, outVec);

        // �ֺ��� ���ع��� ���� ��, Ȥ�� �÷��̾ �߰����� ��, �Ǵ� OUTMAP���°� ���� 10�ʰ� ������ ���ع��� ���� ��
        if (!CheckObstacle() || iGetBool.GetBool() || (time >= 10f && CheckObstacle()))
        {
            MoveToTarget((outVec - myTrans.position).normalized, outVec - myTrans.position);
            if (time > 15f)
			{
                time = 0f;
			}
        }
        // �ֺ��� ���ع��� ���� ��
        else
        {
            // �ڵ������� �Ѵ�.
            Move();
        }
    }
    /// <summary>
    /// Ÿ���� ���� �ٶ󺸰� �̵��Ѵ�.
    /// </summary>
    /// <param name="target"></param>
    /// <param name="fixTarget"></param>
    private void MoveToTarget(Vector3 target, Vector3 fixTarget)
	{
        // �������� target ���������� ȸ����Ŵ
        // �������� �ٶ󺸴� ������ ����
        Vector3 from = (myTrans.position + myTrans.right) - myTrans.position;
        // ���������� �÷��̾� �� ������ ����
        Vector3 to = target;
        // Spin�� false�� �����ϸ� �������� Ÿ���� �ٶ󺸰� ��
        if (!SpinToTarget(GetAngle(from, to)))
        {
            // ���������� Ÿ������ �ٶ󺸴� ����
            Vector3 ve = fixTarget;
            // ������ ���� Ÿ���� ������ ����
            float angle = Mathf.Atan2(ve.y, ve.x) * Mathf.Rad2Deg;
            // Ÿ���� �ٶ󺸰� ������ �ٲ�
            myTrans.rotation = Quaternion.AngleAxis(angle, myTrans.forward);
        }
        // �������� �ٶ󺸴� ������ ������Ų��.
        Straight(1);
    }
    /// <summary>
    /// �̵��� �ؾ��� ��, � �������� �̵����� ���Ѵ�.
    /// </summary>
    /// <returns></returns>
    private MoveRoute FindRoute()
	{
        // FRONT,RIGHTUP, RIGHTDOWN,BACK ��� Ž���Ǵ� ���
        if (colList.FindIndex(a => a.Equals(FRONT)) != -1 && colList.FindIndex(a => a.Equals(BACK)) != -1 &&
                colList.FindIndex(a => a.Equals(RIGHTDOWN)) != -1 && colList.FindIndex(a => a.Equals(RIGHTUP)) != -1)
		{
            // �����Ѵ�.
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
        // FRONT, RIGHTUP, RIGHTDOWN ��� Ž���Ǵ� ���
        else if (colList.FindIndex(a => a.Equals(FRONT)) != -1 &&
                colList.FindIndex(a => a.Equals(RIGHTDOWN)) != -1 && colList.FindIndex(a => a.Equals(RIGHTUP)) != -1)
		{
            // ������ �Ѵ�.
            // LEFTDOWN, LEFTUP�� ���� ���
            if (colList.FindIndex(a => a.Equals(LEFTDOWN)) != -1 && colList.FindIndex(a => a.Equals(LEFTUP)) != -1)
			{
                // �ڷ� �� ������.
                oldRoute = MoveRoute.BACK;
                return oldRoute;
            }
            // LEFTDOWN�� �������
            else if (colList.FindIndex(a => a.Equals(LEFTDOWN)) != -1)
			{
                if (oldRoute == MoveRoute.BACKLEFT || oldRoute == MoveRoute.BACK)
				{
                    return oldRoute;
				}

                // ����Ȯ���� BACKLEFT�Ѵ�. ���� Ȯ���� BACK�Ѵ�.
                oldRoute = Random.Range(0, 100) > 70 ? MoveRoute.BACKLEFT : MoveRoute.BACK;
                return oldRoute;
			}
            // LEFTUP�� �������
            else if (colList.FindIndex(a => a.Equals(LEFTUP)) != -1)
			{
                if (oldRoute == MoveRoute.BACKRIGHT || oldRoute == MoveRoute.BACK)
				{
                    return oldRoute;
				}

                // ����Ȯ���� BACKRIGHT�Ѵ�. ���� Ȯ���� BACK �Ѵ�.
                oldRoute = Random.Range(0, 100) > 70 ? MoveRoute.BACKRIGHT : MoveRoute.BACK;
                return oldRoute;
			}
            // �ڰ� �ȸ��� ���
            else
			{
                // �ڷ� �� ������.
                oldRoute = MoveRoute.BACK;
                return oldRoute;
			}
		}
        // �� �ܿ� ���
        else
		{
            // RIGHTDOWN, FRONT�� �������
            if (colList.FindIndex(a => a.Equals(RIGHTDOWN)) != -1 && colList.FindIndex(a => a.Equals(FRONT)) != -1)
			{
                // ��ȸ��
                oldRoute = MoveRoute.GOLEFT;
                return oldRoute;
			}
            // RIGHTUP, FRONT�� �������
            else if (colList.FindIndex(a => a.Equals(RIGHTUP)) != -1 && colList.FindIndex(a => a.Equals(FRONT)) != -1)
			{
                // ��ȸ��
                oldRoute = MoveRoute.GORIGHT;
                return oldRoute;
			}
            // FRONT�� �������
            else if (colList.FindIndex(a => a.Equals(FRONT)) != -1)
            {
                if (oldRoute == MoveRoute.GOLEFT || oldRoute == MoveRoute.GORIGHT)
                {
                    return oldRoute;
                }

                // ��ȸ�� Ȥ�� ��ȸ��
                oldRoute = Random.Range(0, 100) > 50 ? MoveRoute.GOLEFT : MoveRoute.GORIGHT;
                return oldRoute;
			}
            // �� ��
            else
			{
                // ����
                oldRoute = MoveRoute.GO;
                return oldRoute;
			}
		}
	}

    /// <summary>
    /// �� �� ������ ������ ���Ѵ�.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    private float GetAngle(Vector3 from, Vector3 to)
	{
        // from �� to ������ ������ ���Ѵ�. transform.forward�� �ٸ� ���Ͱ� ȸ���ϴ� ������ �Ǵ� ����
        return Vector3.SignedAngle(from, to, transform.forward);
	}
    /// <summary>
    /// ���ع��� �����ϴ��� Ȯ�� �ϴ� �Լ�
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
    /// MoveRoute�� ���� �̵���Ű�� �Լ��� ȣ���Ѵ�.
    /// </summary>
    private void Move()
	{
        switch (FindRoute())
        {
            case MoveRoute.STOP:
                //Debug.Log("����");
                // �������·� ���� �� �ش� �Լ� ����
                ResetState(PoliceState.STOP);
                return;
                break;
            case MoveRoute.GO:
                //Debug.Log("����");
                Straight(1);
                break;
            case MoveRoute.GORIGHT:
                //Debug.Log("�����ȸ��");
                Spin(-2f);
                Straight(0.1f);
                break;
            case MoveRoute.GOLEFT:
                //Debug.Log("������ȸ��");
                Spin(2f);
                Straight(0.1f);
                break;
            case MoveRoute.BACK:
                //Debug.Log("����");
                Back(1);
                break;
            case MoveRoute.BACKRIGHT:
                //Debug.Log("������ȸ��");
                Spin(2f);
                Back(0.1f);
                break;
            case MoveRoute.BACKLEFT:
                //Debug.Log("������ȸ��");
                Spin(-2f);
                Back(0.1f);
                break;
        }
    }
    /// <summary>
    /// �������� ȸ����ŵ�ϴ�. angle�� ��ȣ�� ���� ȸ�������� �ٸ��ϴ�.
    /// angle�� ��ȣ�� �ٲ�� ������ ����(angle�� 0 ��ó�ϋ�) ��ǥ�� �ٶ󺸴� ������ �����ϰ� false�� ����. ȸ���� ����
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    private bool SpinToTarget(float angle)
	{
        // Mathf.Abs(angle) <= 5���� 5�� 180���� -180���� �Ѿ�� ��츦 �����ϱ� ���� �밭 ������ �� 
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
    /// angle ��ŭ �������� ȸ����Ų��.
    /// </summary>
    /// <param name="angle"></param>
    private void Spin(float angle)
	{
        oldAngle = -999;
        myTrans.localEulerAngles += new Vector3(0,0,angle);
	}
    /// <summary>
    /// �������� �ٶ󺸴� �������� �̵���Ų��.
    /// </summary>
    private void Straight(float k)
	{
        myTrans.localPosition += myTrans.right * Time.deltaTime * Speed * k;
	}
    /// <summary>
    /// �������� �ٶ󺸴� �������� ������Ų��.
    /// </summary>
    /// <param name="k"></param>
    private void Back(float k)
	{
        myTrans.localPosition -= myTrans.right * Time.deltaTime * Speed * k;
        //Debug.Log("�ߵ�??");
    }
    /// <summary>
    /// �÷��̾��� Ʈ�������� ������. ��ġ�� ������ �߰��ϱ� ����
    /// </summary>
    /// <param name="trans"></param>
    public void SetTransform(Transform trans)
	{
        playerTrans = trans;
        GunMethod.PlayerTransfrom = playerTrans;
    }
    /// <summary>
    /// ���¸� �����ϰ� �ð��� �ʱ�ȭ��Ŵ.
    /// </summary>
    /// <param name="state"></param>
    private void ResetState(PoliceState state)
	{
        policeState = state;
        time = 0f;
	}
    /// <summary>
    /// �������� �������� �����Ѵ�.
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
    /// ������ ���¿� ���� �þ��� ���� �ٲ��ش�.
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
    /// ������ �Ͻ� ����. Ǯ���� �̵��Ѵ�.
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
        
        // Ư����Ȳ���� ��� �߰� ������ ����
        if (ControlMove()) 
        {
            if (playerTrans.GetComponent<PlayerMove>().Stop)
            {
                Debug.Log(playerTrans.GetComponent<PlayerMove>().Stop);
                PoliceAudio.Stop();
            }
            return; 
        }
        if (policeState == PoliceState.DESTROY ||  policeState == PoliceState.NONE) 
        {
            if (playerTrans.GetComponent<PlayerMove>().Stop)
            {
                Debug.Log(playerTrans.GetComponent<PlayerMove>().Stop);
                PoliceAudio.Stop();
            }
            return; 
        }

        time += Time.deltaTime;
        ChangeFOVColor(policeState);
        if(!playerTrans.GetComponent<PlayerMove>().Stop)
        {
            if (iGetBool.GetBool() && !PoliceAudio.isPlaying)
            {
                PoliceAudio.Play();
            }
            else if (!iGetBool.GetBool() && PoliceAudio.isPlaying)
            {
                PoliceAudio.Stop();
            }
        }
        else if(playerTrans.GetComponent<PlayerMove>().Stop)
        {
            Debug.Log(playerTrans.GetComponent<PlayerMove>().Stop);
            PoliceAudio.Stop();
        }
        // �÷��̾ �߰��Ѵٸ� ���� �ڼ�
        GunMethod.ShootingStance = iGetBool.GetBool();
        GunMethod.Fire(1f, 10);

        switch (policeState)
        {
            // �÷��̾���� �Ÿ� ������� ������ �Ѿƿ��� ������.
            case PoliceState.SPUERCHASE:
                // �÷��̾ ������ �Ѿƿ�
                SpuerChase();
                // ���� �÷��̾ �߰��Ѵٸ� �ð��� 0�ʷ� ������ SUPERCHASE ���¸� ����.
                if (iGetBool.GetBool())
                {
                    ResetState(PoliceState.SPUERCHASE);
                }
                // ���� 20�ʰ� ������ �ڵ�������� �ٲ�
                if (time >= 20f)
                {
                    ResetState(PoliceState.AUTOMOVE);
                }
                break;
            // ���� �������� ���ƴٴϴ� ������
            case PoliceState.AUTOMOVE:
                AutoMove();
                // ���� �÷��̾ �߰��Ѵٸ� �ð��� 0�ʷ� ������ SUPERCHASE ���·� ��ȯ
                if (iGetBool.GetBool())
				{
                    ResetState(PoliceState.SPUERCHASE);
				}
                // �ڵ����� ��ΰ� 30�ʵ��� �̷������ �� ������ ������ ���°� ��.
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
                // ���� �÷��̾ �߰��Ѵٸ� �ð��� 0�ʷ� ������ SUPERCHASE ���·� ��ȯ
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
