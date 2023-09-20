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
            employee.transform.SetParent(EmployeeMother.transform);

            EmployeeMother.GetComponent<PizzaQuality>().
                Employees.Add(employee);

            GetStat(value, EmployeeCount);
        }
        else
        {
            Debug.Log("인원초과");
        }
    }

    public void GetStat(int SValue, int Gvalue)
    {
        EmployeeMother.GetChild(Gvalue).GetComponent<EmployeeStat>().Handy 
            = EmployeeRecruitMother.GetComponent<EmployeeRecruit>().handy[SValue];
        EmployeeMother.GetChild(Gvalue).GetComponent<EmployeeStat>().Agility
            = EmployeeRecruitMother.GetComponent<EmployeeRecruit>().agility[SValue];
        EmployeeMother.GetChild(Gvalue).GetComponent<EmployeeStat>().Career
            = EmployeeRecruitMother.GetComponent<EmployeeRecruit>().career[SValue];
        EmployeeMother.GetChild(Gvalue).GetComponent<EmployeeStat>().Creativity
            = EmployeeRecruitMother.GetComponent<EmployeeRecruit>().creativity[SValue];
        EmployeeMother.GetChild(Gvalue).GetComponent<EmployeeStat>().Pay
            = EmployeeRecruitMother.GetComponent<EmployeeRecruit>().pay[SValue];
    }
}