using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
	public InputField mainInputField;
    public GameObject ExplainPanel;

    private string[] Key = new string[] {"Player.", "Time."};
    private List<string> player = new List<string>() {"AddMoney", "Dead", "God" };
    private List<string> time = new List<string>() { "Stop", "Play", "NextDay", "DarkDay" };
    private Dictionary<string, List<string>> ConsolDB = new Dictionary<string, List<string>>();

    private bool setActive = false;
    private void Awake()
    {
        ConsolDB.Add(Key[0], player);
        ConsolDB.Add(Key[1], time);
    }
    void InputConsole()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
        {
            if (setActive)
            {
                if (mainInputField.text == "Player.AddMoney")
                {
                    GameManager.Instance.Money += 1000000;
                    mainInputField.text = "";
                }
                else if (mainInputField.text == "Player.Dead")
                {
                    PlayerStat.HP -= PlayerStat.MaxHP;
                    mainInputField.text = "";
                }
                else if (mainInputField.text == "Player.God")
                {
                    PlayerStat.PlayerIsGod = true;
                    mainInputField.text = "";
                }
                else if (mainInputField.text == "Time.Stop")
                {
                    Time.timeScale = 0;
                    mainInputField.text = "";
                }
                else if (mainInputField.text == "Time.Play")
                {
                    Time.timeScale = 1;
                    mainInputField.text = "";
                }
                else if (mainInputField.text == "Time.NextDay")
                {
                    GameManager.Instance.time = 14200;
                    mainInputField.text = "";
                    transform.GetChild(0).gameObject.SetActive(false);
                    setActive = false;
                }
                else if(mainInputField.text == "Time.DarkDay")
                {
                    GameManager.Instance.time = 82800;
                    mainInputField.text = "";
                    transform.GetChild(0).gameObject.SetActive(false);
                    setActive = false;
                }
            }
		}
	}
    int index = 1;
    string firstString = "";
    public void AutoText()
    {
        if (firstString == mainInputField.text) { index = 1; }
        string key = "";
        if(index == 1)
        {
            foreach (var i in Key)
            {
                if (i.Contains(mainInputField.text))
                {
                    key = i;
                    break;
                }
            }
            //자동완성텍스트 업데이트
            if (mainInputField.text.Length > 0)
                ExplainPanel.transform.GetChild(0).GetComponent<Text>().text = key;
            else
            {
                ExplainPanel.transform.GetChild(0).GetComponent<Text>().text = "";
                index = 1;
            }
            //ConsolDB딕셔너리에서 키값이 인풋필드 텍스트와 일치해지면 콘솔치트의 다음블록으로 넘어가짐 백스페이스로 지우지않을때
            if (ConsolDB.ContainsKey(mainInputField.text) && !Input.GetKeyDown(KeyCode.Backspace))
            {
                index = 2;
                firstString = mainInputField.text;
            }
        }
        else if(index == 2)
        {
            for (int i = 0; i < ConsolDB[firstString].Count; i++)
            {
                if (ConsolDB[firstString][i].Contains(mainInputField.text.Substring(firstString.Length)))
                {
                    key = ConsolDB[firstString][i];
                    break;
                }
            }
            if (mainInputField.text.Length > 0)
                ExplainPanel.transform.GetChild(0).GetComponent<Text>().text = firstString + key;
            else
            {
                ExplainPanel.transform.GetChild(0).GetComponent<Text>().text = "";
                index = 1;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote) && !setActive)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            setActive = true;
        }
        else if(Input.GetKeyDown(KeyCode.BackQuote) && setActive)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            setActive = false;
            mainInputField.text = "";
            firstString = "";
            index = 1;
        }
        InputConsole();
	}
}
