using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BuildingAddressNS;
using PizzaNS;

public struct Pizza
{
    public string Name;//피자이름
    public int Perfection;//완성도
    public int ProductionCost;//생산비용
    public int SellCost;//판매비용
    public int Charisma;//매력도
    public List<Ingredient> Ingreds;
    public int TotalDeclineAt;

    public Pizza(string name, int perfection, int productionCost, int sellCost, int charisma, List<Ingredient> Ingreds, int TotalDeclineAt)
    {
        Name = name;
        Perfection = perfection;
        ProductionCost = productionCost;
        SellCost = sellCost;
        Charisma = charisma;
        this.Ingreds = new List<Ingredient>();
        for (int i = 0; i < Ingreds.Count; i++)
        {
            this.Ingreds.Add(Ingreds[i]);
        }
        this.TotalDeclineAt = TotalDeclineAt;
    }
    public string GetName()
    {
        return Name;
    }
    public int GetPerfection()
    {
        return Perfection;
    }
    public int GetProductionCost()
    {
        return ProductionCost;
    }
    public int GetSellCost()
    {
        return SellCost;
    }
    public int GetCharisma()
    {
        return Charisma;
    }
    public List<Ingredient> GetIngreds()
    {
        return Ingreds;
    }
    public int GetTotalDeclineAt()
    {
        return TotalDeclineAt;
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
namespace Gun
{
    public class GunStat
    {
        string Name;
        string Type;
        float FireRate;
        int Damage;
        float Accuracy;
        public GunStat(string name, string type, float fireRate, int damage, float accuracy)
        {
            Name = name;
            Type = type;
            FireRate = fireRate;
            Damage = damage;
            Accuracy = accuracy;
        }
    }

    public interface GunShooting
    {
        /// <summary>
        /// 타겟 위치 받아오는 함수
        /// </summary>
        /// <returns></returns>
        public Vector3 GetTargetPos();
        /// <summary>
        /// 레이캐스트를 쏠 방향 지정
        /// </summary>
        void aiming();
        /// <summary>
        /// 레이캐스트 발사
        /// </summary>
        /// <param name="exception">예외처리할 레이어 이름</param>
        bool ShootRaycast(float fireRate, short damage);
        /// <summary>
        /// 실사격 함수
        /// </summary>
        /// <param name="exeption">예외처리할 레이어 이름</param>
        bool Fire(float fireRate, short damage);
    }
}


namespace Inventory
{

    //인벤토리 슬롯
    public class Slot
    {
        public GameObject InventorySlot;
        public Pizza? Pizza = null;
        bool ButtonActivity = false;

        public Slot(GameObject inventorySlot)
        {
            InventorySlot = inventorySlot;
        }

        /*public void TextUpdate()
        {
            if(Pizza != null)
                InventorySlot.transform.GetChild(0).GetComponent<Text>().text = Pizza?.GetName();
        }*/
    }
}
