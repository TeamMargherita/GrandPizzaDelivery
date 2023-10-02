using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 한석호 작성
public class ChasePoliceCar : MonoBehaviour, ISetTransform
{
    /// <summary>
    /// 추격 경찰차 상태. SPUERCHASE는 거리 상관없이 무조건 플레이어 쫓아오는 상태
    /// </summary>
    private enum ChaserPoliceState { NONE, SPUERCHASE};

    private Transform playerTrans;
    private Coroutine stateCoroutine;

    private RaycastHit2D hit;

    private ChaserPoliceState chaserPoliceState = ChaserPoliceState.SPUERCHASE;

    private float time;

    // Start is called before the first frame update
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

    private void SpuerChase()
	{

	}

    private bool FindPlayer()
	{

        return false;
	}

    /// <summary>
    /// 플레이어의 트랜스폼을 가져옴. 위치를 가져와 추격하기 위함
    /// </summary>
    /// <param name="trans"></param>
    public void SetTransform(Transform trans)
	{
        playerTrans = trans;
	}

	private void FixedUpdate()
	{
        time += Time.deltaTime;

        switch(chaserPoliceState)
		{
            // 플레이어와의 거리 상관없이 무조건 쫓아오는 상태임.
            case ChaserPoliceState.SPUERCHASE:
                // 플레이어를 무조건 쫓아옴
                SpuerChase();
                // 만약 플레이어를 발견한다면

                    break;
		}
	}
}
