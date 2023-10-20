using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ConversationNS;
// 한석호 작성

public class InspectingUIControl : MonoBehaviour, IInspectingUIText, ICoroutineDice
{
    [SerializeField] private GameObject[] diceObjArr;
    [SerializeField] private GameObject[] playerTextObjArr;
    [SerializeField] private Sprite[] policeSprArr; // 0 : 기분좋음 1 : 기분 안좋음 2 : 화남 3 : 극대노
    [SerializeField] private Sprite[] playerSprArr; // 0 : 보통 1 : 설득중 2 : 개무시 3 : 쩔쩔맴
    [SerializeField] private GameObject spawnChaser;
    [SerializeField] private GameObject uiControl;
    [SerializeField] private GameObject storeManager;
    [SerializeField] private RectTransform scrollContents;
    [SerializeField] private Image npcFace;
    [SerializeField] private Image playerFace;
    [SerializeField] private Text diceSuccessText;
    [SerializeField] private Text npcText;

    private IConversationPanelControl iInspectingPanelControl;
    private ISpawnCar iSpawnCar;
    private IInitStore iInitStore;

    private RectTransform[] diceRectArr;
    private Image[] diceImgArr;
    private Text[] playerTextArr;
    private PlayerTexts[] playerTextsArr;
    private Sprite[] npcSprArr; // 상대 이미지 0 : 기분안좋음 1 : 기분좋음 2 : 화남 3 : 극대노
    private Sprite[] firstDiceSprArr;
    private Sprite[] secondDiceSprArr;
    private Coroutine diceCoroutine;    // 주사위를 굴릴 때 쓰는 코루틴
    private PoliceInspecting policeInspecting;  // 경찰의 불심검문이 담긴 대화 그래프 클래스
    private DiceStore diceStore;    // 주사위 가게 대화 그래프 클래스
    private PineAppleStore pineappleStore;  // 파인애플 가게 대화 그래프 클래스
    private IngredientStore ingredientStore;    // 재료 가게 대화 그래프 클래스
    private Conversation temCon;
    private bool isAwake = false;

    private bool isDiceRoll = false;

    private void Awake()
    {
        isAwake = true;

        policeInspecting = new PoliceInspecting();
        diceStore = new DiceStore();
        pineappleStore = new PineAppleStore();
        ingredientStore = new IngredientStore();

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

        iInspectingPanelControl = uiControl.GetComponent<IConversationPanelControl>();
        iSpawnCar = spawnChaser.GetComponent<ISpawnCar>();
        iInitStore = storeManager.GetComponent<IInitStore>();
    }
    /// <summary>
    /// 각종 이미지, 텍스트 초기화
    /// </summary>
    private void InitOnEnable()
	{
        this.gameObject.SetActive(true);
        firstDiceSprArr = Resources.LoadAll<Sprite>(Constant.DiceInfo[Constant.nowDice[0]].Path);
        secondDiceSprArr = Resources.LoadAll<Sprite>(Constant.DiceInfo[Constant.nowDice[1]].Path);
        if (!isAwake) { Awake(); }
        InitPoliceText();
        InitPlayerText();
        InitDice();
        InitFace();
    }
    /// <summary>
    /// 대화 내용을 선택함
    /// </summary>
    /// <param name="num"></param>
    public void ChoiceConversation(int num)
    {
        InitOnEnable();

        switch (num)
		{
            case 1:
                npcSprArr = Resources.LoadAll<Sprite>("UI/Police_400_500");
                InitConversation(policeInspecting);
                break;
            case 2:
                npcSprArr = Resources.LoadAll<Sprite>("UI/DiceStore_400_500");
                InitConversation(diceStore);
                SetIInitStore();
                break;
            case 3:
                npcSprArr = Resources.LoadAll<Sprite>("UI/PineappleStore_400_500");
                InitConversation(pineappleStore);
                break;
            case 4:
                npcSprArr = Resources.LoadAll<Sprite>("UI/IngredientStore_400_500");
                InitConversation(ingredientStore);
                break;
		}
    }
    /// <summary>
    /// 대화 내용선택하기 위한 대화 클래스 멤버변수들 초기화
    /// </summary>
    /// <param name="con"></param>
    private void InitConversation(Conversation con)
    {
        temCon = con;

        temCon.ScrollContents = scrollContents;
        temCon.NpcFace = npcFace;
        temCon.PlayerFace = playerFace;
        temCon.NpcSprArr = npcSprArr;
        temCon.PlayerSprArr = playerSprArr;
        temCon.NpcText = npcText;
        temCon.PlayerTextArr = playerTextArr;
        temCon.PlayerTextsArr = playerTextsArr;
        temCon.InspectingPanelControl = iInspectingPanelControl;
        temCon.SpawnCar = iSpawnCar;
        temCon.CoroutineDice = this;

        temCon.StartText();
    }
    /// <summary>
    /// 가게 정보 초기화(가게 내에서 대화할 때만)
    /// </summary>
    private void SetIInitStore()
	{
        temCon.InitStore = iInitStore;
	}
    /// <summary>
    /// 초상화 초기화
    /// </summary>
    private void InitFace()
    {
        npcFace.sprite = policeSprArr[0];
        playerFace.sprite = playerSprArr[0];
    }

