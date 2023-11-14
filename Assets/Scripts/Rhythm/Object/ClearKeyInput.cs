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

        // 새로 받은 키 저장
        char newKey = str[0];
        newKey = char.ToLower(newKey);

        // 기존 키 저장
        char oldKey = (char)clearKeys[index];

        // 관리한적 있는 키인 경우
        if (keyMap.ContainsKey(newKey))
        {
            // 새로운 키가 이미 중복인 경우
            if (keyMap[newKey])
            {
                // 할당 중인 위치 찾기
                for(int i = 0; i < clearKeys.Length; i++)
                {
                    if ((char)clearKeys[i] == newKey)
                    {
                        // 발견 시 기존 키로 저장
                        clearKeys[i] = (KeyCode)oldKey;
                        manager.ClearKeys[i] = clearKeys[i];
                        KeyInput[i].text = char.ToUpper(oldKey).ToString();
                        break;
                    }
                }
            }

            // 중복되지 않은 경우
            else
            {
                keyMap[oldKey] = false;
                keyMap[newKey] = true;
            }
        }
        // 새로운 키 입력 시
        else
        {
            keyMap[oldKey] = false;
            keyMap.Add(newKey, true);
        }

        clearKeys[index] = (KeyCode)newKey;
        manager.ClearKeys[index] = clearKeys[index];
        KeyInput[index].text = char.ToUpper(newKey).ToString();
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
            keyMap.Add(key, true);
            key = char.ToUpper(key);
            KeyInput[i].text = key.ToString();
        }
        foreach(var i in Select)
        {
            i.gameObject.SetActive(false);
        }
    }
}