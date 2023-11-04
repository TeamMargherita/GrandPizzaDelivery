using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingNS.HouseNS;
using UnityEngine.Experimental.Rendering.Universal;
// 한석호 작성

public class UIControl : MonoBehaviour, IConversationPanelControl, IDeliveryPanelControl, IHouseActiveUIControl, IAlarmMessagePanel
{
    [SerializeField] private GameObject inspectingPanel;    // 대화 패널
    [SerializeField] private GameObject inspectingMaskPanel;    // 대화 마스크 패널
    [SerializeField] private GameObject deliveryPanel;  // 배달 패널
    [SerializeField] private GameObject keyExplainPanel;    // 조작 설명 패널
    [SerializeField] private GameObject pizzaStorePanel;    // 피자 가게 패널
    [SerializeField] private GameObject pizzaStoreMaskPanel;    // 피자 가게 마스크 패널
    [SerializeField] private GameObject pizzaMakePanel; // 피자 만들기 패널
    [SerializeField] private GameObject pizzaMenuPanel; // 피자 메뉴 패널
    [SerializeField] private GameObject employeeRecruitPanel;   // 
    [SerializeField] private GameObject alarmMessagePanel;  // 알람 메세지 패널
    [SerializeField] private GameObject DeliveryJudgmentPanel;
    [SerializeField] private GameObject SpecialPizzaDeliverySelectionPanel;
    [SerializeField] private GameObject DeliveryAppButton;
    [SerializeField] private GameObject DarkDeliveryAppButton;
    [SerializeField] private GameObject player; // 플레이어
    [SerializeField] private GameObject makingPizzaObj;
    [SerializeField] private Light2D light2D;
    [SerializeField] private UnityEngine.UI.Image addPizzaImg;
    [SerializeField] private UnityEngine.UI.Text alarmMessageText;
    [SerializeField] private Map map;
    [SerializeField] private GameObject Menu;

    private IEndConversation iEndInspecting;
    private IHouse iHouse;
    private IStop iStop;
    private IResetPizzaMaking iResetPizzaMaking;

    private HouseType houseType;
    
    private RectTransform inspectTrans; // 대화창 RectTransform
    private RectTransform pizzaStoreTrans;  // 피자 가게 RectTransform
    private RectTransform pizzaMakeTrans;   // 피자 만들기 RectTransform
    private RectTransform pizzaMenuTrans;   // 피자 메뉴 RectTransform
    private RectTransform employeeRecruitTrans;
    private RectTransform alarmMessageTrans;    // 알람 메시지 RectTransform

    private Vector3 alarmMessageStart = new Vector3(0, 590);    // 알람 메시지 이동 시작 위치(위)
    private Vector3 alarmMessageEnd = new Vector3(0, 490);  // 알람 메시지 이동 종료 위치(아래)

    private int inspectingHeight = 0;   // 회화창 높이
    private int pizzaStoreHeight = 0;   // 피자가게 창 높이
    private int pizzaMakeWitdh = 0; // 피자 재료 선택창 너비
    private int pizzaMenuHeight = 0;    // 피자메뉴 창 높이;

    private bool isInspecting = false;  // 회화창이 다 열렸는지 여부
    private bool isPizzaStore = false;  // 피자 가게 창이 다 열렸는지 여부
    private bool isPizzaMake = false;   // 피자 만들기 창이 다 열렸는지 여부
    private bool isPizzaMenu = false;   // 피자 메뉴창이 다 열렸는지 여부
    private bool isPizzaAddButtonBlank = false;
    private bool isAlarmMessage = false;    // 알람메세지 다 내려왔는지 여부
    private bool isColor = false;
    public static bool isIn = false;  // 대화창, 가게 안으로 들어갔는지 여부
    private bool menuSetActive = false;

