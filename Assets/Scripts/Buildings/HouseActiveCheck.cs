using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 한석호 작성

public class HouseActiveCheck : MonoBehaviour
{
    private IActiveHouse iActiveHouse;
    private IHouseActiveUIControl iHouseActiveUIControl;
    private bool isIn = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.tag.Equals("Player")) { return; }
        // 건물 활성화
        if (iActiveHouse.ActiveHouse(true))
        {
            // 조작키 설명 패널을 켜준다.
            iHouseActiveUIControl.ActiveTrueKeyExplainPanel(true);
            // House타입을 UIControl에 전달한다.
            iActiveHouse.IntoHouse(true);
            isIn = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.tag.Equals("Player")) { return; }

        if (isIn)
        {
            iActiveHouse.IntoHouse(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.tag.Equals("Player")) { return; }

        if (iActiveHouse.ActiveHouse(false))
        {
            // 조작키 설명 패널을 꺼준다.
            iHouseActiveUIControl.ActiveTrueKeyExplainPanel(false);
            // UIContorl의 houseType을 초기화해준다.
            iActiveHouse.IntoHouse(false);
            isIn = false;
        }
    }

    public void SetIActiveHouse(IActiveHouse iActiveHouse)
    {
        this.iActiveHouse = iActiveHouse;
    }
    public void SetIHouseActiveUIControl(IHouseActiveUIControl iHouseActiveUIControl)
    {
        this.iHouseActiveUIControl = iHouseActiveUIControl;
    }
}
