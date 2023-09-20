using UnityEngine;

public class Bar : MonoBehaviour
{
    [Range(-1f, 1f)]
    public float Offset;
    [Range(1f, 3f)]
    public float Speed;
    public Vector2 start;
    public Vector2 end;
    public NoteSpawner spawner;
    public decimal Arrive;
    public decimal Timing;

    private Transform pos;

    void Update()
    {
        Timing = Arrive - RhythmManager.Instance.CurrentTime;
        if (Timing > 0m)
            pos.position = Vector2.Lerp(end, start * Speed, (float)Timing / 10 * Speed);
        else
            pos.position = Vector2.Lerp(end, (end - start) * Speed, (float)-Timing / 10 * Speed);

        //if (Timing < -5m)
        //{
        //    gameObject.SetActive(false);
        //}
    }

    public void Init(decimal arriveTime)
    {
        Arrive = arriveTime;

        if (end == Vector2.zero)
            end = GameObject.Find("Judgement").GetComponent<Transform>().position;
        if (pos == null)
            pos = GetComponent<Transform>();
        if (spawner == null)
            spawner = transform.parent.GetComponent<NoteSpawner>();

        start = new Vector2(10f, 0);
    }
}
