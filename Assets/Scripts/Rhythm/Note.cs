using UnityEngine;

public enum Judge { None = 0, Perfect, Great, Good, Miss }

public class Note : MonoBehaviour
{
    [Range(1f, 3f)]
    public float Speed;
    public Vector2 start;
    public Vector2 end;
    public NoteSpawner spawner;
    public decimal Arrive;
    public decimal Timing = 10m;
    public double see;

    private Transform pos;

    void Update()
    {
        Timing = Arrive - RhythmManager.Instance.CurrentTime;
        see = (double)Timing;
        if (Timing > 0m)
            pos.position = Vector2.Lerp(end, start * Speed, (float)Timing / 10 * Speed);
        else
            pos.position = Vector2.Lerp(end, (end - start) * Speed, (float)-Timing / 10 * Speed);

        if (Timing < -5m)
        {
            Debug.Log("Delete Note");
            gameObject.SetActive(false);
        }
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
        Timing = Arrive - RhythmManager.Instance.CurrentTime;
        start = new Vector2(10f, 0);
    }

    public Judge SendJudge()
    {
        if (Mathf.Abs((float)Timing) > 0.12501f)
            return Judge.None;
        //else if (Mathf.Abs((float)Timing) <= 0.04167f)
        //    return Judge.Perfect;
        else if ((float)Timing <= 0f)
            return Judge.Perfect;
        else if (Mathf.Abs((float)Timing) <= 0.08334f)
            return Judge.Great;
        else
            return Judge.Good;
    }
}
