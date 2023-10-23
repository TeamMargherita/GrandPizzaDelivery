using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeText : MonoBehaviour
{
    string hour;
    string min;
    void Update()
    {
        if((int)GameManager.Instance.time / 3600 < 10)
        {
            hour = "0" + System.Convert.ToString((int)GameManager.Instance.time / 3600);
        }
        else
        {
            hour = System.Convert.ToString((int)GameManager.Instance.time / 3600);
        }
        if ((int)(GameManager.Instance.time / 60 % 60) < 10)
        {
            min = "0" + System.Convert.ToString((int)(GameManager.Instance.time / 60 % 60));
        }
        else
        {
            min = System.Convert.ToString((int)(GameManager.Instance.time / 60 % 60));
        }
        GetComponent<Text>().text = hour + " : " + min;
    }
}
