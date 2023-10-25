using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using StoreNS;
// 한석호 작성
public class ItemPanelArr : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] private UnityEngine.UI.Text itemExplain;
	[SerializeField] private UnityEngine.UI.Text itemName;
	
	public ItemS? Item { get; set; }
	public int ItemCost { get; set; }

	public void SetItemName(string name)
	{
		itemName.text = name;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		int n = 0;
		if (Item != null)
		{
			n = Constant.PlayerItemDIc.ContainsKey(Item.Value) ? Constant.PlayerItemDIc[Item.Value] : 0;
			itemExplain.text =
				Item.Value.Explain +
				"\n가격은 " + ItemCost.ToString() + "원이다.\n\n" +
				$"현재 {n}개 보유중이다.";
		}

	}

	public void OnPointerExit(PointerEventData eventData)
	{
		itemExplain.text = "";
	}
}
