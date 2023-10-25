using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaQuality : MonoBehaviour
{
    public int PizzaQualitys;

    public Transform EmployeeParent;

    int EmployeesCount = 0;

    public List<GameObject> Employees = new List<GameObject>();

    int QualityMinValue = -8;
    int QualityMaxValue = 8;

    private void Start()
    {
        FindEmployees();
    }

    public void FindEmployees()
    {
        EmployeesCount = EmployeeParent.childCount;

        for (int i = 0; i < EmployeesCount; i++)
        {
            Employees.Add(EmployeeParent.GetChild(i).gameObject);
        }

        GetEmployeeStat();
    }

    void GetEmployeeStat()
    {
        int min = 0;
        int max = 0;

        for (int i = 0; i < EmployeesCount; i++)
        {
            PizzaQualitys += Employees[i].GetComponent<EmployeeStat>().Handy;

            min += (int)Employees[i].GetComponent<EmployeeStat>().Career;
            max += (int)Employees[i].GetComponent<EmployeeStat>().Creativity;
        }

        min /= EmployeesCount;
        max /= EmployeesCount;

        PizzaQualitys =
            PizzaQualitys / EmployeesCount + Random.Range(QualityMinValue + min, QualityMaxValue + 1 + max);

        if(PizzaQualitys > 100) 
        {
            PizzaQualitys = 100;
        }
        else if(PizzaQualitys < 1)
        {
            PizzaQualitys = 1;
        }
    }

    public int AgilityAverage() // 순발력은 * -1
    {
        int result = 0;

        for (int i = 0; i < EmployeesCount; i++)
        {
            result += (int)Employees[i].GetComponent<EmployeeStat>().Agility;
        }

        result = result / EmployeesCount;

        return -result;
    }
}