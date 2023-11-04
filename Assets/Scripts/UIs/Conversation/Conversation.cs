using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConversationNS;
using UnityEngine.UI;
using StoreNS;

// 한석호 작성
public class Conversation
{

	public RectTransform ScrollContents { get; set; }
	public Image NpcFace { get; set; }
	public Image PlayerFace { get; set; }
	public Sprite[] NpcSprArr { get; set; }
	public Sprite[] PlayerSprArr { get; set; }
	public  Text NpcText { get; set; }
	public  Text[] PlayerTextArr { get; set; }
	public  PlayerTexts[] PlayerTextsArr { get; set; }

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
	/// 대사집. 
	/// </summary>
	public string[] NpcTextStrArr { get; protected set; }
	/// <summary>
	/// 대사를 방향그래프로 연결한 것들
	/// </summary>
	public List<TextNodeC> TextList { get; protected set; }
	/// <summary>
	/// 상점에서 구입한 아이템 품목들
	/// </summary>
	protected Dictionary<ItemS, int> selectStoreItemDIc = new Dictionary<ItemS, int>();
	protected Store store;

	protected MethodS[] methodSArr;
	
	/// <summary>
	/// 내가 선택한 대사의 번호. 예외로 -1은 시작지점, -5는 상점이 닫힌 직후 대사를 의미한다.
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
		switch(met.MethodNum)
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
		Debug.Log(NpcFace.sprite.name);
	}
	public void ChangePlayerImage(int index)
    {
		PlayerFace.sprite = PlayerSprArr[index];
	}
	/// <summary>
	/// 다음에 들어갈 NPC의 대사를 결정한다. -1이 들어가면 맨 처음 나온 대사가 재등장한다.
	/// </summary>
	/// <param name="arr"></param>
	public void SetNpcText(int[] arr, int num = -1)
	{
		Debug.Log("가격 " + num);
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
		//Debug.Log($"{selectStoreItemDIc.Count} 카운트 ");
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
	/// 물건 훔치기
	/// </summary>
	protected void Steel()
	{
		int r = -1;
		bool isFull = true;	// 더 이상 훔칠수 없음 여부
		// 가게에서 더이상 훔칠 것이 없는지 판별함
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

		while(true)
		{
			r = Random.Range(0, store.StoreItemList.Count);

			if (Constant.PlayerItemDIc.ContainsKey(store.StoreItemList[r].Item))
			{
				// 훔치려는 물건을 이미 최대로 가지고 있는 지 판별함
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
	/// 대화도중 다른 상황으로 넘어갔을 때 대화 진행을 저장하기 위함
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
	/// 주사위의 결과
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
	/// 오버로딩된 SettingConversation 메소드를 사용하고 싶다면 isCondition을 켜주는 해당 메소드가 실행되어야 한다.
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
									&& System.Linq.Enumerable.SequenceEqual(b.MethodParameter,methodParamArr)));
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
	/// 어느 텍스트로 넘어가야 할지 대사 인덱스를 결정해준다.
	/// </summary>
	/// <param name="ind"></param>
	public void NextText(int ind)
	{
		List<TextNodeC> tem = TextList.FindAll(a => a.NowTextNum == ind);
		
		int index2 = -1;
		if (tem.Count > 1) 
		{
			if (isCondition) { isCondition = false; }
			index2 = Bifurcation(tem);
			//Debug.Log($"{index2} 전개 0.4");
		}
		else if (isCondition)
		{
			isCondition = false;
			Bifurcation(tem);
			//Debug.Log($"{tem} 전개 0.8");
			return;
		}
		else 
		{
			index2 = TextList.FindIndex(a => a.NowTextNum == ind);
		}
		//Debug.Log($"{index2} 전개 1");
		SettingConversation(index2);
	}
	/// <summary>
	/// 인덱스가 index2인 곳의 대사로 넘어가고, 그에 따른 메소드를 실행시켜 세팅을 한다. -100이 인덱스로 들어오면 즉시 반환한다.
	/// </summary>
	/// <param name="index2">대사 배열의 인덱스</param>
	protected void SettingConversation(int index2)
	{
		if (index2 == -100) { return; }

		for (int i = 0; i < TextList[index2].MethodSArr.Length; i++)
		{
			//Debug.Log($"{index2} 전개 2");
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
			PlayerTextArr[i].gameObject.SetActive(true);
			PlayerTextArr[i].text = NpcTextStrArr[TextList[index2].NextTextNum[i]];
			PlayerTextsArr[i].TextNum = TextList[index2].NextTextNum[i];
		}
	}
	/// <summary>
	/// 인덱스가 index2인 곳의 대사로 넘어가고, 그에 따른 메소드를 실행시켜 세팅을 한다. -100이 인덱스로 들어오면 즉시 반환한다.
	/// </summary>
	/// <param name="index2">대사 배열의 인덱스</param>
	/// <param name="num">치환할 수 </param>
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
	/// 플레이어의 상태에 따라 경찰의 대화가 바뀌는 분기점
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
