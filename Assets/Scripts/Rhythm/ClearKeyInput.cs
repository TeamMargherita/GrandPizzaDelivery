using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ClearKeyInput : MonoBehaviour
{
    public Text[] KeyInput;
    public Image[] Select;
    public UnityEvent Refresh;
    private RhythmManager manager;
    private KeyCode[] clearKeys;
    private bool isEdit;
    private int index;
    private Dictionary<char, bool> keyMap = new Dictionary<char, bool>();

    private void Start()
    {
        Init();
    }

    private void OnEnable()
    {
        Init();
    }

    private void Update()
    {
        if (!isEdit)
            return;

        if (Input.anyKey)
        {
            Next();
            Refresh.Invoke();
        }
    }

    public void Edit()
    {
        index = 0;
        isEdit = true;
        foreach (var i in Select)
        {
            i.gameObject.SetActive(false);
        }
        Select[index].gameObject.SetActive(true);
    }

    private void Next()
    {
        string str = Input.inputString;        

        // 잘못된 입력
        if (str.Length <= 0)
            return;

        // 기존 키 해제
        char oldKey = (char)clearKeys[index];
        keyMap[oldKey] = false;

        // 새로 받은 키 저장
        char newKey = str[0];
        Debug.Log(newKey);

        if (keyMap.ContainsKey(newKey))
            keyMap[newKey] = true;
        else
            keyMap.Add(newKey, true);

        clearKeys[index] = (KeyCode)newKey;
        manager.ClearKeys[index] = clearKeys[index];
        KeyInput[index].text = newKey.ToString();
        Select[index].gameObject.SetActive(false);

        // 다음 자리로 이동
        index++;
        if(index >= clearKeys.Length)
        {
            isEdit = false;
            return;
        }

        Select[index].gameObject.SetActive(true);
    }

    private void Init()
    {
        if(manager == null)
            manager = RhythmManager.Instance;
        clearKeys = new KeyCode[4];
        index = 0;
        isEdit = false;
        keyMap.Clear();
        for (int i = 0; i < manager.ClearKeys.Length; i++)
        {
            clearKeys[i] = manager.ClearKeys[i];
            char key = (char)clearKeys[i];
            KeyInput[i].text = key.ToString();
            keyMap.Add(key, true);
        }
        foreach(var i in Select)
        {
            i.gameObject.SetActive(false);
        }
    }
}