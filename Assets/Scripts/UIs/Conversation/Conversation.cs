using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConversationNS;
using UnityEngine.UI;


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

	public IInspectingPanelControl InspectingPanelControl { get; set; }
	public ISpawnCar SpawnCar { get; set; }
	public ICoroutineDice CoroutineDice { get; set; }
	public string[] NpcTextStrArr { get; protected set; }

	public List<TextNodeC> TextList { get; protected set; }

	protected MethodS[] methodSArr;

	protected int nowTextNum;
	protected int[] nextTextNum;
	protected bool[] nextTextIsAble;
	protected int[] startText;
	protected int temInt = -1;
	public void PlayMethod(MethodS met)
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
				SetNpcText(met.MethodParameter);
				break;
			case MethodEnum.ENDPANEL:
				EndPanel();
				break;
			case MethodEnum.SPAWNPOLICE:
				SpawnPolice(met.MethodParameter[0]);
				break;
        }
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
	public void SetNpcText(int[] arr)
    {
		NpcText.text = NpcTextStrArr[arr[Random.Range(0, arr.Length)]];
    }
	public void EndPanel()
    {
		InspectingPanelControl.ControlInspectUI(false, null, -1);
	}
	public void SpawnPolice(int cnt)
    {
		SpawnCar.SpawnCar(cnt);
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

		//int index = System.Array.FindIndex<string>(NpcTextStrArr, a => a.Equals(NpcText.text));
		int index2 = TextList.FindIndex(a => a.NowTextNum == -1);
		SettingConversation(index2);
	}
	
	public void NextText(int ind)
	{
		List<TextNodeC> tem = TextList.FindAll(a => a.NowTextNum == ind);
		
		int index2 = -1;
		if (tem.Count > 1) 
		{
			index2 = Bifurcation(tem);
		}
		else 
		{
			index2 = TextList.FindIndex(a => a.NowTextNum == ind);
		}
		SettingConversation(index2);
	}
	protected void SettingConversation(int index2)
	{
		if (index2 == -100) { return; }

		for (int i = 0; i < TextList[index2].MethodSArr.Length; i++)
		{
			PlayMethod(TextList[index2].MethodSArr[i]);
		}

		if (TextList[index2].NextTextNum.Length == 1 && TextList[index2].NextTextNum[0] == -1) { return; }

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
