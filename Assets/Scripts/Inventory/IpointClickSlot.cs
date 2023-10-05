using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IpointClickSlot : MonoBehaviour, IPointerClickHandler
{
    public InventoryManager InventoryManager;
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
}
