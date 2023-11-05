using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConversationNS;

// 한석호 작성 
public class IngredientStoreTwo : Conversation
{

	public static string text0;
	public static string text10;
	public static string text11;
	public static string text12;
	public static bool IsTalk = false;  // 가게 주인과 잡담을 한번이라도 했는지 여부
	public static bool IsGalicQuest = false;    // 가게 주인의 마늘 고민을 한번이라도 들었는지 여부(마늘 고민 해결했는지 여부)
	public static bool OneChanceGalicClear = false;  // 마늘 고민 해결 후 첫 대화 
	public static int NowDate = 1;
	public static byte Ingredient = 0;
	public static short Discount = -1;
	public static int BounsDiscount = 0;
	public static int Contract = 0;

	public IngredientStoreTwo()
	{
		NpcTextStrArr = new string[38]
		{
			"싸다 싸 ! 오늘 계약하면 ### 이(가) $$$만원 할인 !",	// 0
			"어서오세요! 신선한 마늘, 양파, 고추 팝니다!",	// 1
			"어서오세요...",	// 2
			"왜 그렇게 기운이 없나요?",	// 3
			"실은 공급이 부족해서..마늘 계약을 기본금 17만원에서 32만원으로 올렸거든요..",	// 4
			"식재료를 계약하러 왔습니다.",	// 5
			"(잡담을 떤다.)",	// 6
			"식재료 계약을 해지하러 왔습니다.",	// 7
			"어떤 걸 계약하러 왔나요!",	// 8
			"어떤 걸 해지하러 왔나요?",	// 9
			"(운송 및 서비스 비용 월 $$$만원) 마늘을 계약하러 왔습니다.",	// 10
			"(운송 및 서비스 비용 월 $$$만원) 양파를 계약하러 왔습니다.",	// 11
			"(운송 및 서비스 비용 월 $$$만원) 고추를 계약하러 왔습니다.",	// 12
			"생각해보니 계약 안해도 될 것 같습니다.",	// 13
			"마늘을 해지하러 왔습니다.",	// 14
			"양파를 해지하러 왔습니다.",	// 15
			"고추를 해지하러 왔습니다.",	// 16
			"(첫 대화로 돌아간다.)생각해보니 해지 안해도 될 것 같습니다.",	// 17
			"감사합니다!",	// 18
			"(첫 대화로 돌아간다.)",	// 19
			"(간다.)",	// 20
			"다른 계약도 둘러볼게요.",	// 21
			"아쉽게됐네요.",	// 22
			"다른 계약도 생각해볼게요.",	// 23
			"갑자기 공급이 부족해진 이유가 뭡니까?",	// 24
			"실은 어떤 마녀같은 할멈이 마늘을 전부 사간 바람에...",	// 25
			"(첫 대화로 돌아간다.)..저런",	// 26
			"근처 점집에 무시무시한 마녀 할멈이 살고 있는데, \n항상 우리가게 마늘을 보고 간단 말이죠.",	// 27
			"돈이 없는 걸까요?",	// 28
			"그럴리가..! 듣자하니 점을 취미로 하는 부자라고 하더라구요.",	// 29
			"고마워요! 마녀 할멈의 마늘 독점을 막아주셨다면서요?",	// 30
			"별 것 아닌 일이었습니다.",	// 31
			"하지만 자기 일도 아닌데 이렇게까지 문제를 해결해주셨죠. 정말 감사합니다!",	// 32
			"흠..전 이런 일을 봉사차원으로 하지 않습니다.",	// 33
			"(간다.)",	// 34
			"당연히 감사를 표해야죠! \n이제부터 당신에게는 2만원씩 할인한 요금으로 계약해 드릴게요!",	// 35
			"돈이 부족한거 아닌가요?",	// 36
			"(첫 대화로 돌아간다.)감사합니다"	// 37
		};

		text0 = NpcTextStrArr[0];
		text10 = NpcTextStrArr[10];
		text11 = NpcTextStrArr[11];
		text12 = NpcTextStrArr[12];

		if (Constant.NowDate == 1)
        {
			IsTalk = false;
			IsGalicQuest = false;
			OneChanceGalicClear = false;
			NowDate = 1;
			Ingredient = 0;
			Discount = 0;
			BounsDiscount = 0;
			Contract = 0;
		}

		if (Constant.NowDate != NowDate || Constant.NowDate == 1)
		{
			Ingredient = (byte)Random.Range(0, 3);
			Discount = (byte)Random.Range(0, 3);
			NowDate = Constant.NowDate;

			NpcTextStrArr[0] = text0;

			string s = null;
			string c = null;
			if (Ingredient == 0) { s = "마늘"; }
			else if (Ingredient == 1) { s = "양파"; }
			else if (Ingredient == 2) { s = "고추"; }

			if (Discount == 0) { c = "1"; }
			else if (Discount == 1) { c = "2"; }
			else if (Discount == 2) { c = "3"; }

			NpcTextStrArr[0] = NpcTextStrArr[0].Replace("###", s);
			NpcTextStrArr[0] = NpcTextStrArr[0].Replace("$$$", c);
		}

		TextList = new List<TextNodeC>();
		InitTextList();
		InitStartText();
	}
	protected override void InitStartText()
	{

		if (OneChanceGalicClear)
		{
			startText = new int[1] { 30 };
			//OneChanceGalicClear = false;
		}
		else
		{
			if (!LuckyStore.ClearGalicQuest)
			{
				startText = new int[1] { 2 };
			}
			else
			{
				startText = new int[2] { 0, 1 };
			}
		}
	}
	protected override void InitPlayerSelectText()
	{
		int n = 0;
		NpcTextStrArr[10] = text10;
		NpcTextStrArr[11] = text11;
		NpcTextStrArr[12] = text12;

		if (!LuckyStore.ClearGalicQuest)
		{
			n = (320000 - (((Discount + 1) * 10000) + BounsDiscount))/10000;
			NpcTextStrArr[10] = NpcTextStrArr[10].Replace("$$$", n.ToString());
		}
		else
		{
			n = (170000 - (((Discount + 1) * 10000) + BounsDiscount))/10000;
			NpcTextStrArr[10] = NpcTextStrArr[10].Replace("$$$", n.ToString());
		}

		n = (200000 - (((Discount + 1) * 10000) + BounsDiscount))/10000;
		NpcTextStrArr[11] = NpcTextStrArr[11].Replace("$$$", n.ToString());

		n = (230000 - (((Discount + 1) * 10000) + BounsDiscount))/10000;
		NpcTextStrArr[12] = NpcTextStrArr[12].Replace("$$$", n.ToString());
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
			if (OneChanceGalicClear && LuckyStore.ClearGalicQuest)
			{
				index = TextList.FindIndex(a => a.NowTextNum == -1 && System.Linq.Enumerable.SequenceEqual(a.NextTextNum,new int[2] {33,31}));
			}
			else if (!OneChanceGalicClear && LuckyStore.ClearGalicQuest)
			{
				index = Findidx(-1, new int[1] { Random.Range(0, 2) });
			}
			else
			{
				index = Findidx(-1, new int[1] { 2 });
			}
			SettingConversation(index);
			OneChanceGalicClear = false;
			index = -100;
		}
		else if (temInt == 10)
		{
			//NpcTextStrArr[10] = text10;

			if (!LuckyStore.ClearGalicQuest)
			{
				int n = 320000 - (((Discount + 1) * 10000) + BounsDiscount);
				//NpcTextStrArr[10] = NpcTextStrArr[10].Replace("$$$", n.ToString());

				if (GameManager.Instance.Money >= n)
				{
					GameManager.Instance.Money -= n;
					Constant.UsableIngredient.Add(13);
					Contract++;
					index = Findidx(10, new int[1] { 18 });
				}
				else
				{
					index = Findidx(10, new int[1] { 36 });
				}
			}
			else
			{
				int n = 170000 - (((Discount + 1) * 10000) + BounsDiscount);
				//NpcTextStrArr[10] = NpcTextStrArr[10].Replace("$$$", n.ToString());

				if (GameManager.Instance.Money >= n)
				{
					GameManager.Instance.Money -= n;
					Constant.UsableIngredient.Add(13);
					Contract++;
					index = Findidx(10, new int[1] { 18 });
				}
				else
				{
					index = Findidx(10, new int[1] { 36 });
				}
			}
		}
		else if (temInt == 11)
		{
			//NpcTextStrArr[11] = text11;

			int n = 200000 - (((Discount + 1) * 10000) + BounsDiscount);
			//NpcTextStrArr[11] = NpcTextStrArr[11].Replace("$$$", n.ToString());

			if (GameManager.Instance.Money >= n)
			{
				GameManager.Instance.Money -= n;
				Constant.UsableIngredient.Add(14);
				Contract++;
				index = Findidx(11, new int[1] { 18 });
			}
			else
			{
				index = Findidx(11, new int[1] { 36 });
			}
		}
		else if (temInt == 12)
		{
			//NpcTextStrArr[12] = text12;

			int n = 230000 - (((Discount + 1) * 10000) + BounsDiscount);
			//NpcTextStrArr[12] = NpcTextStrArr[12].Replace("$$$", n.ToString());

			if (GameManager.Instance.Money >= n)
			{
				GameManager.Instance.Money -= n;
				Constant.UsableIngredient.Add(15);
				Contract++;
				index = Findidx(12, new int[1] { 18 });
			}
			else
			{
				index = Findidx(12, new int[1] { 36 });
			}
		}
		else if (temInt == 13)
		{
			if (!LuckyStore.ClearGalicQuest)
			{
				index = Findidx(-1, new int[1] { 2 });
			}
			else if (Random.Range(0,2) == 0)
			{
				index = Findidx(-1, new int[1] { 1 });
			}
			else
			{
				index = Findidx(-1, new int[1] { 0 });
			}
		}
		else if (temInt == 14)
		{
			Constant.UsableIngredient.Remove(13);
			Contract--;
			SettingConversation(Findidx(14, new int[1] { 22 }));
			index = -100;
		}
		else if (temInt == 15)
		{
			Constant.UsableIngredient.Remove(14);
			Contract--;
			SettingConversation(Findidx(15, new int[1] { 22 }));
			index = -100;
		}
		else if (temInt == 16)
		{
			Constant.UsableIngredient.Remove(15);
			Contract--;
			SettingConversation(Findidx(16, new int[1] { 22 }));
			index = -100;
		}
		else if (temInt == 17)
		{
			if (!LuckyStore.ClearGalicQuest)
			{
				index = Findidx(-1, new int[1] { 2 });
			}
			else if (Random.Range(0, 2) == 0)
			{
				index = Findidx(-1, new int[1] { 1 });
			}
			else
			{
				index = Findidx(-1, new int[1] { 0 });
			}
		}
		else if (temInt == 19)
		{
			if (!LuckyStore.ClearGalicQuest)
			{
				index = Findidx(-1, new int[1] { 2 });
			}
			else if (Random.Range(0, 2) == 0)
			{
				index = Findidx(-1, new int[1] { 1 });
			}
			else
			{
				index = Findidx(-1, new int[1] { 0 });
			}
		}
		else if (temInt == 26)
		{
			if (!LuckyStore.ClearGalicQuest)
			{
				index = Findidx(-1, new int[1] { 2 });
			}
			else if (Random.Range(0, 2) == 0)
			{
				index = Findidx(-1, new int[1] { 1 });
			}
			else
			{
				index = Findidx(-1, new int[1] { 0 });
			}
		}
		else if (temInt == 24)
		{
			IsGalicQuest = true;
			SettingConversation(Findidx(24, new int[1] { 25 }));
			index = -100;
		}
		else if (temInt == 28)
		{
			IsTalk = true;
			SettingConversation(Findidx(28, new int[1] { 29 }));
			index = -100;
		}
		else if (temInt == 37)
		{
			BounsDiscount = 20000;

			if (!LuckyStore.ClearGalicQuest)
			{
				index = Findidx(-1, new int[1] { 2 });
			}
			else if (Random.Range(0, 2) == 0)
			{
				index = Findidx(-1, new int[1] { 1 });
			}
			else
			{
				index = Findidx(-1, new int[1] { 0 });
			}
			SettingConversation(index);
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
		if (num == 7)
		{
			if (Contract <= 0)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		else if (num == 10)
		{
			if (Constant.UsableIngredient.FindIndex(a => a == 13) != -1)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		else if (num == 11)
		{
			if (Constant.UsableIngredient.FindIndex(a => a == 14) != -1)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		else if (num == 12)
		{
			if (Constant.UsableIngredient.FindIndex(a => a == 15) != -1)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		else if (num == 14)
		{
			if (Constant.UsableIngredient.FindIndex(a => a== 13) != -1)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		else if (num == 15)
		{
			if (Constant.UsableIngredient.FindIndex(a => a== 14) != -1)
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
			if (Constant.UsableIngredient.FindIndex(a => a==15) != -1)
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
	/// 텍스트들을 연결해서 그래프로 만듦
	/// </summary>
	/// 점집 주인 이미지  0 : 보통 1 : 좋음 2 : 심기불편 3 : 의미심장
	protected override void InitTextList()
	{
		nowTextNum = -1; nextTextNum = new int[4] { 5,7,6,34 }; nextTextIsAble = new bool[4] { true,false,true,true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 0 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = -1; nextTextNum = new int[4] { 5, 7, 6, 34 }; nextTextIsAble = new bool[4] { true, false, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = -1; nextTextNum = new int[5] { 5, 7, 6, 34, 3 }; nextTextIsAble = new bool[5] { true, false, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 2 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 500 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = -1; nextTextNum = new int[2] { 33,31 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 30 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 3; nextTextNum = new int[1] { 24 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 4 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 5; nextTextNum = new int[4] { 10, 11, 12, 13 }; nextTextIsAble = new bool[4] { false, false, false, true };
		methodSArr = new MethodS[6]
		{
			new MethodS(MethodEnum.INITPLAYERTEXT, new int[0]),
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 8 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 6; nextTextNum = new int[1] { 28 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 27 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 7; nextTextNum = new int[4] { 14, 15, 16, 17 }; nextTextIsAble = new bool[4] { false, false, false, true };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 9 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 10; nextTextNum = new int[3] { 19, 21, 20 }; nextTextIsAble = new bool[3] { true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 18 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 10; nextTextNum = new int[3] { 19, 21, 20 }; nextTextIsAble = new bool[3] { true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 36 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 11; nextTextNum = new int[3] { 19, 21, 20 }; nextTextIsAble = new bool[3] { true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 18 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 11; nextTextNum = new int[3] { 19, 21, 20 }; nextTextIsAble = new bool[3] { true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 36 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 12; nextTextNum = new int[3] { 19, 21, 20 }; nextTextIsAble = new bool[3] { true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 18 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 12; nextTextNum = new int[3] { 19, 21, 20 }; nextTextIsAble = new bool[3] { true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 36 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 13; nextTextNum = new int[4] { 5, 7, 6, 34 }; nextTextIsAble = new bool[4] { true, false ,true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 2 })
		};
		AddTextList();
		nowTextNum = 13; nextTextNum = new int[5] { 5, 7, 6, 34, 3 }; nextTextIsAble = new bool[5] { true, false, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 500 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 2 })
		};
		AddTextList();
		nowTextNum = 14; nextTextNum = new int[3] { 19, 23, 20 }; nextTextIsAble = new bool[3] { true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 22 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 15; nextTextNum = new int[3] { 19, 23, 20 }; nextTextIsAble = new bool[3] { true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 22 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 16; nextTextNum = new int[3] { 19, 23, 20 }; nextTextIsAble = new bool[3] { true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 22 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 17; nextTextNum = new int[4] { 5, 7, 6, 34 }; nextTextIsAble = new bool[4] { true, false, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 2 })
		};
		AddTextList();
		nowTextNum = 17; nextTextNum = new int[5] { 5, 7, 6, 34, 3 }; nextTextIsAble = new bool[5] { true, false, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 500 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 2 })
		};
		AddTextList();
		nowTextNum = 19; nextTextNum = new int[4] { 5, 7, 6, 34 }; nextTextIsAble = new bool[4] { true, false, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 2 })
		};
		AddTextList();
		nowTextNum = 19; nextTextNum = new int[5] { 5, 7, 6, 34, 3 }; nextTextIsAble = new bool[5] { true, false, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 500 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 2 })
		};
		AddTextList();
		nowTextNum = 20; nextTextNum = new int[1] { -1 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[1]
		{
			new MethodS(MethodEnum.ENDPANEL, new int[1] { -1 })
		};
		AddTextList();
		nowTextNum = 21; nextTextNum = new int[4] { 10, 11, 12, 13 }; nextTextIsAble = new bool[4] { false, false, false, true };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 8 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 23; nextTextNum = new int[4] { 14, 15, 16, 17 }; nextTextIsAble = new bool[4] { false, false, false, true };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 9 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 24; nextTextNum = new int[1] { 26 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 25 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 26; nextTextNum = new int[4] { 5, 7, 6, 34 }; nextTextIsAble = new bool[4] { true, false, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 2 })
		};
		AddTextList();
		nowTextNum = 26; nextTextNum = new int[5] { 5, 7, 6, 34, 3 }; nextTextIsAble = new bool[5] { true, false, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 500 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 2 })
		};
		AddTextList();
		nowTextNum = 28; nextTextNum = new int[1] { 19 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 29 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 31; nextTextNum = new int[4] { 5, 7, 6, 34 }; nextTextIsAble = new bool[4] { true, false, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 32 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 2 })
		};
		AddTextList();
		nowTextNum = 33; nextTextNum = new int[1] { 19 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 35 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 34; nextTextNum = new int[1] { -1 }; nextTextIsAble = new bool[1] { false };
		methodSArr = new MethodS[1]
		{
			new MethodS(MethodEnum.ENDPANEL, new int[1] { -1 })
		};
		AddTextList();
		nowTextNum = 37; nextTextNum = new int[4] { 5, 7, 6, 34 }; nextTextIsAble = new bool[4] { true, false, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 2 })
		};
		AddTextList();
	}
}
