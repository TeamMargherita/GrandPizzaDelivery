using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 한석호 작성
public class MainUI : MonoBehaviour
{
    [SerializeField] private GameObject soundObject;
    [SerializeField] private GameObject optionObject;
    [SerializeField] private GameObject leftLisence;
    [SerializeField] private GameObject rightLisence;


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

    public void Quit()
	{
        Application.Quit();
	}

    public void Sound()
	{
        soundObject.SetActive(true);
	}

    public void Close()
	{
        optionObject.SetActive(false);
	}

    public void Open()
	{
        optionObject.SetActive(true);
	}

    public void LicenseOpen()
    {
        leftLisence.SetActive(true); 
        rightLisence.SetActive(true);
    }

    public void LicenseClose()
    {
        leftLisence.SetActive(false);
        rightLisence.SetActive(false);
    }
}
