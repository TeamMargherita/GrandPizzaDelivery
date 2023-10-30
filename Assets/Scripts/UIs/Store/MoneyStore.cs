using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConversationNS;

// 한석호 작성

public class MoneyStore : Conversation
{
	public static bool IsTalk = false;	// 대출업체의 어머니에 관한 이야기를 했는지 여부
	public MoneyStore()
	{
		NpcTextStrArr = new string[57];
	}
}
