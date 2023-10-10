using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteClear : MonoBehaviour
{
    public bool IsAuto;                     // 노트 자동 클리어

    private RhythmManager manager;
    private RhythmStorage storage;


    void Start()
    {
        manager = RhythmManager.Instance;
        storage = manager.Storage;
    }

    void Update()
    {
        // 나와있는 노트가 존재
        if (storage.NoteLoad.Count > 0)
        {
            if (storage.NoteLoad.Peek().Type == NoteType.Normal)
            {
                // 노트 클리어용 키 바인딩 [A S ; ']
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) ||
                    Input.GetKeyDown(KeyCode.Semicolon) || Input.GetKeyDown(KeyCode.Quote))
                {
                    // 노트 클리어
                    if (storage.NoteLoad.Peek().SendJudge() != Judge.NONE)
                    {
                        JudgeCount();
                        storage.NoteClear();
                    }
                }
            }
            if (storage.NoteLoad.Peek().Type == NoteType.Hold)
            {
                // 노트 클리어용 키 바인딩 [A S ; ']
                if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
                {
                    // 노트 클리어
                    if (storage.NoteLoad.Peek().SendJudge() == Judge.PERFECT || storage.NoteLoad.Peek().Timing < 0)
                    {
                        JudgeCount();
                        storage.NoteClear();
                    }
                }
            }
            // 오토 클리어
            if (IsAuto)
            {
                if (storage.NoteLoad.Count > 0 && storage.NoteLoad.Peek().SendJudge() == Judge.PERFECT)
                {
                    JudgeCount();
                    storage.NoteClear();
                }
            }
        }
    }

    /// <summary>
    /// 받은 판정을 카운트 해주는 함수
    /// </summary>
    private void JudgeCount()
    {
        switch (storage.NoteLoad.Peek().SendJudge())
        {
            case Judge.PERFECT:
                manager.Judges.Perfect++;
                break;
            case Judge.GREAT:
                manager.Judges.Great++;
                break;
            case Judge.GOOD:
                manager.Judges.Good++;
                break;
            case Judge.MISS:
                manager.Judges.Miss++;
                break;
            default:
                Debug.LogError("잘못된 정보 (Judge)");
                return;
        }
    }
}
