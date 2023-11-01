using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoInGameScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("GoIn", 2f);
    }

    public void GoIn()
    {
        LoadScene.Instance.ActiveTrueFade("InGameScene");
    }
}
