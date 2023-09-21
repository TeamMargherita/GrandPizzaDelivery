using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 노트 와 바를 생성시키는 클래스
/// </summary>
public class NoteSpawner : MonoBehaviour
{
    [Range(-1f, 1f)]
    public float Sync;                      // 곡 싱크 (추후 로직 수정 필요)
    public static decimal BitSlice;         // 1 비트를 8 등분
    public bool IsAuto;                     // 노트 자동 클리어

    public static Queue<Bar> Bars = new Queue<Bar>();       // 마디 오브젝트 풀
    public static Queue<Note> Notes = new Queue<Note>();    // 노트 오브젝트 풀

    public static Queue<Bar> BarLoad = new Queue<Bar>();    // 나와있는 마디
    public static Queue<Note> NoteLoad = new Queue<Note>(); // 나와있는 노트

    private decimal oneBar;                 // 4 비트당 1 마디
    private decimal nextBar;                // 현재 마디
    private int barCycle = 0;               // 마디 색 변경을 위한 임시 변수

    private Note notePrefab;                // 노트
    private Bar barPrefab;                  // 마디

    private RhythmManager manager;
    void Start()
    {
        manager = RhythmManager.Instance;
        notePrefab = manager.NotePrefab;
        barPrefab = manager.BarPrefab;
    }

    void Update()
    {
        if (manager.Data != null)
            manager.Data.Sync = Sync;

        // 나와있는 노트가 존재
        if (NoteLoad.Count > 0)
        {
            // 노트 클리어용 키 바인딩
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S))
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
        ReturnNote();
    }
    public void Init()
    {
        // 데이터 값 연산
        DataCalculator();

        // 노트 생성
        CreateNote();

        // 바 생성
        CreateBar();
    }

    /// <summary>
    /// 노트 클리어 함수 (추후에 다른 클래스로 이동)
    /// </summary>
    public static void NoteClear()
    {
        Note n = NoteLoad.Peek();
        n.gameObject.SetActive(false);
        Debug.Log(n.SendJudge());
        QueueSwaping(NoteLoad, Notes);
        RhythmManager.Instance.NoteSound.PlayOneShot(RhythmManager.Instance.NoteSound.clip);
    }

    /// <summary>
    /// 노트를 생성하는 함수
    /// </summary>
    public void CreateNote()
    {
        // 리셋
        NoteLoadReset();

        // 생성

        foreach (var v in manager.Data.IsNote)
        {
            // 노트가 존재함
            Note n;

            // 오브젝트 풀에 노트가 존재 (재사용)
            if (Notes.Count > 0)
                n = Notes.Dequeue();

            // 오브젝트 풀에 노트가 존재하지 않음 (새로 생성)
            else
                n = Instantiate(notePrefab, transform);

            // 노트 초기화
            n.Init(BitSlice * v.Key + (decimal)Sync);
            n.gameObject.SetActive(true);
            n.GetComponent<SpriteRenderer>().color = Color.red;

            // 노트를 NoteLoad(나와있는 노트 모음)에 추가
            NoteLoad.Enqueue(n);
        }
    }

    /// <summary>
    /// 마디를 생성하는 함수
    /// </summary>
    private void CreateBar()
    {
        // 리셋
        BarLoadReset();

        // 생성
        barCycle = 0;
        // 5000개의 마디를 생성(추후에 곡 길이에 따른 마디로 변경)
        for (int i = 0; i < 5000; i++)
        {
            Bar bar;

            // 오브젝트 풀에 마디가 존재 (재사용)
            if (Bars.Count > 0)
                bar = Bars.Dequeue();

            // 오브젝트 풀에 마디가 존재하지 않음 (새로 생성)
            else
                bar = Instantiate(barPrefab, transform);

            // 마디 초기화
            bar.Init(nextBar);
            bar.gameObject.SetActive(true);

            // 마디를 BarLoad(나와있는 마디 모음)에 추가
            BarLoad.Enqueue(bar);

            // 다음 마디로 넘어감
            //nextBar += oneBar;

            // 에디터 용 마디 생성 구문
            if (barCycle % 4 == 0)
            {
                bar.GetComponent<SpriteRenderer>().color = new Color(0, 1, 1, 0.5f);
            }
            else
                bar.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 0.5f);
            nextBar += (oneBar / 32m);
            barCycle++;
        }
    }

    /// <summary>
    /// 나와있는 모든 노트들을 풀에 돌려놓는 함수
    /// </summary>
    private void NoteLoadReset()
    {
        while (NoteLoad.Count > 0)
        {
            Note note = NoteLoad.Peek();
            note.gameObject.SetActive(false);
            QueueSwaping(NoteLoad, Notes);
        }
    }

    /// <summary>
    /// 나와있는 모든 마디들을 풀에 돌려놓는 함수
    /// </summary>
    private void BarLoadReset()
    {
        while (BarLoad.Count > 0)
        {
            Bar bar = BarLoad.Peek();
            bar.gameObject.SetActive(false);
            QueueSwaping(BarLoad, Bars);
        }
    }

    /// <summary>
    /// 지나간 노트를 돌려놓는 함수
    /// </summary>
    private void ReturnNote()
    {
        if (NoteLoad.Count > 0 && NoteLoad.Peek().Timing < -0.12501m)
            QueueSwaping(NoteLoad, Notes);
    }

    /// <summary>
    /// 큐 끼리 교환하는 함수
    /// </summary>
    /// <typeparam name="T">제네릭 타입</typeparam>
    /// <param name="start">뽑아낼 큐</param>
    /// <param name="end">넣어줄 큐</param>
    private static void QueueSwaping<T>(Queue<T> start, Queue<T> end)
    {
        end.Enqueue(start.Dequeue());
    }

    /// <summary>
    /// 데이터를 기반으로 변수 값 연산하는 함수
    /// </summary>
    private void DataCalculator()
    {
        // 60m / (decimal)data.BPM = 1 비트
        // 1 마디 = 4 비트
        oneBar = 60m / (decimal)manager.Data.BPM * 4m;
        Sync = manager.Data.Sync;
        nextBar = 0;

        // BitSlice = 비트 / 8 = 마디 / 32
        BitSlice = oneBar / 32m;
    }
}
