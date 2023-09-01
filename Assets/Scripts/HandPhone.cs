using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPhone : SendDeliveryRequest
{
    public GameObject DeliveryAppButton;
    public void DeliveryOnClick()
    {
        DeliveryAppButton.SetActive(false);
    }

    public void HomeButtonOnClick()
    {

    }
}
