using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryScreen : MonoBehaviour
{
    public List<Text> RequestTextList = new List<Text>();
    public SendDeliveryRequest SDR;

    public void TextUpdate()
    {
        if (SDR.RequestList.Count > 0)
        {
            for (int i = 0; i < SDR.RequestList.Count; i++)
            {
                RequestTextList[i].text = SDR.RequestList[i].Name;
            }
        }
    }
    private void Update()
    {
        TextUpdate();
    }
}
