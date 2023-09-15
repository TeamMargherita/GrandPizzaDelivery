using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 한석호 작성

public class InspectingUIControl : MonoBehaviour, IInspectingUIText
{
    [SerializeField] private GameObject[] diceObjArr;
    [SerializeField] private GameObject[] playerTextObjArr;
    [SerializeField] private Sprite[] diceSprArr;

    [SerializeField] private GameObject uiControl;
    [SerializeField] private Text diceSuccessText;
    [SerializeField] private Text policeText;

    private IInspectingPanelControl iInspectingPanelControl;

    private RectTransform[] diceRectArr;
    private Image[] diceImgArr;
    private Text[] playerTextArr;
    private PlayerTexts[] playerTextsArr;
    private Coroutine diceCoroutine;
    
    private string[] inspectingPoliceTextStart = new string[23] 
    {
        "잠깐 이쪽좀 볼까?",   // 0
        "거기 멈춰 주실까?",   // 1
        "이게 무슨 냄새야?",   // 2
        "무슨일인가요?",  // 3
        "무시한다.",    // 4
        "피자배달부 같은데...거기 짐좀 열어봐라.",  // 5
        "잠시 검문을 해야겠어.", // 6
        "피자 냄새가 나는거 같은데? 검문좀 해야겠어.",    // 7
        "한번 살펴보세요. 불법 음식같은 건 없어요.", // 8
        "(설득 주사위 7 이상) 설마 이상한 거라도 있을까봐요? 이봐요, 이 도시에 피자집은 한 곳 뿐이고, \n저희는 언제든지 주문 내역을 공개할 의사가 있어요. 그러니 불시에 이런 짓은 하지 말아주세요.",   // 9
        "(20000원을 준다.)흠...그만 가봐도 되겠습니까?",   // 10
        "이런 파인애플 피자잖아 ! 이런 불법음식을 소지하고 있다니...이건 압수야. \n벌금은 그쪽 가게에 통지할테니 다음부턴 이런 짓 하지마 !",  // 11
        "좋아. 가봐도 좋다.",  // 12
        "확실히 그 말이 맞군. 가봐도 좋다.", // 13
        "혀가 너무 길어. 잔말말고 그 짐이나 열어봐라.",   // 14
        "좋아. 이번엔 눈감아주마. 다음부턴 조심하도록",    // 15
        "음..이 일은 참 고달프단 말이지.",  // 16
        "(20000원을 준다.) 그러지 말고. 어떻게 안되겠습니까?",    // 17
        "더러운 녀석. 너한테 더 줄 돈은 없다.",   // 18
        "도망간다.", // 19
        "시간을 낭비했군. 당장 체포하겠어 !",  // 20
        "(간다.)", // 21
        "(검문을 받는다.)" // 22
    };

    private bool isDiceRoll = false;

    private void Awake()
    {
        playerTextArr = new Text[playerTextObjArr.Length];
        playerTextsArr = new PlayerTexts[playerTextObjArr.Length];

        diceImgArr = new Image[diceObjArr.Length];
        diceRectArr = new RectTransform[diceObjArr.Length];

        for (int i = 0; i < playerTextObjArr.Length; i++)
        {
            playerTextArr[i] = playerTextObjArr[i].GetComponent<Text>();
            playerTextsArr[i] = playerTextObjArr[i].GetComponent<PlayerTexts>();
            playerTextsArr[i].SetIInspectingUIText(this);
        }

        for (int i = 0; i < diceObjArr.Length; i++)
		{
            diceImgArr[i] = diceObjArr[i].GetComponent<Image>();
            diceRectArr[i] = diceObjArr[i].GetComponent<RectTransform>();
		}

        iInspectingPanelControl = uiControl.GetComponent<IInspectingPanelControl>();

    }

	private void OnEnable()
    {
        InitPoliceText();
        InitPlayerText();
        InitDice();

        switch (Random.Range(0,3))
		{
            case 0:
                SetPoliceText(0);
                break;
            case 1:
                SetPoliceText(1);
                break;
            case 2:
                SetPoliceText(2); 
                break;
		}

        SetPlayerText(0, 3, 1000);  // 무슨일인가요?
        SetPlayerText(1, 4, 1001);  // 무시한다.
    }
    // 주사위 초기화
    private void InitDice()
    {
        for (int i = 0; i < diceObjArr.Length; i++)
        {
            diceImgArr[i].sprite = diceSprArr[0];
        }
        diceSuccessText.text = "";
    }

    // 플레이어 선택지창 지워줌
    private void InitPlayerText()
	{
        for (int i = 0; i < playerTextArr.Length; i++)
		{
            playerTextArr[i].gameObject.SetActive(false);
		}
	}

