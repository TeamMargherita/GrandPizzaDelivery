using ClerkNS;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class EmployeeStressCon : EmployeeFire
{
    [SerializeField] bool isMorning = false;

    public static int nowDate = 0;

    private void Awake()
    {
        base.Awake();
        //Debug.Log("작동2");
    }

    private void Update()
    {
        if (nowDate != Constant.NowDate)
        {
            isMorning = true;

            nowDate++;
        }
        else
        {
            isMorning = false;
        }

        if (isMorning)
        {
            WorkStress();
            PayStress();
        }
    }

    string Message = null;

    void PayStress()
    {
        Message = null;

        for (int i = 0; i < Constant.ClerkList.Count; i++)
        {
            if (Constant.ClerkList[i].Pay < Constant.ClerkList[i].MinPayScale)
            {
                if (Constant.ClerkList[i].Stress < Constant.ClerkList[i].MaxStress)
                {
                    Constant.ClerkList[i].Stress += (Constant.ClerkList[i].MinPayScale - Constant.ClerkList[i].Pay) / 100;
                }
            }
            else if(Constant.ClerkList[i].Pay > Constant.ClerkList[i].MaxPayScale)
            {
                if (Constant.ClerkList[i].Stress > 0)
                {
                    Constant.ClerkList[i].Stress -= (Constant.ClerkList[i].Pay - Constant.ClerkList[i].MaxPayScale) / 100; 
                }
            }

            if (Constant.ClerkList[i].Stress < 0)
            {
                Constant.ClerkList[i].Stress = 0;
            }

            if (i > 0)
            {
                if (Constant.ClerkList[i].Stress > Constant.ClerkList[i].MaxStress)
                {
                    if (Message != null)
                    {
                        Message += " ,";
                    }

                    Message += Constant.ClerkList[i].Name;

                    for (int k = 0; k < WorkingDay.Count; k++)
                    {
                        if (WorkingDay[k].Contains(Constant.ClerkList[i]))
                        {
                            WorkingDay[k].Remove(Constant.ClerkList[i]);
                        }
                    }

                    Constant.ClerkList.RemoveAt(i);
                }
            }
        }

        if(Message != null) 
        {
            Message += "가 당신의 부당한 대우를 참지 못하고 떠났습니다.";

            NoticeMessage();
        }
    }

    void WorkStress()
    {
        for (int j = 0; j < 7; j++)
        {
            for (int i = 0; i < Constant.ClerkList.Count; i++)
            {
                if (WorkingDay.ContainsKey(j))
                {
                    if (WorkingDay[j].Contains(Constant.ClerkList[i]) == true)
                    {
                        if (i > 0)
                        {
                            if (Constant.ClerkList[i].PreferredDate.Contains((Day)j) == false)
                            {
                                Constant.ClerkList[i].Stress += 3;
                            }
                        }
                    }
                }
            }
        }
    }

    void NoticeMessage()
    {
        NoticeWin.SetActive(true);

        NoticeWin.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = Message;
    }
}