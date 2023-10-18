using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IPointHandlerInventory : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public InventoryManager InventoryManager;
    [SerializeField]
    GameObject mainInventory;
    [SerializeField]
    GameObject Inventory;
    [SerializeField]
    string InventoryName;
    private void Awake()
    {
        transform.GetComponent<Image>().color = Color.white;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(InventoryName == "Pizza" || InventoryName == "Gun" || InventoryName == "Dice")
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                if (transform.GetChild(0).GetComponent<Text>().text != "")
                {
                    InventoryManager.ItemPanel.SetActive(true);
                    InventoryManager.ItemPanel.transform.position = Input.mousePosition;
                    InventoryManager.SlotNum = int.Parse(name);
                    Debug.Log(name);
                }
            }
        }else if(InventoryName == "Main")
        {
            Inventory.SetActive(true);
            InventoryManager.CurrentInventory = Inventory;
            mainInventory.SetActive(false);
            
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.GetComponent<Image>().color = new Color(190 / 255f, 197 / 255f, 253 / 255f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.GetComponent<Image>().color = Color.white;
    }
}
