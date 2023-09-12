using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
class AudioData
{
    public string Name;         // 곡 이름
    public decimal BPM;         // 곡 BPM
    public decimal Length;      // 곡 길이
    public bool[] IsNote;     // 노트 생성 시간

    public AudioData(string name)
    {

    }
}
public class RhythmManager : MonoBehaviour
{
    public static RhythmManager Instance = null;
    [Range(0f, 1f)]
    public decimal Offset;
    public AudioSource sound;
    public Note NotePrefab;        // 노트
    public Bar BarPrefab;          // 마디

    public decimal BPM = 120m;        // 곡 BPM
    public decimal OneBar = 0m;      // 1 마디 = 4비트
    public decimal NextBar = 0m;      // 현재 마디

    private decimal StartTime = 0m;
    private decimal currentTime;

    private Queue<Bar> Bars = new Queue<Bar>();
    private Queue<Bar> BarLoad = new Queue<Bar>();

    private Queue<Note> Notes = new Queue<Note>();
    private Queue<Note> NoteLoad = new Queue<Note>();

    private AudioData data;
    private Note n;


    private decimal bitSlice;
    private void Awake()
    {
        if (Instance != null)
            Destroy(this);

        Instance = this;
        // Object Initialize
        for (int i = 0; i < 30; i++)
        {
            Bar b = Instantiate(BarPrefab, transform);
            b.gameObject.SetActive(false);
            Bars.Enqueue(b);

            n = Instantiate(NotePrefab, transform);
            n.gameObject.SetActive(false);
            Notes.Enqueue(n);
        }
        //Init();
        DontDestroyOnLoad(Instance);
    }

    public  void Init()
    {
        // OneBar 연산
        OneBar = 60m / BPM * 4m;

        NextBar = Offset;
        bitSlice = (60m / data.BPM) / 4m;    // 1/16
        // 노트 생성
        for (int i = 0; i < data.IsNote.Length; i++)
        {
            if (data.IsNote[i])
            {
                if (Notes.Count > 0)
                    n = Notes.Dequeue();
                else
                    n = Instantiate(NotePrefab, transform);
                n.Init(bitSlice * i);
                n.gameObject.SetActive(true);
                n.GetComponent<Image>().color = Color.red;
                NoteLoad.Enqueue(n);
            }
        }

        // 시작 시간 설정
        StartTime = (decimal)AudioSettings.dspTime;
    }

    public decimal CurrentTime()
    {
        currentTime = (decimal)AudioSettings.dspTime - StartTime;
        return currentTime;
    }
    private void NoteClear()
    {
        Note n = NoteLoad.Peek();
        n.gameObject.SetActive(false);
        QueueSwaping(NoteLoad, Notes);
        sound.PlayOneShot(sound.clip);
    }

    public void CreateBar()
    {
        if (NextBar <= RhythmManager.Instance.CurrentTime())
        {
            Bar b;
            if (Bars.Count > 0)
                b = Bars.Dequeue();
            else
                b = Instantiate(BarPrefab, transform);
            b.Init(NextBar + 6m);
            b.gameObject.SetActive(true);
            b.GetComponent<Image>().color = Color.cyan;
            BarLoad.Enqueue(b);
            NextBar += OneBar;
        }
    }
    public void QueueSwaping<T>(Queue<T> start, Queue<T> end)
    {
        end.Enqueue(start.Dequeue());
    }
}
