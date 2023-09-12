using UnityEngine;

public enum Judge { None = 0, Perfect, Great, Good, Miss }

public class Note : MonoBehaviour
{
    [Range(1f, 3f)]
    public double Speed = 1f;
    public double StartTime;
    public double CurTime;
    public Vector2 start;
    public Vector2 end;
    public NoteSpawner spawner;

    private RectTransform pos;
    void Start()
    {
        Init();
        Speed = 2f;
    }

    void Update()
    {
        CurTime = 6d - (AudioSettings.dspTime - StartTime);
        if (CurTime > 0)
            pos.anchoredPosition = Vector2.Lerp(end, start * (float)Speed, (float)((CurTime / 10) * Speed));
        else
            pos.anchoredPosition = Vector2.Lerp(end, -(start - end) * (float)Speed, (float)((-CurTime / 10) * Speed));

        if (CurTime < -5f)
            gameObject.SetActive(false);
    }

    public void Init()
    {
        if (end == Vector2.zero)
            end = GameObject.Find("Judgement").GetComponent<RectTransform>().anchoredPosition;
        if (pos == null)
            pos = GetComponent<RectTransform>();
        if (spawner == null)
            spawner = transform.parent.GetComponent<NoteSpawner>();

        start = new Vector2(3840f, 0);
        StartTime = AudioSettings.dspTime;
        CurTime = 6d;
    }

    public Judge SendJudge()
    {
        if (Mathf.Abs((float)CurTime) > 0.12501f)
            return Judge.None;
        else if (Mathf.Abs((float)CurTime) <= 0.04167f)
            return Judge.Perfect;
        else if (Mathf.Abs((float)CurTime) <= 0.08334f)
            return Judge.Great;
        else
            return Judge.Good;
    }
}
