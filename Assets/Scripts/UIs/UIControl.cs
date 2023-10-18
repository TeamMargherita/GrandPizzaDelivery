using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingNS.HouseNS;

// 한석호 작성

public class UIControl : MonoBehaviour, IInspectingPanelControl, IDeliveryPanelControl, IHouseActiveUIControl, IAlarmMessagePanel
{
    [SerializeField] private GameObject inspectingPanel;
    [SerializeField] private GameObject inspectingMaskPanel;
    [SerializeField] private GameObject deliveryPanel;
    [SerializeField] private GameObject keyExplainPanel;
    [SerializeField] private GameObject pizzaStorePanel;
    [SerializeField] private GameObject pizzaStoreMaskPanel;
    [SerializeField] private GameObject pizzaMakePanel;
    [SerializeField] private GameObject pizzaMenuPanel;
    [SerializeField] private GameObject employeeRecruitPanel;
    [SerializeField] private GameObject alarmMessagePanel;
    [SerializeField] private GameObject DeliveryJudgmentPanel;

    [SerializeField] private UnityEngine.UI.Image addPizzaImg;
    [SerializeField] private UnityEngine.UI.Text alarmMessageText;
     
    private IEndInspecting iEndInspecting;
    private IHouse iHouse;
    private IStop iStop;

    private HouseType houseType;
    
    private RectTransform inspectTrans;
    private RectTransform pizzaStoreTrans;
    private RectTransform pizzaMakeTrans;
    private RectTransform pizzaMenuTrans;
    private RectTransform employeeRecruitTrans;
    private RectTransform alarmMessageTrans;

    private Vector3 alarmMessageStart = new Vector3(0, 590);
    private Vector3 alarmMessageEnd = new Vector3(0, 490);

    private int inspectingHeight = 0;   // 불심검문 패널창 높이
    private int pizzaStoreHeight = 0;   // 피자집 패널창 높이
    private int pizzaMakeWitdh = 0; // 피자만들기 패널창 너비
    private int pizzaMenuHeight = 0;    // 피자메뉴 패널창 높이;

    private bool isInspecting = false;  // 불심검문중 창이 떠야하는지 여부
    private bool isPizzaStore = false;  // 피자집 창이 떠야하는지 여부
    private bool isPizzaMake = false;
    private bool isPizzaMenu = false;
    private bool isPizzaAddButtonBlank = false;
    private bool isAlarmMessage = false;    // 알람 메시지 창이 떠야하는지 여부
    private bool isColor = false;

    public GameObject PizzaInventory;
    public InventoryManager InventoryManager;
    void Awake()
    {
        Caching();

        houseType = HouseType.NONE;

        if (Constant.IsMakePizza)
		{
            DirectADdPizzaMenu();
        }
        //PizzaInventory = GameObject.FindWithTag("PizzaInventory");
    }
    private void Caching()
	{
        inspectTrans = inspectingMaskPanel.GetComponent<RectTransform>();
        pizzaStoreTrans = pizzaStoreMaskPanel.GetComponent<RectTransform>();
        pizzaMakeTrans = pizzaMakePanel.GetComponent<RectTransform>();
        pizzaMenuTrans = pizzaMenuPanel.GetComponent<RectTransform>();
        employeeRecruitTrans = employeeRecruitPanel.GetComponent<RectTransform>();
        alarmMessageTrans = alarmMessagePanel.GetComponent<RectTransform>();
        alarmMessageText = alarmMessagePanel.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>();
	}
    /// <summary>
    /// 알림 창 열고닫기. 알림창에 보여줄 텍스트도 설정해야됨
    /// </summary>
    /// <param name="isOn"></param>
    /// <param name="text">띄울 텍스트를 적는다.</param>
    public void ControlAlarmMessageUI(bool isOn, string text)
	{
        alarmMessageText.text = text;
        isAlarmMessage = isOn;
	}

