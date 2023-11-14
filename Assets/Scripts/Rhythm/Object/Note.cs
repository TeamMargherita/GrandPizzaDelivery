using UnityEngine;

/// <summary>
/// 판정
/// </summary>
public enum Judge { NONE = 0, PERFECT, GREAT, GOOD, MISS }

/// <summary>
/// 노트
/// </summary>
public class Note : MonoBehaviour
{
    public decimal Timing { set { timing = value; } get { return timing; } }    // 남은 시간 반환
    public NoteType Type;
    private float speed;                                    // 이동 속도
    private decimal arrive;                                 // 도착 시간
    private decimal timing;                                 // 남은 시간
    private Vector2 end;                                    // 도착 위치
    private Transform trans;                                // 자신의 transform 정보를 활용하기 위한 캐싱
    private RhythmManager manager;                          // 리듬 매니저 캐싱

    private void Update()
    {
        // 게임 매니저 값과 도착 시간을 통해 노트 타이밍 초기화
        // 타이밍 = 도착시간 - 현재시간
        timing = arrive - manager.CurrentTime;

        // 노트 움직임
        NoteMove();

        // 지나간 노트 드랍
        NoteDrop();
    }

    /// <summary>
    /// 변수 초기화 함수
    /// </summary>
    /// <param name="arriveTime">도착 시간</param>
    public void Init(decimal arriveTime, Vector2 _end)
    {
        // 리듬 매니저 캐싱
        manager = RhythmManager.Instance;

        // transform 캐싱 안됬을 시 예외처리
        if (trans == null)
            trans = GetComponent<Transform>();

        // 도착시간 설정
        arrive = arriveTime;

        // 노트 재사용 시 timing에 의한 오류 해결을 위한 초기화(timing이 생성과 동시에 드랍되는 문제)
        timing = 300m;

        // 목적지 지정
        end = _end;
    }

    /// <summary>
    /// 판정 전달 함수
    /// </summary>
    /// <returns>판정</returns>
    public Judge SendJudge()
    {
        if (Mathf.Abs((float)timing) > 0.12501f)
            return Judge.NONE;
        else if (Mathf.Abs((float)timing) <= 0.065f)
            return Judge.PERFECT;
        else if (Mathf.Abs((float)timing) <= 0.105f)
            return Judge.GREAT;
        else
            return Judge.GOOD;
    }

    /// <summary>
    /// 노트 이동
    /// </summary>
    private void NoteMove()
    {
        // 속도 동기화
        speed = manager.Speed;

        // 노트 위치 = 목적지 + (right * timing * speed * 보정)
        trans.localPosition = end + Vector2.right * (float)timing * speed * 5f;
    }

    /// <summary>
    /// 지나간 노트 드랍
    /// </summary>
    private void NoteDrop()
    {
        if (timing < -0.12501m)
        {
            gameObject.SetActive(false);
        }
    }
}
