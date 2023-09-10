using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPhone : SendDeliveryRequest
{
    public GameObject DeliveryAppButton;
    public GameObject DeliveryScreen;
    DeliveryScreen DSscript;

    private void Start()
    {
        DSscript = DeliveryScreen.GetComponent<DeliveryScreen>();
    }
    public void DeliveryOnClick()
    {
        DeliveryAppButton.SetActive(false);
        DeliveryScreen.SetActive(true);
        DSscript.TextUpdate();
    }

    public void HomeButtonOnClick()
    {
        DeliveryAppButton.SetActive(true);
    }
    
}
