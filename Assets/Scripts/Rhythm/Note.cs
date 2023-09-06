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
    public AudioSource sound;

    private RectTransform pos;
    void Start()
    {
        Init();
    }

    void Update()
    {
        CurTime = 10 - (AudioSettings.dspTime - StartTime);
        if (StartTime > 0)
            pos.anchoredPosition = Vector2.Lerp(end, start * (float)Speed, (float)((CurTime / 10) * Speed));
        else
            pos.anchoredPosition = Vector2.Lerp(end, start * (float)Speed, (float)((CurTime / 10) * -Speed));

        if(CurTime < 0.1d)
        {
            sound.PlayOneShot(sound.clip);
            spawner.NoteComeBack(this);
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
        if(sound == null)
            sound = GameObject.Find("Metronome").GetComponent<AudioSource>();
        start = new Vector2(3840f, 0);
        StartTime = AudioSettings.dspTime;
        CurTime = 10d;
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
