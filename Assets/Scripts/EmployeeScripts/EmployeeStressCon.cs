using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;
using UnityEngine.UI;

public class EmployeeStressCon : EmployeeFire
{
    [SerializeField] bool isMorning = false;

    private static int nowDate = 0;

    private void Update()
    {
        if (nowDate != Constant.NowDate)
        {
            isMorning = true;

            nowDate = Constant.NowDate;
        }
        else
        {
            isMorning = false;
        }

        Debug.Log(isMorning);

        if (isMorning)
        {
            PayStress();
            WorkStress();
        }
    }

    void PayStress()
    {
        string Message = null;

        Debug.Log(Constant.ClerkList.Count);

        for (int i = 0; i < Constant.ClerkList.Count; i++)
        {
            Debug.Log(Constant.ClerkList[i].MinPayScale);

            if (Constant.ClerkList[i].Pay < Constant.ClerkList[i].MinPayScale)
            {
                if (Constant.ClerkList[i].Stress < Constant.ClerkList[i].MinPayScale)
                {
                    Constant.ClerkList[i].Stress += (Constant.ClerkList[i].MinPayScale - Constant.ClerkList[i].Pay) / 100;
                }
                else
                {
                    if(Message != null)
                    {
                        Message += " ,";
                    }

                    Message += Constant.ClerkList[i].Name;

                    Constant.ClerkList.RemoveAt(i); 
                }
            }
            else if(Constant.ClerkList[i].Pay > Constant.ClerkList[i].MaxPayScale)
            {
                if (Constant.ClerkList[i].Stress > 0)
                {
                    Constant.ClerkList[i].Stress -= (Constant.ClerkList[i].Pay - Constant.ClerkList[i].MaxPayScale) / 100;

                    if(Constant.ClerkList[i].Stress < 0)
                    {
                        Constant.ClerkList[i].Stress = 0;
                    }
                }
            }
        }

        if(Message != null) 
        {
            Message += "가 당신의 부당한 대우를 참지 못하고 떠났습니다.";

            NoticeMessage(Message);
        }
    }

    void WorkStress()
    {
        for (int j = 0; j < 7; j++)
        {
            for (int i = 0; i < Constant.ClerkList.Count; i++)
            {
                if (WorkingDay[j].Contains(Constant.ClerkList[i]) == false)
                {
                    Constant.ClerkList[i].Stress--;
                }
            }
        }
    }

    void NoticeMessage(string Message)
    {
        NoticeWin.SetActive(true);

        NoticeWin.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = Message;
    }
}