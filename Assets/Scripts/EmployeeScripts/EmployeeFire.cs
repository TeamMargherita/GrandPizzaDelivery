using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class EmployeeFire : MonoBehaviour
{
    [SerializeField] Transform FireWinParent;
    [SerializeField] Transform EmployeeParent;

    private void Start()
    {
        WinOff();
    }

    void WinOff()
    {
        FireWinParent.gameObject.SetActive(false);

        for (int i = 0; i < FireWinParent.childCount; i++)
        {
            FireWinParent.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void ShowFireWin()
    {
        bool value = true;

        if (FireWinParent.gameObject.activeInHierarchy)
        {
            value = false;
        }
        else
        {
            value = true;
        }

        if (value == true)
        {
            FireWinParent.gameObject.SetActive(true);

            FindEmployeeData();
        }
        else
        {
            FireWinParent.gameObject.SetActive(false);

            for (int i = 0; i < FireWinParent.childCount; i++)
            {
                FireWinParent.GetChild(i).gameObject.SetActive(false);

                FireWinParent.GetChild(i).GetChild(1).
                   GetComponent<Button>().interactable = true;

                FireWinParent.GetChild(i).GetChild(1).GetChild(0).
                    GetComponent<Text>().text = "해고하기";
            }
        }
    }

    void FindEmployeeData()
    {
        string EmployeeStat = null;

        for (int i = 0; i < EmployeeParent.childCount; i++)
        {
            FireWinParent.GetChild(i).gameObject.SetActive(true);

            for (int j = 0; j < 6; j++)
            {
                EmployeeStat += Stat(i, j) + "\n";
            }

            FireWinParent.GetChild(i).GetChild(0).
                   GetComponent<Text>().text = EmployeeStat;
        }
    }

    string Stat(int Evalue, int Svalue)
    {
        string result = null;

        switch (Svalue)
        {
            case 0:
                result = "손재주 : " + EmployeeParent.GetChild(Evalue).
                    GetComponent<EmployeeStat>().Handy.ToString();
                break;
            case 1:
                switch (EmployeeParent.GetChild(Evalue).GetComponent<EmployeeStat>().Agility)
                {
                    case -1:
                        result = "순발력 : " + EmployeeParent.GetChild(Evalue).
                  GetComponent<EmployeeStat>().AgilityStat[0].ToString();
                        break;
                    case 1:
                        result = "순발력 : " + EmployeeParent.GetChild(Evalue).
                  GetComponent<EmployeeStat>().AgilityStat[1].ToString();
                        break;
                    case 3:
                        result = "순발력 : " + EmployeeParent.GetChild(Evalue).
                  GetComponent<EmployeeStat>().AgilityStat[2].ToString();
                        break;
                    case 6:
                        result = "순발력 : " + EmployeeParent.GetChild(Evalue).
                  GetComponent<EmployeeStat>().AgilityStat[3].ToString();
                        break;
                }
                break;
            case 2:
                switch (EmployeeParent.GetChild(Evalue).GetComponent<EmployeeStat>().Career)
                {
                    case -1:
                        result = "경력 : " + EmployeeParent.GetChild(Evalue).
                  GetComponent<EmployeeStat>().CareerStat[0].ToString();
                        break;
                    case 1:
                        result = "경력 : " + EmployeeParent.GetChild(Evalue).
                  GetComponent<EmployeeStat>().CareerStat[1].ToString();
                        break;
                    case 3:
                        result = "경력 : " + EmployeeParent.GetChild(Evalue).
                  GetComponent<EmployeeStat>().CareerStat[2].ToString();
                        break;
                    case 6:
                        result = "경력 : " + EmployeeParent.GetChild(Evalue).
                 GetComponent<EmployeeStat>().CareerStat[3].ToString();
                        break;
                }
                break;
            case 3:
                switch (EmployeeParent.GetChild(Evalue).GetComponent<EmployeeStat>().Creativity)
                {
                    case -1:
                        result = "창의력 : " + EmployeeParent.GetChild(Evalue).
                  GetComponent<EmployeeStat>().CreativityStat[0].ToString();
                        break;
                    case 1:
                        result = "창의력 : " + EmployeeParent.GetChild(Evalue).
                  GetComponent<EmployeeStat>().CreativityStat[1].ToString();
                        break;
                    case 3:
                        result = "창의력 : " + EmployeeParent.GetChild(Evalue).
                  GetComponent<EmployeeStat>().CreativityStat[2].ToString();
                        break;
                    case 6:
                        result = "창의력 : " + EmployeeParent.GetChild(Evalue).
                 GetComponent<EmployeeStat>().CreativityStat[3].ToString();
                        break;
                }
                break;
            case 4:
                result = "주급 : " + EmployeeParent.GetChild(Evalue).
                   GetComponent<EmployeeStat>().Pay.ToString();
                break;
            case 5:
                result = "스트레스 : " + EmployeeParent.GetChild(Evalue).
                   GetComponent<EmployeeStat>().Stress.ToString();
                break;
        }

        return result;
    }

    public void FireButtonOn(int value)
    {
        if (EmployeeParent.childCount > 1)
        {
            EmployeeParent.GetComponent<PizzaQuality>().Employees.Remove
                (EmployeeParent.GetChild(value).gameObject);

            Destroy(EmployeeParent.GetChild(value).gameObject);

            FireWinParent.GetChild(value).GetChild(5).
                GetComponent<Button>().interactable = false;

            FireWinParent.GetChild(value).GetChild(5).GetChild(0).
                GetComponent<Text>().text = "해고완료";
        }
        else
        {
            Debug.Log("사장님 나빠요");
        }
    }
}