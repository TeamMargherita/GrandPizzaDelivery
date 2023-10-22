using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoreNS;
// 한석호 작성
public class Store
{
	public struct StoreItemS
	{
		public ItemS Item;
		public int Cost;

		public StoreItemS(ItemS itemS, int cost)
		{
			Item = itemS;
			Cost = cost;
		}
	}
	public List<StoreItemS> StoreItemList { get; protected set; } // 판매 상품 목록. int는 가격
	public ICloseStore CloseStore { get; protected set; }

	protected ItemType itemType;
	
}
