using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TestMoneyText : MonoBehaviour
{
    public GameObject money;


    void Update()
    {
        money.GetComponent<Text>().text = GameManager.Instance.Money + "¿ø";
    }
}
