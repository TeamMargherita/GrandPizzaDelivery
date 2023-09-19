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

    private float time = 0;
    public void TextUpdate()
    {
        time += Time.deltaTime;
        if (time > 5)
        {
            time = 0;
            if (SDR.RandomCall())
            {
                for (int i = 0; i < SDR.RequestList.Count; i++)
                {
                    if (i == 5)
                        break;
                    RequestTextList[i].text = SDR.RequestList[i].Pizza.Name;
                    if (!SDR.RequestList[i].Accept)
                        AcceptB[i].SetActive(true);
                }
            }
        }
    }
    private void Update()
    {
        TextUpdate();
    }
    public void OnClickAccept(int i)
    {
        AcceptB[i].SetActive(false);
        SDR.RequestList[i].Accept = true;
    }
    
    public void OnClickCancle(int i)
    {
        AcceptB[i].SetActive(false);
        RequestTextList[i].text = "";
        SDR.RequestList.RemoveAt(i);
    }
}
