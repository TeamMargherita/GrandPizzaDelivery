using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PoliceNS.PoliceStateNS;

// 경찰차가 가지는 공통적인 걸 담았다.
public abstract class Police : MonoBehaviour
{
    [Range(0f, 1000f)] public float PoliceHp;    // 경찰차 체력

    protected ISetTransform smokeEffectTrans;
    protected IStop iStop;

    protected SuperPoliceState spState;

    protected Coroutine smokeEffectCoroutine;

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
            if (PoliceHp <= 0f && spState != SuperPoliceState.DESTROY)
            {
                if (this.GetComponent<Rigidbody2D>() != null)
                {
                    this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                }
                // 파괴 상태로 변경
                ChangeSuperPoliceState(SuperPoliceState.DESTROY);
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
    protected virtual void InitState(bool bo)
	{

	}
    protected virtual void ChangeSuperPoliceState(SuperPoliceState spState)
	{
        this.spState = spState;
	}
    protected void AddForceCar()
	{
        if (spState == SuperPoliceState.DESTROY)
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
        DestroyPolice();
        Destroy(this.gameObject);
    }
    protected virtual void DestroyPolice() { }

    public void SetMap(IStop iStop)
    {
        this.iStop = iStop;
    }
}
