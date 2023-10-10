using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 한석호 작성

public class PoliceCarCollisionCheck : MonoBehaviour
{    
    private List<IPriorityCode> priorityList = new List<IPriorityCode>();

    private IMovingPoliceCarControl iPoliceCarControl;  // 경찰차 제어 인터페이스
    private IPriorityCode iPriorityCode;    // 경찰차 우선순위 인터페이스

    /// <summary>
    /// 경찰차가 다른 경찰차끼리 충돌할 우려가 있는지 체크한다.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //충돌할 우려가 있다면 자동차의 행동을 제어한다.
        if (collision.gameObject.GetComponent<IPriorityCode>() != null)
        {
            priorityList.Add(collision.gameObject.GetComponent<IPriorityCode>());
            // 즉시 우선수위를 고려한다.
            CheckPriority();
        }
    }

    /// <summary>
    /// 근처에 있는 경찰차들과 충돌하지 않게끔하기 위해 우선순위에 따라 먼저 움직일 자동차를 정해주는 함수이다.
    /// </summary>
    private void CheckPriority()
    {

        if (iPoliceCarControl == null) { return; }
        if (priorityList == null) { return; }
        // 현재 콜라이더가 겹쳐있는 경찰차들의 우선순위 중에서 해당 경찰차가 가장 우선순위가 높은지 확인하는 조건이다.
        if (priorityList.FindIndex(a => a.GetPriorityCode() > iPriorityCode.GetPriorityCode()) != -1)
        {
            // 현재 경찰차의 행동을 멈춘다.
            iPoliceCarControl.SetIsBehaviour(false);
        }
        else
        {
            // 현재 경찰차의 행동을 재개한다.
            iPoliceCarControl.SetIsBehaviour(true);
        }
    }
    /// <summary>
    /// 다른 경찰차가 하나 지나가면 1초 후에 우선순위를 다시 고려하여 경찰차의 상태를 따진다.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IMovingPoliceCarControl>() != null)
        {
            priorityList.Remove(collision.gameObject.GetComponent<IPriorityCode>());
            // 1초 후에 우선순위를 고려한다.
            Invoke("CheckPriority", 1f);
        }
    }
    /// <summary>
    /// 경찰차 제어에 관한 인터페이스를 가져온다.
    /// </summary>
    /// <param name="iPoliceCarIsBehaviour"></param>
    public void SetIPoliceCarIsBehaviour(IMovingPoliceCarControl iPoliceCarIsBehaviour)
    {
        this.iPoliceCarControl = iPoliceCarIsBehaviour;
    }
    /// <summary>
    /// 경찰차 우선순위 번호와 관련된 인터페이스를 가져온다.
    /// </summary>
    /// <param name="iPriorityCode"></param>
    public void SetIPriority(IPriorityCode iPriorityCode)
    {
        this.iPriorityCode = iPriorityCode;
    }
}
