using UnityEngine;

/// <summary>
/// 박자 체크 용 바 클래스
/// </summary>
public class Bar : MonoBehaviour
{
    public decimal Timing { get { return timing; } }    // 남은 시간 반환

    private float speed;                                // 이동 속도
    private decimal arrive;                             // 도착 시간
    private decimal timing;                             // 남은 시간
    private Vector2 end = new Vector2(-8f, 0f);         // 도착 위치
    private Transform trans;                            // 자신의 transform 활용을 위한 캐싱
    private RhythmManager manager;                      // 리듬 매니저 캐싱
    void Update()
    {
        // 타이밍 = 도착시간 - 현재시간
        timing = arrive - manager.CurrentTime;

        // 마디 이동
        BarMove();
    }

    /// <summary>
    /// 변수 초기화 함수
    /// </summary>
    /// <param name="arriveTime">도착 시간</param>
    public void Init(decimal arriveTime, Vector2 _end)
    {
        // 리듬 매니저 캐싱
        manager = RhythmManager.Instance;

        // trasform 캐싱 안됬을 때를 위한 예외터리
        if (trans == null)
            trans = GetComponent<Transform>();

        // 도착시간 설정
        arrive = arriveTime;

        // 노트 재사용 시 timing에 의한 오류 해결을 위한 초기화(timing이 생성과 동시에 드랍되는 문제)
        timing = 300m;

        // 목적지 설정
        end = _end;
    }

    /// <summary>
    /// 바 이동
    /// </summary>
    private void BarMove()
    {
        // 속도 동기화
        speed = manager.Speed;

        // 노트 위치 = 목적지 + (right * timing * speed * 보정)
        trans.localPosition = end + Vector2.right * (float)timing * speed * 5f;
    }
}
