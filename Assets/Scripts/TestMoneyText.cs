using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TestMoneyText : MonoBehaviour
{
    [SerializeField] private GameObject MoneyEffectPrefab;
    [SerializeField] private GameObject EffectParent;
    public GameObject money;

    private void Awake()
    {
        GameManager.Instance.MoneyText = this;
    }
    public void CreateMoneyEffect(int changeMoney)
    {
        Debug.Log("머니이펙트생성");
        int digitPosition = ((int)(Math.Log10(GameManager.Instance.Money) + 1) - (int)(Math.Log10(changeMoney) + 1)) * 28;
        EffectParent.GetComponent<RectTransform>().anchoredPosition = new Vector2(digitPosition, -50);
        GameObject moneyEffect = Instantiate(MoneyEffectPrefab, transform.position, transform.rotation, EffectParent.transform);
        moneyEffect.GetComponent<Text>().text = Convert.ToString(changeMoney);
    }

    void Update()
    {
        money.GetComponent<Text>().text = GameManager.Instance.Money + "원";
    }
}
