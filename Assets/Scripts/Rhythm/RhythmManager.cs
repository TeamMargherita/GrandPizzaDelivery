using UnityEngine;

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

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);

        Instance = this;
        DontDestroyOnLoad(Instance);
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
}
