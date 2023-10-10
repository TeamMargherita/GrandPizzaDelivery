using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeText : MonoBehaviour
{

    void Update()
    {
        GetComponent<Text>().text = (int)GameManager.Instance.time / 3600 + " : " + (int)(GameManager.Instance.time / 60 % 60);
    }
}
