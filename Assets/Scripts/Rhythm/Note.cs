using UnityEngine;

public enum Judge { None = 0, Perfect, Great, Good, Miss }

public class Note : MonoBehaviour
{
    [Range(1f, 3f)]
    public decimal Speed = 1m;
    public Vector2 start;
    public Vector2 end;
    public NoteSpawner spawner;
    public decimal Arrive;
    public decimal Timing;

    private RectTransform pos;
    void Start()
    {
        Speed = 2m;
        
    }

    void Update()
    {
        Timing = Arrive - RhythmManager.Instance.CurrentTime();
        if (Timing > 0m)
            pos.anchoredPosition = Vector2.Lerp(end, start * (float)Speed, (float)(Timing / 10 * Speed));
        else
            pos.anchoredPosition = Vector2.Lerp(end, -(start - end) * (float)Speed, (float)(-Timing / 10 * Speed));

        if (Timing < -5m)
            gameObject.SetActive(false);
    }

    public void Init(decimal arriveTime)
    {
        Arrive = arriveTime;

        if (end == Vector2.zero)
            end = GameObject.Find("Judgement").GetComponent<RectTransform>().anchoredPosition;
        if (pos == null)
            pos = GetComponent<RectTransform>();
        if (spawner == null)
            spawner = transform.parent.GetComponent<NoteSpawner>();
        Timing = Arrive - RhythmManager.Instance.CurrentTime();
        start = new Vector2(3840f, 0);
    }

    public Judge SendJudge()
    {
        if (Mathf.Abs((float)Timing) > 0.12501f)
            return Judge.None;
        else if (Mathf.Abs((float)Timing) <= 0.04167f)
            return Judge.Perfect;
        else if (Mathf.Abs((float)Timing) <= 0.08334f)
            return Judge.Great;
        else
            return Judge.Good;
    }
}
