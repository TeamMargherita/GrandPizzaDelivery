using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/// <summary>
/// ¸®µë °ÔÀÓ °ü·ÃµÈ µ¥ÀÌÅÍ¸¦ °ü¸®ÇÏ´Â ½Ì±ÛÅæ Å¬·¡½º
/// </summary>
public class RhythmManager : MonoBehaviour
{
    public static RhythmManager Instance = null;    // ½Ì±ÛÅæ ÀÎ½ºÅÏ½Ì
    public string Title;                            // °ü¸® ÇÒ °î Á¦¸ñ
    public decimal CurrentTime;                     // ÇöÀç ½Ã°£
    public AudioData Data;                          // °î µ¥ÀÌÅÍ
    public float Speed;                             // ¼Óµµ
    public bool SceneChange;
    public AudioSource BgSound;
    public RhythmStorage Storage;
    public JudgeStorage Judges;

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);

        Instance = this;
        Judges = new JudgeStorage();
        DontDestroyOnLoad(Instance);
    }

    private void Update()
    {
        Judges.SetAttractive();
        if ((float)CurrentTime >= Data.Length && !SceneChange)
        {
            EndScene();
        }
        if (Input.GetKeyDown(KeyCode.F5) && SceneManager.GetActiveScene().name == "RhythmScene")
        {
            EndScene();
        }
    }

    /// <summary>
    /// °î µ¥ÀÌÅÍ¸¦ Json ÆÄÀÏ·Î ÀúÀå
    /// </summary>
    public void SaveData()
    {
        JsonManager<AudioData>.Save(Data, Title);
    }

    /// <summary>
    /// Json ÆÄÀÏÀÎ °î µ¥ÀÌÅÍ ºÒ·¯¿À±â
    /// </summary>
    public void LoadData()
    {
        Data = new AudioData(Title);
    }

    public void Init()
    {
        LoadData();
        CurrentTime = 0;
        Judges.Init();
        
        SceneChange = false;
        if (BgSound == null)
            BgSound = GameObject.Find("BGSound").GetComponent<AudioSource>();
        BgSound.Play();
    }

    private void EndScene()
    {
        LoadScene.Instance.LoadPizzaMenu();
        Constant.PizzaAttractiveness = Judges.Attractive;
        SceneChange = true;
    }
}
