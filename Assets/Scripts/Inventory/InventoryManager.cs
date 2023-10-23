using UnityEngine;
using UnityEngine.UI;
using Inventory;
using BuildingAddressNS;

public class InventoryManager : MonoBehaviour
{
    public GameObject Inventory;
    public GameObject CurrentInventory;
    public bool InventoryActive;

    public GameObject[] PizzaInventorySlot;
    [SerializeField] private GameObject[] MainInventorySlot;
    [SerializeField] private GameObject[] GunInventorySlot;
    [SerializeField] private GameObject[] DiceInventorySlot;

    public GoalCheckCollider GoalAddressS;
    public SendDeliveryRequest SDR;
    public DeliveryScreen DeliveryScreen;
    public Minimap Minimap;
    [SerializeField]
    GameObject DeliveryJudgmentPanel;
    private void Awake()
    {
        if(GameManager.Instance.InventorySlotList.Count == 0)
        {
            for (int i = 0; i < PizzaInventorySlot.Length; i++)
            {
                GameManager.Instance.InventorySlotList.Add(new Slot(PizzaInventorySlot[i]));
            }
        }
    }
    public void InventoryAddItem(Pizza pizza)
    {
        for (int i = 0; i < GameManager.Instance.PizzaInventoryData.Length; i++)
        {
            GameManager.Instance.PizzaInventoryData[i] = pizza;
            break;
        }
    }

    void inventoryOpenClose()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (InventoryActive)
            {
                if (CurrentInventory != null)
                    CurrentInventory.SetActive(false);
                Inventory.SetActive(false);
                InventoryActive = false;
            }
            else
            {
                foreach (var i in MainInventorySlot)
                {
                    i.GetComponent<Image>().color = Color.white;
                }
                foreach(var i in PizzaInventorySlot)
                {
                    i.GetComponent<Image>().color = Color.white;
                }
                foreach (var i in GunInventorySlot)
                {
                    i.GetComponent<Image>().color = Color.white;
                }
                foreach (var i in DiceInventorySlot)
                {
                    i.GetComponent<Image>().color = Color.white;
                }
                Inventory.SetActive(true);
                InventoryActive = true;
            }
            Debug.Log("Tab 문자가 입력되었습니다.");
        }
    }

    public void inventoryTextUpdate(string InventoryName)
    {
        if(InventoryName == "PizzaInventory")
        {
            for (int i = 0; i < 5; i++)
            {
                if (GameManager.Instance.PizzaInventoryData[i] != null)
                    PizzaInventorySlot[i].transform.GetChild(0).GetComponent<Text>().text = GameManager.Instance.PizzaInventoryData[i]?.Name;
            }
        }else if(InventoryName == "GunInventory")
        {

        }else if(InventoryName == "DiceInventory")
        {

        }
    }
    
    public void OnClickEat(int index)
    {
        if(GameManager.Instance.PizzaInventoryData[index] != null)
        {
            PizzaInventorySlot[index].transform.GetChild(0).GetComponent<Text>().text = "";
            GameManager.Instance.PizzaInventoryData[index] = null;
        }
        inventoryTextUpdate(CurrentInventory.name);
    }

    public void OnClickDelivery()
    {
        Debug.Log("올클릭딜리버리");
        if(/*GameManager.Instance.PizzaInventory[SlotNum - 1] != null && */GoalAddressS != null)
        {
            int SDRIndex = 0;
            int SlotNum = 0;
            foreach(var i in SDR.RequestList)
            {
                //주문리스트의 집주소 == 플레이어 위치 집주소
                if(i.AddressS.HouseAddress == GoalAddressS.addr.HouseAddress)
                {
                    //인벤토리에서 피자를 찾음
                    foreach(var pizza in GameManager.Instance.PizzaInventoryData)
                    {
                        if (i.Pizza.Name.Equals(pizza?.Name))
                        {
                            GameManager.Instance.Money += (int)pizza?.SellCost;
                            PizzaInventorySlot[SlotNum].transform.GetChild(0).GetComponent<Text>().text = "";
                            GameManager.Instance.PizzaInventoryData[SlotNum] = null;
                            Minimap.DeleteDestination(GoalAddressS.iHouse.GetLocation());
                            GoalAddressS.iHouse.DisableHouse();
                            DeliveryScreen.OnClickCancle(SDRIndex);
                            GoalAddressS = null;
                            DeliveryJudgmentPanel.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "배달이 완료 되었습니다.";
                            DeliveryJudgmentPanel.SetActive(true);
                            return;
                        }
                        SlotNum++;
                    }
                }
                SDRIndex++;
            }
            DeliveryJudgmentPanel.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "가방에 주문받은 피자가 없습니다.";
            DeliveryJudgmentPanel.SetActive(true);
        }
        inventoryTextUpdate("PizzaInventory");
    }

    public void ExitInventory()
    {
        Inventory.SetActive(false);
        InventoryActive = false;
    }

    void Update()
    {
        inventoryOpenClose();
    }
}
