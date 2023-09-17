using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendDeliveryRequest : MonoBehaviour
{
    public List<Pizza> RequestList = new List<Pizza>();
    public void RandomCall()//랜덤피자주문 메서드
    {
        int i = Random.Range(0, GameManager.Instance.PizzaMenu.Count);
        RequestList.Add(GameManager.Instance.PizzaMenu[i]);
    }

    private float time = 0;
    private void Update()
    {
        time += Time.deltaTime;
        if(time > 5)
        {
            time = 0;
            RandomCall();
        }
    }
}