    private void SetPlayerText(int n1, int n2, int n3)
	{
        playerTextArr[n1].gameObject.SetActive(true);
        playerTextArr[n1].text = inspectingPoliceTextStart[n2];
        playerTextsArr[n1].TextNum = n3;
	}

    private void SetPoliceText(int n1)
	{
        policeText.text = inspectingPoliceTextStart[n1];
	}
    // 경찰 대화창 지워줌
    private void InitPoliceText()
	{
        policeText.text = "";
	}

    public void ChoiceText(int num)
	{
        if (isDiceRoll) { return; }
        InitPlayerText();
        InitDice();

        switch (num)
		{
            case 1000:  // 무슨 일인가요? ~
                int r = Random.Range(0, 3);
                if (r == 0) { SetPoliceText(5); }
                else if (r == 1) { SetPoliceText(6); }
                else if (r == 2) { SetPoliceText(7); }

                SetPlayerText(0, 8, 1002);  // 한 번 살펴보세요 ~
                SetPlayerText(1, 9, 1003);  // 설마 이상한 거라도 ~
                SetPlayerText(2, 10, 1004);  // 흠.. 그만 가봐도 ~
                SetPlayerText(3, 4, 1001);  // 무시한다.
                break;
            case 1001:  // 무시한다.
                SetPoliceText(20);

                SetPlayerText(0, 19, 1005);
                break;
            case 1002:  // 한 번 살펴보세요 ~, (검문을 받는다.)
                // 일단 파인애플 피자가 없다고 가정
                SetPoliceText(12);

                SetPlayerText(0, 21, 1006);
                break;
            case 1003:  // 설마 이상한 거라도 ~        
                // 설득 여부에 따라 경찰의 말이 달라짐
                isDiceRoll = true;
                diceCoroutine = StartCoroutine(DIceRoll(num));
                break;
            case 1004:  // 흠.. 그만 가봐도 ~
                // 돈에 만족하냐에 안하냐에 따라 경찰의 말이 달라짐.
                int r1 = Random.Range(0, 100);
                if (r1 < 5) 
                {
                    SetPoliceText(16);

                    SetPlayerText(0, 17, 1004);
                    SetPlayerText(1, 18, 1005);
                }
                else
				{
                    SetPoliceText(15);

                    SetPlayerText(0, 21, 1006);
				}
                break;
            case 1005:  // 도망간다.
                InitPoliceText();
                iInspectingPanelControl.ControlInspectUI(false, null);
                // 경찰 추적

                break;
            case 1006:  // (간다.)
                InitPoliceText();
                iInspectingPanelControl.ControlInspectUI(false, null); 
                break;
		}
	}

    IEnumerator DIceRoll(int num)
    {
        var time = new WaitForSeconds(0.05f);
        int rand = 0;
        int dice1 = 0;
        int dice2 = 0;
        isDiceRoll = true;


        while (true)
        {
            if (num == 1003)
            {
                Vector3[] originVec = new Vector3[diceRectArr.Length];

                for (int j = 0; j < diceRectArr.Length; j++)
                {
                    originVec[j] = diceRectArr[j].anchoredPosition;
                }

                for (int i = 0; i < 20; i++)
                {
                    // 주사위 모양을 보여준다.
                    dice1 = Random.Range(1, 7);
                    dice2 = Random.Range(1, 7);

                    diceImgArr[0].sprite = diceSprArr[dice1 - 1];
                    diceImgArr[1].sprite = diceSprArr[dice2 - 1];

                    for (int j = 0; j < diceRectArr.Length; j++)
					{
                        diceRectArr[j].anchoredPosition = originVec[j] + Vector3.Normalize(new Vector3(Random.Range(0, 100), 0, Random.Range(0, 100))) * 10f;
					}
                    
                    rand = dice1 + dice2;

                    yield return time;
                }

                for (int j = 0; j < diceRectArr.Length; j++)
				{
                    diceRectArr[j].anchoredPosition = originVec[j];
				}

                // 경찰 설득 성공(불심검문 안함)
                if (rand >= 7)
                {
                    diceSuccessText.text = "설득 성공 !";
                    SetPoliceText(13);

                    SetPlayerText(0, 21, 1006);
                }
                else
                {
                    diceSuccessText.text = "설득 실패..";
                    SetPoliceText(14);

                    SetPlayerText(0, 22, 1002);
                }

                isDiceRoll = false;

                break;
            }
        }

        if (diceCoroutine != null)
        {
            StopCoroutine(diceCoroutine);
        }
    }
}
