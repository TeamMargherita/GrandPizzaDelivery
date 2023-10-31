using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Inventory;
using BuildingAddressNS;
using StoreNS;
public class InventoryManager : MonoBehaviour
{
    public GameObject Inventory;
    public GameObject CurrentInventory;
    public bool InventoryActive;

    public GameObject[] PizzaInventorySlot;
    [SerializeField] private GameObject[] MainInventorySlot;
    [SerializeField] private GameObject[] GunInventorySlot;
    [SerializeField] private GameObject[] DiceInventorySlot;
    [SerializeField] private GameObject[] DiceEquipmentSlot;
    [SerializeField] private GameObject GunEquipmentSlot;
    [SerializeField] private GameObject UIGunImage;
    [SerializeField] private Text MagagineText;
    [SerializeField] private PlayerMove Player;

    public ItemS[] DiceInventorySlotParams = new ItemS[5];
    public ItemS[] GunInventorySlotParams = new ItemS[5];

    public Dictionary<ItemS, int> Dice;
    public Dictionary<ItemS, int> Gun;

    public GoalCheckCollider GoalAddressS;
    public SendDeliveryRequest SDR;
    public DeliveryScreen DeliveryScreen;
    public Minimap Minimap;
    [SerializeField]
    GameObject DeliveryJudgmentPanel;

    public int DicePage;
    public int GunPage;
    public object CurrentDragItem;
    [SerializeField] private Text DiceText;
    [SerializeField] private Text GunText;
    private void Awake()
    {
        UIGunImage.GetComponent<Image>().color = Color.clear;
        if (GameManager.Instance.InventorySlotList.Count == 0)
        {
            for (int i = 0; i < PizzaInventorySlot.Length; i++)
            {
                GameManager.Instance.InventorySlotList.Add(new Slot(PizzaInventorySlot[i]));
            }
        }
    }
    /// <summary>
    /// 인벤토리에 주사위 이미지 불러오기
    /// </summary>
    /// <param name="path">이미지 경로</param>
    /// <param name="index">슬롯 주소</param>
    /// <param name="count">플레이어가 가지고 있는 아이템 갯수</param>
    private void SystemIOFileLoad(string path, int index, int count, ItemType type)
    {
        if(type == ItemType.DICE)
        {
            DiceInventorySlot[index].transform.GetChild(0).GetComponent<Image>().enabled = true;
            DiceInventorySlot[index].transform.GetChild(0).GetComponent<Image>().sprite = Resources.LoadAll<Sprite>(path)[0];
            DiceInventorySlot[index].transform.GetChild(0).GetComponent<Image>().color = Color.white;
            DiceInventorySlot[index].transform.GetChild(1).GetComponent<Text>().text = "x " + count;
        }else if(type == ItemType.GUN)
        {
            GunInventorySlot[index].transform.GetChild(0).GetComponent<Image>().enabled = true;
            GunInventorySlot[index].transform.GetChild(0).GetComponent<Image>().sprite = Resources.LoadAll<Sprite>(path)[0];
            GunInventorySlot[index].transform.GetChild(0).GetComponent<Image>().color = Color.white;
            GunInventorySlot[index].transform.GetChild(1).GetComponent<Text>().text = "x " + count;
        }
       
    }

    public Sprite GetItemImage(int index, ItemType type)
    {
        if(type == ItemType.DICE)
        {
            return DiceInventorySlot[index].transform.GetChild(0).GetComponent<Image>().sprite;
        }else if(type == ItemType.GUN)
        {
            return GunInventorySlot[index].transform.GetChild(0).GetComponent<Image>().sprite;
        }
        else
        {
            return null;
        }
    }
    
    /// <summary>
    /// 인벤토리 최신화
    /// </summary>
    private void DiceInventoryUpdate()
    {
        foreach(var i in DiceEquipmentSlot)
        {
            i.GetComponent<EquipmentSlot>().BaseSlotColorClear();
        }
        Dice = Constant.FindAllItemS(Constant.PlayerItemDIc, ItemType.DICE);
        int index = DicePage * 5;
        int startcount = (DicePage - 1) * 5;
        int count = 0;
        int itemIndex = 0;
        foreach(var i in Dice)
        {
            if(startcount <= count && count < index)
            {
                SystemIOFileLoad(Constant.DiceInfo[i.Key.ItemNumber].Path, count - startcount, i.Value, ItemType.DICE);
                DiceInventorySlotParams[itemIndex] = i.Key;
                itemIndex++;
            }
            count++;
        }
        while(count < index)
        {
            if (itemIndex == 5) { break; }
            DiceInventorySlot[itemIndex].transform.GetChild(0).GetComponent<Image>().enabled = false;
            DiceInventorySlot[itemIndex].transform.GetChild(1).GetComponent<Text>().text = "";
            itemIndex++;
            count++;
        }
    }

