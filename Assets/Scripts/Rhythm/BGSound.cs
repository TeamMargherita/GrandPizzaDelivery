using UnityEngine;
using System.Collections;

/// <summary>
/// 리듬게임의 배경 노래 출력과 관련된 클래스
/// </summary>
public class BGSound : MonoBehaviour
{
    public float Timer = 2f;        // 시작 딜레이
    private float timer;            // 타이머 연산을 위한 변수
    private AudioSource source;     // 음악 재생을 위한 오디오 소스 캐싱
    private RhythmManager manager;  // 매니저 캐싱
    
    private void Awake()
    {
        // 오디오 소스 캐싱
        source = GetComponent<AudioSource>();

        // 타이머 초기화
        timer = Timer;
    }
    private void Start()
    {
        // 매니저 캐싱
        manager = RhythmManager.Instance;

        // 노래를 매니저에 있는 클립으로 설정
        source.clip = manager.AudioClip;

        // 코루틴을 통한 플레이 지연
        StartCoroutine(WaitPlaying());
    }

    private void Update()
    {
        // 볼륨을 매니저 값으로 동기화
        source.volume = manager.MusicSound;

        // 시작 딜레이가 아직 도는중(시작 x)
        if(timer > 0f)
        {
            // 타이머를 시간만큼 빼주기
            timer -= Time.deltaTime;

            // 매니저의 현재시간을 타이머 변수로 잡아줌
            manager.CurrentTime = -(decimal)timer;
        }
        else
            // 노래 재생 시간 동기화
            manager.CurrentTime = (decimal)source.time;
    }

    /// <summary>
    /// 플레이 지연 코루틴
    /// </summary>
    /// <returns>지연시간</returns>
    private IEnumerator WaitPlaying()
    {
        // 타이머 시간만큼 기다렸다가 오디오 실행
        yield return new WaitForSeconds(Timer);
        source.Play();
    }
}
