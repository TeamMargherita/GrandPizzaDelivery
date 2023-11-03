using UnityEngine;

public class NoteClear : MonoBehaviour
{
    public bool IsAuto;                     // 노트 자동 클리어
    public AudioSource NoteSound;           // 키음 불러올 오디오 소스
    public RhythmStorage storage;           // 노트를 클리어하며 돌려보낼 저장소 캐싱
    public ClearEffect[] Effects;           // 노트 클리어 시 이펙트 출력할 판정판
    public AudioSource BgSound;             // 배경음 불러올 오디오 소스
    private RhythmManager manager;          // 리듬 매니저 캐싱
    private Judge judge;                    // 판정 정보
    private KeyCode[] clearKeys;

    private void Start()
    {
        // 리듬 매니저 캐싱
        manager = RhythmManager.Instance;
        KeyMapping();
    }

    private void Update()
    {
        // 플레이 중이 아니면 동작 x
        if (!BgSound.isPlaying)
            return;

        // 모든 로드 탐색
        for (int i = 0; i < storage.NoteLoad.Length; i++)
        {
            // 해당 로드에 남은 노트가 존재할 시
            if (storage.NoteLoad[i].Count > 0)
            {
                // 해당 로드의 판정 전달
                judge = storage.NoteLoad[i].Peek().SendJudge();

                // 로드에 맞는 입력과 판정이 유효한 경우
                if (KeyDownInput(i) && judge != Judge.NONE)
                {
                    Clear(i);
                }
                // 맨 앞 노트의 타입이 홀드 타입일 시
                else if (storage.NoteLoad[i].Peek().Type == NoteType.Hold)
                {
                    // 로드에 맞는 키 입력 유지중 이며 해당 판정이 정확할 경우
                    if (KeyHoldInput(i) && (judge == Judge.PERFECT || storage.NoteLoad[i].Peek().Timing <= 0))
                    {
                        Clear(i);
                    }
                }
            }
            // 오토 클리어
            if (IsAuto)
            {
                // 자동 플레이를 위한 조건
                if (storage.NoteLoad[i].Count > 0 && storage.NoteLoad[i].Peek().Timing <= 0 && (float)storage.NoteLoad[i].Peek().Timing > -0.12501f)
                {
                    Clear(i);
                }
            }
        }
    }

    public void KeyMapping()
    {
        clearKeys = new KeyCode[4];
        if (manager.ClearKeys.Length > 0)
        {
            for (int i = 0; i < manager.ClearKeys.Length; i++)
            {
                clearKeys[i] = manager.ClearKeys[i];
            }
        }
    }

    /// <summary>
    /// 받은 판정을 카운트 해주는 함수
    /// </summary>
    private void JudgeCount(Judge judge)
    {
        switch (judge)
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

    private bool KeyDownInput(int index)
    {
        // 1 라인 : [0] [1]
        if (index == 0)
        {
            return Input.GetKeyDown(clearKeys[0]) || Input.GetKeyDown(clearKeys[1]);
        }

        // 2 라인 : [2] [3]
        else if (index == 1)
        {
            return Input.GetKeyDown(clearKeys[2]) || Input.GetKeyDown(clearKeys[3]);
        }
        else
            return false;
    }

    private bool KeyHoldInput(int index)
    {
        // 1 라인 : [0] [1]
        if (index == 0)
        {
            return Input.GetKey(clearKeys[0]) || Input.GetKey(clearKeys[1]);
        }

        // 2 라인 : [2] [3]
        else if (index == 1)
        {
            return Input.GetKey(clearKeys[2]) || Input.GetKey(clearKeys[3]);
        }
        else
            return false;
    }

    private void Clear(int index)
    {
        // 노트 클리어
        JudgeCount(judge);

        // 노트 복귀
        storage.NoteClear(index);

        // 판정판에 판정 전달
        Effects[index].GetJudge(judge);

        // 키음 출력
        NoteSound.PlayOneShot(NoteSound.clip);
    }
}