    private void GunInventoryUpdate()
    {
        GunEquipmentSlot.GetComponent<EquipmentSlot>().BaseSlotColorClear();
        Gun = Constant.FindAllItemS(Constant.PlayerItemDIc, ItemType.GUN);
        int index = GunPage * 5;
        int startcount = (GunPage - 1) * 5;
        int count = 0;
        int itemIndex = 0;
        foreach (var i in Gun)
        {
            if (startcount <= count && count < index)
            {
                SystemIOFileLoad(Constant.GunInfo[i.Key.ItemNumber].Path, count - startcount, i.Value, ItemType.GUN);
                GunInventorySlotParams[itemIndex] = i.Key;
                itemIndex++;
            }
            count++;
        }
        while (count < index)
        {
            if (itemIndex == 5) { break; }
            GunInventorySlot[itemIndex].transform.GetChild(0).GetComponent<Image>().enabled = false;
            GunInventorySlot[itemIndex].transform.GetChild(1).GetComponent<Text>().text = "";
            GunInventorySlotParams[itemIndex] = new ItemS(ItemType.GUN, 0, "", "", -1);
            itemIndex++;
            count++;
        }

    }
    public void InventoryAddItem(Pizza pizza)
    {
        for (int i = 0; i < GameManager.Instance.PizzaInventoryData.Length; i++)
        {
            if(GameManager.Instance.PizzaInventoryData[i] == null)
            {
                GameManager.Instance.PizzaInventoryData[i] = pizza;
                break;
            }
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
                GunInventoryUpdate();
                DiceInventoryUpdate();
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
        }
    }

    public void inventoryTextUpdate(string InventoryName)
    {
        if(InventoryName == "PizzaInventory")
        {
            for (int i = 0; i < 5; i++)
            {
                if (GameManager.Instance.PizzaInventoryData[i] != null)
                {
                    PizzaInventorySlot[i].transform.GetChild(0).GetComponent<Text>().text = GameManager.Instance.PizzaInventoryData[i]?.Name;
                    PizzaInventorySlot[i].transform.GetChild(1).gameObject.SetActive(true);
                }
                else
                {
                    PizzaInventorySlot[i].transform.GetChild(1).gameObject.SetActive(false);
                }
            }
        }
    }
    
    public void EquipmentSlotUpdate()
    {
        int index = 0;
        foreach(var i in DiceEquipmentSlot)
        {
            if (Constant.nowDice[index] != -1)
            {
                i.GetComponent<Image>().sprite = Resources.LoadAll<Sprite>(Constant.DiceInfo[Constant.nowDice[index]].Path)[0];
                i.GetComponent<Image>().color = Color.white;
            }
            else
                i.GetComponent<Image>().color = Color.clear;
           index++;
        }
        if (Constant.nowGun[0] != -1)
        {
            GunEquipmentSlot.GetComponent<Image>().sprite = Resources.LoadAll<Sprite>(Constant.GunInfo[Constant.nowGun[0]].Path)[0];
            GunEquipmentSlot.GetComponent<Image>().color = Color.white;
            Player.CurrentMagagine = 0;
            Player.Gun.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>(Constant.GunInfo[Constant.nowGun[0]].Path)[0];
        }
        else
            GunEquipmentSlot.GetComponent<Image>().color = Color.clear;
    }

    public void OnClickEat(int index)
    {
        if(GameManager.Instance.PizzaInventoryData[index] != null)
        {
            PizzaInventorySlot[index].transform.GetChild(0).GetComponent<Text>().text = "";
            GameManager.Instance.PizzaInventoryData[index] = null;
            Player.HP += 50;
            PizzaInventorySlot[index].transform.GetChild(1).gameObject.SetActive(false);
        }
        inventoryTextUpdate(CurrentInventory.name);
    }
    private void UIGunImageUpdate()
    {
        if(Constant.nowGun[0] == -1)
            UIGunImage.GetComponent<Image>().color = Color.clear;
        else
        {
            UIGunImage.GetComponent<Image>().sprite = GunEquipmentSlot.GetComponent<Image>().sprite;
            UIGunImage.GetComponent<Image>().color = Color.white;
        }
    }

    
    public void UIMagagineTextUpdate(short currentMagagine)
    {
        if (Constant.nowGun[0] == -1)
            MagagineText.text = "";
        else
        {
            MagagineText.text = "" + currentMagagine;
        }
    }
    public void OnClickDelivery()
    {
        if(GoalAddressS != null)
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
                            GoalAddressS.iHouse.DisableHouse(pizza.Value);
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

    public void OnclickDicePageButton(int i)
    {
        DicePage += i;
        if (DicePage < 1)
            DicePage = 1;
        else if (DicePage > 5)
            DicePage = 5;
        DiceText.text = "" + DicePage;
        DiceInventoryUpdate();
    }

    public void OnclickGunPageButton(int i)
    {
        GunPage += i;
        if (GunPage < 1)
            GunPage = 1;
        else if (GunPage > 5)
            GunPage = 5;
        GunText.text = "" + GunPage;
        GunInventoryUpdate();
    }

    public void ExitInventory()
    {
        Inventory.SetActive(false);
        InventoryActive = false;
    }

    void Update()
    {
        inventoryOpenClose();
        UIGunImageUpdate();
    }
}
