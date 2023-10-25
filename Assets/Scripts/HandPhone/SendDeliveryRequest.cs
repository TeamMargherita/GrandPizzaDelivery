using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendDeliveryRequest : MonoBehaviour
{
    //주문리스트
    public List<Request> RequestList = new List<Request>();
    [SerializeField] private GameObject DarkDeliveryOKPanel;
    private float time = 0;
    
    public int SumChrisma()
    {
        int sumChrisma = 0;
        foreach(var i in GameManager.Instance.PizzaMenu)
        {
            sumChrisma += i.Charisma;
        }
        return sumChrisma;
    }
    public int percentage(int sum)
    {
        int findChrisma = 0;
        int count = 0;
        foreach (var i in GameManager.Instance.PizzaMenu)
        {
            findChrisma += i.Charisma;
            if (findChrisma >= sum)
                break;
            count++;
        }
        return count;
    }
    public void RandomCall()//랜덤피자주문 메서드
    {
        if (GameManager.Instance.PizzaMenu.Count > 0)
        {
            int sum = Random.Range(0, SumChrisma());
            RequestList.Add(new Request(GameManager.Instance.PizzaMenu[percentage(sum)], false));
        }
    }
    private void EndDelivery()
    {
        if (DarkDeliveryOKPanel != null)
        {
            if ((RequestList.Count <= 0 && GameManager.Instance.time >= 75600) || GameManager.Instance.time >= 82800)
            {
                RequestList.Clear();
                DarkDeliveryOKPanel.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
    private bool afternoonSDRON()
    {
        if (GameManager.Instance.time >= 32400 && GameManager.Instance.time <= 75600)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool DarkSDRON()
    {
        if(GameManager.Instance.time >= 0 && GameManager.Instance.time <= 14400)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void Update()
    {
        if(afternoonSDRON() || DarkSDRON())
        {
            if (RequestList.Count < 5)
                time += Time.deltaTime;
            if (time > 1)
            {
                time = 0;
                RandomCall();
            }
        }
        EndDelivery();
    }
}
