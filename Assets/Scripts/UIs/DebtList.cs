using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebtList : MonoBehaviour
{
	[SerializeField] private Text text;
	[SerializeField] private RectTransform contentsRect;

	public void InitDebtListText()
	{
		text.text = "";
		int height = 0;
		// key는 날짜
		foreach (var key in Constant.PayMoneyDate.Keys)
		{
			text.text += $"<size=60>{key}일 : </size>\n";
			height += 150;
			// key2는 대출업체 코드
			foreach (var key2 in Constant.PayMoneyDate[key].Keys)
			{
				text.text += $"<size=45>- '{Constant.MoneyStoreName[key2]}' 대출업체에 {Constant.PayMoneyDate[key][key2]}원 갚아야됨. </size>\n";
				height += 100;
			}
		}

		contentsRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
	}

	public void InitDebtListTextForName()
	{
		text.text = "";
		int height = 0;
		for (int i = 0; i < Constant.MoneyStoreName.Length; i++)
		{
			text.text += $"<size=60>'{Constant.MoneyStoreName[i]}' 대출업체 </size> \n";
			height += 150;
			foreach (var key in Constant.PayMoneyDate.Keys)
			{
				if (Constant.PayMoneyDate[key].ContainsKey(i))
				{
					text.text += $"<size=45>'{key}일---{Constant.PayMoneyDate[key][i]}원.' </size> \n";
					height += 100;
				}
			}
		}

		contentsRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);

	}
}
