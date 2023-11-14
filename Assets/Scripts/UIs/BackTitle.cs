using ClerkNS;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackTitle : MonoBehaviour
{
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject soundMenu;
    public void OnClickTitle()
    {
        Destroystatic("GameManager");
        PlayerStat.HP = PlayerStat.MaxHP;
        PlayerStat.PlayerIsGod = false;
        SceneManager.LoadScene("MainPage");
        Destroystatic("RhythmManager");
        // -------------------------------------static -----------------------------------------------------//
        Constant.InitConstant();
        EmployeeFire.WorkingDay = new Dictionary<int, List<ClerkC>>();
        DataManager.SaveData();
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

    public void OnClickSound()
    {
        soundMenu.SetActive(true);
    }
}
