using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteSpawner : MonoBehaviour
{
    public string FilePath;

    [Range(0f, 1f)]
    public double Offset;
    public bool IsAuto;
    public AudioSource sound;

    private decimal bpm;            // 곡 BPM
    private decimal oneBar;         // 1 마디 = 4비트
    private decimal nextBar;        // 현재 마디

    private Note notePrefab;        // 노트
    private Bar barPrefab;          // 마디

    private static Queue<Bar> Bars = new Queue<Bar>();
    private static Queue<Note> Notes = new Queue<Note>();

    private Queue<Bar> BarLoad = new Queue<Bar>();
    private Queue<Note> NoteLoad = new Queue<Note>();

    private AudioData data;
    private decimal bitSlice;
    void Start()
    {
        // Object Initialize
        if (Bars.Count == 0 && Notes.Count == 0)
        {
            for (int i = 0; i < 30; i++)
            {
                Bar b = Instantiate(barPrefab, transform);
                b.gameObject.SetActive(false);
                Bars.Enqueue(b);

                Note n = Instantiate(notePrefab, transform);
                n.gameObject.SetActive(false);
                Notes.Enqueue(n);
            }
        }

        //Init();
        //for(int i = 0; i < times.Count; i++)
        //{
        //    int divide = (int)(times[i] / bitSlice);
        //    if (times[i] % bitSlice < bitSlice / 2m)
        //        times[i] = bitSlice * divide;
        //    else
        //        times[i] = bitSlice * (divide + 1);
        //}
    }

    void Update()
    {
        // 마디 바 생성
        CreateBar();

        if (NoteLoad.Count > 0)
        {
            if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape) && !Input.GetKeyDown(KeyCode.C))
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
        if (BarLoad.Count > 0 && BarLoad.Peek().Timing < -0.12501m)
            QueueSwaping(BarLoad, Bars);

        if (NoteLoad.Count > 0 && NoteLoad.Peek().Timing < -0.12501m)
            QueueSwaping(NoteLoad, Notes);
    }
    public void Init()
    {
        if (sound == null)
            sound = GameObject.Find("Metronome").GetComponent<AudioSource>();

        notePrefab = RhythmManager.Instance.NotePrefab;
        barPrefab = RhythmManager.Instance.BarPrefab;
        data = RhythmManager.Instance.Data;

        // OneBar 연산
        oneBar = 60m / bpm * 4m;
            
        nextBar = (decimal)Offset;
        bitSlice = oneBar / 32m;    // 1/32
        // 노트 생성
        for (int i = 0; i < data.IsNote.Length; i++)
        {
            if (data.IsNote[i])
            {
                Note n;
                if (Notes.Count > 0)
                    n = Notes.Dequeue();
                else
                    n = Instantiate(notePrefab, transform);
                n.Init(bitSlice * i + (decimal)Offset);
                n.gameObject.SetActive(true);
                n.GetComponent<SpriteRenderer>().color = Color.red;
                NoteLoad.Enqueue(n);
            }
        }
        RhythmManager.Instance.SetStartTime();
    }
    private void CreateBar()
    {
        if (nextBar <= RhythmManager.Instance.GetCurrentTime())
        {
            Bar b;
            if (Bars.Count > 0)
                b = Bars.Dequeue();
            else
                b = Instantiate(barPrefab, transform);
            b.Init(nextBar + 6m);
            b.gameObject.SetActive(true);
            b.GetComponent<SpriteRenderer>().color = Color.cyan;
            BarLoad.Enqueue(b);
            nextBar += oneBar;
        }
    }

    private void NoteClear()
    {
        Note n = NoteLoad.Peek();
        n.gameObject.SetActive(false);
        Debug.Log(n.SendJudge());
        QueueSwaping(NoteLoad, Notes);
        sound.PlayOneShot(sound.clip);
    }

    private void QueueSwaping<T>(Queue<T> start, Queue<T> end)
    {
        end.Enqueue(start.Dequeue());
    }
}
