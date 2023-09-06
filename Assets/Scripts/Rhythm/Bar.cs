using UnityEngine;

public class Bar : MonoBehaviour
{
    [Range(-1f, 1f)] 
    public double Offset = 0f;
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
    }

    void Update()
    {
        CurTime = 10 - (AudioSettings.dspTime - StartTime);
        if (CurTime > 0)
            pos.anchoredPosition = Vector2.Lerp(end, start * (float)Speed, (float)((CurTime / 10) * Speed));
        else
            pos.anchoredPosition = Vector2.Lerp(end, -start * (float)Speed, (float)((-CurTime / 10) * Speed));

        if ((float)CurTime < -1f)
        {
            spawner.BarComeBack(this);
            gameObject.SetActive(false);
        }
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
        CurTime = 10d;
    }
}
