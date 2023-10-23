using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ¸®µë °ÔÀÓ °ü·ÃµÈ µ¥ÀÌÅÍ¸¦ °ü¸®ÇÏ´Â ½Ì±ÛÅæ Å¬·¡½º
/// </summary>
public class RhythmManager : MonoBehaviour
{
    public static RhythmManager Instance             // ½Ì±ÛÅæ ÀÎ½ºÅÏ½Ì
    {
        get { return instance; }
    }
    public string Title;                            // °ü¸® ÇÒ °î Á¦¸ñ
    public AudioClip AudioClip;                     // Àç»ýÇÒ °î Å¬¸³
    public decimal CurrentTime;                     // ÇöÀç ½Ã°£
    public AudioData Data;                          // °î µ¥ÀÌÅÍ
    public float Speed;                             // ¼Óµµ
    public float MusicSound;
    public float KeySound;
    public bool SceneChange;
    public JudgeStorage Judges;

    private static RhythmManager instance = null;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        Judges = new JudgeStorage();
        Data = new AudioData();
        MusicSound = 0.5f;
        KeySound = 0.5f;
    }

    private void Update()
    {
        Judges.SetAttractive();

        if ((float)CurrentTime >= Data.Length && !SceneChange)
        {
            EndScene();
        }
        if (Input.GetKeyDown(KeyCode.F5) && SceneManager.GetActiveScene().name == "RhythmScene" && !SceneChange)
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
    }

    private void EndScene()
    {
        SceneChange = true;
        Constant.PizzaAttractiveness = Judges.Attractive;
        LoadScene.Instance.LoadPizzaMenu();
    }
}
