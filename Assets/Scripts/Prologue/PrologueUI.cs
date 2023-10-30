using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 한석호 작성
public class PrologueUI : MonoBehaviour
{
    [SerializeField] private Sprite[] prologueSpr;
    [SerializeField] private Image img;
    [SerializeField] private Text text;
    [SerializeField] private GameObject nextButton;

    private string[] textList = new string[13]
    {
        "위대한! 피자! 딜...",
        "(티비를 끈다.)",
        "어째 볼 방송이 없네..",
        "띵동~!(문 앞에 벨이 울린다.)",
        "(편지가 한통 와 있다.)",
        "누구한테서 온 편지지?",
        "'내 조카에게'",
        "'조카야. 오랜만에 연락하는구나.'",
        "''갑작스럽지만 삼촌의 부탁을 들어줄 수 있니?",
        "'삼촌은 지금 나쁜 사람들에게 쫓기고 있단다. 영영 못 볼수도 있어.'",
        "'걱정되는 건 내가 여태까지 운영했던 피자가게로구나.'",
        "'가능하다면 나대신 이 피자집을 맡아주었으면 하는구나.'",
        "'참고로 빚이...'"
    };
    private int index = 0;

    void Awake()
    {
        img.sprite = prologueSpr[0];
        text.text = textList[0];
        index = 0;
        Invoke("ActiveNextButton", 1.5f);
    }

    private void ActiveNextButton()
    {
        nextButton.SetActive(true);
    }

    public void ClickNextButton()
    {
        if (prologueSpr.Length - 1 > index)
        {
            index++;
            img.sprite = prologueSpr[index];
            text.text = textList[index];
            nextButton.SetActive(false);
            Invoke("ActiveNextButton", 1.5f);
        }
        else
        {
            LoadScene.Instance.LoadPrologueToInGameScene();
        }
    }
}
