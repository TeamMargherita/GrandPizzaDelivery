using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendDeliveryRequest : MonoBehaviour
{
    //주문리스트
    public List<Request> RequestList = new List<Request>();
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
            Debug.Log("피자메뉴에 피자있어요");
            int sum = Random.Range(0, SumChrisma());
            RequestList.Add(new Request(GameManager.Instance.PizzaMenu[percentage(sum)], false));
        }
            
    }
    private void Update()
    {
        time += Time.deltaTime;
        if (time > 5 && RequestList.Count <= 5)
        {
            time = 0;
            RandomCall();
        }
    }
}
