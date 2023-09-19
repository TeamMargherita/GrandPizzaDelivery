using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        this.Name = name;
        this.Perfection = perfection;
        this.ProductionCost = productionCost;
        this.SellCost = sellCost;
        this.Charisma = charisma;
    }
}

public class Request
{
    public Pizza Pizza;
    public bool Accept;
    public AddressS AddressS;
    public Request(Pizza pizza, bool accept)
    {
        this.Pizza = pizza;
        this.Accept = accept;
    }
}
