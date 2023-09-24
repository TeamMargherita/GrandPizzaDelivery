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
    public decimal Timing { get { return timing; } }    // 남은 시간 반환

    private float speed;            // 이동 속도
    private decimal arrive;         // 도착 시간
    private decimal timing;         // 남은 시간
    private Vector2 start;          // 시작 위치
    private Vector2 end;            // 도착 위치
    private NoteSpawner spawner;
    private Transform trans;
    void Update()
    {
        timing = arrive - RhythmManager.Instance.CurrentTime;
        NoteMove();
        NoteDrop();
    }

    /// <summary>
    /// 변수 초기화 함수
    /// </summary>
    /// <param name="arriveTime">도착 시간</param>
    public void Init(decimal arriveTime)
    {
        FindCompnent();
        arrive = arriveTime;
        start = new Vector2(10f, 0);
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
    /// 할당받지 못한 컴포넌트 찾아서 할당
    /// </summary>
    private void FindCompnent()
    {
        if (end == Vector2.zero)
            end = GameObject.Find("Judgement").GetComponent<Transform>().localPosition;
        if (trans == null)
            trans = GetComponent<Transform>();
        if (spawner == null)
            spawner = transform.parent.GetComponent<NoteSpawner>();
    }

    /// <summary>
    /// 노트 이동
    /// </summary>
    private void NoteMove()
    {
        speed = RhythmManager.Instance.Speed;
        if (timing > 0m)
            trans.localPosition = Vector2.Lerp(end, start * speed, (float)timing / 10f * speed);
        else
            trans.localPosition = Vector2.Lerp(end, (end - start) * speed, (float)-timing / 10f * speed);
    }

    /// <summary>
    /// 지나간 노트 드랍
    /// </summary>
    private void NoteDrop()
    {
        if (timing < -0.12501m)
        {
            //Debug.Log("Delete Note");
            gameObject.SetActive(false);
        }
    }
}
