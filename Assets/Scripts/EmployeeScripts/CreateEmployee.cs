using ClerkNS;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CreateEmployee : MonoBehaviour
{
    [SerializeField] Transform EmployeeMother;
    //[SerializeField] GameObject EmployeePrefab;
    [SerializeField] Transform EmployeeRecruitMother;

    [SerializeField] GameObject NoticeWin;

    // 오브젝트 생성하는 방식 폐기
    public void SpawnEmployee(int value)
    {
        int employeeCount = 0;

        employeeCount = Constant.ClerkList.Count;

        if(employeeCount < 29 )
        {
            GetStat(value);
        }
        else // 인원 초과 시 경고창 띄우기
        {
            NoticeMessage("고용 가능한 인원을 초과했습니다.");
        }
    }

    void NoticeMessage(string Message)
    {
        NoticeWin.SetActive(true);

        NoticeWin.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = Message;
    }

    public void GetStat(int SValue)
    {
        EmployeeRecruit employeeStat = EmployeeRecruitMother.GetComponent<EmployeeRecruit>();

        ClerkC clerk = 
            new ClerkC(employeeStat.Handy[SValue], (Tier)employeeStat.Agility[SValue], (Tier)employeeStat.Career[SValue], (Tier)employeeStat.Creativity[SValue], 
             0, employeeStat.Pay[SValue], employeeStat.Name[SValue]);

        Constant.ClerkList.Add(clerk);
    }
}