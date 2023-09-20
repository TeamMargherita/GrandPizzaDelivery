using UnityEngine;

public class Bar : MonoBehaviour
{
    public decimal Timing { get { return timing; } }

    private float speed;
    private decimal arrive;
    private decimal timing;
    private Vector2 start;
    private Vector2 end;
    private NoteSpawner spawner;
    private Transform trans;

    void Update()
    {
        timing = arrive - RhythmManager.Instance.CurrentTime;
        BarMove();
    }

    public void Init(decimal arriveTime)
    {
        FindCompnent();
        arrive = arriveTime;
        start = new Vector2(10f, 0);
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

    private void BarMove()
    {
        if (timing > 0m)
            trans.position = Vector2.Lerp(end, start * speed, (float)timing / 10 * speed);
        else
            trans.position = Vector2.Lerp(end, (end - start) * speed, (float)-timing / 10 * speed);
    }
}
