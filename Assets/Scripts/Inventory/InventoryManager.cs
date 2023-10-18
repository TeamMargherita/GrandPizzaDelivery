using UnityEngine;
using UnityEngine.UI;
using Inventory;
using BuildingAddressNS;

public class InventoryManager : MonoBehaviour
{
    public GameObject Inventory;
    public GameObject CurrentInventory;
    public bool InventoryActive;
    public GameObject[] InventorySlot;
    public GameObject ItemPanel;
    public int SlotNum;
    public GoalCheckCollider GoalAddressS;
    public SendDeliveryRequest SDR;
    public DeliveryScreen DeliveryScreen;
    public Minimap Minimap;

    private void Awake()
    {
        if(GameManager.Instance.InventorySlotList.Count == 0)
        {
            for (int i = 0; i < InventorySlot.Length; i++)
            {
                GameManager.Instance.InventorySlotList.Add(new Slot(InventorySlot[i]));
            }
        }
        
    }

    public void InventoryAddItem(Pizza pizza)
    {
        foreach (var i in GameManager.Instance.InventorySlotList)
        {
            if(i.Pizza?.Name == null)
            {
                i.Pizza = pizza;
                break;
            }
        }
    }

    void inventoryOpenClose()
    {
        switch (Input.inputString)
        {
            case "i":
                if (InventoryActive)
                {
                    ItemPanel.SetActive(false);
                    CurrentInventory.SetActive(false);
                    Inventory.SetActive(false);
                    InventoryActive = false;
                }
                else
                {
                    Inventory.SetActive(true);
                    InventoryActive = true;
                    inventoryDisplay();
                }
                Debug.Log("영문 'i' 문자가 입력되었습니다.");
                break;

            case "ㅑ":
                if (InventoryActive)
                {
                    ItemPanel.SetActive(false);
                    CurrentInventory.SetActive(false);
                    Inventory.SetActive(false);
                    InventoryActive = false;
                }
                else
                {
                    Inventory.SetActive(true);
                    InventoryActive = true;
                    inventoryDisplay();
                }
                Debug.Log("한글 'ㅑ' 문자가 입력되었습니다.");
                break;
        }
    }

    public void inventoryDisplay()
    {
        foreach(var i in GameManager.Instance.InventorySlotList)
        {
            i.TextUpdate();
        }
    }

    public void OnClickEat()
    {
        if(GameManager.Instance.InventorySlotList[SlotNum - 1].Pizza != null)
        {
            GameManager.Instance.InventorySlotList[SlotNum - 1].InventorySlot.transform.GetChild(0).GetComponent<Text>().text = "";
            GameManager.Instance.InventorySlotList[SlotNum - 1].Pizza = null;
            ItemPanel.SetActive(false);
        }
        inventoryDisplay();
    }

    public void OnClickDelivery()
    {
        Debug.Log("올클릭딜리버리");
        if(GameManager.Instance.InventorySlotList[SlotNum - 1].Pizza != null && GoalAddressS != null)
        {
            int j = 0;
            foreach(var i in SDR.RequestList)
            {
                if(i.AddressS.HouseAddress == GoalAddressS.addr.HouseAddress)
                {
                    if(i.Pizza.Name.Equals(GameManager.Instance.InventorySlotList[SlotNum - 1].Pizza?.Name))
                    {
                        GameManager.Instance.Money += (int)GameManager.Instance.InventorySlotList[SlotNum - 1].Pizza?.SellCost;
                        GameManager.Instance.InventorySlotList[SlotNum - 1].InventorySlot.transform.GetChild(0).GetComponent<Text>().text = "";
                        GameManager.Instance.InventorySlotList[SlotNum - 1].Pizza = null;
                        Minimap.DeleteDestination(GoalAddressS.iHouse.GetLocation());
                        GoalAddressS.iHouse.DisableHouse();
                        DeliveryScreen.OnClickCancle(j);
                        ItemPanel.SetActive(false);
                        GoalAddressS = null;
                        break;
                    }
                    else
                    {
                        Debug.Log("다른 피자입니다.");
                    }
                    j++;
                }
            }
        }
        inventoryDisplay();
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
