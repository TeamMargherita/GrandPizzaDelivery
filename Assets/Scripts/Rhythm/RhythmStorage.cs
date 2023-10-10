using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmStorage : MonoBehaviour
{
    public Note NotePrefab;                         // 노트
    public Bar BarPrefab;                           // 마디
    public AudioSource NoteSound;                   // 노트 소리

    public Queue<Bar> Bars;         // 마디 오브젝트 풀
    public Queue<Note> Notes;       // 노트 오브젝트 풀
    public Queue<Bar> BarLoad;      // 나와있는 마디
    public Queue<Note> NoteLoad;    // 나와있는 노트

    void Start()
    {
        Bars = new Queue<Bar>();
        Notes = new Queue<Note>();
        BarLoad = new Queue<Bar>();
        NoteLoad = new Queue<Note>();
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
    /// 노트 클리어 함수
    /// </summary>
    public void NoteClear()
    {
        Note n = NoteLoad.Peek();
        n.ActiveEffect();
        Notes.Enqueue(NoteLoad.Dequeue());
        NoteSound.PlayOneShot(NoteSound.clip);
    }
    public void ReturnNote()
    {
        if (NoteLoad.Count > 0 && NoteLoad.Peek().Timing < -0.12501m)
        {
            Notes.Enqueue(NoteLoad.Dequeue());
            RhythmManager.Instance.Judges.Miss++;
        }
    }
    /// <summary>
    /// 나와있는 모든 노트들을 풀에 돌려놓는 함수
    /// </summary>
    public void NoteLoadReset()
    {
        while (NoteLoad.Count > 0)
        {
            Note note = NoteLoad.Peek();
            note.gameObject.SetActive(false);
            Notes.Enqueue(NoteLoad.Dequeue());
        }
    }

    /// <summary>
    /// 나와있는 모든 마디들을 풀에 돌려놓는 함수
    /// </summary>
    public void BarLoadReset()
    {
        while (BarLoad.Count > 0)
        {
            Bar bar = BarLoad.Peek();
            bar.gameObject.SetActive(false);
            Bars.Enqueue(BarLoad.Dequeue());
        }
    }
}
