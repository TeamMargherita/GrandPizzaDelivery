using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConversationNS;

// 한석호 작성
public class PineAppleStoreTwo : Conversation
{
	public static bool isPineapple = true;
	public static bool isContract = false;
	public static bool isMeet = false;
	public PineAppleStoreTwo()
	{
		NpcTextStrArr = new string[36]
		{
			"여기 가게 안해요",	// 0
			"죄송합니다. 문이 열려있어서 영업하는 줄 알았습니다.",	// 1
			"(간다.)",	// 2
			"어서오세요. 근데 아직 물건이 안들어왔거든요.",	// 3
			"오! 마침 물건이 들어왔는데 딱 오셨네요",	//	4
			"지금 바로 구매할 수 있을까요?",	// 5
			"당연하죠. 55만원이에요",	// 6
			"(구매한다.)",	// 7
			"지금은 돈이 부족하네요. 다음에 오겠습니다.",	// 8
			"네, 다음에 뵈요.",	// 9
			"감사합니다. 다음에 뵈요!",	// 10
			"잠깐만, 혹시 피자가게의?",	// 11
			"내, 제가 새로운 점장입니다.",	// 12
			"잘못보신듯합니다.",	// 13
			"아이고, 아니구나.",	// 14
			"오, 역시. 제가 눈썰미 하나는 좋거든요.",	// 15
			"제 소식이 가게마다 퍼진 기분이네요?",	// 16
			"여기 살면서 그 피자가게를 모르는 사람은 없거든요.",	// 17
			"그나저나 여기는 뭘 파는 가게입니까?",	// 18
			"실은 말이죠..여기 '그 물건'을 팔고 있어요.",	// 19
			"'파인애플'말하는 건가요?",	// 20
			"'그 과일' 말씀이십니까?",	// 21
			"조용히..! 그러다 누가 듣겠어요..맞아요. 그걸 팔고 있죠.",	// 22
			"맞아요. 경찰의 눈을 피해 판매하고 있죠.",	// 23
			"혹시 삼촌은 여기서 자주 그것을 구매하신 건가요?",	// 24
			"그래요. 단골손님이었죠. 물론 그쪽도 앞으로 그러실거죠?",	// 25
			"어서오세요. 혹시 계약금을 가져오셨나요?",	// 26
			"(계약금 40만원을 준다.)",	// 27
			"좀 더 생각해보겠습니다.",	// 28
			"물론이죠.",	// 29
			"흠 생각이 바뀌시면 말씀해주세요. 저희 거래의 계약금은 40만원인 점 알아주시고요.",	// 30
			"좋네요! 40만원만 내주신다면 바로 계약해드릴게요.",	// 31
			"아차차, 돈이 부족하네요.",	// 32
			"느긋하게 모아주세요. 가격이 오르는 건 아니니까요.",	// 33
			"계약금 받았습니다! 계약 기념으로 무료로 '물건' 하나를 드릴게요 \n단, 다음부터는 55만원을 내셔야 살 수 있다는 점 기억해주세요.",	// 34
			"(간다.)알겠습니다. 다음에 뵙겠습니다."	// 35
		};

		if (Constant.NowDate == 1)
        {
			isPineapple = true;
			isContract = false;
			isMeet = false;
		}

		TextList = new List<TextNodeC>();
		InitTextList();
		InitStartText();
	}
	protected override void InitStartText()
	{
		if (!isMeet)
		{
			startText = new int[1] { 0 };
		}
		else
		{
			if (!isContract)
			{
				startText = new int[1] { 26 };
			}
			else
			{
				if (!isPineapple)
				{
					startText = new int[1] { 3 };
				}
				else
				{
					startText = new int[1] { 4 };
				}
			}
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
			if (!isMeet)
			{
				index = Findidx(-1, new int[1] { 0 });
			}
			else
			{
				if (!isContract)
				{
					index = Findidx(-1, new int[1] { 26 });
				}
				else
				{
					if (!isPineapple)
					{
						index = Findidx(-1, new int[1] { 3 });
					}
					else
					{
						index = Findidx(-1, new int[1] { 4 });
					}
				}
			}
		}
		else if (temInt == 2)
		{
			SettingConversation(TextList.FindIndex(a => a.NowTextNum == 2));
			index = -100;
		}
		else if (temInt == 7)
		{
			isPineapple = false;
			Constant.PineappleCount++;
			GameManager.Instance.Money -= 550000;
			index = Findidx(7, new int[1] { 10 });
		}
		else if (temInt == 8)
		{
			index = Findidx(8, new int[1] { 9 });
		}
		else if (temInt == 27)
		{
			isContract = true;
			GameManager.Instance.Money -= 400000;
			SettingConversation(Findidx(27, new int[1] { 34 }));
			index = -100;
		}
		else if (temInt == 28)
		{
			isMeet = true;
			SettingConversation(Findidx(28, new int[1] { 30 }));
			index = -100;
		}
		else if (temInt == 29)
		{
			isMeet = true;
			SettingConversation(Findidx(29, new int[1] { 31 }));
			index = -100;
		}
		else if (temInt == 32)
		{
			SettingConversation(Findidx(32, new int[1] { 33 }));
			index = -100;
		}
		else if (temInt == 35)
		{
			isPineapple = false;
			Constant.PineappleCount++;
			SettingConversation(TextList.FindIndex(a => a.NowTextNum == 35));
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
			if (GameManager.Instance.Money >= 550000)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		else if (num == 8)
		{
			if (GameManager.Instance.Money < 550000)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		else if (num == 27)
		{
			if (GameManager.Instance.Money >= 400000)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		else if (num == 32)
		{
			if (GameManager.Instance.Money < 400000)
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

		nowTextNum = -1; nextTextNum = new int[2] { 1, 2 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 0 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 } )
		};
		AddTextList();
		nowTextNum = -1; nextTextNum = new int[1] { 2 }; nextTextIsAble = new bool[1] {true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 3 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 } )
		};
		AddTextList();
		nowTextNum = -1; nextTextNum = new int[2] { 5, 2 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 4 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 } )
		};
		AddTextList();
		nowTextNum = -1; nextTextNum = new int[3] { 27, 32, 2 }; nextTextIsAble = new bool[3] { false, false, true };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 26 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 1; nextTextNum = new int[2] { 12, 13 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 11 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 } )
		};
		AddTextList();
		nowTextNum = 2; nextTextNum = new int[1] { -1 }; nextTextIsAble = new bool[1] { false };
		methodSArr = new MethodS[1]
		{
			new MethodS(MethodEnum.ENDPANEL, new int[1] { -1 })
		};
		AddTextList();
		nowTextNum = 5; nextTextNum = new int[2] { 7, 8 }; nextTextIsAble = new bool[2] { false, false };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 6 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 } )
		};
		AddTextList();
		nowTextNum = 7; nextTextNum = new int[1] { 2 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 10 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 } )
		};
		AddTextList();
		nowTextNum = 8; nextTextNum = new int[1] { 2 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 9 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 } )
		};
		AddTextList();
		nowTextNum = 12; nextTextNum = new int[1] { 16 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 15 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 } )
		};
		AddTextList();
		nowTextNum = 13; nextTextNum = new int[1] { 2 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 14 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 2 } )
		};
		AddTextList();
		nowTextNum = 16; nextTextNum = new int[1] { 18 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 17 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 } )
		};
		AddTextList();
		nowTextNum = 18; nextTextNum = new int[2] { 20, 21 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 19 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 } )
		};
		AddTextList();
		nowTextNum = 20; nextTextNum = new int[1] { 24 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 22 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 } )
		};
		AddTextList();
		nowTextNum = 21; nextTextNum = new int[1] { 24 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 23 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 } )
		};
		AddTextList();
		nowTextNum = 24; nextTextNum = new int[2] { 28, 29 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 25 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 27; nextTextNum = new int[1] { 35 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 34 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 28; nextTextNum = new int[1] { 2 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 30 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 } )
		};
		AddTextList();
		nowTextNum = 29; nextTextNum = new int[2] { 27, 32 }; nextTextIsAble = new bool[2] { false, false };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 31 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 32; nextTextNum = new int[1] { 2 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 33 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 } )
		};
		AddTextList();
		nowTextNum = 35; nextTextNum = new int[1] { -1 }; nextTextIsAble = new bool[1] { false };
		methodSArr = new MethodS[1]
		{
			new MethodS(MethodEnum.ENDPANEL, new int[1] { -1 }),
		};
		AddTextList();

	}
}
