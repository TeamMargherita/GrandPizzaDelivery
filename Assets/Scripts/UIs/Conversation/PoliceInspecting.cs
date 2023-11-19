using ConversationNS;
using System.Collections.Generic;
using UnityEngine;

// �Ѽ�ȣ �ۼ�
public class PoliceInspecting : Conversation
{
    public PoliceInspecting()
    {
        NpcTextStrArr = new string[24]
        {
            "��� ������ ����?",   // 0
            "�ű� ���� �ֽǱ�?",   // 1
            "�̰� ���� ������?",   // 2
            "�������ΰ���?",  // 3
            "�����Ѵ�.",    // 4
            "���ڹ�޺� ������...�ű� ���� �������.",  // 5
            "��� �˹��� �ؾ߰ھ�.", // 6
            "���� ������ ���°� ������? �˹��� �ؾ߰ھ�.",    // 7
            "�ѹ� ���캸����. �ҹ� ���İ��� �� �����.", // 8
            "(���� �ֻ��� �� 7 �̻�) ���� �̻��� �Ŷ� ���������? �̺���, �� ���ÿ� �������� �� �� ���̰�, \n����� �������� �ֹ� ������ ������ �ǻ簡 �־��. �׷��� �ҽÿ� �̷� ���� ���� �����ּ���.",   // 9
            "(20000���� �ش�.)��...�׸� ������ �ǰڽ��ϱ�?",   // 10
            "�̷� ���ξ��� �����ݾ� ! �̷� �ҹ������� �����ϰ� �ִٴ�...�̰� �м���! �������� �̷��� ������!",  // 11
            "����. ������ ����.",  // 12
            "Ȯ���� �� ���� �±�. ������ ����.", // 13
            "���� �ʹ� ���. �ܸ����� �� ���̳� �������.",   // 14
            "����. �̹��� �������ָ�. �������� �����ϵ���",    // 15
            "��..�� ���� �� ������� ������.",  // 16
            "(20000���� �ش�.) �׷��� ����. ��� �ȵǰڽ��ϱ�?",    // 17
            "������ �༮. ������ �� �� ���� ����.",   // 18
            "��������.", // 19
            "�ð��� �����߱�. ���� ü���ϰھ� !",  // 20
            "(����.)", // 21
            "(�˹��� �޴´�.)",  // 22
            "(����ϰ� ����.)"    // 23
        };

        TextList = new List<TextNodeC>();
        InitTextList();
    }
    /// <summary>
    /// ���ǿ� ���� ��ȭ �б���
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
                if (!GameManager.Instance.PizzaInventoryData[i].Equals(null))
                {
                    if (GameManager.Instance.PizzaInventoryData[i].Value.Ingreds.FindIndex(a => a.Equals(PizzaNS.Ingredient.PINEAPPLE)) != -1)
                    {
                        isIllegal = true;
                        break;
                    }
                }
                else
                {
                    isIllegal = false;
                }
            }
            if (isIllegal)
            {
                for (int i = 0; i < GameManager.Instance.PizzaInventoryData.Length; i++)
                {
                    GameManager.Instance.PizzaInventoryData[i] = null;
                }
                // �ҹ������� �ִ� ���
                index = Findidx(8, new int[1] { 11 });
            }
            else
            {
                // �ҹ������� ���� ���
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
            // ó������ 20000���� �ش�.
            GameManager.Instance.Money -= 20000;

            // ������ Ȯ���� 20000���� �����Ѵ�.
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
            // ù��° ���ķ� 20000���� �ش�.
            // ������ Ȯ���� 20000���� �����Ѵ�.
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
        else if (tem[0].NowTextNum == 22)
        {
            bool isIllegal = false;
            for (int i = 0; i < GameManager.Instance.PizzaInventoryData.Length; i++)
            {
                if (!GameManager.Instance.PizzaInventoryData[i].Equals(null))
                {
                    if (GameManager.Instance.PizzaInventoryData[i].Value.Ingreds.FindIndex(a => a.Equals(PizzaNS.Ingredient.PINEAPPLE)) != -1)
                    {
                        isIllegal = true;
                        break;
                    }
                }
                else
                {
                    isIllegal = false;
                }
            }
            if (isIllegal)
            {
                for (int i = 0; i < GameManager.Instance.PizzaInventoryData.Length; i++)
                {
                    GameManager.Instance.PizzaInventoryData[i] = null;
                }
                // �ҹ������� �ִ� ���
                index = Findidx(22, new int[1] { 11 });
            }
            else
            {
                // �ҹ������� ���� ���
                index = Findidx(22, new int[1] { 12 });
            }
        }
        return index;
    }
    /// <summary>
    /// �ֻ��� ����� ���� ��ȭ �б���
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
    /// �÷��̾��� ���¿� ���� ��ȭ ���� ����
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    protected override bool Condition(int num)
    {
        if (num == 10)
        {
            if (GameManager.Instance.Money >= 20000)
            {
                // ���� 20000������ ���� ���
                return true;
            }
            else
            {
                // �׷��� ���� ���
                return false;
            }
        }
        else if (num == 17)
        {
            if (GameManager.Instance.Money >= 20000)
            {
                // ���� 20000������ ���� ���
                return true;
            }
            else
            {
                // �׷��� ���� ���
                return false;
            }
        }
        return false;
    }
    /// <summary>
    /// �ؽ�Ʈ���� �����ؼ� �׷����� ����
    /// </summary>
    /// ���� �̹���  0 : ������� 1 : ��� ������ 2 : ȭ�� 3 : �ش��
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
        nowTextNum = 3; nextTextNum = new int[4] { 8, 9, 10, 4 }; nextTextIsAble = new bool[4] { true, true, false, true };
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
            new MethodS(MethodEnum.SPAWNPOLICE, new int[1] { 6 } ),
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

        nowTextNum = 22; nextTextNum = new int[1] { 23 }; nextTextIsAble = new bool[1] { true };
        methodSArr = new MethodS[4]
        {
            new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 11 } ),
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
