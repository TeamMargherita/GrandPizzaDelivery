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
    private Transform trans;
    private RhythmManager manager;

    private void Update()
    {
        timing = arrive - manager.CurrentTime;
        NoteMove();
        NoteDrop();
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
    /// 판정 전달 함수
    /// </summary>
    /// <returns>판정</returns>
    public Judge SendJudge()
    {
        if (Mathf.Abs((float)timing) > 0.12501f)
            return Judge.NONE;
        else if (Mathf.Abs((float)timing) <= 0.04167f)
            return Judge.PERFECT;
        else if (Mathf.Abs((float)timing) <= 0.08334f)
            return Judge.GREAT;
        else
            return Judge.GOOD;
    }

    /// <summary>
    /// 노트 이동
    /// </summary>
    private void NoteMove()
    {
        speed = manager.Speed;
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
