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
    //배달앱 텍스트 업데이트
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
    public void OnClickAccept(int i)//피자주문수락버튼클릭
    {
        AcceptB[i].SetActive(false);
        SendDeliveryRequest.RequestList[i].Accept = true;
        SendDeliveryRequest.RequestList[i].AddressS = Map.GetRandAddressS();
        SendDeliveryRequest.RequestList[i].AddressS.IHouse.EnableHouse();
        Minimap.CreateDestination(SendDeliveryRequest.RequestList[i]);
        ma.AddRequestPizza(SendDeliveryRequest.RequestList[i]);
        MyChildRefreshB[i].SetActive(true);
    }
    
    public void OnClickCancle(int i)//피자주문취소버튼클릭
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