    /// <summary>
    /// 리듬게임이 끝난 후 바로 피자 메뉴 UI로 갈 때 사용하는 함수
    /// </summary>
    private void DirectADdPizzaMenu()
	{
        pizzaStorePanel.SetActive(true);
        isPizzaStore = true;
        pizzaStoreHeight = 1080;
        pizzaStoreTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, pizzaStoreHeight);
        iStop.StopMap(true);
        pizzaMenuPanel.SetActive(true);
        isPizzaMenu = true;
        pizzaMenuHeight = 1080;
        pizzaMenuTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, pizzaMenuHeight);
        isPizzaAddButtonBlank = true;
    }
    /// <summary>
    /// 불심검문창 열고닫기
    /// </summary>
    /// <param name="isOn"></param>
    /// <param name="iEndInspecting"></param>
    public void ControlInspectUI(bool isOn, IEndInspecting iEndInspecting, int type)
    {
        if (iEndInspecting != null)
		{
            this.iEndInspecting = iEndInspecting; 
		}

        if (isOn)
        {
            inspectingPanel.SetActive(isOn);
            isInspecting = isOn;
            inspectingPanel.GetComponent<InspectingUIControl>().ChoiceConversation(type);
        }
        else if (!isOn && inspectingHeight >= 1080)
        {
            isInspecting = false;
            this.iEndInspecting.EndInspecting();
            this.iEndInspecting = null;
        }
        ChasePoliceCar.isStop = isOn;
    }
    public void ControlPizzaStore(bool isOn)
    {
        if (isOn)
        {
            ChasePoliceCar.isStop = isOn;
            pizzaStorePanel.SetActive(isOn);
            isPizzaStore = isOn;
        }
        else
        {
            isPizzaStore = isOn;
        }
    }
    public void ControlPizzaMake(bool isOn)
    {
        if (isOn)
        {
            pizzaMakePanel.SetActive(isOn);
            isPizzaMake = isOn;
        }
        else
        {
            isPizzaMake = isOn;
        }
    }

    public void ControlPizzaMenu(bool isOn)
	{
        if (isOn)
		{
            pizzaMenuPanel.SetActive(isOn);
            isPizzaMenu = isOn;
		}
        else
		{
            isPizzaMenu = isOn;
            isPizzaAddButtonBlank = false;
            addPizzaImg.color = Color.white;
		}
	}

	public void ControlEmployeeRecruit(bool isOn)
	{
		employeeRecruitPanel.SetActive(isOn);
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

        //iHouse.DisableHouse();
        deliveryPanel.SetActive(false);
        //PizzaInventory.SetActive(true);
        //InventoryManager.InventoryActive = true;
        //InventoryManager.CurrentInventory = PizzaInventory;
        InventoryManager.OnClickDelivery();
        InventoryManager.inventoryTextUpdate(PizzaInventory.name);
    }
    public void NODeliveryUI()
    {
        deliveryPanel.SetActive(false);
    }

    public void OKDeliveryJudgmentPanel()
    {
        DeliveryJudgmentPanel.SetActive(false);
    }

    public void ActiveTrueKeyExplainPanel(bool bo)
    {
        keyExplainPanel.SetActive(bo);
    }

    public void SetHouseType(HouseType houseType)
    {
        this.houseType = houseType;
    }

    public void SetIStop(IStop iStop)
    {
        this.iStop = iStop;
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
        
        if (isPizzaStore && pizzaStoreHeight < 1080)
        {
            pizzaStoreHeight += 40;
            pizzaStoreTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, pizzaStoreHeight);
        }
        else if (!isPizzaStore && pizzaStoreHeight >= 1080)
        {
            pizzaStoreHeight = 0;
            pizzaStoreTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, pizzaStoreHeight);
            pizzaStorePanel.SetActive(false);
            iStop.StopMap(false);
            ChasePoliceCar.isStop = false;
        }

        if (isPizzaMake && pizzaMakeWitdh < 1920)
        {
            pizzaMakeWitdh += 80;
            pizzaMakeTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, pizzaMakeWitdh);
        }
        else if (!isPizzaMake && pizzaMakeWitdh >= 1920)
        {
            pizzaMakeWitdh = 0;
            pizzaMakeTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, pizzaMakeWitdh);
            pizzaMakePanel.SetActive(false);
        }

        if (isPizzaMenu && pizzaMenuHeight < 1080)
        {
            pizzaMenuHeight += 40;
            pizzaMenuTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, pizzaMenuHeight);
        }
        else if (!isPizzaMenu && pizzaMenuHeight >= 1080)
        {
            pizzaMenuHeight = 0;
            pizzaMenuTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, pizzaMenuHeight);
            pizzaMenuPanel.SetActive(false);
        }

        if (isPizzaAddButtonBlank)
        {
            if (isColor)
            {
                addPizzaImg.color += new Color(5 / 255f, 0, 0, 0);
                if (addPizzaImg.color.r >= 1) { isColor = false; }
            }
            else
			{
                addPizzaImg.color -= new Color(5 / 255f, 0, 0, 0);
                if (addPizzaImg.color.r <= 0) { isColor = true; }
            }
        }

        if (isAlarmMessage && alarmMessageTrans.localPosition != alarmMessageEnd)
		{
            alarmMessageTrans.localPosition = Vector3.Lerp(alarmMessageTrans.localPosition, alarmMessageEnd, 0.1f);

            if (Mathf.Abs(alarmMessageTrans.localPosition.y - alarmMessageEnd.y) <= 0.1f)
			{
                alarmMessageTrans.localPosition = alarmMessageEnd;
                isAlarmMessage = false;
			}
		}
        else if (!isAlarmMessage && alarmMessageTrans.localPosition != alarmMessageStart)
		{
            alarmMessageTrans.localPosition = Vector3.Lerp(alarmMessageTrans.localPosition, alarmMessageStart, 0.1f);

            if (Mathf.Abs(alarmMessageTrans.localPosition.y - alarmMessageStart.y) <= 0.1f)
            {
                alarmMessageTrans.localPosition = alarmMessageStart;
            }
        }
    }

    public void Update()
    {
        // 특별한 장소에서 Z키를 누를 시
        if (houseType != HouseType.NONE && houseType != HouseType.HOUSE
            && Input.GetKeyDown(KeyCode.Z))
        {
            switch(houseType)
            {
                case HouseType.PIZZASTORE:
                    //houseType = HouseType.NONE;
                    //플레이어 멈추고, 경찰차 멈춰야함.
                    iStop.StopMap(true);
                    // 피자창 활성화
                    ControlPizzaStore(true);
                    break;
            }
        }
    }
}
