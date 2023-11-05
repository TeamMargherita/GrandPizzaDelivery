using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PizzaNS;
using Inventory;
using UnityEngine.SceneManagement;
using ClerkNS;
using StoreNS;
using DayNS;

public class BackTitle : MonoBehaviour
{
    [SerializeField] private GameObject gameManager;

    public void OnClickTitle()
    {
        Destroystatic("GameManager");
        PlayerStat.HP = PlayerStat.MaxHP;
        PlayerStat.PlayerIsGod = false;
        SceneManager.LoadScene("MainPage");
        Destroystatic("RhythmManager");
        // -------------------------------------static -----------------------------------------------------//
        Constant.InitConstant();
        EmployeeFire.WorkingDay = new Dictionary<int, List<ClerkC>>(); ;
        // -------------------------------------------------------------------------------------------------//
    }
    private void Destroystatic(string gameOB)
    {
        GameObject temporary = GameObject.Find(gameOB);
        if (temporary != null)
        {
            Destroy(temporary);
        }
    }

    public void OnClickGameQuit()
    {
        Application.Quit();
    }
}
