using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//한석호 작성
public class Fade : MonoBehaviour
{
    public static Fade Instance = null;

    public int AlphaTerm = 5;

    private UnityEngine.UI.Image img;

    private string loadSceneName;
    private int alpha = 255;
    private bool inOut = true;
    
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null) //instance가 null. 즉, 시스템상에 존재하고 있지 않을때
        {
            Instance = this; //내자신을 instance로 넣어줍니다.
            DontDestroyOnLoad(gameObject); //OnLoad(씬이 로드 되었을때) 자신을 파괴하지 않고 유지
        }
        else
        {
            if (Instance != this) //instance가 내가 아니라면 이미 instance가 하나 존재하고 있다는 의미
            {
                Destroy(this.gameObject); //둘 이상 존재하면 안되는 객체이니 방금 AWake된 자신을 삭제
            }
        }
        img = this.gameObject.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>();
        this.gameObject.SetActive(false);
    }
	private void OnEnable()
	{
        alpha = 0;
	}
    public void SetLoadSceneName(string str)
	{
        loadSceneName = str;
	}
    // Update is called once per frame
    void Update()
    {
        if (inOut)
        {
            alpha += AlphaTerm;
            img.color = new Color(0f, 0f, 0f, alpha / 255f);

            if (alpha >= 255)
            {
                inOut = false;
                LoadScene.Instance.LoadS(loadSceneName);
            }
        }
        else
		{
            alpha -= AlphaTerm;
            img.color = new Color(0f, 0f, 0f, alpha / 255f);

            if (alpha <= 0)
			{
                inOut = true;
                this.gameObject.SetActive(false);
			}
        }
    }
}