    public GameObject PizzaInventory;
    public InventoryManager InventoryManager;
    void Awake()
    {
        Caching();

        houseType = HouseType.NONE;

        if (Constant.IsMakePizza)
		{
            Constant.IsMakePizza = false;
            DirectADdPizzaMenu();
            player.transform.position = new Vector3(9f, 3.8f);

        }
        else if (Constant.isStartGame)
        {
            Constant.isStartGame = false;
            DirectPizzaStore();
            player.transform.position = new Vector3(9f, 3.8f);
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
        iResetPizzaMaking = makingPizzaObj.GetComponent<IResetPizzaMaking>();
	}
    /// <summary>
    /// 알람메세지 등장을 제어하는 메소드
    /// </summary>
    /// <param name="isOn"></param>
    /// <param name="text">알람을 표시할 텍스트 내용</param>
    public void ControlAlarmMessageUI(bool isOn, string text)
	{
        // 나중에 가서 알람들이 쌓일 수가 있으니 리스트에 넣어서 관리해야됨.
        alarmMessageText.text = text;
        isAlarmMessage = isOn;
	}
    /// <summary>
    /// 곧바로 피자 가게까지 열어주는 메소드
    /// </summary>
    private void DirectPizzaStore()
    {
        pizzaStorePanel.SetActive(true);
        isPizzaStore = true;
        pizzaStoreHeight = 1080;
        pizzaStoreTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, pizzaStoreHeight);
        iStop.StopMap(true);
    }
    /// <summary>
    /// 곧바로 피자 메뉴까지 열어주는 메소드
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
    /// 대화창을 제어한다.(열고 닫는다.)
    /// </summary>
    /// <param name="isOn"></param>
    /// <param name="iEndInspecting"></param>
    public void ControlConversationUI(bool isOn, IEndConversation iEndInspecting, int type)
    {
        if (inspectTrans.rect.height != 0 && inspectTrans.rect.height != 1080) { return; }

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
            if (this.iEndInspecting != null)
            {
                this.iEndInspecting.EndConversation();
                this.iEndInspecting = null;
            }
        }
        ChasePoliceCar.isStop = isOn;
        Constant.StopTime = true;
    }
    /// <summary>
    ///  피자 가게 창을 제어한다.
    /// </summary>
    /// <param name="isOn"></param>
    public void ControlPizzaStore(bool isOn)
    {
        if (pizzaStoreTrans.rect.height != 0 && pizzaStoreTrans.rect.height != 1080) { return; }

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
    /// <summary>
    /// 피자 만들기 창을 제어한다.
    /// </summary>
    /// <param name="isOn"></param>
    public void ControlPizzaMake(bool isOn)
    {
        if (pizzaMakeTrans.rect.width != 0 && pizzaMakeTrans.rect.width != 1920) { return; }

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
    /// <summary>
    ///  피자 메뉴 창을 제어한다.
    /// </summary>
    /// <param name="isOn"></param>
    public void ControlPizzaMenu(bool isOn)
    {
        if (pizzaMenuTrans.rect.height != 0 && pizzaMenuTrans.rect.height != 1080) { return; }

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
    /// <summary>
    /// 배달 패널을 제어한다.
    /// </summary>
    /// <param name="isOn"></param>
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
        player.GetComponent<PlayerMove>().Stop = false;
        deliveryPanel.SetActive(false);
        InventoryManager.OnClickDelivery();
        InventoryManager.inventoryTextUpdate(PizzaInventory.name);
    }
    public void NODeliveryUI()
    {
        deliveryPanel.SetActive(false);
        player.GetComponent<PlayerMove>().Stop = false;
    }

    public void OKDeliveryJudgmentPanel()
    {
        DeliveryJudgmentPanel.SetActive(false);
    }
    public void OKDarkDeliveryPanel()
    {
        SpecialPizzaDeliverySelectionPanel.SetActive(false);
        DeliveryAppButton.SetActive(false);
        DarkDeliveryAppButton.SetActive(true);
        GameManager.Instance.time = 0;
        GameManager.Instance.isDarkDelivery = true;
        iResetPizzaMaking.ResetPizzaMaking();
        Time.timeScale = 1;
        light2D.color = new Color(80 / 255f, 80 / 255f, 80 / 255f);
        map.OnStreetLamp();
    }

    public void NoDarkDeliveryPanel()
    {
        SpecialPizzaDeliverySelectionPanel.SetActive(false);
        GameManager.Instance.NextDay();
        //GameManager.Instance.isDarkDelivery = false;
        Time.timeScale = 1;
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
    /// <summary>
    /// 패널창이 서서히 열리는 것을 표현하기 위함
    /// </summary>
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
            iStop.StopMap(false);
            isIn = false;
            ChasePoliceCar.isStop = false;
            Constant.StopTime = false;
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
            isIn = false;
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
            isIn = false;
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
            isIn = false;
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
        // 알람 메시지를 띄우기 위함.
        // 알람 메시지를 아래로 내려서 화면상에 보이게 함.
        if (isAlarmMessage && alarmMessageTrans.localPosition != alarmMessageEnd)
		{
            alarmMessageTrans.localPosition = Vector3.Lerp(alarmMessageTrans.localPosition, alarmMessageEnd, 0.1f);

            if (Mathf.Abs(alarmMessageTrans.localPosition.y - alarmMessageEnd.y) <= 0.1f)
			{
                alarmMessageTrans.localPosition = alarmMessageEnd;
                isAlarmMessage = false;
			}
		}
        // 알람 메시지를 위로 올려서 화면상에 안 보이게 함.
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
        if (Input.GetKeyDown(KeyCode.Escape) && !menuSetActive)
        {
            Menu.SetActive(true);
            menuSetActive = true;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && menuSetActive)
        {
            Menu.SetActive(false);
            menuSetActive = false;
        }
        // 일반 집이 아닌 곳에서 z키를 눌렀을 때
        if (houseType != HouseType.NONE && houseType != HouseType.HOUSE
            && Input.GetKeyDown(KeyCode.Z) && !isIn)
        {
            isIn = true;    // 들어갔음을 표시
            switch (houseType)
            {
                case HouseType.PIZZASTORE:
                    //houseType = HouseType.NONE;
                    //맵에 오브젝트를 정지시킨다.
                    iStop.StopMap(true);
                    // 피자가게 창을 연다
                    ControlPizzaStore(true);
                    break;
                case HouseType.DICESTORE:
                    // 맵에 오브젝트를 정지시킨다.
                    iStop.StopMap(true);
                    // 대화창을 연다.
                    ControlConversationUI(true, null, 2);
                    break;
                case HouseType.PINEAPPLESTORE:
                    // 맵에 오브젝트를 정지시킨다.
                    iStop.StopMap(true);
                    // 대화창을 연다.
                    ControlConversationUI(true, null, 3);
                    break;
                case HouseType.INGREDIENTSTORE:
                    // 맵에 오브젝트를 정지시킨다.
                    iStop.StopMap(true);
                    // 대화창을 연다.
                    ControlConversationUI(true, null, 4);
                    break;
                case HouseType.PINEAPPLESTORETWO:
                    // 맵에 오브젝트를 정지시킨다.
                    iStop.StopMap(true);
                    // 대화창을 연다.
                    ControlConversationUI(true, null, 5);
                    break;
                case HouseType.GUNSTORE:
                    // 맵에 오브젝트를 정지시킨다.
                    iStop.StopMap(true);
                    // 대화창을 연다.
                    ControlConversationUI(true, null, 6);
                    break;
                case HouseType.HOSPITAL:
                    if (GameManager.Instance.time >= 32400 && GameManager.Instance.time <= 64800)
                    {
                        iStop.StopMap(true);
                        ControlConversationUI(true, null, 7);
                    }
                    else
					{
                        break;
					}
                    break;
                case HouseType.INGREDIENTSTORETWO:
                    iStop.StopMap(true);
                    ControlConversationUI(true, null, 8);
                    break;
                case HouseType.LUCKYSTORE:
                    iStop.StopMap(true);
                    ControlConversationUI(true, null, 9);
                    break;
                case HouseType.MONEYSTORE:
                    iStop.StopMap(true);
                    ControlConversationUI(true, null, 10);
                    break;
            }
        }
    }
}
