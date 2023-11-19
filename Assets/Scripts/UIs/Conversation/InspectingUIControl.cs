using System.Collections;
using UnityEngine;
using UnityEngine.UI;
// �Ѽ�ȣ �ۼ�

public class InspectingUIControl : MonoBehaviour, IInspectingUIText, ICoroutineDice
{
    [SerializeField] private GameObject[] diceObjArr;
    [SerializeField] private GameObject[] playerTextObjArr;
    [SerializeField] private Sprite[] policeSprArr; // 0 : ������� 1 : ��� ������ 2 : ȭ�� 3 : �ش��
    [SerializeField] private Sprite[] playerSprArr; // 0 : ���� 1 : ������ 2 : ������ 3 : ¿¿��
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
    private Sprite[] npcSprArr; // ��� �̹��� 0 : ��о����� 1 : ������� 2 : ȭ�� 3 : �ش��
    private Sprite[] firstDiceSprArr;
    private Sprite[] secondDiceSprArr;
    private Coroutine diceCoroutine;    // �ֻ����� ���� �� ���� �ڷ�ƾ
    private PoliceInspecting policeInspecting;  // ������ �ҽɰ˹��� ��� ��ȭ �׷��� Ŭ����
    private DiceStore diceStore;    // �ֻ��� ���� ��ȭ �׷��� Ŭ����
    private PineAppleStore pineappleStore;  // ���ξ��� ���� ��ȭ �׷��� Ŭ����
    private IngredientStore ingredientStore;    // ��� ���� ��ȭ �׷��� Ŭ����
    private PineAppleStoreTwo pineappleStoreTwo;    // ���ξ��� ����2 ��ȭ �׷��� Ŭ����
    private GunStore gunStore;  // �� ���� ��ȭ �׷��� Ŭ����
    private Hospital hospital;  // ���� ��ȭ �׷��� Ŭ����
    private IngredientStoreTwo ingredientStoreTwo;  // ��� ����2 ��ȭ �׷��� Ŭ����
    private LuckyStore luckyStore;  // ���� ��ȭ �׷��� Ŭ����
    private MoneyStore moneyStore;  // �����ü �׷��� Ŭ����
    private MoneyStoreTwo moneyStoreTwo;    // �����ü 2 �׷��� Ŭ����
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
        pineappleStoreTwo = new PineAppleStoreTwo();
        gunStore = new GunStore();
        hospital = new Hospital();
        ingredientStoreTwo = new IngredientStoreTwo();
        luckyStore = new LuckyStore();
        moneyStore = new MoneyStore();
        moneyStoreTwo = new MoneyStoreTwo();

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
    /// ���� �̹���, �ؽ�Ʈ �ʱ�ȭ
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
    /// ��ȭ ������ ������
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
            case 5:
                npcSprArr = Resources.LoadAll<Sprite>("UI/PineappleStoreTwo_400_500");
                InitConversation(pineappleStoreTwo);
                break;
            case 6:
                npcSprArr = Resources.LoadAll<Sprite>("UI/GunStore_400_500");
                InitConversation(gunStore);
                SetIInitStore();
                break;
            case 7:
                npcSprArr = Resources.LoadAll<Sprite>("UI/Hospital_400_500");
                InitConversation(hospital);
                break;
            case 8:
                npcSprArr = Resources.LoadAll<Sprite>("UI/IngredientStoreTwo_400_500");
                InitConversation(ingredientStoreTwo);
                break;
            case 9:
                npcSprArr = Resources.LoadAll<Sprite>("UI/LuckyStore_400_500");
                InitConversation(luckyStore);
                break;
            case 10:
                npcSprArr = Resources.LoadAll<Sprite>("UI/MoneyStore_400_500");
                InitConversation(moneyStore);
                break;
            case 11:
                npcSprArr = Resources.LoadAll<Sprite>("UI?MoneyStoreTwo_400_500");
                InitConversation(moneyStoreTwo);
                break;
        }
    }
    /// <summary>
    /// ��ȭ ���뼱���ϱ� ���� ��ȭ Ŭ���� ��������� �ʱ�ȭ
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
    /// ���� ���� �ʱ�ȭ(���� ������ ��ȭ�� ����)
    /// </summary>
    private void SetIInitStore()
    {
        temCon.InitStore = iInitStore;
    }
    /// <summary>
    /// �ʻ�ȭ �ʱ�ȭ
    /// </summary>
    private void InitFace()
    {
        npcFace.sprite = policeSprArr[0];
        playerFace.sprite = playerSprArr[0];
    }

    /// <summary>
    /// �ֻ��� 1�� �ʱ�ȭ
    /// </summary>
    private void InitDice()
    {

        diceImgArr[0].sprite = firstDiceSprArr[0];
        diceImgArr[1].sprite = secondDiceSprArr[0];

        diceSuccessText.text = $"(�ֻ��� ���ʽ� {Constant.DiceBonus})";
    }

    /// <summary>
    /// �÷��̾� ������â ������
    /// </summary>
    private void InitPlayerText()
    {
        for (int i = 0; i < playerTextArr.Length; i++)
        {
            playerTextArr[i].gameObject.transform.parent.gameObject.SetActive(false);
            playerTextArr[i].gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// ������ ��ȭâ ������
    /// </summary>
    private void InitPoliceText()
    {
        npcText.text = "";
    }
    /// <summary>
    /// �������� ����� �� �ֻ��� �ʱ�ȭ, �ؽ�Ʈ ����
    /// </summary>
    /// <param name="num"></param>
    public void ChoiceText(int num)
    {
        if (isDiceRoll) { return; }
        InitPlayerText();
        InitDice();
        Debug.Log($"{num} ���� 0");
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
                // �ֻ��� ����� �����ش�.
                dice1 = Random.Range(0, firstDiceSprArr.Length);
                dice2 = Random.Range(0, secondDiceSprArr.Length);

                diceImgArr[0].sprite = firstDiceSprArr[dice1];
                diceImgArr[1].sprite = secondDiceSprArr[dice2];

                for (int j = 0; j < diceRectArr.Length; j++)
                {
                    diceRectArr[j].anchoredPosition = originVec[j] + Vector3.Normalize(new Vector3(Random.Range(0, 100), 0, Random.Range(0, 100))) * 10f;
                }

                rand = Constant.DiceInfo[Constant.nowDice[0]].DiceArr[dice1] + Constant.DiceInfo[Constant.nowDice[1]].DiceArr[dice2] + Constant.DiceBonus;

                yield return Constant.OneTime;
                yield return Constant.OneTime;
            }

            for (int j = 0; j < diceRectArr.Length; j++)
            {
                diceRectArr[j].anchoredPosition = originVec[j];
            }

            if (num >= 0)
            {
                if (num < 10000)
                {
                    if (rand >= num)
                    {
                        diceSuccessText.text = $"���� ! (�ֻ��� ���ʽ� {Constant.DiceBonus})";
                        temCon.DiceResult(true);
                    }
                    else
                    {
                        diceSuccessText.text = $"����... (�ֻ��� ���ʽ� {Constant.DiceBonus})";
                        temCon.DiceResult(false);
                    }
                }
                else
                {
                    if (num / 10000 == 1)
                    {
                        if (rand % 2 == 1 && rand >= num - 10000)
                        {
                            diceSuccessText.text = $"���� ! (�ֻ��� ���ʽ� {Constant.DiceBonus})";
                            temCon.DiceResult(true);
                        }
                        else
                        {
                            diceSuccessText.text = $"����... (�ֻ��� ���ʽ� {Constant.DiceBonus})";
                            temCon.DiceResult(false);
                        }
                    }
                    else if (num / 10000 == 2)
                    {
                        if (rand % 2 == 0 && rand >= num - 20000)
                        {
                            diceSuccessText.text = $"���� ! (�ֻ��� ���ʽ� {Constant.DiceBonus})";
                            temCon.DiceResult(true);
                        }
                        else
                        {
                            diceSuccessText.text = $"����... (�ֻ��� ���ʽ� {Constant.DiceBonus})";
                            temCon.DiceResult(false);
                        }
                    }
                }
            }
            else
            {
                if (num > -10000)
                {
                    if (rand < num * -1)
                    {
                        diceSuccessText.text = $"���� ! (�ֻ��� ���ʽ� {Constant.DiceBonus})";
                        temCon.DiceResult(true);
                    }
                    else
                    {
                        diceSuccessText.text = $"����... (�ֻ��� ���ʽ� {Constant.DiceBonus})";
                        temCon.DiceResult(false);
                    }
                }
                else
                {
                    if ((num * -1) / 10000 == 1)
                    {
                        if (rand % 2 == 1 && rand <= (num * -1) - 10000)
                        {
                            diceSuccessText.text = $"���� ! (�ֻ��� ���ʽ� {Constant.DiceBonus})";
                            temCon.DiceResult(true);
                        }
                        else
                        {
                            diceSuccessText.text = $"����... (�ֻ��� ���ʽ� {Constant.DiceBonus})";
                            temCon.DiceResult(false);
                        }
                    }
                    else if ((num * -1) / 20000 == 2)
                    {
                        if (rand % 2 == 0 && rand <= (num * -1) - 20000)
                        {
                            diceSuccessText.text = $"���� ! (�ֻ��� ���ʽ� {Constant.DiceBonus})";
                            temCon.DiceResult(true);
                        }
                        else
                        {
                            diceSuccessText.text = $"����... (�ֻ��� ���ʽ� {Constant.DiceBonus})";
                            temCon.DiceResult(false);
                        }
                    }
                }
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
