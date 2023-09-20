using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IpointClickSlot : MonoBehaviour, IPointerClickHandler
{
    public InventoryManager InventoryManager;
    public GameObject ItemPanel;
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            if(transform.GetChild(0).GetComponent<Text>().text != "")
            {
                ItemPanel.SetActive(true);
                ItemPanel.transform.position = Input.mousePosition;
                InventoryManager.SlotNum = int.Parse(name);
                Debug.Log(name);
            }
        }
    }
}
