using UnityEngine;

public class ClearEffect : MonoBehaviour
{
    public float Speed = 3f;
    public GameObject Ring;
    private SpriteRenderer render;
    private Color[] color;
    private Color startColor;
    private Color endColor;
    private float timer = 0f;
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        color = new Color[] { Color.cyan, Color.green, Color.yellow };
        startColor = Color.white;
    }

    void Update()
    {
        if (timer <= 0)
            return;
        render.color = Color.Lerp(startColor, endColor, timer);
        timer -= Time.deltaTime * Speed;
    }
    public void GetJudge(Judge judge)
    {
        GameObject ring;
        switch (judge)
        {
            case Judge.PERFECT:
                endColor = color[0];
                Ring.GetComponent<SpriteRenderer>().color = color[0];
                ring = Instantiate(Ring, transform);
                ring.transform.localPosition = Vector2.zero;
                timer = 1f;
                break;
            case Judge.GREAT:
                endColor = color[1];
                Ring.GetComponent<SpriteRenderer>().color = color[1];
                ring = Instantiate(Ring, transform);
                ring.transform.localPosition = Vector2.zero;
                timer = 1f;
                break;
            case Judge.GOOD:
                endColor = color[2];
                Ring.GetComponent<SpriteRenderer>().color = color[2];
                ring = Instantiate(Ring, transform);
                ring.transform.localPosition = Vector2.zero;
                timer = 1f;
                break;
            default:
                break;
        }
    }
}
