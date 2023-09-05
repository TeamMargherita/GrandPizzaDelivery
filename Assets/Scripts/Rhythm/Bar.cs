using UnityEngine;

public class Bar : MonoBehaviour
{
    public Vector2 start;
    public Vector2 end;
    public double StartTime;
    public double CurTime;
    [Range(-1f, 1f)] public double Offset = 0f;
    [Range(1f, 3f)] public double Speed = 1f;
    RectTransform pos;

    void Start()
    {
        end = GameObject.Find("Judgement").GetComponent<RectTransform>().anchoredPosition;
        pos = GetComponent<RectTransform>();
        start = new Vector2(3840f, 0);
        StartTime = AudioSettings.dspTime;
    }

    void Update()
    {
        CurTime = 10 - (AudioSettings.dspTime - StartTime);
        if (CurTime > 0)
            pos.anchoredPosition = Vector2.Lerp(end, start * (float)Speed, (float)((CurTime / 10) * Speed));
        else
            pos.anchoredPosition = Vector2.Lerp(end, -start * (float)Speed, (float)((-CurTime / 10) * Speed));
    }
}
