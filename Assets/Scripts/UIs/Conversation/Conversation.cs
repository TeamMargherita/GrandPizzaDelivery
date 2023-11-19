using ConversationNS;
using StoreNS;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �Ѽ�ȣ �ۼ�
public class Conversation
{

    public RectTransform ScrollContents { get; set; }
    public Image NpcFace { get; set; }
    public Image PlayerFace { get; set; }
    public Sprite[] NpcSprArr { get; set; }
    public Sprite[] PlayerSprArr { get; set; }
    public Text NpcText { get; set; }
    public Text[] PlayerTextArr { get; set; }
    public PlayerTexts[] PlayerTextsArr { get; set; }

    public IConversationPanelControl InspectingPanelControl { get; set; }
    public ISpawnCar SpawnCar { get; set; }
    public ICoroutineDice CoroutineDice { get; set; }
    public IInitStore InitStore
    {
        get
        {
            return iInitStore;
        }
        set
        {
            iInitStore = value;
            iInitStore.InitStore(store);
        }
    }
    /// <summary>
    /// �����. 
    /// </summary>
    public string[] NpcTextStrArr { get; protected set; }
    /// <summary>
    /// ��縦 ����׷����� ������ �͵�
    /// </summary>
    public List<TextNodeC> TextList { get; protected set; }
    /// <summary>
    /// �������� ������ ������ ǰ���
    /// </summary>
    protected Dictionary<ItemS, int> selectStoreItemDIc = new Dictionary<ItemS, int>();
    protected Store store;

    protected MethodS[] methodSArr;

    /// <summary>
    /// ���� ������ ����� ��ȣ. ���ܷ� -1�� ��������, -5�� ������ ���� ���� ��縦 �ǹ��Ѵ�.
    /// </summary>
    protected int nowTextNum;
    protected int[] nextTextNum;
    protected bool[] nextTextIsAble;
    protected int[] startText;
    protected int temInt = -1;
    protected bool isCondition = false;

