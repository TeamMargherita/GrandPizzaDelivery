using ClerkNS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateEmployee : MonoBehaviour
{
    [SerializeField] Transform EmployeeMother;
    [SerializeField] GameObject EmployeePrefab;
    [SerializeField] Transform EmployeeRecruitMother;

    [SerializeField] GameObject NoticeWin;

    public void SpawnEmployee(int value)
    {
        int employeeCount = 0;

        employeeCount = EmployeeMother.transform.childCount;

        if(employeeCount < 5 )
        {
            GameObject employee;

            employee = Instantiate(EmployeePrefab);
            employee.transform.SetParent(EmployeeMother.transform);

            EmployeeMother.GetComponent<PizzaQuality>().
                Employees.Add(employee);

            GetStat(value, employeeCount);
        }
        else // 인원 초과 시 경고창 띄우기
        {
            NoticeMessage("고용 가능한 인원을 초과합니다.");
        }
    }

    void NoticeMessage(string Message)
    {
        NoticeWin.SetActive(true);

        NoticeWin.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = Message;
    }

    public void GetStat(int SValue, int Gvalue)
    {
        EmployeeMother.GetChild(Gvalue).GetComponent<EmployeeStat>().Handy 
            = EmployeeRecruitMother.GetComponent<EmployeeRecruit>().Handy[SValue];
        EmployeeMother.GetChild(Gvalue).GetComponent<EmployeeStat>().Agility
            = EmployeeRecruitMother.GetComponent<EmployeeRecruit>().Agility[SValue];
        EmployeeMother.GetChild(Gvalue).GetComponent<EmployeeStat>().Career
            = EmployeeRecruitMother.GetComponent<EmployeeRecruit>().Career[SValue];
        EmployeeMother.GetChild(Gvalue).GetComponent<EmployeeStat>().Creativity
            = EmployeeRecruitMother.GetComponent<EmployeeRecruit>().Creativity[SValue];
        EmployeeMother.GetChild(Gvalue).GetComponent<EmployeeStat>().Pay
            = EmployeeRecruitMother.GetComponent<EmployeeRecruit>().Pay[SValue];
    }
}