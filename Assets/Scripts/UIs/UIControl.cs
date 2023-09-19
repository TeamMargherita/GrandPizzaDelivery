using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 한석호 작성

public class UIControl : MonoBehaviour, IInspectingPanelControl, IDeliveryPanelControl
{
    [SerializeField] private GameObject inspectingPanel;
    [SerializeField] private GameObject inspectingMaskPanel;
    [SerializeField] private GameObject deliveryPanel;

    private IEndInspecting iEndInspecting;
    private IHouse iHouse;
    private RectTransform inspectTrans;

    private int inspectingHeight = 0;
    private bool isInspecting = false;  // 불심검문중 창이 떠야하는지 여부
    void Awake()
    {
        inspectTrans = inspectingMaskPanel.GetComponent<RectTransform>();
    }

    public void ControlInspectUI(bool isOn, IEndInspecting iEndInspecting)
    {
        if (iEndInspecting != null)
		{
            this.iEndInspecting = iEndInspecting; 
		}

        if (isOn)
        {
            inspectingPanel.SetActive(isOn);
            isInspecting = isOn;
        }
        else if (!isOn && inspectingHeight >= 1080)
        {
            isInspecting = false;
            this.iEndInspecting.EndInspecting();
            this.iEndInspecting = null;
        }

    }

    public void ControlDeliveryUI(bool isOn)
    {
        deliveryPanel.SetActive(isOn);
    }
    public void SetIHouseDeliveryUI(IHouse iHouse)
    {
        this.iHouse = iHouse;
    }
    public void OKDeliveryUI()
    {
        if (iHouse == null) { return; }

        iHouse.DisableHouse();
        deliveryPanel.SetActive(false);
    }
    public void NODeliveryUI()
    {
        deliveryPanel.SetActive(false);
    }

    void FixedUpdate()
    {
        if (isInspecting && inspectingHeight < 1080)
        {
            inspectingHeight += 40;
            inspectTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, inspectingHeight);
        }
        else if (!isInspecting && inspectingHeight >= 1080)
        {
            inspectingHeight = 0;
            inspectTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, inspectingHeight);
            inspectingPanel.SetActive(false);
        }
    }
}
