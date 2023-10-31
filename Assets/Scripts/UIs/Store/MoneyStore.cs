using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConversationNS;

// 한석호 작성

public class MoneyStore : Conversation
{
	public static bool IsTalk = false;  // 대출업체의 어머니에 관한 이야기를 했는지 여부
	public static bool StartSonQuest = false;   // 가족 관련 퀘스트를 시작했는지 여부
	public static bool OneChanceClearSon = false;   // 클리어 이후 첫 대사
	public static bool IsTalkOneChanceDiscount = false;	// 한번에 한해서 이자를 깎을 수 있음. true가 되면 할인 시도 이미 한 것.
	public static Dictionary<int, int> BorrowMoneyDate = new Dictionary<int, int>();    // 돈빌린 날짜, 금액
	public static Dictionary<int, int> PayMoneyDate = new Dictionary<int, int>();	// 돈빌린 날짜, 갚을 금액
	public static int SumBorrow = 0;    // 총 빌린 금액
	public static float PlusMoney = 1.1f;   // 매일 복리 이자 
	public static int NowDate = 1;  // 날짜
	public static int ClearMoney = 0;	// 퀘스트 성공시 받는 돈
	public MoneyStore()
	{
		NpcTextStrArr = new string[57]
        {
			"손님~ 곤란해 보이는 얼굴을 하고 있네요.",	// 0
			"흠. 여기 올 사람처럼은 안보이는데?",	// 1
			"돈을 빌리러 왔습니다.",	// 2
			"(간다.)",	// 3
			"아이고 손님 얼마나 빌리러 왔습니까?\n(오늘 하루 더 빌릴 수 있는 금액 $$$원)",		// 4
			"(100만원 빌린다.)",	// 5
			"(200만원 빌린다.)",	// 6
			"(300만원 빌린다.)",	// 7
			"(400만원 빌린다.)",	// 8
			"(첫 대화로 돌아간다.)생각해보니 안 빌려도 될 것 같습니다.",	// 9
			"저희 업체를 사용해주셔서 감사하고요, 아이고. 이자는\n 복리로 매일 1%씩 증가한다는 점 알아주세요~",		// 10
			"(첫 대화로 돌아간다.)네 알겠습니다.)",	// 11
			"뭐야 그런 이야긴 못들었어요.",	// 12
			"좀 더 빌리겠습니다.",	// 13
			"손님. 저희가 땅파서 장사합니까?",	// 14
			"(주사위 합 15이상)그러지말고 좀만 깎아주세요.",		// 15
			"(첫 대화로 돌아간다.)죄송합니다. 한번 반항해봤어요.",	// 16
			"손님~힘이 쌔시네..좋아요. 매일 0.5% 복리로 받을게요.",	// 17
			"(체력 40 소모, 체력이 40이하면 1만 남기고 전부 소모)\n손님~너무 반항하면 더 맞는 수가 있어요?",	// 18
			"돈을 갚으러 왔습니다.",	// 19
			"아이고 손님~ 얼마나 갚으러 왔나요?(갚아야 할 금액 $$$원)",	// 20
			"(100만원 갚는다.)",	// 21
			"(200만원 갚는다.)",	// 22
			"(400만원 갚는다.)",	// 23
			"(첫 대화로 돌아간다.)생각해보니 안 갚아도 될 것 같습니다.",	// 24
			"(남은 금액 전부 갚는다.)",	// 25
			"손님~성실도 하시네. 좀 더 가지고 계셔도 되는데",	// 26
			"(첫 대화로 돌아간다.)",	// 27
			"좀 더 갚을게 있습니다.",	// 28
			"그 목걸이 속 인물은 누구인가요?",	// 29
			"아이 참 부끄럽게. 저희 어머니에요.",		// 30
			"효심이 지극하네요.",	// 31
			"후..그렇다 해도 알아주시질 않으니 원..사실 저희 어머니가 요 근방에서 \n점집을 하시거든요. 근데 저를 어찌나 싫어하시는지..",	// 32
			"이유라도 있습니까?",		// 33
			"모르죠 저는. 돈도 꼬박꼬박 드리는데. 집에다가 마늘을 치덕치덕 \n달아놓으셔서..알레르기 때문에 근처도 못갑니다.",		// 34
			"제가 도와드리겠습니다.",	// 35
			"으흠? 사례를 바라는거죠? 저는 가진게 돈밖에 없으니...얼마면 되죠?",	// 36
			"(100만원을 요구한다.)",	// 37
			"(주사위 합 4 이상)(200만원을 요구한다.)",	// 38
			"(주사위 합 8 이상)(400만원을 요구한다.)",	// 39
			"(주사위 합 16 이상)(800만원을 요구한다.)",		// 40
			"(주사위 합 30 이상)(1600만원을 요구한다.)",	// 41
			"좋아요. 그렇게 할게요.",		// 42
			"그렇게 많은 돈은 줄 수 없어요. 100만원으로 합의하죠.",	// 43
			"어머님 관련 이야기입니다.",	// 44
			"아직 해결하지 못했나요? 아니면 제가 급한건가요?",		// 45
			"고마워요. 덕분에 어머니를 몇년만에 보는 건지 참..\n사례금은 여기 있어요. 가져가요.",	// 46
			"(첫 대화로 돌아간다.)좀 더 기다려주세요.",		// 47
			"(첫 대화로 돌아간다.)감사히 받겠습니다.",		// 48
			"요즘은 어떻게 지내나요.",	// 49
			"덕분에 화목하게 지내고 있죠~.가끔식 식사도 하고요. \n어머니가 마늘을 좋아하는지 몰랐다니까요.",	// 50
			"(첫 대화로 돌아간다.)다행이네요.",	// 51
			"대출 규칙이 어떻게 되나요?",	// 52
			"고객님~저희는 하루에 3천만원까지 빌리실 수 있고요. \n최대 한도는 5천만원입니다. 그리고..",		// 53
			"그리고?",	// 54
			"빌린 기간 별로 1달의 시간을 드릴텐데..\n기간을 지나면 손님이 매일 버신 돈이 다 이쪽으로 빠져나간다는 점 잊지 마세요.",		// 55
			"(첫 대화로 돌아간다.)알겠습니다.",	// 56
        };

		if (Constant.NowDate != NowDate)
		{
			Constant.NowDate = NowDate;
			List<int> li = new List<int>();
			foreach (var key in PayMoneyDate.Keys)
			{
				PayMoneyDate[key]++;
				li.Add(key);
			}
			for (int i = 0; i < li.Count; i++)
			{
				PayMoneyDate[li[i]] = (int)(PayMoneyDate[li[i]] * PlusMoney);
			}
		}

		TextList = new List<TextNodeC>();
		InitTextList();
		InitStartText();
	}

