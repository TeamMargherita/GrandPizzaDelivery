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
    private bool Effect;
    private float fade = 1f;

    private void Update()
    {
        if (Effect)
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, fade);
            fade -= Time.deltaTime * 5f;
            if (fade <= 0f)
                gameObject.SetActive(false);
        }
        else
        {
            timing = arrive - RhythmManager.Instance.CurrentTime;
            NoteMove();
            NoteDrop();
        }
    }

    /// <summary>
    /// 변수 초기화 함수
    /// </summary>
    /// <param name="arriveTime">도착 시간</param>
    public void Init(decimal arriveTime, Vector2 _end)
    {
        if (trans == null)
            trans = GetComponent<Transform>();
        GetComponent<SpriteRenderer>().color = Color.white;
        Effect = false;
        arrive = arriveTime;
        fade = 1f;
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

    public void ActiveEffect()
    {
        Effect = true;
    }

    /// <summary>
    /// 노트 이동
    /// </summary>
    private void NoteMove()
    {
        speed = RhythmManager.Instance.Speed;
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
