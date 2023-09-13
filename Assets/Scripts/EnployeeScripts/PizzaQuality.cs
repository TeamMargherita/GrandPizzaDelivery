using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaQuality : MonoBehaviour
{
    public int pizzaQuality;

    [SerializeField] Transform EmployeeParent;

    int EmployeesCount = 0;

    [SerializeField]List<GameObject> Employees = new List<GameObject>();

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
            pizzaQuality += Employees[i].GetComponent<EmployeeStat>().handy;

            min += Employees[i].GetComponent<EmployeeStat>().career;
            max += Employees[i].GetComponent<EmployeeStat>().creativity;
        }

        min /= EmployeesCount;
        max /= EmployeesCount;

        pizzaQuality = 
            pizzaQuality / EmployeesCount + Random.Range(QualityMinValue + min, QualityMaxValue + 1 + max);

        if(pizzaQuality > 100) 
        {
            pizzaQuality = 100;
        }
        else if(pizzaQuality < 1)
        {
            pizzaQuality = 1;
        }
    }

    public int AgilityAverage()
    {
        int result = 0;

        for (int i = 0; i < EmployeesCount; i++)
        {
            result += Employees[i].GetComponent<EmployeeStat>().agility;
        }

        result /= EmployeesCount;

        return result;
    }
}