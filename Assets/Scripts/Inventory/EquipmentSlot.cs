using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using StoreNS;

public class EquipmentSlot : MonoBehaviour , IDropHandler
{
    [SerializeField] private InventoryManager inventoryManager;
    public void OnDrop(PointerEventData eventData)
    {
        if(name == "DiceSlot1")
        {
            Constant.nowDice[0] = ((ItemS)inventoryManager.CurrentDragItem).ItemNumber;
            GetComponent<Image>().sprite = Resources.LoadAll<Sprite>(Constant.DiceInfo[Constant.nowDice[0]].Path)[0];
        }
        else if(name == "DiceSlot2")
        {
            Constant.nowDice[1] = ((ItemS)inventoryManager.CurrentDragItem).ItemNumber;
            GetComponent<Image>().sprite = Resources.LoadAll<Sprite>(Constant.DiceInfo[Constant.nowDice[1]].Path)[0];
        }
        else if(name == "GunSlot")
        {
            Constant.nowGun[0] = ((ItemS)inventoryManager.CurrentDragItem).ItemNumber;
            GetComponent<Image>().sprite = Resources.LoadAll<Sprite>(Constant.DiceInfo[Constant.nowGun[0]].Path)[0];
        }
    }
    private void Awake()
    {
        if (name == "DiceSlot1")
            GetComponent<Image>().sprite = Resources.LoadAll<Sprite>(Constant.DiceInfo[Constant.nowDice[0]].Path)[0];
        else if(name == "DiceSlot2")
            GetComponent<Image>().sprite = Resources.LoadAll<Sprite>(Constant.DiceInfo[Constant.nowDice[1]].Path)[0];
    }
}
