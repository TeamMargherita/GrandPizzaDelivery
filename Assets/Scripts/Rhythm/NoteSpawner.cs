using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [Range(-1f, 1f)]
    public static float Offset;
    public static decimal bitSlice;
    public bool IsAuto;
    public static AudioSource sound;

    private decimal oneBar;         // 1 마디 = 4비트
    private decimal nextBar;        // 현재 마디
    private int barCycle = 0;

    private Note notePrefab;        // 노트
    private Bar barPrefab;          // 마디

    public static Queue<Bar> Bars = new Queue<Bar>();
    public static Queue<Note> Notes = new Queue<Note>();

    public static Queue<Bar> BarLoad = new Queue<Bar>();
    public static Queue<Note> NoteLoad = new Queue<Note>();

    private AudioData data;
    void Start()
    {
        notePrefab = RhythmManager.Instance.NotePrefab;
        barPrefab = RhythmManager.Instance.BarPrefab;

        // Object Initialize
        if (Bars.Count == 0 && Notes.Count == 0)
        {
            for (int i = 0; i < 30; i++)
            {
                Note n = Instantiate(notePrefab, transform);
                n.gameObject.SetActive(false);
                Notes.Enqueue(n);
            }

            for (int i = 0; i < 30; i++)
            {
                Bar b = Instantiate(barPrefab, transform);
                b.gameObject.SetActive(false);
                Bars.Enqueue(b);
            }
        }
        
        //Init();
    }

    void Update()
    {
        if (data != null)
            data.Sync = Offset;

        if (NoteLoad.Count > 0)
        {
            if (Input.anyKeyDown
                && !Input.GetKeyDown(KeyCode.Escape)
                && !Input.GetKeyDown(KeyCode.C)
                && !Input.GetKeyDown(KeyCode.V))
            {
                // 노트 클리어
                if (NoteLoad.Peek().SendJudge() != Judge.NONE)
                    NoteClear();
            }

            // 오토 클리어
            if (IsAuto)
            {
                if (NoteLoad.Count > 0 && NoteLoad.Peek().SendJudge() == Judge.PERFECT)
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
        
        data = RhythmManager.Instance.Data;

        // 데이터 값 연산
        DataCalculator();

        // 노트 생성
        CreateNote();

        // 바 생성
        CreateBar();
        
    }
    public static void NoteClear()
    {
        Note n = NoteLoad.Peek();
        n.gameObject.SetActive(false);
        Debug.Log(n.SendJudge());
        QueueSwaping(NoteLoad, Notes);
        sound.PlayOneShot(sound.clip);
    }
    private void CreateNote()
    {
        // Reset
        NoteLoadReset();

        // Create
        int index = (int)((RhythmManager.Instance.CurrentTime + (decimal)Offset) / bitSlice);
        for (int i = index; i < data.IsNote.Length; i++)
        {
            if (i < 0) continue;

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
    }
    private void CreateBar()
    {
        // Reset
        BarLoadReset();
        // Create
        barCycle = 0;
        for (int i = 0; i < 5000; i++)
        {
            Bar bar;
            if (Bars.Count > 0)
                bar = Bars.Dequeue();
            else
                bar = Instantiate(barPrefab, transform);
            bar.Init(nextBar);
            bar.gameObject.SetActive(true);
            if (barCycle % 4 == 0)
            {
                bar.GetComponent<SpriteRenderer>().color = new Color(0, 1, 1, 0.5f);
            }
            else
                bar.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 0.5f);
            BarLoad.Enqueue(bar);
            nextBar += oneBar;
            //nextBar += (oneBar / 32);
            //barCycle++;
        }
    }
    private void NoteLoadReset()
    {
        while (NoteLoad.Count > 0)
        {
            Note note = NoteLoad.Peek();
            note.gameObject.SetActive(false);
            QueueSwaping(NoteLoad, Notes);
        }
    }
    private void BarLoadReset()
    {
        while (BarLoad.Count > 0)
        {
            Bar bar = BarLoad.Peek();
            bar.gameObject.SetActive(false);
            QueueSwaping(BarLoad, Bars);
        }
    }
    private static void QueueSwaping<T>(Queue<T> start, Queue<T> end)
    {
        end.Enqueue(start.Dequeue());
    }
    private void DataCalculator()
    {
        oneBar = 60m / (decimal)data.BPM * 4m;
        Offset = data.Sync;
        nextBar = (decimal)Offset;
        bitSlice = oneBar / 32m;
    }
}
