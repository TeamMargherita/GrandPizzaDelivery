using UnityEngine;

public class ClearEffect : MonoBehaviour
{
    public float Speed = 3f;
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
        switch (judge)
        {
            case Judge.PERFECT:
                endColor = color[0];
                timer = 1f;
                break;
            case Judge.GREAT:
                endColor = color[1];
                timer = 1f;
                break;
            case Judge.GOOD:
                endColor = color[2];
                timer = 1f;
                break;
            default:
                break;
        }
    }
}
