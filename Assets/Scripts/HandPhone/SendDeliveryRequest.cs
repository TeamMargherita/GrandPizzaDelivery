using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendDeliveryRequest : MonoBehaviour
{
    public List<Request> RequestList = new List<Request>();
    public bool RandomCall()//랜덤피자주문 메서드
    {
        int i = Random.Range(0, GameManager.Instance.PizzaMenu.Count);
        RequestList.Add(new Request(GameManager.Instance.PizzaMenu[i], false));
        return true;
    }


    private void Update()
    {
        
    }
}
