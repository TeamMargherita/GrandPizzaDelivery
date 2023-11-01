using ClerkNS;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmployeeFire : MonoBehaviour
{
    [SerializeField] Transform FireWinParent;
    [SerializeField] Transform FireWinBG;
    [SerializeField] Transform EmployeeParent;
    [SerializeField] Transform WorkDayWinParent;

    public static Dictionary<int, List<ClerkC>> WorkingDay = new Dictionary<int, List<ClerkC>>(); // 0~6 월~일

    private void Awake()
    {
        SetEmployee();
    }

    private void Update()
    {
        UILerp(DscrollAmount);
    }

    [SerializeField] bool isApear = false;

    public void ApearCheck(bool Check)
    {
        EmploeeWinOff();

        isApear = Check;    

        ShowFireWin();
    }

    void EmploeeWinOff()
    {
        for (int i = 0; i < FireWinParent.childCount; i++)
        {
            FireWinParent.GetChild(i).gameObject.SetActive(false);

            if (i % 2 == 0)
            {
                FireWinParent.GetChild(i).GetComponent<Button>().interactable = true;
            }
        }
    }

    public void ShowFireWin()
    {
        string EmployeeStat = null;

        if (isApear == true)
        {
            FireWinBG.gameObject.SetActive(true);

            for (int i = 0; i < Constant.ClerkList.Count; i++)
            {
                FireWinParent.GetChild(i * 2).gameObject.SetActive(true);

                EmployeeStat = Constant.ClerkList[i].Name + "\n" + "손재주 : " + Constant.ClerkList[i].Handicraft.ToString() + "\n" + "일급 : " + 
                    Constant.ClerkList[i].Pay.ToString();

                FireWinParent.GetChild(i * 2).GetChild(0).
                  GetComponent<Text>().text = EmployeeStat;
            }
        }
        else
        {
            for (int i = 0; i < FireWinParent.childCount; i++)
            {
                FireWinParent.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public void ShowDetail(int value)
    {
        FireWinParent.GetChild(value + 1).gameObject.SetActive(true);

        FireWinParent.GetChild(value).GetComponent<Button>().interactable = false; 

        FindEmployeeData(value);

        pay[value / 2] = 0;
    }

    [SerializeField] string[] DayText;

    void FindEmployeeData(int value)
    {
        string EmployeeStat = null;

        EmployeeStat += Constant.ClerkList[value / 2].Name + "\n";

        for (int j = 0; j < 6; j++)
        {
            if (j == 0)
            {
                EmployeeStat += Stat(value / 2, j);

                EmployeeStat += "                     선호요일 : ";

                for (int k = 0; k < (int)Constant.ClerkList[value / 2].PreferredDateCount; k++)
                {
                    if(k != (int)Constant.ClerkList[value / 2].PreferredDateCount - 1)
                    EmployeeStat += DayText[(int)Constant.ClerkList[value / 2].PreferredDate[k]] + ", ";
                }

                if((int)Constant.ClerkList[value / 2].PreferredDateCount == 0)
                {
                    EmployeeStat += "없음";
                }

                EmployeeStat += "\n";
            }
            else
            {
                EmployeeStat += Stat(value / 2, j) + "\n";
            }
        }

        FireWinParent.GetChild(value + 1).GetChild(0).GetComponent<Text>().text = EmployeeStat;
    }

    string Stat(int Evalue, int Svalue)
    {
        string result = null;

        switch (Svalue)
        {
            case 0:
                result = "손재주 : " + Constant.ClerkList[Evalue].Handicraft.ToString();
                break;
            case 1:
                result = "순발력 : ";

                switch (Constant.ClerkList[Evalue].Agility)
                {
                    case ClerkNS.Tier.ONE:
                        result += EmployeeParent.GetComponent<EmployeeStat>().AgilityStat[0].ToString();
                        break;
                    case ClerkNS.Tier.TWO:
                        result += EmployeeParent.GetComponent<EmployeeStat>().AgilityStat[1].ToString();
                        break;
                    case ClerkNS.Tier.THREE:
                        result += EmployeeParent.GetComponent<EmployeeStat>().AgilityStat[2].ToString();
                        break;
                    case ClerkNS.Tier.FOUR:
                        result += EmployeeParent.GetComponent<EmployeeStat>().AgilityStat[3].ToString();
                        break;
                }
                break;
            case 2:
                result = "경력 : ";

                switch (Constant.ClerkList[Evalue].Career)
                {
                    case ClerkNS.Tier.ONE:
                        result += EmployeeParent.GetComponent<EmployeeStat>().CareerStat[0].ToString();
                        break;
                    case ClerkNS.Tier.TWO:
                        result += EmployeeParent.GetComponent<EmployeeStat>().CareerStat[1].ToString();
                        break;
                    case ClerkNS.Tier.THREE:
                        result += EmployeeParent.GetComponent<EmployeeStat>().CareerStat[2].ToString();
                        break;
                    case ClerkNS.Tier.FOUR:
                        result += EmployeeParent.GetComponent<EmployeeStat>().CareerStat[3].ToString();
                        break;
                }
                break;
            case 3:
                result = "창의력 : ";

                switch (Constant.ClerkList[Evalue].Creativity)
                {
                    case ClerkNS.Tier.ONE:
                        result += EmployeeParent.GetComponent<EmployeeStat>().CreativityStat[0].ToString();
                        break;
                    case ClerkNS.Tier.TWO:
                        result += EmployeeParent.GetComponent<EmployeeStat>().CreativityStat[1].ToString();
                        break;
                    case ClerkNS.Tier.THREE:
                        result += EmployeeParent.GetComponent<EmployeeStat>().CreativityStat[2].ToString();
                        break;
                    case ClerkNS.Tier.FOUR:
                        result += EmployeeParent.GetComponent<EmployeeStat>().CreativityStat[3].ToString();
                        break;
                }
                break;
            case 4:
                result = "스트레스 : " + Constant.ClerkList[Evalue].Stress.ToString();
                break;
            case 5:
                result = "일급 :     " + Constant.ClerkList[Evalue].Pay.ToString();
                break;
        }

        return result;
    }

    public void FireButtonOn(int value)
    {
        if (Constant.ClerkList.Count > 1 && value != 0)
        {
            string name = Constant.ClerkList[value].Name + "가 해고되었습니다.";

            NoticeMessage(name);

            Constant.ClerkList.RemoveAt(value);

            EmploeeWinOff();

            ShowFireWin();
        }
        else if(value == 0 && Constant.ClerkList.Count > 1)
        {
            NoticeMessage("상주인원은 해고할 수 없습니다.");
        }
        else
        {
            NoticeMessage("가게에는 1명 이상의 직원이 필요합니다.");
        }
    }

    int[] pay = new int[5];

    public void PayRateButton(int value)// â�� ���� pay�� ���� �� Ȯ�� ��ư ������ ���� ������ �ʱ�ȭ
    {
        string EmployeeStat = null;

        if (value > 0)
        {
            pay[value - 1]++;

            for (int j = 0; j < 5; j++)
            {
                EmployeeStat += Stat(value - 1, j) + "\n";
            }

            EmployeeStat += "일급 :     " + (Constant.ClerkList[value - 1].Pay + pay[value - 1]).ToString() + "\n";

            FireWinParent.GetChild((value - 1) * 2 + 1).GetChild(0).
                   GetComponent<Text>().text = EmployeeStat;
        }
        else if(value < 0)
        {
            pay[(value + 1) * -1]--;

            for (int j = 0; j < 5; j++)
            {
                EmployeeStat += Stat((value + 1) * -1, j) + "\n";
            }

            EmployeeStat += "일급 :     " + (Constant.ClerkList[(value + 1) * -1].Pay + pay[(value + 1) * -1]).ToString() + "\n";

            FireWinParent.GetChild(((value + 1) * -1) * 2 + 1).GetChild(0).
                   GetComponent<Text>().text = EmployeeStat;
        }
    }

    public void FireWinHeightCon(bool value)
    {
        RectTransform rect = FireWinParent.GetComponent<RectTransform>();

        if (value)
        {
            rect.sizeDelta = new Vector3(rect.sizeDelta.x, rect.sizeDelta.y + 150);
        }
        else
        {
            rect.sizeDelta = new Vector3(rect.sizeDelta.x, rect.sizeDelta.y - 150);
        }
    }

    public void SavePayRate(int value)
    {
        Constant.ClerkList[value].Pay += pay[value];

        pay[value] = 0;

        ShowFireWin();
    }

    [SerializeField] GameObject NoticeWin;

    void NoticeMessage(string Message)
    {
        NoticeWin.SetActive(true);

        NoticeWin.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = Message;
    }

    Vector3 scrollContentPos;
    Vector3 newPos;

    bool isLerp = false;

    public void ScollButtonOn(float scrollAmount)
    {
        isLerp = true;

        DscrollAmount = scrollAmount;
    }

    float DscrollAmount = 0;

    float LerpTime = 1f;
    float DLerpTime = 1f;

    void UILerp(float scrollAmount)
    {
        if (isLerp)
        {
            scrollContentPos = FireWinBG.GetComponent<ScrollRect>().content.position;

            newPos = scrollContentPos + new Vector3(0f, scrollAmount, 0f);

            FireWinBG.GetComponent<ScrollRect>().content.position = Vector3.Lerp(scrollContentPos, newPos, LerpTime * Time.deltaTime);

            DLerpTime -= 1f * Time.deltaTime;

            if (DLerpTime <= 0f)
            {
                DLerpTime = LerpTime;

                isLerp = false;
            }

            Debug.Log(DLerpTime);
        }
    }

    public void ShowWorkDay(int value)
    {
        WorkDayWinParent.gameObject.SetActive(true);

        SetEmployeeDay(value);
    }

    void SetEmployee()
    {
        for (int i = 0; i < 7; i++)
        {
            if (WorkingDay.ContainsKey(i) == false)
            {
                WorkingDay.Add(i, new List<ClerkC>());

                WorkingDay[i].Add(Constant.ClerkList[0]);
            }
        }
    }

    void SetEmployeeDay(int value)
    {
        Transform imageParent = WorkDayWinParent.GetChild(0).GetChild(0);
        Transform TextParent = WorkDayWinParent.GetChild(0).GetChild(0);

        string NameText = null;

        employeeValue = value;

        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < imageParent.GetChild(i).GetChild(1).childCount; j++)
            {
                imageParent.GetChild(i).GetChild(1).GetChild(j).GetComponent<Image>().color = Color.gray;
            }

            for (int j = 0; j < WorkingDay[i].Count; j++)
            {
                imageParent.GetChild(i).GetChild(1).GetChild(j).GetComponent<Image>().color = Color.green;
            }

            for (int j = 0; j < WorkingDay[i].Count; j++)
            {
                NameText += WorkingDay[i][j].Name + "\n";
            }

            TextParent.GetChild(i).GetChild(2).GetChild(0).GetComponent<Text>().text = NameText;

            NameText = "";

            if (WorkingDay[i].Contains(Constant.ClerkList[employeeValue]))
            {
                WorkDayWinParent.GetChild(0).GetChild(0).GetChild(i).GetComponent<Image>().color = Color.red;
            }
            else
            {
                WorkDayWinParent.GetChild(0).GetChild(0).GetChild(i).GetComponent<Image>().color = Color.white;
            }
        }
    } // 일하는 날짜 보여주는

    int employeeValue = 0;

    public void SetWorkingDay(int value)
    {
        if (WorkingDay[value].Contains(Constant.ClerkList[employeeValue]))
        {
            if (WorkingDay[value].Count < 5)
            {
                WorkingDay[value].Remove(Constant.ClerkList[employeeValue]);
            }
            else
            {
                NoticeMessage("한 요일에는 5명 이상의 사람들이 근무할 수 없습니다.");
            }

        }
        else
        {
            WorkingDay[value].Add(Constant.ClerkList[employeeValue]);
        }

        SetEmployeeDay(employeeValue);
    }
}