using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryScreen : MonoBehaviour
{
    public List<Text> RequestTextList = new List<Text>();
    public SendDeliveryRequest SDR;
    public List<GameObject> Accept;
    public void TextUpdate()
    {
        if (SDR.RequestList.Count > 0)
        {
            for (int i = 0; i < SDR.RequestList.Count; i++)
            {
                if (i == 5)
                    break;
                RequestTextList[i].text = SDR.RequestList[i].Name;
            }
        }
    }
    private void Update()
    {
        TextUpdate();
    }
    public void OnClickAccept(int i)
    {
        Accept[i].SetActive(false);
    }
}