    /// <summary>
    /// 주사위 1로 초기화
    /// </summary>
    private void InitDice()
    {

        diceImgArr[0].sprite = firstDiceSprArr[0];
        diceImgArr[1].sprite = secondDiceSprArr[0];

        diceSuccessText.text = "";
    }

    /// <summary>
    /// 플레이어 선택지창 지워줌
    /// </summary>
    private void InitPlayerText()
	{
        for (int i = 0; i < playerTextArr.Length; i++)
		{
            playerTextArr[i].gameObject.SetActive(false);
		}
	}
    /// <summary>
    /// 경찰차 대화창 지워줌
    /// </summary>
    private void InitPoliceText()
    {
        npcText.text = "";
    }
    /// <summary>
    /// 선택지를 골랐을 때 주사위 초기화, 텍스트 변경
    /// </summary>
    /// <param name="num"></param>
    public void ChoiceText(int num)
	{
        if (isDiceRoll) { return; }
        InitPlayerText();
        InitDice();
        Debug.Log($"{num} 전개 0");
        temCon.NextText(num);
	}
    public void StartDice(int num)
    {
        diceCoroutine = StartCoroutine(DiceRoll(num));
    }
    public IEnumerator DiceRoll(int num)
    {
        int rand = 0;
        int dice1 = 0;
        int dice2 = 0;
        isDiceRoll = true;

        while (true)
        {
            Vector3[] originVec = new Vector3[diceRectArr.Length];

            for (int j = 0; j < diceRectArr.Length; j++)
            {
                originVec[j] = diceRectArr[j].anchoredPosition;
            }

            for (int i = 0; i < 20; i++)
            {
                // 주사위 모양을 보여준다.
                dice1 = Random.Range(0, firstDiceSprArr.Length);
                dice2 = Random.Range(0, secondDiceSprArr.Length);

                diceImgArr[0].sprite = firstDiceSprArr[dice1];
                diceImgArr[1].sprite = secondDiceSprArr[dice2];

                for (int j = 0; j < diceRectArr.Length; j++)
                {
                    diceRectArr[j].anchoredPosition = originVec[j] + Vector3.Normalize(new Vector3(Random.Range(0, 100), 0, Random.Range(0, 100))) * 10f;
                }

                rand = Constant.DiceInfo[Constant.nowDice[0]].DiceArr[dice1] + Constant.DiceInfo[Constant.nowDice[1]].DiceArr[dice2];

                yield return Constant.OneTime;
                yield return Constant.OneTime;
            }

            for (int j = 0; j < diceRectArr.Length; j++)
            {
                diceRectArr[j].anchoredPosition = originVec[j];
            }

            if (rand >= num)
            {
                diceSuccessText.text = "성공 !";
                temCon.DiceResult(true);
            }
            else
            {
                diceSuccessText.text = "실패... ";
                temCon.DiceResult(false);
            }

            isDiceRoll = false;

            break;
        }

        if (diceCoroutine != null)
        {
            StopCoroutine(diceCoroutine);
        }
    }
}
