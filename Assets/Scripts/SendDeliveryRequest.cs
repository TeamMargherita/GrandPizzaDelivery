using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendDeliveryRequest : MonoBehaviour
{
    public bool IsCompleteDelivery = false;

    public GameManager.Pizza RandomCall()
    {
        int i = Random.Range(0, GameManager.Instance.PizzaMenu.Count);
        return GameManager.Instance.PizzaMenu[i];
    }
}
