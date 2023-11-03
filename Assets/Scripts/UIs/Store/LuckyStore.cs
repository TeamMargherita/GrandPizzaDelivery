using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConversationNS;

// 한석호 작성
public class LuckyStore : Conversation
{
	public static bool IsAngry = false;
	public static bool IsLuckyTest = false;
	public static bool ClearGalicQuest = false;
	public static bool ClearSonQuest = false;
	public static bool BigDicePlus = false;
	public static bool BigDiceMinus = false;
	public static bool SmallDicePlus = false;
	public static bool SmallDiceMinus = false;
	public static int AngryDate = 0;
	public static int NowDate = 1;
	public LuckyStore()
	{
		NpcTextStrArr = new string[39]
		{
			"보인다..나에겐 너의 모든게 보여..", // 0
			"여긴 뭘 하는 곳인가요?",	// 1
			"너의 운명을 뒤틀어주는 곳이지..",	// 2
			"(첫 대화로 돌아간다.)",	// 3
			"(주위를 둘러보며)마늘이 많네요?",	// 4
			"이것은 신의 축복..축복에 둘러싸인 이곳은 성역이나 다름없지!",	// 5
			"그런 이유때문에 마늘을 곳곳에 둔 건가요?",	// 6
			"마늘은 항암작용 및 예방에 기여하며, 강정, 강장에 도움을 주고 고혈압, \n노화를 예방하며 피부 노화를 방지하고 간 기능 활성화에 정장 및 소화..",	// 7
			"(첫 대화로 돌아간다.)(그만 듣는다.)",	// 8
			"할머니가 마늘을 전부 독점하는 바람에 힘든 가게가 있습니다.",	// 9
			"그게 뭐? 어쨌다는 거냐.",	// 10
			"(주사위 합 11이상)아무리 그래도 마늘을 모조리 사는 건 아니죠. \n다음부턴 이러지 말아주세요.",	// 11
			"너 같이 운도 없고 설득도 못하는 녀석의 말 따위를 내가 들을 것 같냐? \n (기분 안좋아짐)",	// 12
			"오호. 재미있는 녀석이로구나. 좋아. 약속하마. \n그래, 내친김에 마늘도 절반 보내주지.",	// 13
			"뭐냐 애송이. 저리가라.",	// 14
			"(간다.)",	// 15
			"점을 보러 왔습니다.",	// 16
			"오호호. 점을 보려면 선금 20만원을 내야하지.",	// 17
			"(돈을 지불한다.)",	// 18
			"(첫 대화로 돌아간다.)다음에 보겠습니다.",	// 19
			"좋아..너의 미래, 운명이 보여..자, 너도 이리와서 이 수정구를 봐라.",	// 20
			"(주사위 합 8 이상의 짝수)(들여다본다.)",	// 21
			"(주사위 합 5 이하의 홀수)(들여다보지 않는다.)",	// 22
			"어때 꿈같은 미래가 아니니? (하루간 주사위 보너스 4 추가)",	// 23
			"아쉽지만 너의 미래는 뜨거운 불구덩이 속을 헤엄치는구나! \n(하루간 주사위 보너스 4 감소)",	// 24
			"훌륭해. 내가 끔찍한 미래를 보여주려는 걸 피했구나..!\n(하루간 주사위 보너스 2 추가)",	// 25
			"이 좋은 미래를 마다하다니, 어쩌면 그게 너의 운명이겠지..!\n(하루간 주사위 보너스 2 감소)",	// 26
			"(잡담을 한다.)",	// 27
			"들리는 소문에 의하면 부자라고 들었습니다.",	// 28
			"돈? 그런건 부질없어. 마늘 살 때만 빼고...",	// 29
			"할머님 아들에게 전부 들었습니다. \n 마늘 알레르기가 있는 아들이 집에 오지 못하도록 사는 거라면서요?",	// 30
			"뭐! 그놈이 그렇게 말해! 그래 맞다! 그런 일이나 하는 녀석이 뭐가 이쁘냐?",	// 31
			"(주사위 합 14이상)그래도 그분은 효심이 지극한 사람입니다. 마늘때문에\n 집도 못가지만 꾸준히 돈을 보내주고 저한테 할머니 안부까지 듣길 원하시잖아요.",	// 32
			"웃기지마! 저리가라!(기분 안좋아짐)",	// 33
			"확실히 일리가 있군..내가 너무 심했을지도 모르겠다.",	// 34
			"그럼 마늘은 치우시는 건가요?",	// 35
			"안돼! 배고플 때마다 간식으로 까먹는 거란 말이다...\n대신 내가 직접 아들을 보러 가야 겠구나.",	// 36
			"음..선금 15만원이다.",	// 37
			"무슨 이야기가 할게 있는거냐?"	// 38
		};

		if (IsAngry)
		{
			AngryDate += (Constant.NowDate - NowDate);
			if (AngryDate >= 3) { IsAngry = false; AngryDate = 0; }
		}

		if (Constant.NowDate != NowDate)
		{
			IsLuckyTest = false;
			NowDate = Constant.NowDate;

			if (BigDicePlus) { Constant.DiceBonus -= 4; BigDicePlus = false; }
			if (BigDiceMinus) { Constant.DiceBonus += 4; BigDiceMinus = false; }
			if (SmallDicePlus) { Constant.DiceBonus -= 2; SmallDicePlus = false; }
			if (SmallDiceMinus) { Constant.DiceBonus += 2; SmallDiceMinus = false; }
		}

		TextList = new List<TextNodeC>();
		InitTextList();
		InitStartText();
	}
	protected override void InitStartText()
	{
		if (!IsAngry)
		{
			startText = new int[1] { 0 };
		}
		else
		{
			startText = new int[1] { 14 };
		}
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

		if (temInt == -1)
		{
			if (!IsAngry)
			{
				index = Findidx(-1, new int[1] { 0 });
			}
			else
			{
				index = Findidx(-1, new int[1] { 14 });
			}
		}
		else if (temInt == 11)
		{
			DiceRoll(11);
			index = -100;
		}
		else if (temInt == 16)
		{
			if (ClearSonQuest)
			{
				SettingConversation(Findidx(16, new int[1] { 37 }));
			}
			else
			{
				SettingConversation(Findidx(16, new int[1] { 17 }));
			}
			IsLuckyTest = true;
			isCondition = true;
			index = -100;
		}
		else if (temInt == 18)
		{
			if (ClearSonQuest)
			{
				GameManager.Instance.Money -= 150000;
			}
			else
			{
				GameManager.Instance.Money -= 200000;
			}
			SettingConversation(Findidx(18, new int[1] { 20 }));
			index = -100;
		}
		else if (temInt == 19)
		{
			IsLuckyTest = false;
			SettingConversation(Findidx(19, new int[1] { -1 }));
			index = -100;
		}
		else if (temInt == 21)
		{
			DiceRoll(20008);
			index = -100;
		}
		else if (temInt == 22)
		{
			DiceRoll(-10005);
			index = -100;
		}
		else if (temInt == 32)
		{
			DiceRoll(14);
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
		if (num == 9)
		{
			if (IngredientStoreTwo.IsGalicQuest && !ClearGalicQuest)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		else if (num == 16)
		{
			if (!IsLuckyTest)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		else if (num == 18)
		{
			if (ClearSonQuest)
			{
				if (GameManager.Instance.Money >= 150000)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				if (GameManager.Instance.Money >= 200000)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}
		else if (num == 27)
		{
			// 재료 가게2에서 잡담을 해야한다.
			if (IngredientStoreTwo.IsTalk)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		else if (num == 30)
		{
			if (MoneyStore.IsTalk && !ClearSonQuest)
			{
				return true;
			}
			else
			{
				return false;
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
				ClearGalicQuest = true;
				IngredientStoreTwo.OneChanceGalicClear = true;
				index = Findidx(11, new int[1] { 13 });
			}
			else
			{
				IsAngry = true;
				index = Findidx(11, new int[1] { 12 });
			}
		}
		else if (temInt == 21)
		{
			if (bo)
			{
				BigDicePlus = true;
				Constant.DiceBonus += 4;
				index = Findidx(21, new int[1] { 23 });
			}
			else
			{
				BigDiceMinus = true;
				Constant.DiceBonus -= 4;
				index = Findidx(21, new int[1] { 24 });
			}
		}
		else if (temInt == 22)
		{
			if (bo)
			{
				SmallDicePlus = true;
				Constant.DiceBonus += 2;
				index = Findidx(22, new int[1] { 25 });
			}
			else
			{
				SmallDiceMinus = true;
				Constant.DiceBonus -= 2;
				index = Findidx(22, new int[1] { 26 });
			}
		}
		else if (temInt == 32)
		{
			if (bo)
			{
				ClearSonQuest = true;
				index = Findidx(32, new int[1] { 34 });
			}
			else
			{
				IsAngry = true;
				index = Findidx(32, new int[1] { 33 });
			}
		}

		SettingConversation(index);
	}
	/// <summary>
	/// 텍스트들을 연결해서 그래프로 만듦
	/// </summary>
	/// 점집 주인 이미지  0 : 보통 1 : 좋음 2 : 심기불편 3 : 화남
	protected override void InitTextList()
	{
		startText = new int[1] { 0 };

		nowTextNum = -1; nextTextNum = new int[1] { 15 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 14 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = -1; nextTextNum = new int[5] { 1,4,16,27,15 }; nextTextIsAble = new bool[5] { true,true,false,false,true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 0 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 500 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = 1; nextTextNum = new int[1] { 3 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 2 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 3; nextTextNum = new int[5] { 1,4,16,27,15 }; nextTextIsAble = new bool[5] { true, true, false, false, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 500 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = 4; nextTextNum = new int[1] { 6 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 5 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 6; nextTextNum = new int[2] { 8, 9 }; nextTextIsAble = new bool[2] { true, false };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 7 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 8; nextTextNum = new int[5] { 1,4,16,27,15 }; nextTextIsAble = new bool[5] { true, true, false, false, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 500 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = 9; nextTextNum = new int[2] { 3, 11 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 10 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 11; nextTextNum = new int[1] { 3 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 13 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 11; nextTextNum = new int[1] { 15 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 12 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 15; nextTextNum = new int[1] { -1 }; nextTextIsAble = new bool[1] { false };
		methodSArr = new MethodS[1]
		{
			new MethodS(MethodEnum.ENDPANEL, new int[1] {-1})
		};
		AddTextList();
		nowTextNum = 16; nextTextNum = new int[2] { 18, 19 }; nextTextIsAble = new bool[2] { false, true };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 17 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 16; nextTextNum = new int[2] { 18,19 }; nextTextIsAble = new bool[2] { false,true };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 37 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 18; nextTextNum = new int[2] { 21,22 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 20 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 19; nextTextNum = new int[5] { 1, 4, 16, 27, 15 }; nextTextIsAble = new bool[5] { true, true, false, false, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 500 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = 21; nextTextNum = new int[1] { 3 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 23 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 2 })
		};
		AddTextList();
		nowTextNum = 21; nextTextNum = new int[1] { 3 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 24 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 22; nextTextNum = new int[1] { 3 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 25 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 2 })
		};
		AddTextList();
		nowTextNum = 22; nextTextNum = new int[1] { 3 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 26 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 27; nextTextNum = new int[1] { 28 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 38 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 28; nextTextNum = new int[2] { 30,3 }; nextTextIsAble = new bool[2] { false, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 29 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 30; nextTextNum = new int[1] { 32 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 31 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 32; nextTextNum = new int[1] { 15 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 33 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 32; nextTextNum = new int[1] { 35 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 34 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 35; nextTextNum = new int[1] { 3 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 36 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
	}
}
