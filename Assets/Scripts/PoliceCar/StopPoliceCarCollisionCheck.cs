using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PoliceNS.PoliceStateNS;
// 한석호 작성

public class StopPoliceCarCollisionCheck : MonoBehaviour
{
    private List<IPriorityCode> priorityList = new List<IPriorityCode>();

    private IMovingPoliceCarControl iPoliceCarControl;
    private IPriorityCode iPriorityCode;
    private IInspectingPanelControl iInspectingPanelControl;
    private IInspectingPoliceCarControl iInspectingPoliceCarControl;
    private IEndInspecting iEndInspecting;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 경찰차들의 불심검문 범위가 중복되었을 때 한 곳에서만 불심검문을 발동시키도록 한다. 
        if (collision.gameObject.GetComponent<IPriorityCode>() != null)
        {
            priorityList.Add(collision.gameObject.GetComponent<IPriorityCode>());
            // 우선수위를 고려하여 가장 우선순위가 높은 경찰차를 제외한 나머지는 불심검문을 막는다.
            if (!CheckPriority())
			{
                return;
			}
        }

        // 태그가 플레이어라면 경찰차는 불심검문중 상태에 들어간다.
        if (collision.tag == "Player")
        {

            // 불심검문 중일 때 다른 경찰차에게 중복해서 불심검문을 받지 못하도록 한다.
            if (!PoliceCar.IsInspecting)
            {
                // 상태를 불심검문중으로 바꾼다.
                iInspectingPoliceCarControl.SetPoliceState(PoliceState.INSPECTING);

                // 카메라를 해당 경찰차쪽으로 확대 및 이동시키고, 불심검문중 창을 띄운다.
                iInspectingPanelControl.ControlInspectUI(true, iEndInspecting, 1);
            }
        }
    }

    public void SetIInspectingPoliceCarControl(IInspectingPoliceCarControl iInspectingPoliceCarControl)
    {
        this.iInspectingPoliceCarControl = iInspectingPoliceCarControl;
    }

    public void SetIInspectingPanelControl(IInspectingPanelControl iInspectingPanelControl)
    {
        this.iInspectingPanelControl = iInspectingPanelControl;
    }

    // 근처에 있는 경찰차들의 불심검문 범위가 중복되었을 때 어느 차에서 불심검문을 해야할지 정해주는 함수이다.
    private bool CheckPriority()
    {
        if (iPoliceCarControl == null) { return false; }
        if (priorityList == null) { return false; }

        if (priorityList.FindIndex(a => a.GetPriorityCode() > iPriorityCode.GetPriorityCode()) != -1)
        {
            // 이 경찰차는 불심검문을 할 수 없다.
            return false;
        }
        else
        {
            // 이 경찰차는 불심검문을 할 수 있다.
            return true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IMovingPoliceCarControl>() != null)
        {
            priorityList.Remove(collision.gameObject.GetComponent<IPriorityCode>());
        }
    }

    public void SetIPoliceCarIsBehaviour(IMovingPoliceCarControl iPoliceCarIsBehaviour)
    {
        this.iPoliceCarControl = iPoliceCarIsBehaviour;
    }
    
    public void SetIEndInspecting(IEndInspecting iEndInspecting)
	{
        this.iEndInspecting = iEndInspecting;
	}
    public void SetIPriority(IPriorityCode iPriorityCode)
    {
        this.iPriorityCode = iPriorityCode;
    }
}
