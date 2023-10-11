using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IpointClickSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public InventoryManager InventoryManager;

    private void Awake()
    {
        transform.GetComponent<Image>().color = Color.gray;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            if(transform.GetChild(0).GetComponent<Text>().text != "")
            {
                InventoryManager.ItemPanel.SetActive(true);
                InventoryManager.ItemPanel.transform.position = Input.mousePosition;
                InventoryManager.SlotNum = int.Parse(name);
                Debug.Log(name);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.GetComponent<Image>().color = Color.black;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.GetComponent<Image>().color = Color.gray;
        if(!(InventoryManager.SlotNum == int.Parse(name)))
            InventoryManager.ItemPanel.SetActive(false);
    }
}
