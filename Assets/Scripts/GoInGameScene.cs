using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoInGameScene : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Text text;
    // Start is called before the first frame update
    void Start()
    {
        text.text = $"{Constant.NowDate}¿œ";
        Invoke("GoIn", 2f);
    }

    public void GoIn()
    {
        LoadScene.Instance.ActiveTrueFade("InGameScene");
    }
}
