using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConversationNS;

// 한석호 작성
public class Hospital : Conversation
{
	private int hpToMoney = -1;
	public Hospital()
	{
		NpcTextStrArr = new string[17]
		{
			"(강렬한 눈빛)",	// 0
			"여기 병원은 무엇이 전문인가요?",	// 1
			"치료를 받으러 왔습니다.",	// 2
			"병원입니다.",	// 3
			"아..그렇군요",	//	4
			"총 $$$원입니다.",	// 5
			"(돈을 지불한다.)(1시간 소모)(주사위 합 3 이상) 치료를 받는다.",	// 6
			"치료 끝났습니다.",	// 7
			"치료 실패했습니다.",	// 8
			"(간다.)",	// 9
			"(1시간 소모)(주사위 합 5 이상)뭐라고요? 전화..아니 다시 수술합시다.",	// 10
			"완쾌하셨습니다. 축하드립니다.",	// 11
			"실패했습니다. 죄송합니다.",	// 12
			"(주사위 합 7 이상)이게 뭐에요. 치료비라도 돌려줘요.",	// 13
			"그건..곤란할것 같습니다. 선생.",	// 14
			"(치료비 전액 돌려받음)알겠습니다. 드리겠습니다.",	// 15
			"(간다.)돈이 없네요. 이런.."	// 16
		};

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

		if (temInt == 1)
		{
			SettingConversation(Findidx(1, new int[1] { 3 }));
			index = -100;
		}
		else if (temInt == 2)
		{
			hpToMoney = 100 - (int)(((float)PlayerMove.HP / (float)PlayerMove.MaxHP) * 100);

			SettingConversation(Findidx(2, new int[1] { 5 }), hpToMoney * 1000);

			index = -100;
		}
		else if (temInt == 6)
		{
			GameManager.Instance.Money -= hpToMoney * 1000;
			GameManager.Instance.time += 3600;
			DiceRoll(3);
			index = -100;
		}
		else if (temInt == 9)
		{
			EndPanel();
			index = -100;
		}
		else if (temInt == 10)
		{
			GameManager.Instance.time += 3600;
			DiceRoll(5);
			index = -100;
		}
		else if (temInt == 13)
		{
			DiceRoll(7);
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
		if (num == 6)
		{
			if (GameManager.Instance.Money >= hpToMoney * 1000)
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
			if (GameManager.Instance.Money < hpToMoney * 1000)
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

		if (temInt == 6)
		{
			if (bo)
			{
				PlayerStat.HP = PlayerStat.MaxHP;
				index = Findidx(6, new int[1] { 7 });
			}
			else
			{
				index = Findidx(6, new int[1] { 8 });
			}
		}
		else if (temInt == 10)
		{
			if (bo)
			{
				PlayerStat.HP = PlayerStat.MaxHP;
				index = Findidx(10, new int[1] { 11 });
			}
			else
			{
				index = Findidx(10, new int[1] { 12 });
			}
		}
		else if (temInt == 13)
		{
			if (bo)
			{
				GameManager.Instance.Money += hpToMoney * 1000;
				index = Findidx(13, new int[1] { 15 });
			}
			else
			{
				index = Findidx(13, new int[1] { 14 });
			}
		}

		SettingConversation(index);
	}
	/// <summary>
	/// 텍스트들을 연결해서 그래프로 만듦
	/// </summary>
	/// 병원 의사 이미지  0 : 작음 1 : 중간 2 : 큼 3 : 매우큼
	protected override void InitTextList()
	{
		startText = new int[1] { 0 };

		nowTextNum = -1; nextTextNum = new int[3] { 1,2, 9 }; nextTextIsAble = new bool[3] { true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])

		};
		AddTextList();
		nowTextNum = 1; nextTextNum = new int[1] { 4 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 3 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 2; nextTextNum = new int[2] { 6, 16 }; nextTextIsAble = new bool[2] { false, false };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 5 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
		};
		AddTextList();
		nowTextNum = 4; nextTextNum = new int[3] { 1,2,9 }; nextTextIsAble = new bool[3] { true, true, true };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 6; nextTextNum = new int[1] { 9 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 7 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 6; nextTextNum = new int[2] { 10,9 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 8 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 9; nextTextNum = new int[1] { -1 }; nextTextIsAble = new bool[1] { false };
		methodSArr = new MethodS[1]
		{
			new MethodS(MethodEnum.ENDPANEL, new int[1] {-1})
		};
		AddTextList();
		nowTextNum = 10; nextTextNum = new int[1] { 9 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 11 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 10; nextTextNum = new int[2] { 13, 9 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 12 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 13; nextTextNum = new int[1] { 9 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 15 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 13; nextTextNum = new int[1] { 9 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 14 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
	}
}
