using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PoliceNS.PoliceStateNS;

// 한석호 작성
// 경찰차가 가지는 공통적인 걸 담았다.
public abstract class Police : MonoBehaviour
{
    [Range(0f, 1000f)] public float PoliceHp;    // 경찰차 체력
    public ISpawnCar SpawnCar { get; set; }    // 추격차를 소환하기 위한 인터페이스

    protected ISetTransform smokeEffectTrans;
    protected IStop iStop;

    //protected SuperPoliceState spState;
    protected PoliceState policeState;    //  경찰차의 상태
    protected PoliceType policeType;    // 경찰차 타입
    protected Coroutine smokeEffectCoroutine;   // 피해 입을 시 생기는 연기 코루틴
    protected Coroutine damagedCoroutine;   // 피해 입을시 차량 색상이 바뀌는 이펙트 코루틴
    protected SpriteRenderer spr;

    protected  virtual void Awake()
	{
        if (this.GetComponent<SpriteRenderer>() != null)
		{
            spr = this.GetComponent<SpriteRenderer>();
		}
	}

	protected virtual void Start()
	{
        smokeEffectCoroutine = StartCoroutine(PoliceSmokeCoroutine());
    }

    public void SetSmokeEffectTrans(ISetTransform iSetTransform)
	{
        smokeEffectTrans = iSetTransform;
	}
    /// <summary>
    /// 경찰차의 체력을 확인하고 지속적으로 연기를 내뿜게할지 정하는 코루틴
    /// </summary>
    /// <returns></returns>
    protected IEnumerator PoliceSmokeCoroutine()
    {
        var time = new WaitForSeconds(0.01f);
        int r = 0;
        while (true)
        {
            r = Random.Range(5, 15);

            if (PoliceHp < 70f)
            {
                smokeEffectTrans.SetTransform(this.transform);
            }
            // 경찰차 체력이 0이 되면 rigidbody-constrait을 해제하고 10초 후 제거하도록함.
            if (PoliceHp <= 0f && policeState != PoliceState.DESTROY)
            {
                if (this.GetComponent<Rigidbody2D>() != null)
                {
                    this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                }
                // 파괴 상태로 변경
                policeState = PoliceState.DESTROY;
                // 상태에 따른 콜라이더 초기화
                InitState(false);
                // 파괴
                //Destroy(this.gameObject, 10f);
                //StopCoroutine(smokeEffectCoroutine);
                Invoke("AddForceCar", 9f);
            }

            for (int i = 0; i < r; i++)
            {
                yield return time;
            }
        }
    }
    protected IEnumerator Damaged()
	{
        spr.color = Color.red;
        yield return Constant.OneTime;
        spr.color = Color.white;
        yield return Constant.OneTime;
        spr.color = Color.green;
        yield return Constant.OneTime;
        spr.color = Color.white;
        yield return Constant.OneTime;
        spr.color = Color.red;
        yield return Constant.OneTime;
        spr.color = Color.white;
        yield return Constant.OneTime;
    }
    protected virtual void InitState(bool bo)
	{
        
	}
    protected virtual void OnCollisionEnter2D(Collision2D collision)
	{
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
    }
    protected void AddForceCar()
	{
        if (policeState == PoliceState.DESTROY)
        {
            if (this.GetComponent<Rigidbody2D>() != null)
            {
                this.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(0, 10f), Random.Range(0, 10f)), ForceMode2D.Impulse);
            }
        }

        Invoke("DestroyCar", 5f);
    }

    protected void DestroyCar()
    {
        StopCoroutine(smokeEffectCoroutine);
        if (policeType == PoliceType.NORMAL)
        {
            SpawnCar.SpawnCar(1);
        }
        DestroyPolice();
        Destroy(this.gameObject);
    }
    protected virtual void DestroyPolice() { }

    public void SetMap(IStop iStop)
    {
        this.iStop = iStop;
    }

    public virtual void PausePoliceCar(bool bo)
    {

    }
}
