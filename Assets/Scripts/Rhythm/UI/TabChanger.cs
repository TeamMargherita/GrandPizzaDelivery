using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TabChanger : MonoBehaviour
{
    [SerializeField] private List<GameObject> Tabs;
    [SerializeField] private UnityEvent[] Orders;
    [SerializeField] private UnityEvent MusicUp;
    [SerializeField] private UnityEvent MusicDown;
    [SerializeField] private UnityEvent MenuOnOff;
    [SerializeField] private UnityEvent MusicSelect;
    [SerializeField] private float[] delays;

    private UnityEvent Work;
    private int currentTab;
    private bool isNext = false;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "SelectScene" && RhythmManager.Instance.IsSelectGuide)
        {
            currentTab = 0;
            Invoke("NextTabOpen", delays[currentTab]);
        }
        else if (SceneManager.GetActiveScene().name == "RhythmScene" && RhythmManager.Instance.IsRhythmGuide)
        {
            currentTab = 0;
            Invoke("NextTabOpen", delays[currentTab]);
        }
        else
        {
            return;
        }
    }

    private void Update()
    {
        Work?.Invoke();
    }

    public void OnChangeTab()
    {
        if (isNext)
            return;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            NextStep();
            MusicUp.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            NextStep();
            MusicDown.Invoke();
        }
    }
    public void OnMenuOpenTab()
    {
        if (isNext)
            return;

        if (Input.GetKeyDown(KeyCode.F10))
        {
            NextStep();
            MenuOnOff.Invoke();
        }
    }
    public void OnMenuCloseTab()
    {
        if (isNext)
            return;

        if (Input.GetKeyDown(KeyCode.F10))
        {
            NextStep();
            MenuOnOff.Invoke();
        }
    }
    public void OnSelectTab()
    {
        if (isNext)
            return;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MusicUp.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MusicDown.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            NextStep();
            MusicSelect.Invoke();
        }
    }
    public void NextTab()
    {
        if (isNext)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextStep();
        }
    }

    public void NextStep()
    {
        isNext = true;
        if (currentTab >= Tabs.Count)
            return;

        if (currentTab >= 0)
            Tabs[currentTab].SetActive(false);
        currentTab++;
        if (currentTab >= Tabs.Count)
        {
            if (SceneManager.GetActiveScene().name == "SelectScene" && RhythmManager.Instance.IsSelectGuide)
            {
                RhythmManager.Instance.IsSelectGuide = false;
            }
            else if (SceneManager.GetActiveScene().name == "RhythmScene" && RhythmManager.Instance.IsRhythmGuide)
            {
                RhythmManager.Instance.IsRhythmGuide = false;
            }
        }
        else
        {
            Invoke("NextTabOpen", delays[currentTab]);
        }
    }

    private void NextTabOpen()
    {
        Tabs[currentTab].SetActive(true);
        Work = Orders[currentTab];
        isNext = false;
    }
}