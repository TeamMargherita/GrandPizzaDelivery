using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 한석호 작성

public class UIControl : MonoBehaviour, IInspectingPanelControl
{
    [SerializeField] private GameObject inspectingPanel;
    [SerializeField] private GameObject inspectingMaskPanel;

    private RectTransform inspectTrans;

    private int inspectingHeight = 0;
    private bool isInspecting = false;  // 불심검문중 창이 떠야하는지 여부
    void Awake()
    {
        inspectTrans = inspectingMaskPanel.GetComponent<RectTransform>();
    }

    public void ControlInspectUI(bool isOn)
    {
        if (isOn)
        {
            inspectingPanel.SetActive(isOn);
            isInspecting = isOn;
        }
        else if (!isOn && inspectingHeight >= 1080)
        {
            isInspecting = false;
        }

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
