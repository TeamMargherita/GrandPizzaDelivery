using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEmployee : MonoBehaviour
{
    [SerializeField] Transform EmployeeMother;
    [SerializeField] GameObject EmployeePrefab;
    [SerializeField] Transform EmployeeRecruitMother;

    public void SpawnEmployee(int value)
    {
        int EmployeeCount = 0;
        EmployeeCount = EmployeeMother.transform.childCount;

        if(EmployeeCount < 5 )
        {
            GameObject employee;

            employee = Instantiate(EmployeePrefab);
            employee.transform.SetParent( EmployeeMother.transform);

            GetStat(employee, value);
        }
        else
        {
            Debug.Log("인원초과");
        }
    }

    public void GetStat(GameObject employee, int value)
    {
        employee.GetComponent<EmployeeStat>().handy 
            = EmployeeRecruitMother.GetChild(0).GetChild(value).GetComponent<EmployeeStat>().handy;
        employee.GetComponent<EmployeeStat>().agility
            = EmployeeRecruitMother.GetChild(0).GetChild(value).GetComponent<EmployeeStat>().agility;
        employee.GetComponent<EmployeeStat>().career
            = EmployeeRecruitMother.GetChild(0).GetChild(value).GetComponent<EmployeeStat>().career;
        employee.GetComponent<EmployeeStat>().creativity
            = EmployeeRecruitMother.GetChild(0).GetChild(value).GetComponent<EmployeeStat>().creativity;
        employee.GetComponent<EmployeeStat>().pay
            = EmployeeRecruitMother.GetChild(0).GetChild(value).GetComponent<EmployeeStat>().pay;
    }
}