	protected override void InitStartText()
    {
		if (GameManager.Instance.Money < 20000000)
        {
			startText = new int[1] { 0 };
        }
		else if (GameManager.Instance.Money >= 20000000)
        {
			startText = new int[1] { 1 };
        }
    }
	/// <summary>
	/// 플레이어의 상태에 따른 대화 등장 유무
	/// </summary>
	/// <param name="num"></param>
	/// <returns></returns>
	protected override bool Condition(int num)
	{
		if (num == 5)
        {
			if (!BorrowMoneyDate.ContainsKey(Constant.NowDate))
			{
				if (SumBorrow <= 50000000 - 30000000) { return true; }
				else
				{
					if (50000000 - SumBorrow <= 1000000) { return true; }
					else { return false; }
				}
			}
			else
			{
				if (SumBorrow <= 50000000 - 30000000)
				{
					if (30000000 - BorrowMoneyDate[Constant.NowDate] <= 1000000) { return true; }
					else { return false; }
				}
				else
				{
					if ((50000000 - SumBorrow) - BorrowMoneyDate[Constant.NowDate] <= 1000000) { return true; }
					else { return false; }
				}
			}
		}
		else if (num == 6)
        {
			if (!BorrowMoneyDate.ContainsKey(Constant.NowDate))
			{
				if (SumBorrow <= 50000000 - 30000000) { return true; }
				else
				{
					if (50000000 - SumBorrow <= 2000000) { return true; }
					else { return false; }
				}
			}
			else
			{
				if (SumBorrow <= 50000000 - 30000000)
				{
					if (30000000 - BorrowMoneyDate[Constant.NowDate] <= 2000000) { return true; }
					else { return false; }
				}
				else
				{
					if ((50000000 - SumBorrow) - BorrowMoneyDate[Constant.NowDate] <= 2000000) { return true; }
					else { return false; }
				}
			}
		}
		else if (num == 7)
        {
			if (!BorrowMoneyDate.ContainsKey(Constant.NowDate))
			{
				if (SumBorrow <= 50000000 - 30000000) { return true; }
				else
				{
					if (50000000 - SumBorrow <= 3000000) { return true; }
					else { return false; }
				}
			}
			else
			{
				if (SumBorrow <= 50000000 - 30000000)
				{
					if (30000000 - BorrowMoneyDate[Constant.NowDate] <= 3000000) { return true; }
					else { return false; }
				}
				else
				{
					if ((50000000 - SumBorrow) - BorrowMoneyDate[Constant.NowDate] <= 3000000) { return true; }
					else { return false; }
				}
			}
		}
		else if (num == 8)
        {
			if (!BorrowMoneyDate.ContainsKey(Constant.NowDate))
			{
				if (SumBorrow <= 50000000 - 30000000) { return true; }
				else
				{
					if (50000000 - SumBorrow <= 4000000) { return true; }
					else { return false; }
				}
			}
			else
			{
				if (SumBorrow <= 50000000 - 30000000)
				{
					if (30000000 - BorrowMoneyDate[Constant.NowDate] <= 4000000) { return true; }
					else { return false; }
				}
				else
				{
					if ((50000000 - SumBorrow) - BorrowMoneyDate[Constant.NowDate] <= 4000000) { return true; }
					else { return false; }
				}
			}
		}
		else if (num == 12)
        {
			if (!IsTalkOneChanceDiscount)
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
			int money = 0;
			foreach (var key in PayMoneyDate.Keys)
            {
				money += PayMoneyDate[key];
            }

			if (money >= 1000000 && GameManager.Instance.Money >= 1000000)
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
			int money = 0;
			foreach (var key in PayMoneyDate.Keys)
			{
				money += PayMoneyDate[key];
			}

			if (money >= 2000000 && GameManager.Instance.Money >= 2000000)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		else if (num == 23)
        {
			int money = 0;
			foreach (var key in PayMoneyDate.Keys)
			{
				money += PayMoneyDate[key];
			}

			if (money >= 4000000 && GameManager.Instance.Money >= 4000000)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		else if (num == 25)
        {
			int money = 0;
			foreach (var key in PayMoneyDate.Keys)
			{
				money += PayMoneyDate[key];
			}

			if (money <= GameManager.Instance.Money)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		else if (num == 29)
		{
			if (!StartSonQuest)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		else if (num == 44)
        {
			if (StartSonQuest && !OneChanceClearSon)
            {
				return true;
            }
			else
            {
				return false;
            }
        }
		else if (num == 49)
		{
			if (LuckyStore.ClearSonQuest && StartSonQuest)
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
	/// 조건에 따른 대화 분기점
	/// </summary>
	/// <param name="tem"></param>
	/// <returns></returns>
	protected override int Bifurcation(List<TextNodeC> tem)
    {
		int index = -1;
		temInt = tem[0].NowTextNum;

		if (temInt == 2)
        {
			if (!BorrowMoneyDate.ContainsKey(Constant.NowDate))
            {
				if (SumBorrow <= 50000000 - 30000000)
                {
					SettingConversation(Findidx(2, new int[1] { 4 }), 30000000);
                }
				else
                {
					SettingConversation(Findidx(2, new int[1] { 4 }), (50000000 - SumBorrow) );
                }
            }
			else
            {
				if (SumBorrow <= 50000000 - 30000000)
                {
					SettingConversation(Findidx(2, new int[1] { 4 }), 30000000 - BorrowMoneyDate[Constant.NowDate]);
                }
				else
                {
					SettingConversation(Findidx(2, new int[1] { 4 }), (50000000 - SumBorrow) - BorrowMoneyDate[Constant.NowDate]);
                }
            }
			index = -100;
        }
		else if (temInt == 5)
        {
			if (BorrowMoneyDate.ContainsKey(Constant.NowDate))
            {
				BorrowMoneyDate[Constant.NowDate] += 1000000;
            }
			else
            {
				BorrowMoneyDate.Add(Constant.NowDate, 1000000);
            }
			SettingConversation(Findidx(5, new int[1] { 10 }));
			index = -100;
        }
		else if (temInt == 6)
        {
			if (BorrowMoneyDate.ContainsKey(Constant.NowDate))
			{
				BorrowMoneyDate[Constant.NowDate] += 2000000;
			}
			else
			{
				BorrowMoneyDate.Add(Constant.NowDate, 2000000);
			}
			SettingConversation(Findidx(6, new int[1] { 10 }));
			index = -100;
		}
		else if (temInt == 7)
        {
			if (BorrowMoneyDate.ContainsKey(Constant.NowDate))
			{
				BorrowMoneyDate[Constant.NowDate] += 3000000;
			}
			else
			{
				BorrowMoneyDate.Add(Constant.NowDate, 3000000);
			}
			SettingConversation(Findidx(7, new int[1] { 10 }));
			index = -100;
		}
		else if (temInt == 8)
        {
			if (BorrowMoneyDate.ContainsKey(Constant.NowDate))
			{
				BorrowMoneyDate[Constant.NowDate] += 4000000;
			}
			else
			{
				BorrowMoneyDate.Add(Constant.NowDate, 4000000);
			}
			SettingConversation(Findidx(8, new int[1] { 10 }));
			index = -100;
		}
		else if (temInt == 9)
        {
			SettingConversation(Findidx(9, new int[1] { -1 }));
			index = -100;
        }
		else if (temInt == 15)
        {
			IsTalkOneChanceDiscount = true;
            DiceRoll(15);
			index = -100;
        }
		else if (temInt == 19)
        {
			int n = 0;
			foreach (var key in PayMoneyDate.Keys)
            {
				n += PayMoneyDate[key];
            }

			SettingConversation(Findidx(19, new int[1] { 20 }), n);
			index = -100;
        }
		else if (temInt == 21)
        {
			int m = 1000000;
			while (true)
            {
				int k = 0;
				foreach (var key in PayMoneyDate.Keys)
                {
					k = key;
					break;
                }
				if (PayMoneyDate[k] <= m)
                {
					m -= PayMoneyDate[k];
					PayMoneyDate.Remove(k);

					if (m == 0) { break; }
                }
				else
                {
					PayMoneyDate[k] -= m;
					break;
                }
            }

			SettingConversation(Findidx(21, new int[26]));
			index = -100;
        }
		else if (temInt == 22)
        {
			int m = 2000000;
			while (true)
			{
				int k = 0;
				foreach (var key in PayMoneyDate.Keys)
				{
					k = key;
					break;
				}
				if (PayMoneyDate[k] <= m)
				{
					m -= PayMoneyDate[k];
					PayMoneyDate.Remove(k);

					if (m == 0) { break; }
				}
				else
				{
					PayMoneyDate[k] -= m;
					break;
				}
			}

			SettingConversation(Findidx(22, new int[26]));
			index = -100;
		}
		else if (temInt == 23)
        {
			int m = 4000000;
			while (true)
			{
				int k = 0;
				foreach (var key in PayMoneyDate.Keys)
				{
					k = key;
					break;
				}
				if (PayMoneyDate[k] <= m)
				{
					m -= PayMoneyDate[k];
					PayMoneyDate.Remove(k);

					if (m == 0) { break; }
				}
				else
				{
					PayMoneyDate[k] -= m;
					break;
				}
			}

			SettingConversation(Findidx(23, new int[26]));
			index = -100;
		}
		else if (temInt == 24)
        {
			SettingConversation(Findidx(24, new int[1] { 26 }));
        }
		else if (temInt == 25)
		{
			int m = 0;
			foreach (var key in PayMoneyDate.Keys)
            {
				m += PayMoneyDate[key];
            }
			while (true)
			{
				int k = 0;
				foreach (var key in PayMoneyDate.Keys)
				{
					k = key;
					break;
				}
				if (PayMoneyDate[k] <= m)
				{
					m -= PayMoneyDate[k];
					PayMoneyDate.Remove(k);

					if (m == 0) { break; }
				}
				else
				{
					PayMoneyDate[k] -= m;
					break;
				}
			}

			SettingConversation(Findidx(25, new int[1] { 26 }));
			index = -100;
		}
		else if (temInt == 35)
        {
			StartSonQuest = true;
			SettingConversation(Findidx(35, new int[1] { 36 }));
			index = -100;
        }
		else if (temInt == 37)
        {
			ClearMoney = 1000000;
			SettingConversation(Findidx(37, new int[1] { 42 }));
			index = -100;
        }
		else if (temInt == 38)
        {
			DiceRoll(4);
			index = -100;
        }
		else if (temInt == 39)
        {
			DiceRoll(8);
			index = -100;
        }
		else if (temInt == 40)
        {
			DiceRoll(16);
			index = -100;
        }
		else if (temInt == 41)
        {
			DiceRoll(30);
			index = -100;
        }
		else if (temInt == 44)
        {
			if (LuckyStore.ClearSonQuest)
            {
				index = Findidx(44, new int[1] { 45 });
            }
			else
            {
				index = Findidx(44, new int[1] { 46 });
            }
        }
		else if (temInt == 48)
        {
			GameManager.Instance.Money += ClearMoney;
			SettingConversation(Findidx(48, new int[1] { -1 }));
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
		if (temInt == 15)
        {
			if (bo)
            {
				PlusMoney = 0.5f;
				SettingConversation(Findidx(15, new int[1] { 17 }));
            }
			else
            {
				if (PlayerStat.HP < 40) { PlayerStat.HP = 1; }
				else { PlayerStat.HP -= 40; }
				SettingConversation(Findidx(15, new int[1] { 18 }));
            }
        }
		else if (temInt == 38)
        {
			if (bo)
            {
				ClearMoney = 2000000;
				SettingConversation(Findidx(38, new int[1] { 42 }));
            }
			else
            {
				ClearMoney = 1000000;
				SettingConversation(Findidx(38, new int[1] { 43 }));
            }
        }
		else if (temInt == 39)
        {
			if (bo)
			{
				ClearMoney = 4000000;
				SettingConversation(Findidx(39, new int[1] { 42 }));
			}
			else
			{
				ClearMoney = 1000000;
				SettingConversation(Findidx(39, new int[1] { 43 }));
			}
		}
		else if (temInt == 40)
        {
			if (bo)
			{
				ClearMoney = 8000000;
				SettingConversation(Findidx(40, new int[1] { 42 }));
			}
			else
			{
				ClearMoney = 1000000;
				SettingConversation(Findidx(40, new int[1] { 43 }));
			}
		}
		else if (temInt == 41)
        {
			if (bo)
			{
				ClearMoney = 16000000;
				SettingConversation(Findidx(41, new int[1] { 42 }));
			}
			else
			{
				ClearMoney = 1000000;
				SettingConversation(Findidx(41, new int[1] { 43 }));
			}
		}
    }
	/// <summary>
	/// 텍스트들을 연결해서 그래프로 만듦
	/// </summary>
	/// 점집 주인 이미지  0 : 보통 1 : 좋음 2 : 심기불편 3 : 화남
	protected override void InitTextList()
    {
		startText = new int[1] { 0 };

		nowTextNum = -1; nextTextNum = new int[7] { 52, 49, 29, 44, 2, 19, 3}; nextTextIsAble = new bool[7] { true, false, false, false, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 700 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 2; nextTextNum = new int[5] { 5,6,7,8,9 }; nextTextIsAble = new bool[5] { false, false, false, false, true };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 4 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 500 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 3; nextTextNum = new int[1] { -1 }; nextTextIsAble = new bool[1] { true  };
		methodSArr = new MethodS[1]
		{
			new MethodS(MethodEnum.ENDPANEL, new int[1] { -1 })
		};
		AddTextList();
		nowTextNum = 5; nextTextNum = new int[3] { 12, 11, 13 }; nextTextIsAble = new bool[3] { false, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 10 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 } )
		};
		AddTextList();
		nowTextNum = 6; nextTextNum = new int[3] { 12, 11, 13 }; nextTextIsAble = new bool[3] { false, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 10 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 7; nextTextNum = new int[3] { 12, 11, 13 }; nextTextIsAble = new bool[3] { false, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 10 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 8; nextTextNum = new int[3] { 12, 11, 13 }; nextTextIsAble = new bool[3] { false, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 10 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 9; nextTextNum = new int[7] { 52, 49, 29, 44, 2, 19, 3 }; nextTextIsAble = new bool[7] { true, false, false, false, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 700 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum =11; nextTextNum = new int[7] { 52, 49, 29, 44, 2, 19, 3 }; nextTextIsAble = new bool[7] { true, false, false, false, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 700 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 12; nextTextNum = new int[2] { 15, 16 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 14 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 500 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 2 })
		};
		AddTextList();
		nowTextNum = 13; nextTextNum = new int[5] { 5, 6, 7, 8, 9 }; nextTextIsAble = new bool[5] { false, false, false, false, true };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 4 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 500 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 15; nextTextNum = new int[1] { 11 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 17 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 2 }),
		};
		AddTextList();
		nowTextNum = 15; nextTextNum = new int[1] { 3 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 18 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 }),
		};
		AddTextList();
		nowTextNum = 16; nextTextNum = new int[7] { 52, 49, 29, 44, 2, 19, 3 }; nextTextIsAble = new bool[7] { true, false, false, false, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 700 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 19; nextTextNum = new int[5] { 21, 22, 23, 24, 25 }; nextTextIsAble = new bool[5] { false, false, false, true, false};
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 20 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 500 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 21; nextTextNum = new int[2] { 27, 28 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 26 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
		};
		AddTextList();
		nowTextNum = 22; nextTextNum = new int[2] { 27, 28 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 26 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
		};
		AddTextList();
		nowTextNum = 23; nextTextNum = new int[2] { 27, 28 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 26 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
		};
		AddTextList();
		nowTextNum = 24; nextTextNum = new int[7] { 52, 49, 29, 44, 2, 19, 3 }; nextTextIsAble = new bool[7] { true, false, false, false, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 700 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 25; nextTextNum = new int[2] { 27, 28 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 26 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
		};
		AddTextList();
		nowTextNum = 27; nextTextNum = new int[7] { 52, 49, 29, 44, 2, 19, 3 }; nextTextIsAble = new bool[7] { true, false, false, false, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 700 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 28; nextTextNum = new int[5] { 21, 22, 23, 24, 25 }; nextTextIsAble = new bool[5] { false, false, false, true, false };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 20 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 500 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 29; nextTextNum = new int[1] { 31 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 30 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 31; nextTextNum = new int[1] { 33 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 32 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 33; nextTextNum = new int[1] { 35 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 34 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[0])
		};
		AddTextList();
		nowTextNum = 35; nextTextNum = new int[5] { 37, 38, 39, 40, 41 }; nextTextIsAble = new bool[5] { true, true, true, true, true };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 36 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 500 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 37; nextTextNum = new int[1] { 27 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 42 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 38; nextTextNum = new int[1] { 27 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 42 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 38; nextTextNum = new int[1] { 27 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 43 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 39; nextTextNum = new int[1] { 27 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 42 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 39; nextTextNum = new int[1] { 27 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 43 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 40; nextTextNum = new int[1] { 27 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 42 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 40; nextTextNum = new int[1] { 27 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 43 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 41; nextTextNum = new int[1] { 27 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 42 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 41; nextTextNum = new int[1] { 27 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 43 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 44; nextTextNum = new int[1] { 47 }; nextTextIsAble = new bool[1] { false };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 45 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 44; nextTextNum = new int[1] { 48 }; nextTextIsAble = new bool[1] { false };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 46 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 47; nextTextNum = new int[7] { 52, 49, 29, 44, 2, 19, 3 }; nextTextIsAble = new bool[7] { true, false, false, false, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 700 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 48; nextTextNum = new int[7] { 52, 49, 29, 44, 2, 19, 3 }; nextTextIsAble = new bool[7] { true, false, false, false, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 700 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 49; nextTextNum = new int[1] { 51 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 50 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 51; nextTextNum = new int[7] { 52, 49, 29, 44, 2, 19, 3 }; nextTextIsAble = new bool[7] { true, false, false, false, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 700 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 52; nextTextNum = new int[1] { 54 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 53 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 54; nextTextNum = new int[1] { 56 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 55 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 56; nextTextNum = new int[7] { 52, 49, 29, 44, 2, 19, 3 }; nextTextIsAble = new bool[7] { true, false, false, false, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 700 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
	}
}
