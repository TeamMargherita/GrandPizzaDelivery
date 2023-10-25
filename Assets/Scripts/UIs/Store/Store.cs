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
	/// <summary>
	/// 판매 상품 목록
	/// </summary>
	public List<StoreItemS> StoreItemList { get; protected set; }
	public ICloseStore CloseStore { get; protected set; }

	protected ItemType itemType;
	
}
