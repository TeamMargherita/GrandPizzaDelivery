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
	public IInspectingPanelControl InspectingPanelControl { get; set; }
	public ISpawnCar SpawnCar { get; set; }
	public string[] NpcTextStrArr { get; protected set; }

	public List<TextNodeC> TextList { get; protected set; }

	protected MethodS[] methodSArr;

	protected int nowTextNum;
	protected int[] nextTextNum;
	protected bool[] nextTextIsAble;
	protected int[] startText;
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
	public void ChangeNPCImage(int index)
    {
		NpcFace.sprite = NpcSprArr[index];
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
		InspectingPanelControl.ControlInspectUI(false, null);
	}
	public void SpawnPolice(int cnt)
    {
		SpawnCar.SpawnCar(cnt);
    }
	protected void AddTextList()
	{
		TextNodeC t = new TextNodeC(nowTextNum, nextTextNum, methodSArr, nextTextIsAble);

		TextList.Add(t);
	}
}
