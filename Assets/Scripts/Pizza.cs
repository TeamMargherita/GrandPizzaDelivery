using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Pizza
{
    public string Name;//피자이름
    public int Perfection;//완성도
    public int Production_Cost;//생산비용
    public int Sell_Cost;//판매비용

    public Pizza(string name, int perfection, int production_Cost, int Sell_Cost)
    {
        this.Name = name;
        this.Perfection = perfection;
        this.Production_Cost = production_Cost;
        this.Sell_Cost = Sell_Cost;
    }
}
