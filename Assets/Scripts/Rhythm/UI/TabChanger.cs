using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    private static int guideCount = 2;
    private bool isNext = false;

    private void Start()
    {
        if (guideCount <= 0)
            return;

        guideCount--;
        currentTab = 0;
        Invoke("NextTabOpen", delays[currentTab]);
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            NextStep();
            MenuOnOff.Invoke();
        }
    }
    public void OnMenuCloseTab()
    {
        if (isNext)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
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
            RhythmManager.Instance.IsSelectGuide = false;
        }
    }
    public void NextStep()
    {
        isNext = true;
        if (currentTab >= Tabs.Count)
            return;

        Tabs[currentTab - 1].SetActive(false);
        Invoke("NextTabOpen", delays[currentTab]);
    }
    private void NextTabOpen()
    {
        if (currentTab >= Tabs.Count)
            return;

        Tabs[currentTab].SetActive(true);
        Work = Orders[currentTab];
        currentTab++;
        isNext = false;
    }
}