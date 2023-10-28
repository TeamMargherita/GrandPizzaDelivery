using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClerkNS;

public class PizzaQuality : MonoBehaviour
{
    public int PizzaQualitys;

    public Transform EmployeeParent;

    int EmployeesCount = 0;

    int QualityMinValue = -8;
    int QualityMaxValue = 8;

    private void Start()
    {
        FindEmployees();
    }

    public void FindEmployees()
    {
        EmployeesCount = Constant.ClerkList.Count;

        GetEmployeeStat();
    }

    void GetEmployeeStat()
    {
        int min = 0;
        int max = 0;

        for (int i = 0; i < EmployeesCount; i++)
        {
            PizzaQualitys += Constant.ClerkList[i].Handicraft;

            min += (int)Constant.ClerkList[i].Career;
            max += (int)Constant.ClerkList[i].Creativity;
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
            result += (int)Constant.ClerkList[i].Agility;
        }

        result = result / EmployeesCount;

        return -result;
    }
}