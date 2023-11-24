using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryScreen : MonoBehaviour
{
    [SerializeField] private MakingPizza ma;
    [SerializeField] private GameObject[] MyChildRefreshB;
    [SerializeField] private GameObject[] MyChildFaceImage;
    public List<Text> RequestTextList = new List<Text>();
    public SendDeliveryRequest SDR;
    public List<GameObject> AcceptB;
    public List<GameObject> CancleB;
    public Map Map;
    public House House;
    public Minimap Minimap;

    public void Start()
    {
        for (int i = 0; i < SendDeliveryRequest.RequestList.Count; i++)
        {
            if (SendDeliveryRequest.RequestList[i].Accept)
            {
                SendDeliveryRequest.RequestList[i].AddressS = Map.GetSpecAddress(Constant.TemAddress[i]);
                SendDeliveryRequest.RequestList[i].AddressS.IHouse.EnableHouse();
                Minimap.CreateDestination(SendDeliveryRequest.RequestList[i], i);
            }
        }
    }
	//��޾� �ؽ�Ʈ ������Ʈ
	public void TextUpdate()
    {
        if(SendDeliveryRequest.RequestList.Count > 0)
        {
            for (int i = 0; i < 5; i++)
            {
                if (i < SendDeliveryRequest.RequestList.Count)
                {
                    RequestTextList[i].text = SendDeliveryRequest.RequestList[i].Pizza.Name;
                    if (!SendDeliveryRequest.RequestList[i].Accept)
                    {
                        AcceptB[i].SetActive(true);
                        MyChildFaceImage[i].SetActive(true);
                        MyChildRefreshB[i].SetActive(false);
                        MyChildFaceImage[i].GetComponent<RawImage>().color = Color.white;
                    }
                    else
                    {
                        MyChildRefreshB[i].SetActive(true);
                        MyChildFaceImage[i].SetActive(true);
                        MyChildFaceImage[i].GetComponent<RawImage>().color = Color.green;
                        AcceptB[i].SetActive(false);
                    }
                }
                else
                {
                    RequestTextList[i].text = "";
                    AcceptB[i].SetActive(false);
                    MyChildRefreshB[i].SetActive(false);
                    MyChildFaceImage[i].SetActive(false);
                }
            }
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                RequestTextList[i].text = "";
                AcceptB[i].SetActive(false);
                MyChildRefreshB[i].SetActive(false);
                MyChildFaceImage[i].SetActive(false);
                MyChildRefreshB[i].SetActive(false);
            }
        }
    }

    private void Update()
    {
        TextUpdate();
    }
    public void OnClickAccept(int i)//�����ֹ�������ưŬ��
    {
        AcceptB[i].SetActive(false);
        SendDeliveryRequest.RequestList[i].Accept = true;
        SendDeliveryRequest.RequestList[i].AddressS = Map.GetRandAddressS();
        Constant.TemAddress[i] = Map.GetSpecAddress();
        SendDeliveryRequest.RequestList[i].AddressS.IHouse.EnableHouse();
        Minimap.CreateDestination(SendDeliveryRequest.RequestList[i], i);
        ma.AddRequestPizza(SendDeliveryRequest.RequestList[i]);
        MyChildRefreshB[i].SetActive(true);
    }
    
    public void OnClickCancle(int i)//�����ֹ���ҹ�ưŬ��
    {
        MyChildRefreshB[i].SetActive(false);
        AcceptB[i].SetActive(false);
        RequestTextList[i].text = "";
        SendDeliveryRequest.RequestList.RemoveAt(i);
    }

    public void OnClickAddRequestPizza(int i)
    {
        ma.AddRequestPizza(SendDeliveryRequest.RequestList[i]);
    }
}
