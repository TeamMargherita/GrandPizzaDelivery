using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IPointHandlerInventory : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IEndDragHandler
{
    public InventoryManager InventoryManager;
    [SerializeField] GameObject mainInventory;
    [SerializeField] GameObject Inventory;
    [SerializeField] string InventoryName;
    [SerializeField] private GameObject DragDrop;
    [SerializeField] private GameObject exPlainPanel;

    private void Awake()
    {
        transform.GetComponent<Image>().color = Color.white;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(InventoryName == "Pizza")
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (transform.GetChild(0).GetComponent<Text>().text != "")
                {
                    InventoryManager.OnClickEat(int.Parse(name) - 1);
                }
            }
        }
        else if(InventoryName == "Main")
        {
            Inventory.SetActive(true);
            InventoryManager.inventoryTextUpdate(Inventory.name);
            InventoryManager.CurrentInventory = Inventory;
            mainInventory.SetActive(false);
        }
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.GetComponent<Image>().color = new Color(190 / 255f, 197 / 255f, 253 / 255f);
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (InventoryName == "Dice")
            {
                if (transform.GetChild(0).GetComponent<Image>().enabled)
                {
                    //exPlainPanel.transform.SetParent(this.transform);
                    exPlainPanel.SetActive(true);
                    exPlainPanel.transform.GetChild(0).GetComponent<Text>().text = InventoryManager.DiceInventorySlotParams[int.Parse(name) - 1].Explain;
                    exPlainPanel.transform.position = new Vector3(Input.mousePosition.x + 10, Input.mousePosition.y - 30, Input.mousePosition.z);
                    DragDrop.GetComponent<Image>().enabled = true;
                    DragDrop.GetComponent<Image>().sprite = InventoryManager.GetItemImage(int.Parse(name) - 1, StoreNS.ItemType.DICE);
                }
                else
                {
                    DragDrop.GetComponent<Image>().enabled = false;
                }
            }else if(InventoryName == "Gun")
            {
                if (transform.GetChild(0).GetComponent<Image>().enabled)
                {
                    exPlainPanel.SetActive(true);
                    exPlainPanel.transform.GetChild(0).GetComponent<Text>().text = InventoryManager.GunInventorySlotParams[int.Parse(name) - 1].Explain;
                    exPlainPanel.transform.position = new Vector3(Input.mousePosition.x + 10, Input.mousePosition.y - 30, Input.mousePosition.z);
                    DragDrop.GetComponent<Image>().enabled = true;
                    DragDrop.GetComponent<Image>().sprite = InventoryManager.GetItemImage(int.Parse(name) - 1, StoreNS.ItemType.GUN);
                }
                else
                {
                    DragDrop.GetComponent<Image>().enabled = false;
                }
            }else if(InventoryName == "Pizza")
            {
                exPlainPanel.SetActive(true);
                exPlainPanel.transform.GetChild(0).GetComponent<Text>().text = GameManager.Instance.PizzaInventoryData[int.Parse(name) - 1]?.GetExplain();
                exPlainPanel.transform.position = new Vector3(Input.mousePosition.x + 10, Input.mousePosition.y - 30, Input.mousePosition.z);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.GetComponent<Image>().color = Color.white;
        if(exPlainPanel != null)
            exPlainPanel.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (InventoryName == "Dice")
        {
            DragDrop.SetActive(true);
            DragDrop.transform.position = eventData.position;
            InventoryManager.CurrentDragItem = InventoryManager.DiceInventorySlotParams[int.Parse(name) - 1];
        }
        else if(InventoryName == "Gun")
        {
            DragDrop.SetActive(true);
            DragDrop.transform.position = eventData.position;
            InventoryManager.CurrentDragItem = InventoryManager.GunInventorySlotParams[int.Parse(name) - 1];
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(InventoryName == "Dice")
        {
            DragDrop.SetActive(false);
        }else if(InventoryName == "Gun")
        {
            DragDrop.SetActive(false);
        }
    }
}
