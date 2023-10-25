using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoreNS;
// 한석호 작성
public class DiceStoreItem : Store
{
	public DiceStoreItem(ICloseStore iCloseStore)
	{
		CloseStore = iCloseStore;

		itemType = ItemType.DICE;
		
		StoreItemList = new List<StoreItemS>();

		StoreItemList.Add(new StoreItemS(Constant.DiceItem[0], 5000));
		StoreItemList.Add(new StoreItemS(Constant.DiceItem[1], 10500));
		StoreItemList.Add(new StoreItemS(Constant.DiceItem[2], 9000));
		StoreItemList.Add(new StoreItemS(Constant.DiceItem[3], 30000));
		StoreItemList.Add(new StoreItemS(Constant.DiceItem[4], 15000));
		StoreItemList.Add(new StoreItemS(Constant.DiceItem[5], 15000));
		StoreItemList.Add(new StoreItemS(Constant.DiceItem[6], 45000));
		StoreItemList.Add(new StoreItemS(Constant.DiceItem[7], 60000));
		StoreItemList.Add(new StoreItemS(Constant.DiceItem[8], 7000));
	}
}
