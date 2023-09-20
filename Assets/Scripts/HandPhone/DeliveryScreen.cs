using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryScreen : MonoBehaviour
{
    public List<Text> RequestTextList = new List<Text>();
    public SendDeliveryRequest SDR;
    public List<GameObject> AcceptB;
    public List<GameObject> CancleB;
    public Map Map;
    public House House;

    public void TextUpdate()
    {
        
        if(SDR.RequestList.Count > 0)
        {
            for (int i = 0; i < 5; i++)
            {
                if (i < SDR.RequestList.Count)
                {
                    RequestTextList[i].text = SDR.RequestList[i].Pizza.Name;
                    if (!SDR.RequestList[i].Accept)
                        AcceptB[i].SetActive(true);
                }
                else
                {
                    RequestTextList[i].text = "";
                    AcceptB[i].SetActive(false);
                }
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
        SDR.RequestList[i].Accept = true;
        SDR.RequestList[i].AddressS = Map.GetRandAddressS();
        SDR.RequestList[i].AddressS.IHouse.EnableHouse();
    }
    
    public void OnClickCancle(int i)//피자주문취소버튼클릭
    {
        AcceptB[i].SetActive(false);
        RequestTextList[i].text = "";
        SDR.RequestList.RemoveAt(i);
    }
}
