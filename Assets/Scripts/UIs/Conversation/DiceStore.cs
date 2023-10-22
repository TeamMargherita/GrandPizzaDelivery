using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConversationNS;

// 한석호 작성
public class DiceStore : Conversation , ICloseStore
{
	public static bool isOneDayDiceStore = false;
	
	private int DiceLuck = -1;
	private int ItemSumCost = -1;
	private bool isDiscount = true;
	public DiceStore()
	{
		store = new DiceStoreItem(this);

		NpcTextStrArr = new string[39]
		{
			"인생은 주사위론 정할 수 없는 법이야...",	// 0
			"우리 집은 대대로 주사위를 만들었어..\n나도 주사위에 인생을 걸고 있어.",	// 1
			"오늘의 운세는 6. 오늘 너는 신의 축복을 받고 있군.",	// 2
			"오늘의 운세는 5. 축복받은 하루가 되겠어.",	// 3
			"오늘의 운세는 4. 운이 좋다고 자만하지 말길.",	// 4
			"오늘의 운세는 3. 그저 하루에 충실할 것.",	// 5
			"오늘의 운세는 2. 조심해, 너에게 안좋은 기류가 흐른다.",	// 6
			"오늘의 운세는 1. 빨랑 사고 돌아가. 나까지 재수가 없어지겠어.",	// 7
			"(돌아간다.)",	// 8
			"어떤 물건을 팔고 있나요?",	// 9
			"상품을 보고 싶습니다.",	// 10
			"(주사위 합 9 이상)물건을 몰래 훔친다.",	// 11
			"(간다.)",	// 12
			"주사위를 팔고 있어. 어떤 사람에겐 놀이에 불과할 수 있는 이것이 \n누군가에겐 살아가는 데 있어 중요한 선택을 도울지 모르지.",	// 13
			"전부 내가 깎은 것들이야. \n너의 운명을 맡길 주사위를 잘 선택해봐",	// 14
			"아무것도 아닙니다.",	// 15
			"음. 방금 뭔가 했나?",	// 16
			"너 같은 애송이가 전에도 몇번 있었지. 잘 가라.",	// 17
			"(도망간다.)",	// 18
			"총 $$$원이야.",	// 19
			"총 $$$원이야. 돈은 충분히 있겠지?",	// 20
			"총 $$$원이야. 너무 무리하지 않았으면 하는군.",	// 21
			"(돈을 지불한다.)",	// 22
			"죄송합니다. 돈이 부족하네요.",	// 23
			"가격을 좀 깎아줘도 될까요?",	//24
			"다음에 또 보자고.",	// 25
			"나도 가끔 계산 실수는 하니까. 다시 잘 선택해봐.",	// 26
			"좋아. 단, 이걸 결정하는 건 주사위가 될거야. 자 던져봐.",	// 27
			"이 정도 금액 차라면 내가 할인을 해줄 수도 있어. 단, 이걸 결정하는 건 주사위가 될거야. \n 넌 오늘 운수가 좋은 듯 하니 이정돈 일도 아니겠지?",	// 28
			"(간다)",	// 29
			"다시 골라볼게요.",	// 30
			"(주사위 합 10 이상)(주사위를 던진다.)",	// 31
			"좋은 걸 봤어. 구매 고마워.",	// 32
			"아쉽게 됐어. 어쩌면 운세가 잘못된 걸까?",	// 33
			"더 살게 있습니다.",	// 34
			"너에게 대운이 따르는군. 좋아 $$$원에 팔지.",	// 35
			"아쉽게 됐어. 할인은 없는 걸로 하지. $$$원이야.",	// 36
			"(구매를 취소한다.)",	// 37
			"(주사위 합 8 이상)한번 해보겠습니다."	// 38
		};

		TextList = new List<TextNodeC>();
		InitTextList();
		InitStartText();
	}
	protected override void InitStartText()
	{
		if (isOneDayDiceStore)
		{
			startText = new int[3] { 0, 1, DiceLuck };
		}
		else
		{
			isDiscount = true;
			//Debug.Log("디스카운트 실화?");
			DiceLuck = Random.Range(2, 8);
			startText = new int[1] { DiceLuck };
			isOneDayDiceStore = true;
		}
	}
	public override void JumpConversation(int num)
	{
		int index = -1;
		// 플레이어가 물건을 고르고 구매하기를 눌렀을 때
		if (temInt == -5)
		{
			ItemSumCost = num;
			if (num <= 5000)
			{
				index = Findidx(-5, new int[1] { 19 });
			}
			else if (num > 5000 && num <= 30000)
			{
				index = Findidx(-5, new int[1] { 20 });
			}
			else if (num > 30000)
			{
				index = Findidx(-5, new int[1] { 21 });
			}
			SettingConversation(index, ItemSumCost);
			return;
		}
		
	}
	public override void JumpConversation()
	{
		int index = -1;
		// 플레이어가 고른 물건을 취소하고 상점 패널을 닫았을 때
		if (temInt == -5)
		{
			ItemSumCost = -1;
			index = Findidx(-5, new int[1] { -1 });
		}
		SettingConversation(index);
	}
	/// <summary>
	/// 조건에 따른 대화 분기점
	/// </summary>
	/// <param name="tem"></param>
	/// <returns></returns>
	protected override int Bifurcation(List<TextNodeC> tem)
	{
		int index = -1;
		temInt = tem[0].NowTextNum;
		if (temInt == 11)
		{
			DiceRoll(9);
			index = -100;
		}
		else if (temInt == 22)
		{
			// 산 물건들 인벤에 들어감
			AddPlayerItemDic();
			// 금액지불
			GameManager.Instance.Money -= ItemSumCost;
			ItemSumCost = -1;
			SettingConversation(Findidx(22, new int[1] { 25 }));
			index = -100;
		}
		else if (temInt == 23)
		{
			// 현재 가진 돈보다 가격이 10% 초과일 때
			if (GameManager.Instance.Money * 1.1f < ItemSumCost)
			{
				index = Findidx(23, new int[1] { 26 });
			}
			// 현재 가진 돈보다 10% 이하로 비쌀 때, 오늘의 운세가 6이 나왔을 때
			else if (GameManager.Instance.Money * 1.1f >= ItemSumCost && GameManager.Instance.Money < ItemSumCost && DiceLuck == 2)
			{
				index = Findidx(23, new int[1] { 28 });
			}
			else
			{
				index = Findidx(23, new int[1] { 26 });
			}
			SettingConversation(index);
			index = -100;
		}
		else if (temInt == 24)
		{
			index = Findidx(24, new int[1] { 27 });
			SettingConversation(index);
			index = -100;
		}
		else if (temInt == 31)
		{
			DiceRoll(10);
			index = -100;
		}
		else if (temInt == 38)
		{
			DiceRoll(8);
			index = -100;
		}
		return index;
	}
	/// <summary>
	/// 플레이어의 상태에 따른 대화 등장 유무
	/// </summary>
	/// <param name="num"></param>
	/// <returns></returns>
	protected override bool Condition(int num)
	{
		if (num == 22)
		{
			if (GameManager.Instance.Money >= ItemSumCost)
			{
				SetISCondition();
				return true;
			}
		}
		else if (num == 23)
		{
			if (GameManager.Instance.Money < ItemSumCost)
			{
				return true;
			}
		}
		else if (num == 24)
		{
			if (isDiscount)
			{
				return true;
			}
		}

		return false;
	}
	/// <summary>
	/// 주사위 결과에 따른 대화 분기점
	/// </summary>
	/// <param name="bo"></param>
	public override void DiceResult(bool bo)
	{
		int index = -1;

		if (temInt == 11)
		{
			if (bo)
			{
				index = Findidx(11, new int[1] { 16 });
			}
			else
			{
				index = Findidx(11, new int[1] { 17 });
			}
		}
		else if (temInt == 31)
		{
			if (bo)
			{
				index = Findidx(31, new int[1] { 35 });
				if (ItemSumCost > 2000)
				{
					ItemSumCost -= 2000;
				}
				else
				{
					ItemSumCost = 0;
				}
			}
			else
			{
				index = Findidx(31, new int[1] { 36 });
				isDiscount = false;
			}
			SettingConversation(index, ItemSumCost);
			return;
		}
		else if (temInt == 38)
		{
			if (bo)
			{
				index = Findidx(38, new int[1] { 32 });
				if (ItemSumCost > 2000)
				{
					ItemSumCost -= 2000;
				}
				else
				{
					ItemSumCost = 0;
				}
				GameManager.Instance.Money -= ItemSumCost;
			}
			else
			{
				index = Findidx(38, new int[1] { 33 });
			}
		}
		SettingConversation(index);
	}
	/// <summary>
	/// 텍스트를 연결해서 그래프로 만듦
	/// </summary>
	/// 상점주인 이미지 0 : 보통 , 1 : 좋음 , 2 : 도발 , 3 : 화남
	protected override void InitTextList()
	{
		startText = new int[6] { 2, 3, 4, 5, 6, 7 };
		nowTextNum = -5; nextTextNum = new int[1] { 8 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = -5; nextTextNum = new int[3] { 22, 23, 24 }; nextTextIsAble = new bool[3] { false, false, false };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 19 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = -5; nextTextNum = new int[3] { 22, 23, 24 }; nextTextIsAble = new bool[3] { false, false, false };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 20 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = -5; nextTextNum = new int[3] { 22, 23, 24 }; nextTextIsAble = new bool[3] { false, false, false };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 21 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = -1; nextTextNum = new int[4] { 9, 10, 11, 12 }; nextTextIsAble = new bool[4] { true, true, true, true };
		methodSArr = new MethodS[3]
		{
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 } )
		};
		AddTextList();
		nowTextNum = 8; nextTextNum = new int[4] { 9, 10, 11, 12 }; nextTextIsAble = new bool[4] { true, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = 9; nextTextNum = new int[1] { 8 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 13 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] {1, 100 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = 10; nextTextNum = new int[0]; nextTextIsAble = new bool[0];
		methodSArr = new MethodS[6]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 14 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 0 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 }),
			new MethodS(MethodEnum.SAVETEXTINDEX, new int[1] { -5}),
			new MethodS(MethodEnum.OPENSTORE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = 11; nextTextNum = new int[1] { 15 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 16 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 } )
		};
		AddTextList();
		nowTextNum = 11; nextTextNum = new int[1] { 18 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 17 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 2 }),
			new MethodS(MethodEnum.SPAWNPOLICE, new int[1] { 4 })
		};
		AddTextList();
		nowTextNum = 12; nextTextNum = new int[1] { -1 }; nextTextIsAble = new bool[1] { false };
		methodSArr = new MethodS[1]
		{
			new MethodS(MethodEnum.ENDPANEL, new int[1] { -1 } )
		};
		AddTextList();
		nowTextNum = 15; nextTextNum = new int[4] { 9, 10, 11, 12 }; nextTextIsAble = new bool[4] { true, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = 18; nextTextNum = new int[1] { -1 }; nextTextIsAble = new bool[1] { false };
		methodSArr = new MethodS[1]
		{
			new MethodS(MethodEnum.ENDPANEL, new int[1] { - 1} )
		};
		AddTextList();
		nowTextNum = 22; nextTextNum = new int[2] { 29, 34 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 25 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = 23; nextTextNum = new int[3] { 30, 8, 12 }; nextTextIsAble = new bool[3] { true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 26 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 23; nextTextNum = new int[2] { 38, 12 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 28 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 24; nextTextNum = new int[1] { 31 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 27 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 29; nextTextNum = new int[1] { -1 }; nextTextIsAble = new bool[1] { false };
		methodSArr = new MethodS[1]
		{
			new MethodS(MethodEnum.ENDPANEL, new int[1] { - 1} )
		};
		AddTextList();
		nowTextNum = 30; nextTextNum = new int[0]; nextTextIsAble = new bool[0];
		methodSArr = new MethodS[6]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 14 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 0 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 }),
			new MethodS(MethodEnum.SAVETEXTINDEX, new int[1] { -5 }),
			new MethodS(MethodEnum.OPENSTORE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = 31; nextTextNum = new int[3] { 22, 23, 24 }; nextTextIsAble = new bool[3] { false, false, false };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 35 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 31; nextTextNum = new int[3] { 22, 23, 24 }; nextTextIsAble = new bool[3] { false, false, false };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 36 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 34; nextTextNum = new int[0]; nextTextIsAble = new bool[0];
		methodSArr = new MethodS[6]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 14 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 0 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 }),
			new MethodS(MethodEnum.SAVETEXTINDEX, new int[1] { -5 }),
			new MethodS(MethodEnum.OPENSTORE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = 38; nextTextNum = new int[2] { 34, 29 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 32 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 }),
		};
		AddTextList();
		nowTextNum = 38; nextTextNum = new int[3] { 30, 8, 12 }; nextTextIsAble = new bool[3] { true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 33 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 }),
		};
		AddTextList();

	}
}
