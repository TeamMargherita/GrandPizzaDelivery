using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPhone : MonoBehaviour
{
    public GameObject DeliveryAppButton;
    public GameObject DarkDeliveryAppButton;
    public GameObject DeliveryScreen;
    DeliveryScreen DSscript;

    public bool StartDarkApp;
    private void Start()
    {
        StartDarkApp = false;
        DSscript = DeliveryScreen.GetComponent<DeliveryScreen>();
    }
    public void DeliveryOnClick()//¹è´Þ¾Û ¹öÆ° ´­·¶À»½Ã
    {
        DeliveryAppButton.SetActive(false);
        DeliveryScreen.SetActive(true);
        DSscript.TextUpdate();
    }

    public void DarkDeliveryOnclick()//¾îµÒÀÇ ¹è´Þ¾Û ¹öÆ° ´­·¶À»½Ã
    {
        DarkDeliveryAppButton.SetActive(false);
        DeliveryScreen.SetActive(true);
        DSscript.TextUpdate();
    }
    public void HomeButtonOnClick()//È¨¹öÆ° ´­·¶À»½Ã
    {
        DeliveryAppButton.SetActive(true);
        DeliveryScreen.SetActive(false);
    }
}
