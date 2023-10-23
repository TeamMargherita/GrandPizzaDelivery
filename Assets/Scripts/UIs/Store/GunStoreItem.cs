using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoreNS;

// 한석호 작성

public class GunStoreItem : Store
{
	public GunStoreItem(ICloseStore iCloseStore)
	{
		CloseStore = iCloseStore;

		itemType = ItemType.GUN;

		StoreItemList = new List<StoreItemS>();

		StoreItemList.Add(new StoreItemS(Constant.GunItem[0], 120000));
		StoreItemList.Add(new StoreItemS(Constant.GunItem[1], 450000));
		StoreItemList.Add(new StoreItemS(Constant.GunItem[2], 80000));
		StoreItemList.Add(new StoreItemS(Constant.GunItem[3], 270000));
		StoreItemList.Add(new StoreItemS(Constant.GunItem[4], 300000));
		StoreItemList.Add(new StoreItemS(Constant.GunItem[5], 360000));
		StoreItemList.Add(new StoreItemS(Constant.GunItem[6], 550000));
		StoreItemList.Add(new StoreItemS(Constant.GunItem[7], 170000));
	}
}
