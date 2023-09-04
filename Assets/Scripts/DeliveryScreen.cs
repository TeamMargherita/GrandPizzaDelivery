using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryScreen : MonoBehaviour
{
    public List<GameManager.Pizza> RequestList;
    public List<Text> RequestTextList;

    private void Update()
    {
        if(RequestList.Count > 0)
        {
            for(int i = 0; i < 5; i++)
            {
                if(RequestTextList[i].text == null)
                {
                    RequestTextList[i].text = RequestList[0].Name;
                }
            }
        }
    }
}
