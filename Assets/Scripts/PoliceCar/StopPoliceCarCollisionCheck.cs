using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PoliceNS.PoliceStateNS;
// 한석호 작성

public class StopPoliceCarCollisionCheck : MonoBehaviour
{
    private IInspectingPanelControl iInspectingPanelControl;
    private IInspectingPoliceCarControl iInspectingPoliceCarControl;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 태그가 플레이어라면 경찰차는 불심검문중 상태에 들어간다.
        if (collision.tag == "Player")
        {
            // 상태를 불심검문중으로 바꾼다.
            iInspectingPoliceCarControl.SetPoliceState(PoliceState.INSPECTING);
            // 플레이어를 멈춘다.

            // 카메라를 해당 경찰차쪽으로 확대 및 이동시키고, 불심검문중 창을 띄운다.
            iInspectingPanelControl.ControlInspectUI(true);
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
}
