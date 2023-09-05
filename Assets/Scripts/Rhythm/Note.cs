using UnityEngine;

public class Note : MonoBehaviour
{
    public Vector2 start;
    public Vector2 end;
    public float timer = 10f;
    [Range(1f, 5f)] public float speed = 1f;
    RectTransform pos;

    void Start()
    {
        end = GameObject.Find("Destroyer").GetComponent<RectTransform>().anchoredPosition;
        start = GetComponent<RectTransform>().anchoredPosition;
        pos = GetComponent<RectTransform>();
    }

    void Update()
    {
        //timer -= Time.deltaTime;
        if (timer > 0)
            pos.anchoredPosition = Vector2.Lerp(end, start * speed, (timer / 10) * speed);
        if (timer <= 0)
            pos.anchoredPosition = Vector2.Lerp(end, -start * speed, (timer / 10) * -speed);
        //Debug.Log(timer / 10 * speed);
    }
}
