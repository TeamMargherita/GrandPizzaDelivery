using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPhone : MonoBehaviour
{
    public GameObject DeliveryAppButton;
    public GameObject DeliveryScreen;
    DeliveryScreen DSscript;

    private void Start()
    {
        DSscript = DeliveryScreen.GetComponent<DeliveryScreen>();
    }
    public void DeliveryOnClick()//배달앱 버튼 눌렀을시
    {
        DeliveryAppButton.SetActive(false);
        DeliveryScreen.SetActive(true);
        DSscript.TextUpdate();
    }

    public void HomeButtonOnClick()//홈버튼 눌렀을시
    {
        DeliveryAppButton.SetActive(true);
        DeliveryScreen.SetActive(false);
    }
    
}
