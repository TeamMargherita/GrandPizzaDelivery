using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConversationNS;

// 한석호 작성
public class MoneyStoreTwo : Conversation
{
    public static int SumBorrow = 0;    // 총 빌린 금액
    public static int NowDate = 1;  // 날짜
    public const int MoneyStoreCode = 1;    // 대출업체 코드

    public static int loseMoney = 0;	// 오늘 하루 빌릴수 있는 돈
    public MoneyStoreTwo()
    {
        NpcTextStrArr = new string[19]
        {
            "저렴한 이자로 대출해드립니다! \n일 0.5% 대출! 최대 3천만원까지 가능합니다!",  // 0
            "대출하러 왔습니다.",   // 1
            "돈을 갚으러 왔습니다.", // 2
            "고객님~저희는 500만원 단위로 대출이 가능합니다! \n얼마를 빌리러 왔나요?(현재 대출 가능 금액 $$$원)",    // 3
            "고객님~저희는 500만원 단위로만 갚으실 수 있어요.\n얼마를 갚으러 왔나요?(현재 남은 빚 $$$원)",    // 4
            "(500만원)",  // 5
            "(1000만원)", // 6
            "(1500만원)", // 7
            "(2000만원)", // 8
            "(첫 대화로 돌아간다)생각해보니 대출 안해도 될 것 같습니다.",   // 9
            "(500만원)",  // 10
            "(1000만원)", // 11
            "(1500만원)", // 12
            "(2000만원)", // 13
            "(첫 대화로 돌아간다.) 생각해보니 갚을 게 없네요.",    // 14
            "감사합니다 고객님~",   // 15
            "(첫대화로 돌아간다.)", // 16
            "(간다.)",    // 17
            "(전부 갚을게요)" // 18
        };

        //if (Constant.NowDate == 1 && GameManager.Instance.time >= 32400 && GameManager.Instance.time <= 32500)
        //{
        //    SumBorrow = 0;
        //    NowDate = 1;
        //    loseMoney = 0;
        //}

        if (Constant.NowDate != NowDate || Constant.NowDate == 1)
        {
            SumBorrow = 0;

            NowDate = Constant.NowDate;
            List<int> li = new List<int>();
            foreach (var key in Constant.PayMoneyDate.Keys)
            {
                //Constant.PayMoneyDate[key][MoneyStoreCode]++;
                if (Constant.PayMoneyDate[key].ContainsKey(MoneyStoreCode))
                {
                    SumBorrow += Constant.PayMoneyDate[key][MoneyStoreCode];
                } 
            }

            loseMoney = Constant.MoneyMaxBorrow[MoneyStoreCode] - SumBorrow;
            loseMoney = loseMoney <= 0 ? 0 : loseMoney;
            Debug.Log($"SumBorrow {SumBorrow } loseMoney {loseMoney}");
        }

        TextList = new List<TextNodeC>();
        InitTextList();
    }
    /// <summary>
    /// 플레이어의 상태에 따른 대화 등장 유무
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    protected override bool Condition(int num)
    {
        if (num ==5)
        {
            if (loseMoney >= 5000000)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (num == 6)
        {
            if (loseMoney >= 10000000)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (num == 7)
        {
            if (loseMoney >= 15000000)
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
            if (loseMoney >= 20000000)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (num == 10)
        {
            if (SumBorrow >= 5000000 && GameManager.Instance.Money >= 5000000 )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (num == 11)
        {
            if (SumBorrow >= 10000000 && GameManager.Instance.Money >= 10000000)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (num == 12)
        {
            if (SumBorrow >= 15000000 && GameManager.Instance.Money >= 15000000)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (num == 13)
        {
            if (SumBorrow >= 20000000 && GameManager.Instance.Money >= 20000000)
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
            if (GameManager.Instance.Money >= SumBorrow)
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

        if (temInt == 1)
        {
            SettingConversation(Findidx(1, new int[1] { 3 }), loseMoney);
            index = -100;
        }
        else if (temInt == 2)
        {
            int n = 0;
            foreach (var key in Constant.PayMoneyDate.Keys)
            {
                if (Constant.PayMoneyDate[key].ContainsKey(MoneyStoreCode))
                {
                    n += Constant.PayMoneyDate[key][MoneyStoreCode];
                }
            }
            Debug.Log($"n { n } loseMoney { loseMoney }");
            SettingConversation(Findidx(2, new int[1] { 4 }), n);
            index = -100;
        } 
        else if (temInt == 5)
        {
            if (Constant.PayMoneyDate.ContainsKey(Constant.NowDate))
            {
                if (Constant.PayMoneyDate[Constant.NowDate].ContainsKey(MoneyStoreCode))
                {
                    Constant.PayMoneyDate[Constant.NowDate][MoneyStoreCode] += 5000000;
                }
                else
                {
                    Constant.PayMoneyDate[Constant.NowDate].Add(MoneyStoreCode, 5000000);
                }
            }
            else
            {
                Constant.PayMoneyDate.Add(Constant.NowDate, new Dictionary<int, int>() { { MoneyStoreCode, 5000000 } });
            }
            Constant.Dept += 5000000;
            SumBorrow += 5000000;
            GameManager.Instance.Money += 5000000;
            loseMoney -= 5000000;

            SettingConversation(Findidx(5, new int[1] { 15 }));

            index = -100;
        }
        else if (temInt == 6)
        {
            if (Constant.PayMoneyDate.ContainsKey(Constant.NowDate))
            {
                if (Constant.PayMoneyDate[Constant.NowDate].ContainsKey(MoneyStoreCode))
                {
                    Constant.PayMoneyDate[Constant.NowDate][MoneyStoreCode] += 10000000;
                }
                else
                {
                    Constant.PayMoneyDate[Constant.NowDate].Add(MoneyStoreCode, 10000000);
                }
            }
            else
            {
                Constant.PayMoneyDate.Add(Constant.NowDate, new Dictionary<int, int>() { { MoneyStoreCode, 10000000 } });
            }
            Constant.Dept += 10000000;
            SumBorrow += 10000000;
            GameManager.Instance.Money += 10000000;
            loseMoney -= 10000000;

            SettingConversation(Findidx(6, new int[1] { 15 }));

            index = -100;
        }
        else if (temInt == 7)
        {
            if (Constant.PayMoneyDate.ContainsKey(Constant.NowDate))
            {
                if (Constant.PayMoneyDate[Constant.NowDate].ContainsKey(MoneyStoreCode))
                {
                    Constant.PayMoneyDate[Constant.NowDate][MoneyStoreCode] += 15000000;
                }
                else
                {
                    Constant.PayMoneyDate[Constant.NowDate].Add(MoneyStoreCode, 15000000);
                }
            }
            else
            {
                Constant.PayMoneyDate.Add(Constant.NowDate, new Dictionary<int, int>() { { MoneyStoreCode, 15000000 } });
            }
            Constant.Dept += 15000000;
            SumBorrow += 15000000;
            GameManager.Instance.Money += 15000000;
            loseMoney -= 15000000;

            SettingConversation(Findidx(7, new int[1] { 15 }));

            index = -100;
        }
        else if (temInt == 8)
        {
            if (Constant.PayMoneyDate.ContainsKey(Constant.NowDate))
            {
                if (Constant.PayMoneyDate[Constant.NowDate].ContainsKey(MoneyStoreCode))
                {
                    Constant.PayMoneyDate[Constant.NowDate][MoneyStoreCode] += 20000000;
                }
                else
                {
                    Constant.PayMoneyDate[Constant.NowDate].Add(MoneyStoreCode, 20000000);
                }
            }
            else
            {
                Constant.PayMoneyDate.Add(Constant.NowDate, new Dictionary<int, int>() { { MoneyStoreCode, 20000000 } });
            }
            Constant.Dept += 20000000;
            //Debug.Log("???????????????????????");
            SumBorrow += 20000000;
            GameManager.Instance.Money += 20000000;
            loseMoney -= 20000000;

            SettingConversation(Findidx(8, new int[1] { 15 }));

            index = -100;
        }
        else if (temInt == 9)
        {
            SettingConversation(Findidx(9, new int[1] { -1 }));
            index = -100;
        }
        else if (temInt == 10)
        {
            int m = 5000000;
            while (true)
            {
                int k = 0;
                foreach (var key in Constant.PayMoneyDate.Keys)
                {
                    if (Constant.PayMoneyDate[key].ContainsKey(MoneyStoreCode))
                    {
                        k = key;

                        break;
                    }
                }
                if (Constant.PayMoneyDate[k][MoneyStoreCode] <= m)
                {
                    m -= Constant.PayMoneyDate[k][MoneyStoreCode];
                    Constant.PayMoneyDate[k].Remove(MoneyStoreCode);

                    if (m == 0) { break; }
                }
                else
                {
                    Constant.PayMoneyDate[k][MoneyStoreCode] -= m;
                    break;
                }
            }
            Constant.Dept -= 5000000;
            SumBorrow -= 5000000;
            loseMoney += 5000000;

            GameManager.Instance.Money -= 5000000;
            SettingConversation(Findidx(10, new int[1] { 15 }));
            index = -100;
        }
        else if (temInt == 11)
        {
            int m = 10000000;
            while (true)
            {
                int k = 0;
                foreach (var key in Constant.PayMoneyDate.Keys)
                {
                    if (Constant.PayMoneyDate[key].ContainsKey(MoneyStoreCode))
                    {
                        k = key;

                        break;
                    }
                }
                if (Constant.PayMoneyDate[k][MoneyStoreCode] <= m)
                {
                    m -= Constant.PayMoneyDate[k][MoneyStoreCode];
                    Constant.PayMoneyDate[k].Remove(MoneyStoreCode);

                    if (m == 0) { break; }
                }
                else
                {
                    Constant.PayMoneyDate[k][MoneyStoreCode] -= m;
                    break;
                }
            }
            Constant.Dept -= 10000000;
            SumBorrow -= 10000000;
            loseMoney += 10000000;

            GameManager.Instance.Money -= 10000000;
            SettingConversation(Findidx(11, new int[1] { 15 }));
            index = -100;
        }
        else if (temInt == 12)
        {
            int m = 15000000;
            while (true)
            {
                int k = 0;
                foreach (var key in Constant.PayMoneyDate.Keys)
                {
                    if (Constant.PayMoneyDate[key].ContainsKey(MoneyStoreCode))
                    {
                        k = key;

                        break;
                    }
                }
                if (Constant.PayMoneyDate[k][MoneyStoreCode] <= m)
                {
                    m -= Constant.PayMoneyDate[k][MoneyStoreCode];
                    Constant.PayMoneyDate[k].Remove(MoneyStoreCode);

                    if (m == 0) { break; }
                }
                else
                {
                    Constant.PayMoneyDate[k][MoneyStoreCode] -= m;
                    break;
                }
            }
            Constant.Dept -= 15000000;
            SumBorrow -= 15000000;
            loseMoney += 15000000;

            GameManager.Instance.Money -= 15000000;
            SettingConversation(Findidx(12, new int[1] { 15 }));
            index = -100;
        }
        else if (temInt == 13)
        {
            int m = 20000000;
            while (true)
            {
                int k = 0;
                foreach (var key in Constant.PayMoneyDate.Keys)
                {
                    if (Constant.PayMoneyDate[key].ContainsKey(MoneyStoreCode))
                    {
                        k = key;
                        Debug.Log($"k {k}");
                        Debug.Log($"Constant.PayMoneyDate[k][MoneyStoreCode] {Constant.PayMoneyDate[k][MoneyStoreCode]}");
                        break;
                    }
                }
                if (Constant.PayMoneyDate[k][MoneyStoreCode] <= m)
                {
                    m -= Constant.PayMoneyDate[k][MoneyStoreCode];
                    //Debug.Log($"Constant.PayMoneyDate[k][MoneyStoreCode] {Constant.PayMoneyDate[k][MoneyStoreCode]}");
                    Constant.PayMoneyDate[k].Remove(MoneyStoreCode);
                    //Debug.Log("발동??????");
                    //Debug.Log(Constant.PayMoneyDate[k].ContainsKey(MoneyStoreCode));
                    if (m == 0) { break; }
                }
                else
                {
                    Constant.PayMoneyDate[k][MoneyStoreCode] -= m;
                    break;
                }
                //Debug.Log("발동????2222");
            }
            Constant.Dept -= 20000000;
            SumBorrow -= 20000000;
            loseMoney += 20000000;

            GameManager.Instance.Money -= 20000000;
            SettingConversation(Findidx(13, new int[1] { 15 }));
            index = -100;
        }
        else if (temInt == 14)
        {
            Debug.Log(Findidx(14, new int[1] { -1 }));
            SettingConversation(Findidx(14, new int[1] { -1 }));
            index = -100;
        }
        else if (temInt == 17)
        {
            SettingConversation(TextList.FindIndex(a => a.NowTextNum == 17 && System.Linq.Enumerable.SequenceEqual(a.NextTextNum, new int[1] { -1 })));
            index = -100;
        }
        else if (temInt == 18)
        {
            int m = 0;
            foreach (var key in Constant.PayMoneyDate.Keys)
            {
                if (Constant.PayMoneyDate[key].ContainsKey(MoneyStoreCode))
                {
                    m += Constant.PayMoneyDate[key][MoneyStoreCode];
                }
            }
            Constant.Dept -= m;
            GameManager.Instance.Money -= m;
            while (true)
            {
                int k = 0;
                foreach (var key in Constant.PayMoneyDate.Keys)
                {
                    if (Constant.PayMoneyDate[key].ContainsKey(MoneyStoreCode))
                    {
                        k = key;

                        break;
                    }
                }
                if (Constant.PayMoneyDate[k][MoneyStoreCode] <= m)
                {
                    m -= Constant.PayMoneyDate[k][MoneyStoreCode];
                    Constant.PayMoneyDate[k].Remove(MoneyStoreCode);

                    if (m == 0) { break; }
                }
                else
                {
                    Constant.PayMoneyDate[k][MoneyStoreCode] -= m;
                    break;
                }
            }
            SumBorrow = 0;
            loseMoney = 30000000;

            SettingConversation(Findidx(18, new int[1] { 15 }));
            index = -100;
        }
        return index;
    }
    /// <summary>
    /// 텍스트들을 연결해서 그래프로 만듦
    /// </summary>
    ///  대출업체2 이미지  0 : 보통 1 : 좋음 2 : 심기불편 3 : 화남
    protected override void InitTextList() 
    {
        startText = new int[1] { 0 };

        nowTextNum = -1; nextTextNum = new int[3] { 1, 2, 17 }; nextTextIsAble = new bool[3] { true, true, true };
        methodSArr = new MethodS[4]
        {
            new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 } ),
            new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
            new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 }),
            new MethodS(MethodEnum.SETISCONDITION, new int[0])
        };
        AddTextList();
        nowTextNum = 1; nextTextNum = new int[5] { 5,6,7,8,9 }; nextTextIsAble = new bool[5] { false, false, false, false, true };
        methodSArr = new MethodS[5]
        {
            new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 3 }),
            new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 500 } ),
            new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
            new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
            new MethodS(MethodEnum.SETISCONDITION, new int[0])
        };
        AddTextList();
        nowTextNum = 2; nextTextNum = new int[6] { 10, 11, 12, 13, 14, 18 }; nextTextIsAble = new bool[6] { false, false, false, false, true, false };
        methodSArr = new MethodS[5]
        {
            new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 4 }),
            new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 600 } ),
            new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
            new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
            new MethodS(MethodEnum.SETISCONDITION, new int[0])
        };
        AddTextList();
        nowTextNum = 5; nextTextNum = new int[2] { 16,17 }; nextTextIsAble = new bool[2] { true, true };
        methodSArr = new MethodS[4]
        {
            new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 15 }),
            new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
            new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
            new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
        };
        AddTextList();
        nowTextNum = 6; nextTextNum = new int[2] { 16, 17 }; nextTextIsAble = new bool[2] { true, true };
        methodSArr = new MethodS[4]
        {
            new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 15 }),
            new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
            new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
            new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
        };
        AddTextList();
        nowTextNum = 7; nextTextNum = new int[2] { 16, 17 }; nextTextIsAble = new bool[2] { true, true };
        methodSArr = new MethodS[4]
        {
            new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 15 }),
            new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
            new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
            new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
        };
        AddTextList();
        nowTextNum = 8; nextTextNum = new int[2] { 16, 17 }; nextTextIsAble = new bool[2] { true, true };
        methodSArr = new MethodS[4]
        {
            new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 15 }),
            new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
            new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
            new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
        };
        AddTextList();
        nowTextNum = 9; nextTextNum = new int[3] { 1, 2, 17 }; nextTextIsAble = new bool[3] { true, true, true };
        methodSArr = new MethodS[5]
        {
            new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
            new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 } ),
            new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
            new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 }),
            new MethodS(MethodEnum.SETISCONDITION, new int[0])
        };
        AddTextList();
        nowTextNum = 10; nextTextNum = new int[2] { 16, 17 }; nextTextIsAble = new bool[2] { true, true };
        methodSArr = new MethodS[4]
        {
            new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 15 }),
            new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
            new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
            new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
        };
        AddTextList();
        nowTextNum = 11; nextTextNum = new int[2] { 16, 17 }; nextTextIsAble = new bool[2] { true, true };
        methodSArr = new MethodS[4]
        {
            new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 15 }),
            new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
            new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
            new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
        };
        AddTextList();
        nowTextNum = 12; nextTextNum = new int[2] { 16, 17 }; nextTextIsAble = new bool[2] { true, true };
        methodSArr = new MethodS[4]
        {
            new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 15 }),
            new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
            new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
            new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
        };
        AddTextList();
        nowTextNum = 13; nextTextNum = new int[2] { 16, 17 }; nextTextIsAble = new bool[2] { true, true };
        methodSArr = new MethodS[4]
        {
            new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 15 }),
            new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
            new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
            new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
        };
        AddTextList();
        nowTextNum = 14; nextTextNum = new int[3] { 1, 2, 17 }; nextTextIsAble = new bool[3] { true, true, true };
        methodSArr = new MethodS[5]
        {
            new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
            new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 } ),
            new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
            new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 }),
            new MethodS(MethodEnum.SETISCONDITION, new int[0])
        };
        AddTextList();
        nowTextNum = 16; nextTextNum = new int[3] { 1, 2, 17 }; nextTextIsAble = new bool[3] { true, true, true };
        methodSArr = new MethodS[5]
        {
            new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
            new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 } ),
            new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
            new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 }),
            new MethodS(MethodEnum.SETISCONDITION, new int[0])
        };
        AddTextList();
        nowTextNum = 17; nextTextNum = new int[1] { -1 }; nextTextIsAble = new bool[1] { false };
        methodSArr = new MethodS[1]
        {
            new MethodS(MethodEnum.ENDPANEL, new int[1] { -1 })    
        };
        AddTextList();
        nowTextNum = 18; nextTextNum = new int[3] { 1, 2, 17 }; nextTextIsAble = new bool[3] { true, true, true };
        methodSArr = new MethodS[5]
        {
            new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 15 }),
            new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 } ),
            new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
            new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 }),
            new MethodS(MethodEnum.SETISCONDITION, new int[0])
        };
        AddTextList();
    }
}
