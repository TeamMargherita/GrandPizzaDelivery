using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public string FilePath;

    [Range(-1f, 1f)]
    public double Offset;
    public bool IsAuto;
    public AudioSource sound;

    private decimal oneBar;         // 1 마디 = 4비트
    private decimal nextBar;        // 현재 마디
    private decimal bitSlice;
    private int barCycle = 0;

    private Note notePrefab;        // 노트
    private Bar barPrefab;          // 마디

    private static Queue<Bar> Bars = new Queue<Bar>();
    private static Queue<Note> Notes = new Queue<Note>();

    private Queue<Bar> BarLoad = new Queue<Bar>();
    private Queue<Note> NoteLoad = new Queue<Note>();

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
        // 마디 바 생성


        if (Input.GetKeyDown(KeyCode.C))
        {
            int index = (int)((RhythmManager.Instance.CurrentTime - (decimal)Offset) / bitSlice);
            data.IsNote[index] = true;
        }
        if (Input.GetKey(KeyCode.V))
        {
            int index = (int)((RhythmManager.Instance.CurrentTime - (decimal)Offset) / bitSlice);
            if (data.IsNote[index])
            {
                NoteClear();
                data.IsNote[index] = false;
            }
        }
        if (NoteLoad.Count > 0)
        {
            if (Input.anyKeyDown
                && !Input.GetKeyDown(KeyCode.Escape)
                && !Input.GetKeyDown(KeyCode.C)
                && !Input.GetKeyDown(KeyCode.V))
            {
                // 노트 클리어
                if (NoteLoad.Peek().SendJudge() != Judge.None)
                    NoteClear();
            }

            // 오토 클리어
            if (IsAuto)
            {
                if (NoteLoad.Count > 0 && NoteLoad.Peek().SendJudge() == Judge.Perfect)
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

        // OneBar 연산
        oneBar = 60m / (decimal)data.BPM * 4m;

        nextBar = 0;
        bitSlice = oneBar / 32m;    // 1/32

        // 노트 생성
        CreateNote();
        // 바 생성
        CreateBar();
        barCycle = 0;
    }

    public void CreateNote()
    {
        while (NoteLoad.Count > 0)
            NoteLoadReset();
        int index = (int)((RhythmManager.Instance.CurrentTime - (decimal)Offset) / bitSlice);
        for (int i = index; i < data.IsNote.Length; i++)
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
    }

    private void CreateBar()
    {
        while (BarLoad.Count > 0)
            BarLoadReset();

        for (int i = 0; i < 5000; i++)
        {
            Bar b;
            if (Bars.Count > 0)
                b = Bars.Dequeue();
            else
                b = Instantiate(barPrefab, transform);
            b.Init(nextBar + oneBar);
            b.gameObject.SetActive(true);
            if (barCycle % 4 == 0)
            {
                b.GetComponent<SpriteRenderer>().color = new Color(0, 1, 1, 0.2f);
            }
            else
                b.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 0.2f);
            BarLoad.Enqueue(b);
            //nextBar += oneBar;
            nextBar += (oneBar / 32);
            barCycle++;
        }
    }

    private void NoteLoadReset()
    {
        Note n = NoteLoad.Peek();
        n.gameObject.SetActive(false);
        QueueSwaping(NoteLoad, Notes);
    }

    private void BarLoadReset()
    {
        Bar n = BarLoad.Peek();
        n.gameObject.SetActive(false);
        QueueSwaping(BarLoad, Bars);
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
