public struct Pizza
{
    public string Name;//피자이름
    public int Perfection;//완성도
    public int ProductionCost;//생산비용
    public int SellCost;//판매비용
    public int Charisma;//매력도

    public Pizza(string name, int productionCost, int sellCost, int charisma, int perfection = 0)
    {
        Name = name;
        Perfection = perfection;
        ProductionCost = productionCost;
        SellCost = sellCost;
        Charisma = charisma;
    }
}
