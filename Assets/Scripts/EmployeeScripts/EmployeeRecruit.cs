using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ClerkNS;

public class EmployeeRecruit : MonoBehaviour
{
    [SerializeField] GameObject RecruitWin;

    [SerializeField] int limitCount = 3;

    [SerializeField] string[] Stat = new string[5];
    [SerializeField] string[] WorkDay = new string[7];

    public int[] Handy = new int[3];
    public int[] Career = new int[3];
    public int[] Creativity = new int[3];
    public int[] Agility = new int[3];
    public int[] Pay = new int[3];
    public string[] Name = new string[3];

    public List<int> preferedDateCount = new List<int>();
    public Dictionary<int, List<Day>> preferedDay = new Dictionary<int, List<Day>>();

    private bool isMorning = false;

    Tier tier = Tier.ONE;

    private void Awake()
    {
        for (int i = 0; i < limitCount; i++)
        {
            preferedDay.Add(i, new List<Day>());
        }
    }

    private void Update()
    {
        ShowApplicant();

        if (GameManager.Instance.time == 9 * 3600)
        {
            isMorning = true;
        }
        else
        {
            isMorning = false;
        }
    }

    // 고용인원 스텟 표시 및 저장
    void ShowApplicant()
    {
        string StatText = null;

        int Day = 0;

        if (isMorning == true)
        {
            preferedDateCount.Clear();

            for (int i = 0; i < limitCount; i++)
            {
                preferedDay[i].Clear();

                Name[i] = RecruitWin.transform.GetChild(i).GetComponent<EmployeeStat>().RanName[Random.Range(0, 31)];

                StatText += Name[i] + "\n";

                for (int j = 0; j < Stat.Length; j++)
                {
                    StatText += Stat[j] +
                     State(i, j, RecruitWin.transform.GetChild(i)) + "\n";
                }

                Day = Random.Range(1, limitCount);

                preferedDateCount.Add(Day);

                StatText += "선호 근무 요일 : ";

                for (int j = 0; j < preferedDateCount[i]; j++)
                {
                    Day = Random.Range(0, 7);
                    if (j >= 1)
                    {
                        while (preferedDay[i].Contains((Day)Day) == true)
                        {
                            Day = Random.Range(0, 7);
                        }
                    }

                    preferedDay[i].Add((Day)Day);

                    if (j < preferedDateCount[i] - 1)
                    {
                        StatText += WorkDay[Day] + ",";
                    }
                    else if (j == preferedDateCount[i] - 1)
                    {
                        StatText += WorkDay[Day];
                    }
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