using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryScreen : MonoBehaviour
{
    
    public List<Text> RequestTextList = new List<Text>();

    public void TextUpdate()
    {
        if (GameManager.Instance.RequestList.Count > 0)
        {
            for (int i = 0; i < GameManager.Instance.RequestList.Count; i++)
            {
                RequestTextList[i].text = GameManager.Instance.RequestList[i].Name;
            }
        }
    }
    
}
