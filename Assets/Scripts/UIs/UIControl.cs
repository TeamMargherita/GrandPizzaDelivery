using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingNS.HouseNS;

// �Ѽ�ȣ �ۼ�

public class UIControl : MonoBehaviour, IConversationPanelControl, IDeliveryPanelControl, IHouseActiveUIControl, IAlarmMessagePanel
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

    private IEndConversation iEndInspecting;
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

    private int inspectingHeight = 0;   // �ҽɰ˹� �г�â ����
    private int pizzaStoreHeight = 0;   // ������ �г�â ����
    private int pizzaMakeWitdh = 0; // ���ڸ���� �г�â �ʺ�
    private int pizzaMenuHeight = 0;    // ���ڸ޴� �г�â ����;

    private bool isInspecting = false;  // �ҽɰ˹��� â�� �����ϴ��� ����
    private bool isPizzaStore = false;  // ������ â�� �����ϴ��� ����
    private bool isPizzaMake = false;
    private bool isPizzaMenu = false;
    private bool isPizzaAddButtonBlank = false;
    private bool isAlarmMessage = false;    // �˶� �޽��� â�� �����ϴ��� ����
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
    /// �˸� â ����ݱ�. �˸�â�� ������ �ؽ�Ʈ�� �����ؾߵ�
    /// </summary>
    /// <param name="isOn"></param>
    /// <param name="text">��� �ؽ�Ʈ�� ���´�.</param>
    public void ControlAlarmMessageUI(bool isOn, string text)
	{
        alarmMessageText.text = text;
        isAlarmMessage = isOn;
	}

    /// <summary>
    /// ��������� ���� �� �ٷ� ���� �޴� UI�� �� �� ����ϴ� �Լ�
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
    /// ��ȭâ ����ݱ�
    /// </summary>
    /// <param name="isOn"></param>
    /// <param name="iEndInspecting"></param>
    public void ControlConversationUI(bool isOn, IEndConversation iEndInspecting, int type)
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
            if (this.iEndInspecting != null)
            {
                this.iEndInspecting.EndConversation();
                this.iEndInspecting = null;
            }
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
            iStop.StopMap(false);
            ChasePoliceCar.isStop = false;
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
        // Ư���� ��ҿ��� ZŰ�� ���� ��
        if (houseType != HouseType.NONE && houseType != HouseType.HOUSE
            && Input.GetKeyDown(KeyCode.Z))
        {
            switch(houseType)
            {
                case HouseType.PIZZASTORE:
                    //houseType = HouseType.NONE;
                    //�÷��̾� ���߰�, ������ �������.
                    iStop.StopMap(true);
                    // ����â Ȱ��ȭ
                    ControlPizzaStore(true);
                    break;
                case HouseType.DICESTORE:
                    iStop.StopMap(true);
                    ControlConversationUI(true, null, 2);
                    break;
            }
        }
    }
}
