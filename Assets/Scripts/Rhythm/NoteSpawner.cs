using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteSpawner : MonoBehaviour
{
    public Note NotePrefab;        // 노트
    public Bar BarPrefab;          // 마디
    public double BPM = 300d;        // 곡 BPM
    public double OneBit = 0d;      // 1비트당 생성 속도
    public double NextBit = 0d;      // 현재 비트
    public double OneBar = 0d;      // 1 마디 = 4비트
    public double NextBar = 0d;      // 현재 마디
    [Range(0f, 1f)]
    public double Offset;
    public bool IsAuto;
    public AudioSource sound;

    Queue<Bar> Bars = new Queue<Bar>();
    Queue<Bar> BarLoad = new Queue<Bar>();

    Queue<Note> Notes = new Queue<Note>();
    Queue<Note> NoteLoad = new Queue<Note>();

    List<double> times = new List<double>();
    int index = 0;
    void Start()
    {
        // OneBit 연산
        OneBit = BPM / 60d;
        OneBit = 1 / OneBit;

        // OneBar 연산
        OneBar = OneBit * 4d;

        NextBar = Offset;
        NextBit = Offset;

        if (sound == null)
            sound = GameObject.Find("Metronome").GetComponent<AudioSource>();

        Debug.Log(OneBit);
        // Bar 생성
        for (int i = 0; i < 30; i++)
        {
            Bar b = Instantiate(BarPrefab, transform);
            b.gameObject.SetActive(false);
            Bars.Enqueue(b);

            Note n = Instantiate(NotePrefab, transform);
            n.gameObject.SetActive(false);
            Notes.Enqueue(n);

            times.Add(Random.Range(0f, 20f));
        }
        times.Sort();
    }

    // Update is called once per frame
    void Update()
    {
        // 마디 바 생성
        if (NextBar <= RhythmManager.Instance.CurrentTime())
        {
            Bar b;
            if (Bars.Count > 0)
                b = Bars.Dequeue();
            else
                b = Instantiate(BarPrefab, transform);
            b.Init();
            b.gameObject.SetActive(true);
            b.GetComponent<Image>().color = Color.cyan;
            BarLoad.Enqueue(b);
            NextBar += OneBar;
        }

        // 임의 값 대로 노트 생성
        if (index < times.Count && times[index] <= RhythmManager.Instance.CurrentTime())
        {
            Note n;
            if (Notes.Count > 0)
                n = Notes.Dequeue();
            else
                n = Instantiate(NotePrefab, transform);
            n.Init();
            n.gameObject.SetActive(true);
            n.GetComponent<Image>().color = Color.red;
            NoteLoad.Enqueue(n);
            index++;
        }

        if (NoteLoad.Count > 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // 노트 클리어
                if (NoteLoad.Peek().SendJudge() != Judge.None)
                    NoteClear();
            }
            // 오토 클리어
            if (IsAuto)
            {
                if (NoteLoad.Peek().SendJudge() == Judge.Perfect)
                    NoteClear();
            }
        }

        // 나와있는 노트와 바를 다시 오브젝트 풀에 저장
        if (BarLoad.Count > 0 && BarLoad.Peek().CurTime < -0.12501f)
            QueueSwaping(BarLoad, Bars);

        if (NoteLoad.Count > 0 && NoteLoad.Peek().CurTime < -0.12501f)
            QueueSwaping(NoteLoad, Notes);
    }

    private void NoteClear()
    {
        Note n = NoteLoad.Peek();
        n.gameObject.SetActive(false);
        QueueSwaping(NoteLoad, Notes);
        sound.PlayOneShot(sound.clip);
    }

    private void QueueSwaping<T>(Queue<T> start, Queue<T> end)
    {
        end.Enqueue(start.Dequeue());
    }
}
