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
    private Transform trans;
    private RhythmManager manager;
    void Update()
    {
        timing = arrive - manager.CurrentTime;
        BarMove();
    }

    /// <summary>
    /// 변수 초기화 함수
    /// </summary>
    /// <param name="arriveTime">도착 시간</param>
    public void Init(decimal arriveTime, Vector2 _end)
    {
        manager = RhythmManager.Instance;
        if (trans == null)
            trans = GetComponent<Transform>();
        arrive = arriveTime;
        timing = 300m;
        end = _end;
    }

    /// <summary>
    /// 바 이동
    /// </summary>
    private void BarMove()
    {
        speed = manager.Speed;
        trans.localPosition = end + Vector2.right * (float)timing * speed * 5f;
    }
}
