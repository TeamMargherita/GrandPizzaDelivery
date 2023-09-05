using UnityEngine;

public enum Judge { None = 0, Perfect, Great, Good, Miss }

public class Note : MonoBehaviour
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
        start = pos.parent.GetComponent<RectTransform>().anchoredPosition;
        StartTime = AudioSettings.dspTime;
    }

    void Update()
    {
        CurTime = 10 - (AudioSettings.dspTime - StartTime);
        if (StartTime > 0)
            pos.anchoredPosition = Vector2.Lerp(start * (float)Speed, end, (float)((CurTime / 10) * Speed));
        else
            pos.anchoredPosition = Vector2.Lerp(start * (float)Speed, end, (float)((CurTime / 10) * -Speed));
    }

    public Judge SendJudge()
    {
        if (Mathf.Abs((float)CurTime) < 0.1f)
            return Judge.Perfect;
        else if (Mathf.Abs((float)CurTime) < 0.4f)
            return Judge.Great;
        else if (Mathf.Abs((float)CurTime) < 1f)
            return Judge.Good;
        else
            return Judge.None;
    }
}
