using UnityEngine;

public enum Judge { NONE = 0, PERFECT, GREAT, GOOD, MISS }

public class Note : MonoBehaviour
{
    public decimal Timing { get { return timing; } }

    public float Speed;
    private decimal arrive;
    private decimal timing;
    private Vector2 start;
    private Vector2 end;
    private NoteSpawner spawner;
    private Transform trans;
    void Update()
    {
        timing = arrive - RhythmManager.Instance.CurrentTime;
        NoteMove();
        NoteDrop();
    }

    public void Init(decimal arriveTime)
    {
        FindCompnent();
        arrive = arriveTime;
        start = new Vector2(10f, 0);
    }

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

    private void FindCompnent()
    {
        if (end == Vector2.zero)
            end = GameObject.Find("Judgement").GetComponent<Transform>().position;
        if (trans == null)
            trans = GetComponent<Transform>();
        if (spawner == null)
            spawner = transform.parent.GetComponent<NoteSpawner>();
    }

    private void NoteMove()
    {
        if (timing > 0m)
            trans.position = Vector2.Lerp(end, start * Speed, (float)timing / 10 * Speed);
        else
            trans.position = Vector2.Lerp(end, (end - start) * Speed, (float)-timing / 10 * Speed);
    }

    private void NoteDrop()
    {
        if (timing < -0.12501m)
        {
            Debug.Log("Delete Note");
            gameObject.SetActive(false);
        }
    }
}
