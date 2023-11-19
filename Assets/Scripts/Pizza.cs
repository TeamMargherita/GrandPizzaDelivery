using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BuildingAddressNS;
using PizzaNS;

public struct Pizza
{
    public string Name;//�����̸�
    public int Perfection;//�ϼ���
    public int ProductionCost;//������
    public int SellCost;//�Ǹź��
    public int Charisma;//�ŷµ�
    public List<Ingredient> Ingreds;
    public int TotalDeclineAt;
    public int Freshness;
    public float ProductTime;

    public Pizza(string name, int perfection, int productionCost, int sellCost, int charisma, List<Ingredient> Ingreds, int TotalDeclineAt, int freshness, float productTime)
    {
        Name = name;
        Perfection = perfection;
        ProductionCost = productionCost;
        SellCost = sellCost;
        Charisma = charisma;
        Freshness = freshness;
        ProductTime = productTime;
        this.Ingreds = new List<Ingredient>();
        for (int i = 0; i < Ingreds.Count; i++)
        {
            this.Ingreds.Add(Ingreds[i]);
        }
        this.TotalDeclineAt = TotalDeclineAt;
    }

    public int FreshnessUpdate(float time)
    {
        if(time - ProductTime >= 1800 && time - ProductTime < 3600)
        {
            Freshness = 50;
        }else if(time - ProductTime >= 3600)
        {
            Freshness = 0;
        }
        return Freshness;
    }

    public string GetExplain()
    {
        string text;
        text = "�̸� : " + Name + "\n�ϼ��� : " + Perfection + "\n������ : " + ProductionCost + "\n�ǸŰ� : " + SellCost + "\n�ŷµ� : " + Charisma;
        return text;
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

public class Request//�����ֹ�
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
        /// Ÿ�� ��ġ �޾ƿ��� �Լ�
        /// </summary>
        /// <returns></returns>
        public Vector3 GetTargetPos();
        /// <summary>
        /// ����ĳ��Ʈ�� �� ���� ����
        /// </summary>
        void aiming();
        /// <summary>
        /// ����ĳ��Ʈ �߻�
        /// </summary>
        /// <param name="exception">����ó���� ���̾� �̸�</param>
        bool ShootRaycast(float fireRate, short damage);
        /// <summary>
        /// �ǻ�� �Լ�
        /// </summary>
        /// <param name="exeption">����ó���� ���̾� �̸�</param>
        bool Fire(float fireRate, short damage);
    }
}


namespace Inventory
{

    //�κ��丮 ����
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
