using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConversationNS;

// 한석호 작성
public class PineAppleStore : Conversation
{
	public static bool isFirstTime = true;
	public static bool isFineapple = true;

	private int goodPoint = 0;
	private int requireMoney = 0;
	public PineAppleStore()
	{
		NpcTextStrArr = new string[42]
		{
			"내가 하는 일에 잘못된 건 없어.",	// 0
			"자유. 오직 자유..",	// 1
			"'그 과일'을 언급만해도 잡혀간다니 이게 과연 옳은 세상인가?",	// 2
			"안녕하세요.",	// 3
			"안녕, 처음보는 얼굴인데. 어떻게 여길 찾아왔지?",	// 4
			"또 왔구나. 근데 아직 물건이 들어오지 않았어. 미안하게 됐군.",	// 5
			"왔구나. 기다리고 있었어.",	// 6
			"삼촌에게 소개받았습니다.",	// 7
			"아. 너구나. 그 가게를 물려받은 꼬마가.",	// 8
			"그런데 여긴 무슨 가게인가요?",	// 9
			"알고싶니? 여긴 특별한 걸 팔고 있어. 경찰들이 말하는 불법음식이지.",	// 10
			"그런 걸 쉽게 말해도 되는 건가요?",	// 11
			"과연..그렇군요.",	// 12
			"혹시 '파인애플'을 말하는 겁니까?",	// 13
			"겁이 많구나. 하지만 너도 곧 구매하게 될거야. \n빚이 있다고 들었는데 어떻게든 갚아야하지 않겠니?",	// 14
			"침착하구나. 아니면 익숙한건가? 혹시 먹어본적 있어? '파인애플'",	// 15
			"겁이 없네. 그 단어를 쉽게 말하다니. 마음에 드는 녀석이야. \n혹시 먹어본적 있니? '파인애플'",	// 16
			"저보고 파인애플 피자를 만들어 팔라는 건가요?",	// 17
			"사실은 생각하고 있었습니다...빚을 갚으려면 뭐라도 해야니까요.",	// 18
			"먹어본적 없습니다. 앞으로도 먹을 생각은 없어요.",	// 19
			"먹어본적 없습니다. 하지만 기회가 되면 먹어보고 싶네요.",	// 20
			"몰랐나 본데, 너희 삼촌은 내 단골 고객이었어. \n가게를 물려받았으니 너가 대신해야하지 않겠니?",	// 21
			"말이 잘 통해서 다행이네. 그럼 살 준비가 된 거지?",	// 22
			"딱히 이유라도 있는건가? 아니 별로 중요하진 않겠지. \n그래서 살 준비는 되었겠지?",	// 23
			"너와는 잘 맞을 거 같아. 그럼 당장 사는 거지?",	// 24
			"얼마입니까?",	// 25
			"60만원이야.",	// 26
			"40만원이야. 꽤 마음에 들었으니 이번만 할인해주는 거야.",	// 27
			"20만원에 줄게. 너와는 오래 알고지낼 듯 하니 이번만 이렇게 팔도록 하지.",	// 28
			"난 너같은 녀석을 좋아해. 이번만 공짜로 줄게. 가져가.",	// 29
			"돈이 없네요. 다음에 올게요.",	// 30
			"(금액을 지불한다.)",	// 31
			"그러도록 해",	// 32
			"구매 고마워. \n 상품은 함부로 들고 다니면 경찰에 걸릴테니 내가 따로 가게에 보내줄게.",	// 33
			"(간다)",	// 34
			"구매하러 왔습니다. '파인애플'",	// 35
			"그래. 60만원이야.",	// 36
			"아차차. 돈이 없네요.",	// 37
			"(금액을 지불한다.)",	// 38
			"아쉽네. 다음에 보자",	// 39
			"항상 고마워.",	// 40
			"(간다)"	// 41
		};

		if (Constant.NowDate == 1 && GameManager.Instance.time >= 32400 && GameManager.Instance.time <= 32500)
        {
			isFirstTime = true;
			isFineapple = true;
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

		if (temInt == 3)
		{
			// 초면인 경우
			if (isFirstTime)
			{
				index = Findidx(3, new int[1] { 4 });
				isFirstTime = false;
			}
			// 구면인경우
			else
			{
				// 파인애플이 없는경우
				if (!isFineapple)
				{
					index = Findidx(3, new int[1] { 5 });
				}
				// 파인애플이 있는 경우
				else
				{
					index = Findidx(3, new int[1] { 6 });
				}
			}
		}
		else if (temInt == 11)
		{
			SettingConversation(Findidx(11, new int[1] { 14 }));
			index = -100;
		}
		else if (temInt == 12)
		{
			SettingConversation(Findidx(12, new int[1] { 15 }));
			goodPoint += 1;
			index = -100;
		}
		else if (temInt == 13)
		{
			SettingConversation(Findidx(13, new int[1] { 16 }));
			goodPoint += 2;
			index = -100;
		}
		else if (temInt == 17)
		{
			SettingConversation(Findidx(17, new int[1] { 21 }));
			index = -100;
		}
		else if (temInt == 18)
		{
			SettingConversation(Findidx(18, new int[1] { 22 }));
			goodPoint += 1;
			index = -100;
		}
		else if (temInt == 19)
		{
			SettingConversation(Findidx(19, new int[1] { 23 }));
			index = -100;
		}
		else if (temInt == 20)
		{
			SettingConversation(Findidx(20, new int[1] { 24 }));
			goodPoint += 1;
			index = -100;
		}
		else if (temInt == 25)
		{
			if (goodPoint == 0)
			{
				index = Findidx(25, new int[1] { 26 });
				requireMoney = 600000;
			}
			else if (goodPoint == 1)
			{
				index = Findidx(25, new int[1] { 27 });
				requireMoney = 400000;
			}
			else if (goodPoint == 2)
			{
				index = Findidx(25, new int[1] { 28 });
				requireMoney = 200000;
			}
			else if (goodPoint >= 3)
			{
				index = Findidx(25, new int[1] { 29 });
				requireMoney = 0;
			}
		}
		else if (temInt == 30)
		{
			SettingConversation(Findidx(30, new int[1] { 32 }));
			index = -100;
		}
		else if (temInt == 31)
		{
			GameManager.Instance.Money -= requireMoney;
			Constant.PineappleCount++;
			isFineapple = false;
			SettingConversation(Findidx(31, new int[1] { 33 }));
			index = -100;
		}
		else if (temInt == 37)
		{
			SettingConversation(Findidx(37, new int[1] { 39 }));
			index = -100;
		}
		else if (temInt == 38)
		{
			GameManager.Instance.Money -= 600000;
			Constant.PineappleCount++;
			isFineapple = false;
			SettingConversation(Findidx(38, new int[1] { 40 }));
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
		if (num == 30)
		{
			if (requireMoney > GameManager.Instance.Money)
			{
				return true;
			}
		}
		else if (num == 31)
		{
			if (requireMoney <= GameManager.Instance.Money)
			{
				return true;
			}
		}
		else if (num == 37)
		{
			if (600000 > GameManager.Instance.Money)
			{
				return true;
			}
		}
		else if (num == 38)
		{
			if (600000 <= GameManager.Instance.Money)
			{
				return true;
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
		startText = new int[3] { 0, 1, 2 };

		nowTextNum = -1; nextTextNum = new int[1] { 3 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[3]
		{
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 } )
		};
		AddTextList();
		nowTextNum = 3;	nextTextNum = new int[1] { 7 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 4 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = 3; nextTextNum = new int[1] { 41 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 5 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = 3; nextTextNum = new int[1] { 35 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 6 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = 7; nextTextNum = new int[1] { 9 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 8 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 9; nextTextNum = new int[3] { 11, 12, 13 }; nextTextIsAble = new bool[3] { true, true, true };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 10 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])

		};
		AddTextList();
		nowTextNum = 11; nextTextNum = new int[2] { 17, 18 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 14 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 12; nextTextNum = new int[2] { 19, 20 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 15 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 13; nextTextNum = new int[2] { 19, 20 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 16 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 17; nextTextNum = new int[1] { 25 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 21 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 18; nextTextNum = new int[1] { 25 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 22 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 19; nextTextNum = new int[1] { 25 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 23 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 20; nextTextNum = new int[1] { 25 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 24 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 25; nextTextNum = new int[2] { 30, 31 }; nextTextIsAble = new bool[2] { false, false };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 26 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 25; nextTextNum = new int[2] { 30, 31 }; nextTextIsAble = new bool[2] { false, false };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 27 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 25; nextTextNum = new int[2] { 30, 31 }; nextTextIsAble = new bool[2] { false, false };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 28 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 25; nextTextNum = new int[2] { 30, 31 }; nextTextIsAble = new bool[2] { false, false };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 29 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 2 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 30; nextTextNum = new int[1] { 34 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 32 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 31; nextTextNum = new int[1] { 34 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 33 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 34; nextTextNum = new int[1] { -1 }; nextTextIsAble = new bool[1] { false };
		methodSArr = new MethodS[1]
		{
			new MethodS(MethodEnum.ENDPANEL, new int[1] { -1 } )
		};
		AddTextList();
		nowTextNum = 35; nextTextNum = new int[2] { 37, 38 }; nextTextIsAble = new bool[2] { false, false };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 36 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 37; nextTextNum = new int[1] { 41 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 39 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 38; nextTextNum = new int[1] { 41 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 40 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 41; nextTextNum = new int[1] { -1 }; nextTextIsAble = new bool[1] { false };
		methodSArr = new MethodS[1]
		{
			new MethodS(MethodEnum.ENDPANEL, new int[1] { -1 } )
		};
		AddTextList();
	}
}
