using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConversationNS;

// 한석호 작성
public class IngredientStore : Conversation
{
	public static int Contract = 0;
	public static bool Hint = false;
	public static bool OneChance = true;
	public IngredientStore()
	{
		NpcTextStrArr = new string[41]
		{
			"야채 사세요. 사세요. 싸진 않아도 품질이 좋습니다.",	// 0
			"무엇을 팔고 있나요?",	// 1
			"계약 관련해서 할 말이 있습니다.",	// 2
			"(잡담을 떤다.)",	// 3
			"사과, 당근 파..대파를 팔고 있어.",	// 4
			"(돌아간다.) 그렇군요.",	// 5
			"그중에 사과는 과일 아닌가요?",	// 6
			"어쩌다보니..그보다 살거야 말거야?",	// 7
			"(돌아간다.)",	// 8
			"납품 계약을 하고 싶습니다.",	// 9
			"납품 계약을 해지하고 싶습니다.",	// 10
			"그래. 어떤걸 계약할래?",	// 11
			"어떤걸 해지할래?",	// 12
			"(배송 및 서비스 비용 월 20만원)사과를 계약하고 싶습니다.",	// 13
			"(배송 및 서비스 비용 월 17만원)당근을 계약하고 싶습니다.",	// 14
			"(배송 및 서비스 비용 월 14만원)대파를 계약하고 싶습니다.",	// 15
			"좋아. 대신 배송, 서비스 비용과 별도로 재료값은 \n사용할 때마다 따로 든다는 거 잊지마.",	// 16
			"(돌아간다.)아차차. 돈이 없네요.",	//17
			"알겠습니다.(돌아간다.)",	// 18
			"(간다.)",	// 19
			"사과를 해지하고 싶습니다.",	// 20
			"당근을 해지하고 싶습니다.",	// 21
			"대파를 해지하고 싶습니다.",	// 22
			"(돌아간다.)생각해보니 해지 안해도 될 거 같습니다.",	// 23
			"그래. 아쉽지만 뭐. 나중에 계약하고 싶으면 다시 말해줘.",	// 24
			"그래, 가게는 잘 적응하고 있니?",	// 25
			"그럭저럭 적응중입니다.",	// 26
			"실은 꽤 어려운 상황이에요.",	// 27
			"그럼 다행이네",	// 28
			"(돌아간다.)",	// 29
			"과연..하긴 힘들겠지..역시 알려줘야 하나..",	// 30
			"흠..좀 더 단골손님이 되어준다면 내가 도움을 줄게",	// 31
			"(돌아간다.)알겠습니다.",	// 32
			"(주사위 합 10 이상) 부디 알려주세요..!",	// 33
			"좋아. 그럼 기억해둬. '자유의 헤일로' \n 이 이상은 알아서 찾아봐.",	// 34
			"안돼 안돼. 너에겐 아직 이른거 같아.",	// 35
			"기억하고 있지? '자유의 헤일로'야. 이게 도움이 될 거라고.",	// 36
			"(돌아간다.)알겠습니다.",	// 37
			"뭔가 있다면 알고 싶습니다.",	// 38
			"무슨 할말?",	// 39
			"돈이 부족해 보이는걸?"	// 40
		};

		if (Constant.NowDate == 1)
        {
			Contract = 0;
			Hint = false;
			OneChance = true;
		}

		TextList = new List<TextNodeC>();
		InitTextList();
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
		if (temInt == 13)
		{
			if (GameManager.Instance.Money >= 200000)
			{
				GameManager.Instance.Money -= 200000;
				Constant.UsableIngredient.Add(10);
				Contract++;
				index = Findidx(13, new int[1] { 16 });
			}
			else
			{
				index = Findidx(13, new int[1] { 40 });
			}
		}
		else if (temInt == 14)
		{
			if (GameManager.Instance.Money >= 170000)
			{
				GameManager.Instance.Money -= 170000;
				Constant.UsableIngredient.Add(11);
				Contract++;
				index = Findidx(14, new int[1] { 16 });
			}
			else
			{
				index = Findidx(14, new int[1] { 40 });
			}
		}
		else if (temInt == 15)
		{
			if (GameManager.Instance.Money >= 140000)
			{
				GameManager.Instance.Money -= 140000;
				Constant.UsableIngredient.Add(12);
				Contract++;
				index = Findidx(15, new int[1] { 16 });
			}
			else
			{
				index = Findidx(15, new int[1] { 40 });
			}
		}
		else if (temInt == 20)
		{
			Constant.UsableIngredient.Remove(10);
			Contract--;
			SettingConversation(Findidx(20, new int[1] { 24 }));
			index = -100;
		}
		else if (temInt == 21)
		{
			Constant.UsableIngredient.Remove(11);
			Contract--;
			SettingConversation(Findidx(21, new int[1] { 24 }));
			index = -100;
		}
		else if (temInt == 22)
		{
			Constant.UsableIngredient.Remove(12);
			Contract--;
			SettingConversation(Findidx(22, new int[1] { 24 }));
			index = -100;
		}
		else if (temInt == 23)
		{
			SettingConversation(Findidx(23, new int[1] { -1 }));
			index = -100;
		}
		else if (temInt == 27)
		{
			if (!Hint)
			{
				if (Contract >= 2)
				{
					Hint = true;
					index = Findidx(27, new int[1] { 30 });
				}
				else if (Contract < 2)
				{
					index = Findidx(27, new int[1] { 31 });
				}
			}
			else
			{
				index = Findidx(27, new int[1] { 36 });
			}
		}
		else if (temInt == 33)
		{
			OneChance = false;
			DiceRoll(10);
			index = -100;
		}
		return index;
	}
	/// <summary>
	/// 주사위 결과에 따른 대화 분기점
	/// </summary>
	/// <param name="bo"></param>
	public override void DiceResult(bool bo)
	{
		int index = -1;
		if (temInt == 33)
		{
			if (bo)
			{
				index = Findidx(33, new int[1] { 34 });
			}
			else
			{
				index = Findidx(33, new int[1] { 35 });
			}
		}

		SettingConversation(index);
	}
	/// <summary>
	/// 플레이어의 상태에 따른 대화 등장 유무
	/// </summary>
	/// <param name="num"></param>
	/// <returns></returns>
	protected override bool Condition(int num)
	{
		if (num == 13)
		{
			if (Constant.UsableIngredient.FindIndex(a => a == 10) != -1)
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
			if (Constant.UsableIngredient.FindIndex(a => a == 11) != -1)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		else if (num == 15)
		{
			if (Constant.UsableIngredient.FindIndex(a => a == 12) != -1)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		else if (num == 20)
		{
			if (Constant.UsableIngredient.FindIndex(a => a == 10) != -1)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		else if (num == 21)
		{
			if (Constant.UsableIngredient.FindIndex(a => a == 11) != -1)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		else if (num == 22)
		{
			if (Constant.UsableIngredient.FindIndex(a => a == 12) != -1)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		else if (num == 33)
		{
			if (OneChance)
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
	/// 가게 주인 이미지  0 : 기분좋음 1 : 관심없음 2 : 보통 3 : 의미심장
	protected override void InitTextList()
	{
		startText = new int[1] { 0 };

		nowTextNum = -1; nextTextNum = new int[4] { 1, 2, 19, 3 }; nextTextIsAble = new bool[4] { true, true, true, true };
		methodSArr = new MethodS[3]
		{
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 } )
		};
		AddTextList();
		nowTextNum = 1; nextTextNum = new int[2] { 5, 6 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 4 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = 2; nextTextNum = new int[2] { 9, 10 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 39 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 3; nextTextNum = new int[2] { 26, 27 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 25 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 5; nextTextNum = new int[4] { 1, 2, 19, 3 }; nextTextIsAble = new bool[4] { true, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = 6; nextTextNum = new int[1] { 8 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 7 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 8; nextTextNum = new int[4] { 1, 2, 19, 3 }; nextTextIsAble = new bool[4] { true, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = 9; nextTextNum = new int[4] { 13, 14, 15, 17 }; nextTextIsAble = new bool[4] { false, false, false, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 11 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 10; nextTextNum = new int[4] { 20, 21, 22, 23 }; nextTextIsAble = new bool[4] { false, false, false, true };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 12 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 13; nextTextNum = new int[1] { 18 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 16 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 13; nextTextNum = new int[1] { 17 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 40 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 14; nextTextNum = new int[1] { 18 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 16 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 14; nextTextNum = new int[1] { 17 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 40 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 15; nextTextNum = new int[1] { 18 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 16 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 15; nextTextNum = new int[1] { 17 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 40 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 17; nextTextNum = new int[4] { 1, 2, 19, 3 }; nextTextIsAble = new bool[4] { true, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 18; nextTextNum = new int[4] { 1, 2, 19, 3 }; nextTextIsAble = new bool[4] { true, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 19; nextTextNum = new int[1] { -1 }; nextTextIsAble = new bool[1] { false };
		methodSArr = new MethodS[1]
		{
			new MethodS(MethodEnum.ENDPANEL, new int[1] { -1 } )
		};
		AddTextList();
		nowTextNum = 20; nextTextNum = new int[1] { 18 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 24 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 21; nextTextNum = new int[1] { 18 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 24 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 22; nextTextNum = new int[1] { 18 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 24 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 23; nextTextNum = new int[4] { 1, 2, 19, 3 }; nextTextIsAble = new bool[4] { true, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 26; nextTextNum = new int[1] { 29 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 28 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = 27; nextTextNum = new int[1] { 38 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 30 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 27; nextTextNum = new int[2] { 32, 33 }; nextTextIsAble = new bool[2] { true, false };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 31 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 27; nextTextNum = new int[1] { 37 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 36 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 29; nextTextNum = new int[4] { 1, 2, 19, 3 }; nextTextIsAble = new bool[4] { true, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = 32; nextTextNum = new int[4] { 1, 2, 19, 3 }; nextTextIsAble = new bool[4] { true, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = 33; nextTextNum = new int[1] { 18 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 34 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 33; nextTextNum = new int[1] { 32 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 35 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 37; nextTextNum = new int[4] { 1, 2, 19, 3 }; nextTextIsAble = new bool[4] { true, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 38; nextTextNum = new int[1] { 18 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 34 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
	}
}
