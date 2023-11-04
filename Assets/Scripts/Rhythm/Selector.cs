using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// 음악 선택을 컨트롤 해주는 클래스
/// </summary>
public class Selector : MonoBehaviour
{
    [SerializeField] private string[] Titles;           // 데이터 파일 이름
    [SerializeField] private AudioSource Sound;         // 배경음
    [SerializeField] private AudioClip[] Highlights;    // 미리듣기 하이라이트 클립들
    [SerializeField] private AudioClip[] Clips;         // 변경 시켜줄 클립들
    [SerializeField] private Text Title;                // 곡 제목 텍스트
    [SerializeField] private Text Bpm;                  // Bpm 출력 텍스트
    [SerializeField] private Text Level;                // 난이도 출력 텍스트
    [SerializeField] private RectTransform LPDisk;      // 회전연출을 위한 변수
    [SerializeField] private GameObject Menu;
    [SerializeField] private UnityEvent AnimationStart;
    [SerializeField] private UnityEvent AnimationStop;

    private float startAngle = 0f;  // 회전 시작 각
    private float endAngle = 0f;    // 회전 끝 각
    private float angle;            // 현재 각
    private bool isChange = false;  // 음악 변경 중 인지 확인
    private static int index = 0;   // 음악 골라줄 인덱서
    private AudioData audioData;    // 곡 정보를 불러올 데이터
    private float timer = 0f;       // 선형 보간을 위한 타이머 변수
    void Start()
    {
        // index 값을 통해 음악 변경
        ChangeSong(index);
    }

    void Update()
    {
        // 클립 변경중
        if (isChange)
        {
            // 아직 덜 돌아감
            if (timer < 1f)
            {
                // 타이머에 시간 연산
                timer += Time.deltaTime * 2f;

                // 타이머에 따른 각도 반환
                angle = Mathf.LerpAngle(startAngle, endAngle, timer);

                // 회전
                LPDisk.eulerAngles = new Vector3(0, 0, angle);
            }
            else
            {
                // 타이머 0으로 되돌림
                timer = 0f;

                // 변경 완료
                isChange = false;

                // Rotate 초기화
                LPDisk.eulerAngles = new Vector3(0, 0, 0);

                // 곡 변경
                ChangeSong(index);
            }
        }
        // 클립 변경중이 아님
        else
        {
            if (Menu.activeSelf)
                return;

            // UpArrow = 다음 클립
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                // index + 1, 만약 배열 범위 벗어나면 0부터 시작
                index = (index + 1) >= Titles.Length ? 0 : index + 1;

                // 목표 각 90도로 설정
                endAngle = 90f;

                // 클립 변경중
                isChange = true;

                AnimationStop.Invoke();
            }

            // DownArrow = 이전 클립
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                // index - 1, 만약 배열 범위 벗어나면 마지막 인덱스부터 시작
                index = (index - 1) < 0 ? Titles.Length - 1 : index - 1;

                // 목표 각 90도로 설정
                endAngle = -90f;

                // 클립 변경중
                isChange = true;

                AnimationStop.Invoke();
            }

            // Enter = 클립 선택
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                // 리듬 매니저 타이틀, 클립 변경
                RhythmManager.Instance.Title = Titles[index];
                RhythmManager.Instance.AudioClip = Clips[index];

                // 씬 전환
                LoadScene.Instance.ActiveTrueFade("RhythmScene");
            }
        }
    }

    /// <summary>
    /// 클립 변경하는 함수
    /// </summary>
    /// <param name="index">변경할 인덱스</param>
    private void ChangeSong(int index)
    {
        // 타이틀 기준으로 데이터 로드
        audioData = new AudioData(Titles[index]);

        // 로드한 데이터 표시
        Title.text = audioData.Name;
        Bpm.text = audioData.BPM.ToString() + "bpm";
        Level.text = audioData.Level;

        // 해당 인덱스의 클립으로 변경 후 재생
        Sound.clip = Highlights[index];
        Sound.Play();
        AnimationStart.Invoke();
    }
}
