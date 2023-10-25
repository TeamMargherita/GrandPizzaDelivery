using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    private bool isFade = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void FadeBackground()
    {
        isFade = true;
    }

    public void Update()
    {
        if (isFade)
        {
            Camera.main.backgroundColor = new Color(Camera.main.backgroundColor.r - 1f / 255f, Camera.main.backgroundColor.g - 1f / 255f, Camera.main.backgroundColor.b - 1f / 255f);
        }
        if (Camera.main.backgroundColor.g <= 0f && isFade)
        {
            isFade = false;
            Invoke("LoadS", 1f);
        }
    }

    private void LoadS()
    {
        LoadScene.Instance.ActiveTrueFade("Prologue");
    }
}
