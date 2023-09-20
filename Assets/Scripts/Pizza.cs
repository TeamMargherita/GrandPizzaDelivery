using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BuildingAddressNS;

public struct Pizza
{
    public string Name;//피자이름
    public int Perfection;//완성도
    public int ProductionCost;//생산비용
    public int SellCost;//판매비용
    public int Charisma;//매력도

    public Pizza(string name, int perfection, int productionCost, int sellCost, int charisma)
    {
        Name = name;
        Perfection = perfection;
        ProductionCost = productionCost;
        SellCost = sellCost;
        Charisma = charisma;
    }
}

public class Request//피자주문
{
    public Pizza Pizza;
    public bool Accept;
    public AddressS AddressS;
    public Request(Pizza pizza, bool accept)
    {
        Pizza = pizza;
        Accept = accept;
    }
}
namespace Inventory
{
    public class Slot
    {
        public GameObject InventorySlot;
        public Pizza? Pizza = null;
        bool ButtonActivity = false;

        public Slot(GameObject inventorySlot)
        {
            InventorySlot = inventorySlot;
        }

        public void TextUpdate()
        {
            if(Pizza != null)
                InventorySlot.transform.GetChild(0).GetComponent<Text>().text = Pizza?.Name;
        }
    }
}
