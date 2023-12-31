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

    [SerializeField] private GameObject notice;
    void Notice()
    {
        if (SendDeliveryRequest.RequestList.Count > 0)
        {
            if (!DeliveryScreen.activeSelf)
                notice.SetActive(true);
            else
                notice.SetActive(false);
        }
        else
        {
            notice.SetActive(false);
        }
    }

    private void Start()
    {
        StartDarkApp = false;
        DSscript = DeliveryScreen.GetComponent<DeliveryScreen>();
    }
    public void DeliveryOnClick()//��޾� ��ư ��������
    {
        DeliveryAppButton.SetActive(false);
        DeliveryScreen.SetActive(true);
        DSscript.TextUpdate();
    }

    public void DarkDeliveryOnclick()//����� ��޾� ��ư ��������
    {
        DarkDeliveryAppButton.SetActive(false);
        DeliveryScreen.SetActive(true);
        DSscript.TextUpdate();
    }
    public void HomeButtonOnClick()//Ȩ��ư ��������
    {
        if (GameManager.Instance.isDarkDelivery)
        {
            DarkDeliveryAppButton.SetActive(true);
            DeliveryScreen.SetActive(false);
        }
        else
        {
            DeliveryAppButton.SetActive(true);
            DeliveryScreen.SetActive(false);
        }
    }
    private void Update()
    {
        Notice();
    }
}
