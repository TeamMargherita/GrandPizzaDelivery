using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 리듬 게임 관련된 데이터를 관리하는 싱글톤 클래스
/// </summary>
public class RhythmManager : MonoBehaviour
{
    public static RhythmManager Instance             // 싱글톤 인스턴싱
    {
        get { return instance; }
    }
    public string Title;                            // 관리 할 곡 제목
    public AudioClip AudioClip;                     // 재생할 곡 클립
    public decimal CurrentTime;                     // 현재 시간
    public AudioData Data;                          // 곡 데이터
    public float Speed;                             // 속도
    public float MusicSound;                        // 배경음
    public float KeySound;                          // 키음
    public bool SceneChange;                        // 씬 전환 여부
    public JudgeStorage Judges;                     // 판정 저장소
    public KeyCode[] ClearKeys;

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
        ClearKeys[0] = KeyCode.A;
        ClearKeys[1] = KeyCode.S;
        ClearKeys[2] = KeyCode.Semicolon;
        ClearKeys[3] = KeyCode.Quote;
    }

    private void Update()
    {
        Judges.SetAttractive();

        // 음악이 끝났을 경우
        if ((float)CurrentTime >= Data.Length && !SceneChange)
        {
            EndScene();
        }
        // 중도 스킵 = F5
        else if (Input.GetKeyDown(KeyCode.F5) && SceneManager.GetActiveScene().name == "RhythmScene" && !SceneChange)
        {
            EndScene();
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

    /// <summary>
    /// 리듬게임 시작 시 데이터 초기화를 위한 함수
    /// </summary>
    public void Init()
    {
        // 현재 갖고 있는 Title을 통해 Json데이터를 불러옴
        LoadData();

        // 현재 시간 초기화
        CurrentTime = 0;

        // 판정 초기화
        Judges.Init();

        // 씬 전환 여부 초기화
        SceneChange = false;
    }

    /// <summary>
    /// 씬을 전환하며 매력도를 전달하는 함수
    /// </summary>
    public void EndScene()
    {
        // 씬 전환
        SceneChange = true;

        // 매력도 전달
        Constant.PizzaAttractiveness = Judges.Attractive;

        // 피자 구조체 생성 함수 호출
        LoadScene.Instance.LoadPizzaMenu();
    }
}
