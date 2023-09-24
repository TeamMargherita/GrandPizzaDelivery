using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Inventory;

public class InventoryManager : MonoBehaviour
{
    public GameObject Inventory;
    private bool InventoryActive;
    public GameObject[] InventorySlot;
    
    public int SlotNum;

    private void Awake()
    {
        for(int i = 0; i < InventorySlot.Length; i++)
        {
            GameManager.Instance.InventorySlotList.Add(new Slot(InventorySlot[i]));
        }
    }

    public void InventoryAddItem(Pizza pizza)
    {
        foreach (var i in GameManager.Instance.InventorySlotList)
        {
            if(i.Pizza == null)
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

    void inventoryDisplay()
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
            GameManager.Instance.InventorySlotList[SlotNum - 1].Pizza = null;
        }
        inventoryDisplay();
    }

    public void OnClickDelivery()
    {
        if(GameManager.Instance.InventorySlotList[SlotNum - 1].Pizza != null)
        {
            GameManager.Instance.InventorySlotList[SlotNum - 1].Pizza = null;
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
