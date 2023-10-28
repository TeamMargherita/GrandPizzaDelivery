using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ClerkNS;

public class EmployeeRecruit : MonoBehaviour
{
    [SerializeField] GameObject RecruitWin;

    int limitCount = 3;

    [SerializeField] string[] Stat = new string[5];

    public int[] Handy = new int[3];
    public int[] Career = new int[3];
    public int[] Creativity = new int[3];
    public int[] Agility = new int[3];
    public int[] Pay = new int[3];

    public string[] Name = new string[3];

    private bool isMorning = false;

    Tier tier = Tier.ONE;

    private void Update()
    {
        ShowApplicant();

        isMorning = false;
    }

    void ShowApplicant()
    {
        string StatText = null;

        if (isMorning == true)
        {
            for (int i = 0; i < limitCount; i++)
            {
                Name[i] = RecruitWin.transform.GetChild(i).GetComponent<EmployeeStat>().RanName[Random.Range(0, 32)];

                StatText += Name[i] + "\n";

                for (int j = 0; j < Stat.Length; j++)
                {
                    StatText += Stat[j] +
                     State(i, j, RecruitWin.transform.GetChild(i)) + "\n";
                }

                RecruitWin.transform.GetChild(i).GetChild(0).
                        GetComponent<Text>().text = StatText;

                RecruitWin.transform.GetChild(i).GetChild(1).GetComponent<Button>().interactable
                    = true;

                StatText = null;
            }

            isMorning = false;
        }
    }

    string State(int index, int statValue, Transform employee)
    {
        string result = null;

        switch (statValue)
        {
            case 0:
                Handy[index] = Random.Range(20, 81);

                result = Handy[index].ToString();
                break;
            case 1:
                Agility[index] = RandomStat();

                result = employee.GetComponent<EmployeeStat>().AgilityStat[ChangeStatMark(Agility[index])];
                break;
            case 2:
                Career[index] = RandomStat();

                result = employee.GetComponent<EmployeeStat>().CareerStat[ChangeStatMark(Career[index])];
                break;
            case 3:
                Creativity[index] = RandomStat();

                result = employee.GetComponent<EmployeeStat>().CreativityStat[ChangeStatMark(Creativity[index])];
                break;
            case 4:
                Pay[index] = Handy[index] + Agility[index] + Creativity[index] + Career[index] + Random.Range(-10, 11);

                result = Pay[index].ToString();
                break;
            case 5:
                result = employee.GetComponent<EmployeeStat>().Stress.ToString();
                break;
        }

        return result;
    }

    int RandomStat()
    {
        int RanCount = Random.Range(0, 4);

        switch (RanCount)
        {
            case 0:
                tier = Tier.ONE;
                break;
            case 1:
                tier = Tier.TWO;
                break;
            case 2:
                tier = Tier.THREE;
                break;
            case 3:
                tier = Tier.FOUR;
                break;
        }

        RanCount = (int)tier;

        return RanCount;
    }

    int ChangeStatMark(int value)
    {
        int StringValue = 0;

        switch (value)
        {
            case -1:
                StringValue = 0;
                break;
            case 1:
                StringValue = 1;
                break;
            case 3:
                StringValue = 2;
                break;
            case 6:
                StringValue = 3;
                break;
        }

        return StringValue;
    }

    public void EmployeeDataReset()
    {
        isMorning = true;
    }// 나중에 시간 설정되면 날짜 바뀔때마다 설정되게 바꾸기~
}