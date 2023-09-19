using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 한석호 작성

public class PoliceCarCollisionCheck : MonoBehaviour
{    
    private List<IPriorityCode> priorityList = new List<IPriorityCode>();

    private IMovingPoliceCarControl iPoliceCarControl;
    private IPriorityCode iPriorityCode;
    //경찰차가 다른 경찰차끼리 충돌할 우려가 있는지 체크한다.
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

    // 근처에 있는 경찰차들과 충돌하지 않게끔하기 위해 우선순위에 따라 먼저 움직일 자동차를 정해주는 함수이다.
    private void CheckPriority()
    {
        if (iPoliceCarControl == null) { return; }
        if (priorityList == null) { return; }

        if (priorityList.FindIndex(a => a.GetPriorityCode() > iPriorityCode.GetPriorityCode()) != -1)
        {
            iPoliceCarControl.SetIsBehaviour(false);
        }
        else
        {
            iPoliceCarControl.SetIsBehaviour(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IMovingPoliceCarControl>() != null)
        {
            priorityList.Remove(collision.gameObject.GetComponent<IPriorityCode>());
            // 1초 후에 우선순위를 고려한다.
            Invoke("CheckPriority", 1f);
        }
    }

    public void SetIPoliceCarIsBehaviour(IMovingPoliceCarControl iPoliceCarIsBehaviour)
    {
        this.iPoliceCarControl = iPoliceCarIsBehaviour;
    }

    public void SetIPriority(IPriorityCode iPriorityCode)
    {
        this.iPriorityCode = iPriorityCode;
    }
}