    private IInitStore iInitStore;
    private bool isOpenStore = false;
    public void PlayMethod(MethodS met, int num = -1)
    {
        switch (met.MethodNum)
        {
            case MethodEnum.SETSIZECONTENTS:
                SetSizeScrollContents(met.MethodParameter[0] != 0, met.MethodParameter[1]);
                break;
            case MethodEnum.CHANGENPCIMAGE:
                ChangeNPCImage(met.MethodParameter[0]);
                break;
            case MethodEnum.CHANGEPLAYERIMAGE:
                ChangePlayerImage(met.MethodParameter[0]);
                break;
            case MethodEnum.SETRANDNPCTEXT:
                SetNpcText(met.MethodParameter, num);
                break;
            case MethodEnum.ENDPANEL:
                EndPanel();
                break;
            case MethodEnum.SPAWNPOLICE:
                SpawnPolice(met.MethodParameter[0]);
                break;
            case MethodEnum.OPENSTORE:
                OpenStore();
                break;
            case MethodEnum.SAVETEXTINDEX:
                SaveTextIndex(met.MethodParameter[0]);
                break;
            case MethodEnum.SETISCONDITION:
                SetISCondition();
                break;
            case MethodEnum.INITPLAYERTEXT:
                InitPlayerSelectText();
                break;
        }
    }
    protected virtual void InitPlayerSelectText()
    {

    }
    public void SetSizeScrollContents(bool isVert, int size)
    {
        ScrollContents.position = Vector3.zero;
        if (isVert)
        {
            ScrollContents.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);
        }
        else
        {
            ScrollContents.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size);
        }
    }
    public void ChangeNPCImage(int index)
    {
        NpcFace.sprite = NpcSprArr[index];
    }
    public void ChangePlayerImage(int index)
    {
        PlayerFace.sprite = PlayerSprArr[index];
    }
    /// <summary>
    /// ������ �� NPC�� ��縦 �����Ѵ�. -1�� ���� �� ó�� ���� ��簡 ������Ѵ�.
    /// </summary>
    /// <param name="arr"></param>
    public void SetNpcText(int[] arr, int num = -1)
    {
        Debug.Log("���� " + num);
        if (arr[0] == -1 && arr.Length == 1)
        {
            InitStartText();
            NpcText.text = NpcTextStrArr[startText[Random.Range(0, startText.Length)]];
        }
        else
        {
            if (num == -1)
            {
                NpcText.text = NpcTextStrArr[arr[Random.Range(0, arr.Length)]];
            }
            else
            {
                NpcText.text = NpcTextStrArr[arr[Random.Range(0, arr.Length)]].Replace("$$$", num.ToString());
            }
        }
    }
    protected virtual void InitStartText()
    {

    }
    public void EndPanel()
    {
        InspectingPanelControl.ControlConversationUI(false, null, -1);
    }
    public void SpawnPolice(int cnt)
    {
        SpawnCar.SpawnCar(cnt);
    }
    public void OpenStore()
    {
        isOpenStore = true;
        InitStore.OpenStore();
    }
    public void CloseStore(int cost, Dictionary<ItemS, int> dic)
    {
        isOpenStore = false;
        if (cost >= 0)
        {
            JumpConversation(cost);
            selectStoreItemDIc = dic;
        }
        else
        {
            JumpConversation();
            selectStoreItemDIc = dic;
        }
    }
    protected void AddPlayerItemDic()
    {
        //Debug.Log($"{selectStoreItemDIc.Count} ī��Ʈ ");
        foreach (var key in selectStoreItemDIc.Keys)
        {
            if (Constant.PlayerItemDIc.ContainsKey(key))
            {
                Constant.PlayerItemDIc[key] += selectStoreItemDIc[key];
            }
            else
            {
                Constant.PlayerItemDIc.Add(key, selectStoreItemDIc[key]);
            }
        }
        InitStore.InitSelectItemCnt();
        selectStoreItemDIc.Clear();
    }
    /// <summary>
    /// ���� ��ġ��
    /// </summary>
    protected void Steel()
    {
        int r = -1;
        bool isFull = true; // �� �̻� ��ĥ�� ���� ����
                            // ���Կ��� ���̻� ��ĥ ���� ������ �Ǻ���
        for (int i = 0; i < store.StoreItemList.Count; i++)
        {
            if (Constant.PlayerItemDIc.ContainsKey(store.StoreItemList[i].Item))
            {
                if (Constant.PlayerItemDIc[store.StoreItemList[i].Item] < store.StoreItemList[i].Item.MaxCnt)
                {
                    isFull = false;
                    break;
                }
            }
            else
            {
                isFull = false;
                break;
            }
        }

        if (isFull) { return; }

        while (true)
        {
            r = Random.Range(0, store.StoreItemList.Count);

            if (Constant.PlayerItemDIc.ContainsKey(store.StoreItemList[r].Item))
            {
                // ��ġ���� ������ �̹� �ִ�� ������ �ִ� �� �Ǻ���
                if (Constant.PlayerItemDIc[store.StoreItemList[r].Item] < store.StoreItemList[r].Item.MaxCnt)
                {
                    Constant.PlayerItemDIc[store.StoreItemList[r].Item]++;
                    break;
                }
                else
                {
                    continue;
                }
            }
            else
            {
                Constant.PlayerItemDIc.Add(store.StoreItemList[r].Item, 1);
                break;
            }
        }
    }
    /// <summary>
    /// ��ȭ���� �ٸ� ��Ȳ���� �Ѿ�� �� ��ȭ ������ �����ϱ� ����
    /// </summary>
    /// <param name="ind"></param>
    public void SaveTextIndex(int ind)
    {
        temInt = ind;
    }
    public void DiceRoll(int num)
    {
        CoroutineDice.StartDice(num);
    }
    /// <summary>
    /// �ֻ����� ���
    /// </summary>
    /// <param name="bo"></param>
    public virtual void DiceResult(bool bo)
    {

    }
    public virtual void JumpConversation()
    {

    }
    public virtual void JumpConversation(int num)
    {

    }
    /// <summary>
    /// �����ε��� SettingConversation �޼ҵ带 ����ϰ� �ʹٸ� isCondition�� ���ִ� �ش� �޼ҵ尡 ����Ǿ�� �Ѵ�.
    /// </summary>
    protected void SetISCondition()
    {
        isCondition = true;
    }
    protected int Findidx(int nowTextNum, int[] methodParamArr)
    {
        return TextList.FindIndex(
                            a =>
                            a.NowTextNum == nowTextNum &&
                                -1 != System.Array.FindIndex<MethodS>(
                                a.MethodSArr, b => b.MethodNum == MethodEnum.SETRANDNPCTEXT
                                    && System.Linq.Enumerable.SequenceEqual(b.MethodParameter, methodParamArr)));
    }
    public void StartText()
    {
        NpcText.text = NpcTextStrArr[startText[Random.Range(0, startText.Length)]];
        int index2 = -1;
        List<TextNodeC> tem = TextList.FindAll(a => a.NowTextNum == -1);
        if (tem.Count > 1)
        {
            index2 = Bifurcation(tem);
        }
        else
        {
            index2 = TextList.FindIndex(a => a.NowTextNum == -1);
        }

        SettingConversation(index2);
    }
    /// <summary>
    /// ��� �ؽ�Ʈ�� �Ѿ�� ���� ��� �ε����� �������ش�.
    /// </summary>
    /// <param name="ind"></param>
    public void NextText(int ind)
    {
        Debug.Log($"Constant.Dept {Constant.Dept}");
        List<TextNodeC> tem = TextList.FindAll(a => a.NowTextNum == ind);

        int index2 = -1;
        if (tem.Count > 1)
        {
            if (isCondition) { isCondition = false; }
            index2 = Bifurcation(tem);
            //Debug.Log($"{index2} ���� 0.4");
        }
        else if (isCondition)
        {
            isCondition = false;
            Bifurcation(tem);
            //Debug.Log($"{tem} ���� 0.8");
            return;
        }
        else
        {
            index2 = TextList.FindIndex(a => a.NowTextNum == ind);
        }
        //Debug.Log($"{index2} ���� 1");
        SettingConversation(index2);
    }
    /// <summary>
    /// �ε����� index2�� ���� ���� �Ѿ��, �׿� ���� �޼ҵ带 ������� ������ �Ѵ�. -100�� �ε����� ������ ��� ��ȯ�Ѵ�.
    /// </summary>
    /// <param name="index2">��� �迭�� �ε���</param>
    protected void SettingConversation(int index2)
    {
        if (index2 == -100) { return; }

        for (int i = 0; i < TextList[index2].MethodSArr.Length; i++)
        {
            //Debug.Log($"{index2} ���� 2");
            PlayMethod(TextList[index2].MethodSArr[i]);
        }

        if (TextList[index2].NextTextNum.Length == 1 && TextList[index2].NextTextNum[0] == -1) { return; }
        if (TextList[index2].NextTextNum.Length == 0) { return; }

        for (int i = 0; i < TextList[index2].NextTextNum.Length; i++)
        {
            if (!TextList[index2].NextTextIsAble[i])
            {
                if (!Condition(TextList[index2].NextTextNum[i])) { continue; }
            }
            Debug.Log(i);
            PlayerTextArr[i].gameObject.transform.parent.gameObject.SetActive(true);
            PlayerTextArr[i].gameObject.SetActive(true);
            PlayerTextArr[i].text = NpcTextStrArr[TextList[index2].NextTextNum[i]];
            PlayerTextsArr[i].TextNum = TextList[index2].NextTextNum[i];
        }
    }
    /// <summary>
    /// �ε����� index2�� ���� ���� �Ѿ��, �׿� ���� �޼ҵ带 ������� ������ �Ѵ�. -100�� �ε����� ������ ��� ��ȯ�Ѵ�.
    /// </summary>
    /// <param name="index2">��� �迭�� �ε���</param>
    /// <param name="num">ġȯ�� �� </param>
    protected void SettingConversation(int index2, int num)
    {
        if (index2 == -100) { return; }

        for (int i = 0; i < TextList[index2].MethodSArr.Length; i++)
        {
            PlayMethod(TextList[index2].MethodSArr[i], num);
        }

        if (TextList[index2].NextTextNum.Length == 1 && TextList[index2].NextTextNum[0] == -1) { return; }
        if (TextList[index2].NextTextNum.Length == 0) { return; }

        for (int i = 0; i < TextList[index2].NextTextNum.Length; i++)
        {
            if (!TextList[index2].NextTextIsAble[i])
            {
                if (!Condition(TextList[index2].NextTextNum[i])) { continue; }
            }
            PlayerTextArr[i].gameObject.transform.parent.gameObject.SetActive(true);
            PlayerTextArr[i].gameObject.SetActive(true);
            PlayerTextArr[i].text = NpcTextStrArr[TextList[index2].NextTextNum[i]];
            PlayerTextsArr[i].TextNum = TextList[index2].NextTextNum[i];
        }
    }
    protected virtual bool Condition(int num)
    {
        return false;
    }

    /// <summary>
    /// �÷��̾��� ���¿� ���� ������ ��ȭ�� �ٲ�� �б���
    /// </summary>
    /// <param name="tem"></param>
    /// <returns></returns>
    protected virtual int Bifurcation(List<TextNodeC> tem)
    {
        return -1;
    }
    protected void AddTextList()
    {
        TextNodeC t = new TextNodeC(nowTextNum, nextTextNum, methodSArr, nextTextIsAble);

        TextList.Add(t);
    }

    protected virtual void InitTextList() { }
}
