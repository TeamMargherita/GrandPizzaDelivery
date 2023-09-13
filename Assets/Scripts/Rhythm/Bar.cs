using UnityEngine;

public class Bar : MonoBehaviour
{
    [Range(-1f, 1f)]
    public double Offset = 0f;
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
        {
            gameObject.SetActive(false);
        }
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

        start = new Vector2(3840f, 0);
    }
}
