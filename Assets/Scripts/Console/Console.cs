using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
	public InputField mainInputField;

    private bool setActive = false;
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
        }
        InputConsole();
	}
}
