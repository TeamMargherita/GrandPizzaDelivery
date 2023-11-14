using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TestMoneyText : MonoBehaviour
{
    [SerializeField] private GameObject PlusMoneyEffectPrefab;
    [SerializeField] private GameObject MinusMoneyEffectPrefab;
    [SerializeField] private GameObject EffectParent;
    public GameObject money;

    private void Awake()
    {
        if (GameManager.Instance.MoneyText == null)
        {
            GameManager.Instance.MoneyText = this;
        }
    }
    public void CreateMoneyEffect(int changeMoney)
    {
        Debug.Log("머니이펙트생성");
        int digitPosition = ((int)(Math.Log10(GameManager.Instance.Money) + 1) - (int)(Math.Log10((changeMoney >= 0) ? changeMoney : -changeMoney) + 1))  * 28;
        EffectParent.GetComponent<RectTransform>().anchoredPosition = new Vector2(digitPosition, -50);
        if(changeMoney >= 0)
        {
            GameObject moneyEffect = Instantiate(PlusMoneyEffectPrefab, transform.position, transform.rotation, EffectParent.transform);
            moneyEffect.GetComponent<Text>().text = "+" + changeMoney;
            Destroy(moneyEffect, 2f);
        }
        else
        {
            GameObject moneyEffect = Instantiate(MinusMoneyEffectPrefab, transform.position, transform.rotation, EffectParent.transform);
            moneyEffect.GetComponent<Text>().text = ""+changeMoney;
            Destroy(moneyEffect, 2f);
        }
    }

    void Update()
    {
        money.GetComponent<Text>().text = GameManager.Instance.Money + "원";
    }
}
