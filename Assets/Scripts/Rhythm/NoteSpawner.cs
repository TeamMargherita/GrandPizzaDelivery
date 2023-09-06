using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class NoteSpawner : MonoBehaviour
{
    public Note NotePrefab;        // 노트
    public Bar BarPrefab;          // 마디
    public double BPM = 60d;        // 곡 BPM
    public double OneBit = 0d;      // 1비트당 생성 속도
    public double CurBit = 0d;      // 현재 비트
    public double OneBar = 0d;      // 1 마디 = 4비트
    public double CurBar = 0d;      // 현재 마디
    [Range(0f, 1f)]
    public double Offset;

    Queue<Bar> Bars = new Queue<Bar>();
    Queue<Note> Notes = new Queue<Note>();

    void Start()
    {
        // OneBit 연산
        OneBit = BPM / 60d;
        OneBit = 1 / OneBit;

        // OneBar 연산
        OneBar = OneBit * 4d;

        CurBar = Offset;
        CurBit = Offset;
        Debug.Log(OneBit);
        // Bar 생성
        for(int i = 0; i < 30; i++)
        {
            Bar b = Instantiate(BarPrefab, transform);
            b.gameObject.SetActive(false);
            Bars.Enqueue(b);

            Note n = Instantiate(NotePrefab, transform);
            n.gameObject.SetActive(false);
            Notes.Enqueue(n);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 마디 바 생성
        if (CurBar <= RhythmManager.Instance.CurrentTime())
        {
            Bar b;
            if (Bars.Count > 0)
                b = Bars.Dequeue();
            else
                b = Instantiate(BarPrefab, transform);
            b.Init();
            b.gameObject.SetActive(true);
            b.GetComponent<Image>().color = Color.cyan;
            CurBar += OneBar;
        }

        // 노트 생성 BPM 대로
        if (CurBit <= RhythmManager.Instance.CurrentTime())
        {
            Note n;
            if (Notes.Count > 0)
                n = Notes.Dequeue();
            else
                n = Instantiate(NotePrefab, transform);
            n.Init();
            n.gameObject.SetActive(true);
            n.GetComponent<Image>().color = Color.red;
            CurBit += OneBit;
        }

        // 임의 값 대로 생성
    }

    public void BarComeBack(Bar bar)
    {
        Bars.Enqueue(bar);
    }
    public void NoteComeBack(Note note)
    {
        Notes.Enqueue(note);
    }
}
