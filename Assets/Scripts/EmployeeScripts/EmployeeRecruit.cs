using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ClerkNS;
using System.Reflection;

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
        //if (Constant.NowDate == 1 && GameManager.Instance.time >= 32400 && GameManager.Instance.time > 32500)
        //{
        //    nowDate = 0;
        //}

        for (int i = 0; i < limitCount; i++)
        {
            preferedDay.Add(i, new List<Day>());
        }
    }

    public static int nowDate = 0;

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

        ShowApplicant();
    }

    public static ClerkC[] RecruitClerk = new ClerkC[3];

    public static bool[] IsRecruited = new bool[3] { false, false, false };

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
                IsRecruited[i] = true;

                preferedDay[i].Clear();

                Name[i] = RecruitWin.transform.GetChild(i).GetComponent<EmployeeStat>().RanName[Random.Range(0, 41)];

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

                    StatText += "선호 근무 요일 : ";
                }

                RecruitWin.transform.GetChild(i).GetChild(0).
                        GetComponent<Text>().text = StatText;

                RecruitWin.transform.GetChild(i).GetChild(1).GetComponent<Button>().interactable
                    = true;

                StatText = null;

                ClerkC clerk = new ClerkC(Handy[i], (Tier)Agility[i], (Tier)Career[i], (Tier)Creativity[i], 0, Pay[i], Name[i], preferedDay[i], preferedDateCount[i]);

                RecruitClerk[i] = clerk;    
            }

            isMorning = false;
        }
        else
        {
            for (int i = 0; i < limitCount; i++)
            {
                StatText += RecruitClerk[i].Name + "\n";

                for (int j = 0; j < Stat.Length; j++)
                {
                    StatText += Stat[j];

                    switch (j)
                    {
                        case 0:
                            StatText += RecruitClerk[i].Handicraft;
                            break;
                        case 1:
                            StatText += RecruitWin.transform.GetChild(i).GetComponent<EmployeeStat>().AgilityStat[ChangeStatMark((int)RecruitClerk[i].Agility)];
                            break;
                        case 2:
                            StatText += RecruitWin.transform.GetChild(i).GetComponent<EmployeeStat>().CareerStat[ChangeStatMark((int)RecruitClerk[i].Career)];
                            break;
                        case 3:
                            StatText += RecruitWin.transform.GetChild(i).GetComponent<EmployeeStat>().CreativityStat[ChangeStatMark((int)RecruitClerk[i].Creativity)];
                            break;
                        case 4:
                            StatText += RecruitClerk[i].Pay;
                            break;
                        case 5:
                            StatText += RecruitClerk[i].Stress;
                            break;
                    }

                    StatText += "\n";
                }

                StatText += "선호 근무 요일 : ";

                for (int j = 0; j < RecruitClerk[i].PreferredDateCount; j++)
                {
                    if (j < RecruitClerk[i].PreferredDateCount - 1)
                    {
                        StatText += WorkDay[(int)RecruitClerk[i].PreferredDate[j]] + ",";
                    }
                    else if (j == RecruitClerk[i].PreferredDateCount - 1)
                    {
                        StatText += WorkDay[(int)RecruitClerk[i].PreferredDate[j]];
                    }
                }
                    RecruitWin.transform.GetChild(i).GetChild(0).
                        GetComponent<Text>().text = StatText;

                RecruitWin.transform.GetChild(i).GetChild(1).GetComponent<Button>().interactable
                    = IsRecruited[i];

                StatText = null;
            }
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
                Pay[index] = (Handy[index] + Agility[index] + Creativity[index] + Career[index]) * 100 + Random.Range(-1000, 1001);

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