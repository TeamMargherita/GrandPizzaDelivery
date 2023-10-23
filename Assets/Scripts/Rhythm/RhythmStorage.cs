using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 리듬게임 오브젝트를 저장할 저장소 클래스
/// </summary>
public class RhythmStorage : MonoBehaviour
{
    public Note NotePrefab;                         // 노트
    public Bar BarPrefab;                           // 마디

    public Queue<Bar> Bars = new Queue<Bar>();          // 마디 오브젝트 풀
    public Queue<Note> Notes = new Queue<Note>();       // 노트 오브젝트 풀

    public Queue<Bar>[] BarLoad = new Queue<Bar>[2];       // 나와있는 마디
    public Queue<Note>[] NoteLoad = new Queue<Note>[2];    // 나와있는 노트

    private RhythmManager manager;                  // 리듬 매니저 캐싱

    private void Awake()
    {
        for (int i = 0; i < 2; i++)
        {
            BarLoad[i] = new Queue<Bar>();
            NoteLoad[i] = new Queue<Note>();
        }
    }

    private void Start()
    {
        // 리듬 매니저 캐싱
        manager = RhythmManager.Instance;
    }

    private void Update()
    {
        // 나와있는 노트와 바를 다시 오브젝트 풀에 저장
        ReturnNote();
    }

    public Note DequeueNote()
    {
        Note note;

        // 오브젝트 풀에 노트가 존재 (재사용)
        if (Notes.Count > 0)
            note = Notes.Dequeue();

        // 오브젝트 풀에 노트가 존재하지 않음 (새로 생성)
        else
            note = Instantiate(NotePrefab, transform);

        return note;
    }

    public Bar DequeueBar()
    {
        Bar bar;

        // 오브젝트 풀에 마디가 존재 (재사용)
        if (Bars.Count > 0)
            bar = Bars.Dequeue();

        // 오브젝트 풀에 마디가 존재하지 않음 (새로 생성)
        else
            bar = Instantiate(BarPrefab, transform);

        return bar;
    }

    /// <summary>
    /// 입력받은 라인의 노트를 클리어 하는 함수
    /// </summary>
    public void NoteClear(int line)
    {
        // 해당 라인의 큐에 담긴 노트 담기
        Note n = NoteLoad[line].Peek();

        // 해당 노트 비활성화
        n.gameObject.SetActive(false);

        // 노트를 다시 오브젝트 풀에 담기
        Notes.Enqueue(NoteLoad[line].Dequeue());
    }

    /// <summary>
    /// 나와있는 오래된 노트를 돌려받는 함수
    /// </summary>
    public void ReturnNote()
    {
        // 모든 로드 탐색
        foreach (var load in NoteLoad)
        {
            // 해당 로드에 있는 노트가 지나간 노트면 돌려받고 Miss 카운팅
            if (load.Count > 0 && load.Peek().Timing < -0.12501m)
            {
                Notes.Enqueue(load.Dequeue());
                manager.Judges.Miss++;
            }
        }
    }

    /// <summary>
    /// 나와있는 모든 노트들을 풀에 돌려놓는 함수
    /// </summary>
    public void NoteLoadReset()
    {
        // 모든 로드 탐색
        foreach (var load in NoteLoad)
        {
            // 로드에 남은 노트가 없어질 때 까지 오브젝트 풀에 돌려주기
            while (load.Count > 0)
            {
                Note note = load.Peek();
                note.gameObject.SetActive(false);
                Notes.Enqueue(load.Dequeue());
            }
        }
    }

    /// <summary>
    /// 나와있는 모든 마디들을 풀에 돌려놓는 함수
    /// </summary>
    public void BarLoadReset()
    {
        // 모든 로드 탐색
        foreach (var load in BarLoad)
        {
            // 로드에 남은 노트가 없어질 때 까지 오브젝트 풀에 돌려주기
            while (load.Count > 0)
            {
                Bar bar = load.Peek();
                bar.gameObject.SetActive(false);
                Bars.Enqueue(load.Dequeue());
            }
        }
    }
}
