using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EmployeeFire : MonoBehaviour
{
    [SerializeField] Transform FireWinParent;
    [SerializeField] Transform FireWinBG;
    [SerializeField] Transform EmployeeParent;

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

            FireWinParent.GetChild(i).GetComponent<Button>().interactable = true;
        }
    }

    // 항상 켜져있게, 알바 스텟 버튼 안눌러도 바로 되게 바꾸기!!
    public void ShowFireWin()
    {
        string EmployeeStat = null;

        if (isApear == true)
        {
            FireWinBG.gameObject.SetActive(true);

            for (int i = 0; i < Constant.ClerkList.Count; i++)
            {
                FireWinParent.GetChild(i * 2).gameObject.SetActive(true);

                EmployeeStat = Constant.ClerkList[i].Name + "\n" + "스텟 : " + Constant.ClerkList[i].Handicraft.ToString() + "\n" + "급여 : " + Constant.ClerkList[i].Pay.ToString();

                FireWinParent.GetChild(i * 2).GetChild(0).
                  GetComponent<Text>().text = EmployeeStat;
            }
        }
        else
        {
            for (int i = 0; i < FireWinParent.childCount / 2; i++)
            {
                FireWinParent.GetChild(i).gameObject.SetActive(false);
                FireWinParent.GetChild(i + 1).gameObject.SetActive(false);
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

    void FindEmployeeData(int value)
    {
        string EmployeeStat = null;

        EmployeeStat += Constant.ClerkList[value / 2].Name + "\n";

        for (int j = 0; j < 6; j++)
        {
            EmployeeStat += Stat(value / 2, j) + "\n";
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
        if (Constant.ClerkList.Count > 1)
        {
            Constant.ClerkList.RemoveAt(value);

            EmploeeWinOff();

            ShowFireWin();

            NoticeMessage("직원을 해고했습니다.");
        }
        else
        {
            NoticeMessage("직원은 최소 한명 이상이 필요합니다.");
        }
    }

    int[] pay = new int[5];

    public void PayRateButton(int value)// 창을 열때 pay값 저장 후 확인 버튼 누르면 고정 닫으면 초기화
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

            EmployeeStat += "주급 :     " + (Constant.ClerkList[(value + 1) * -1].Pay + pay[(value + 1) * -1]).ToString() + "\n";

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
}