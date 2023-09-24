using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 리듬 게임 관련된 데이터를 관리하는 싱글톤 클래스
/// </summary>
public class RhythmManager : MonoBehaviour
{
    public static RhythmManager Instance = null;    // 싱글톤 인스턴싱
    public string Title;                            // 관리 할 곡 제목
    public decimal CurrentTime;                     // 현재 시간
    public AudioSource NoteSound;                   // 노트 소리
    public AudioData Data;                          // 곡 데이터
    public Note NotePrefab;                         // 노트
    public Bar BarPrefab;                           // 마디
    public float Speed;                             // 속도
    public float Accuracy;                          // 정확도
    public int Attractive;                          // 매력도
    public int Perfect;
    public int Great;
    public int Good;
    public int Miss;
    public bool SceneChange;
    public AudioSource BgSound;

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);

        Instance = this;
        DontDestroyOnLoad(Instance);
    }

    private void Update()
    {
        if (Perfect + Great + Good + Miss > 0)
            Accuracy = (float)(Perfect + Great * 0.7f + Good * 0.5f) / (Perfect + Great + Good + Miss) * 100f;
        else
            Accuracy = 100;
        Attractive = (int)(Constant.PizzaAttractiveness * (Accuracy / 100));
        if ((float)CurrentTime >= Data.Length && !SceneChange)
        {
            LoadScene.Instance.LoadPizzaMenu();
            Constant.PizzaAttractiveness = Attractive;
            SceneChange = true;
        }
        if (Input.GetKeyDown(KeyCode.F5) && SceneManager.GetActiveScene().name == "RhythmScene")
        {
            LoadScene.Instance.LoadPizzaMenu();
            Constant.PizzaAttractiveness = Attractive;
            SceneChange = true;
        }
    }

    /// <summary>
    /// 곡 데이터를 Json 파일로 저장
    /// </summary>
    public void SaveData()
    {
        JsonManager<AudioData>.Save(Data, Title);
    }

    /// <summary>
    /// Json 파일인 곡 데이터 불러오기
    /// </summary>
    public void LoadData()
    {
        Data = new AudioData(Title);
    }

    public void Init()
    {
        LoadData();
        CurrentTime = 0;
        Accuracy = 0;
        Attractive = 0;
        Perfect = 0;
        Great = 0;
        Good = 0;
        Miss = 0;
        SceneChange = false;
        if (BgSound == null)
            BgSound = GameObject.Find("BGSound").GetComponent<AudioSource>();
        BgSound.Play();
    }
}
