using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConversationNS;

// 한석호 작성
public class PoliceInspecting : Conversation
{
    public PoliceInspecting()
    {
        NpcTextStrArr = new string[24]
        {
            "잠깐 이쪽좀 볼까?",   // 0
            "거기 멈춰 주실까?",   // 1
            "이게 무슨 냄새야?",   // 2
            "무슨일인가요?",  // 3
            "무시한다.",    // 4
            "피자배달부 같은데...거기 짐좀 열어봐라.",  // 5
            "잠시 검문을 해야겠어.", // 6
            "피자 냄새가 나는거 같은데? 검문좀 해야겠어.",    // 7
            "한번 살펴보세요. 불법 음식같은 건 없어요.", // 8
            "(설득 주사위 합 7 이상) 설마 이상한 거라도 있을까봐요? 이봐요, 이 도시에 피자집은 한 곳 뿐이고, \n저희는 언제든지 주문 내역을 공개할 의사가 있어요. 그러니 불시에 이런 짓은 하지 말아주세요.",   // 9
            "(20000원을 준다.)흠...그만 가봐도 되겠습니까?",   // 10
            "이런 파인애플 피자잖아 ! 이런 불법음식을 소지하고 있다니...이건 압수야! 다음부턴 이런짓 하지마!",  // 11
            "좋아. 가봐도 좋다.",  // 12
            "확실히 그 말이 맞군. 가봐도 좋다.", // 13
            "혀가 너무 길어. 잔말말고 그 짐이나 열어봐라.",   // 14
            "좋아. 이번엔 눈감아주마. 다음부턴 조심하도록",    // 15
            "음..이 일은 참 고달프단 말이지.",  // 16
            "(20000원을 준다.) 그러지 말고. 어떻게 안되겠습니까?",    // 17
            "더러운 녀석. 너한테 더 줄 돈은 없다.",   // 18
            "도망간다.", // 19
            "시간을 낭비했군. 당장 체포하겠어 !",  // 20
            "(간다.)", // 21
            "(검문을 받는다.)",  // 22
            "(사과하고 간다.)"    // 23
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
        if (tem[0].NowTextNum == 8)
        {
            bool isIllegal = false;
            for (int i = 0; i < GameManager.Instance.PizzaInventoryData.Length; i++)
            {
                if (GameManager.Instance.PizzaInventoryData[i].Value.Ingreds.FindIndex(a => a.Equals(PizzaNS.Ingredient.PINEAPPLE)) != -1)
                {
                    isIllegal = true;
                    break;
                }
            }
            if (isIllegal)
            {
                for (int i = 0; i < GameManager.Instance.PizzaInventoryData.Length; i++)
                {
                    GameManager.Instance.PizzaInventoryData[i] = null;
                }
                // 불법음식이 있는 경우
                index = Findidx(8, new int[1] { 11 });
            }
            else
            {
                // 불법음식이 없는 경우
                index = Findidx(8, new int[1] { 12 });
            }
        }
        else if (tem[0].NowTextNum == 9)
        {
            DiceRoll(7);
            index = -100;
        }
        else if (tem[0].NowTextNum == 10)
        {
            // 처음으로 20000원을 준다.
            GameManager.Instance.Money -= 20000;

            // 랜덤한 확률로 20000원에 만족한다.
            if (Random.Range(0, 100) > 33)
            {
                index = Findidx(10, new int[1] { 15 });
            }
            else
            {
                index = Findidx(10, new int[1] { 16 });
            }
        }
        else if (tem[0].NowTextNum == 17)
        {
            // 첫번째 이후로 20000원을 준다.
            // 랜덤한 확률로 20000원에 만족한다.
            GameManager.Instance.Money -= 20000;

            if (Random.Range(0, 100) > 22)
            {
                index = Findidx(17, new int[1] { 15 });
            }
            else
            {
                index = Findidx(17, new int[1] { 16 });
            }
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
        if (temInt == 9)
        {
            if (bo)
            {
                index = Findidx(9, new int[1] { 13 });
            }
            else
            {
                index = Findidx(9, new int[1] { 14 });
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
        if (num == 10)
        {
            if (GameManager.Instance.Money >= 20000)
            {
                // 만약 20000만원이 있을 경우
                return true;
            }
            else
            {
                // 그렇지 못할 경우
                return false;
            }
        }
        else if (num == 17)
        {
            if (GameManager.Instance.Money >= 20000)
            {
                // 만약 20000만원이 있을 경우
                return true;
            }
            else
            {
                // 그렇지 못할 경우
                return false;
            }
        }
        return false;
	}
    /// <summary>
    /// 텍스트들을 연결해서 그래프로 만듦
    /// </summary>
    /// 경찰 이미지  0 : 기분좋음 1 : 기분 안좋음 2 : 화남 3 : 극대노
    protected override void InitTextList()
    {
        startText = new int[3] { 0, 1, 2 };

        nowTextNum = -1; nextTextNum = new int[2] { 3, 4 }; nextTextIsAble = new bool[2] { true, true };
        methodSArr = new MethodS[3]
        {
            new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
            new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
            new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 } )
        };
        AddTextList();
        nowTextNum = 3; nextTextNum = new int[4] { 8, 9, 10, 4}; nextTextIsAble = new bool[4] { true, true, false, true };
        methodSArr = new MethodS[4]
        {
            new MethodS(MethodEnum.SETRANDNPCTEXT, new int[3] { 5, 6, 7 } ),
            new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
            new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
            new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
        };
        AddTextList();
        nowTextNum = 4; nextTextNum = new int[1] { 19 }; nextTextIsAble = new bool[1] { true };
        methodSArr = new MethodS[4]
        {
            new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 20 } ),
            new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
            new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
            new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 2 } )
        };
        AddTextList();
        nowTextNum = 8; nextTextNum = new int[1] { 21 }; nextTextIsAble = new bool[1] { true };
        methodSArr = new MethodS[4]
        {
            new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 12 } ),
            new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
            new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
            new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 } )
        };
        AddTextList();
        nowTextNum = 8; nextTextNum = new int[1] { 23 }; nextTextIsAble = new bool[1] { true };
        methodSArr = new MethodS[4]
        {
            new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 11 } ),
            new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
            new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
            new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 } )
        };
        AddTextList();
        nowTextNum = 9; nextTextNum = new int[1] { 21 }; nextTextIsAble = new bool[1] { true };
        methodSArr = new MethodS[4]
        {
            new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 13 } ),
            new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
            new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
            new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 } )
        };
        AddTextList();
        nowTextNum = 9; nextTextNum = new int[1] { 22 }; nextTextIsAble = new bool[1] { true };
        methodSArr = new MethodS[4]
        {
            new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 14 } ),
            new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
            new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
            new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 } )
        };
        AddTextList();
        nowTextNum = 10; nextTextNum = new int[1] { 21 }; nextTextIsAble = new bool[1] { true };
        methodSArr = new MethodS[4]
        {
            new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 15 } ),
            new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
            new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
            new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 } )
        };
        AddTextList();
        nowTextNum = 10; nextTextNum = new int[2] { 17, 18 }; nextTextIsAble = new bool[2] { false, true };
        methodSArr = new MethodS[4]
        {
            new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 16 } ),
            new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
            new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
            new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 } )
        };
        AddTextList();
        nowTextNum = 17; nextTextNum = new int[1] { 21 }; nextTextIsAble = new bool[1] { true };
        methodSArr = new MethodS[4]
        {
            new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 15 } ),
            new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
            new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
            new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 } )
        };
        AddTextList();
        nowTextNum = 17; nextTextNum = new int[2] { 17, 18 }; nextTextIsAble = new bool[2] { false, true };
        methodSArr = new MethodS[4]
        {
            new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 16 } ),
            new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
            new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
            new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 } )
        };
        AddTextList();
        nowTextNum = 18; nextTextNum = new int[1] { 19 }; nextTextIsAble = new bool[2] { true, true };
        methodSArr = new MethodS[4]
        {
            new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 20 } ),
            new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
            new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
            new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 2 } )
        };
        AddTextList();
        nowTextNum = 19; nextTextNum = new int[1] { -1 }; nextTextIsAble = new bool[2] { true, true };
        methodSArr = new MethodS[2]
        {
            new MethodS(MethodEnum.SPAWNPOLICE, new int[1] { 4 } ),
            new MethodS(MethodEnum.ENDPANEL, new int[1] { -1 } )
        };
        AddTextList();
        nowTextNum = 21; nextTextNum = new int[1] { -1 }; nextTextIsAble = new bool[1] { false };
        methodSArr = new MethodS[1]
        {
            new MethodS(MethodEnum.ENDPANEL, new int[1] { -1 } )
        };
        AddTextList();
        nowTextNum = 22; nextTextNum = new int[1] { 21 }; nextTextIsAble = new bool[1] { true };
        methodSArr = new MethodS[4]
        {
            new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 12 } ),
            new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
            new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
            new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 } )
        };
        AddTextList();
        nowTextNum = 23; nextTextNum = new int[1] { -1 }; nextTextIsAble = new bool[1] { false };
        methodSArr = new MethodS[1]
        {
            new MethodS(MethodEnum.ENDPANEL, new int[1] { -1 } )
        };
        AddTextList();
    }

}
