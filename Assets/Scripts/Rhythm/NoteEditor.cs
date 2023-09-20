using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 노트 패턴 편집 클래스
/// </summary>
public class NoteEditor : MonoBehaviour
{
    public Button FrontButton;      // 되감기 버튼
    public Button BackButton;       // 감기 버튼
    public AudioSource BgSound;     // 배경 음악

    private decimal calculator;     // 노트 배열 인덱스 계산용

    void Update()
    {
        AddNote();
        RemoveNote();
        Wind();
        SetPitch();
        Playing();
    }
    public void Front(int second)
    {
        BgSound.time = Mathf.Clamp(BgSound.time - second, 0f, BgSound.clip.length);
    }
    public void Back(int second)
    {
        BgSound.time = Mathf.Clamp(BgSound.time + second, 0f, (int)BgSound.clip.length);
    }
    public void Play()
    {
        if (!BgSound.isPlaying)
            BgSound.Play();
    }
    public void Pause()
    {
        BgSound.Pause();
    }
    public void Stop()
    {
        BgSound.Stop();
    }

    /// <summary>
    /// 노트 추가
    /// </summary>
    private void AddNote()
    {
        if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.D))
        {
            calculator = (RhythmManager.Instance.CurrentTime - (decimal)RhythmManager.Instance.Data.Sync) / NoteSpawner.BitSlice;
            int index = calculator % NoteSpawner.BitSlice < NoteSpawner.BitSlice / 2 ? (int)calculator : (int)calculator + 1;
            RhythmManager.Instance.Data.IsNote[index] = true;
        }
    }

    /// <summary>
    /// 노트 제거
    /// </summary>
    private void RemoveNote()
    {
        if (Input.GetKey(KeyCode.V))
        {
            calculator = (RhythmManager.Instance.CurrentTime - (decimal)RhythmManager.Instance.Data.Sync) / NoteSpawner.BitSlice;
            int index = calculator % NoteSpawner.BitSlice < NoteSpawner.BitSlice / 2 ? (int)calculator : (int)calculator + 1;
            if (RhythmManager.Instance.Data.IsNote[index])
            {
                if (NoteSpawner.NoteLoad.Count > 0)
                    NoteSpawner.NoteClear();
                RhythmManager.Instance.Data.IsNote[index] = false;
            }
        }
    }

    /// <summary>
    /// 곡 감기
    /// </summary>
    private void Wind()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            FrontButton.onClick.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            BackButton.onClick.Invoke();
        }
    }

    /// <summary>
    /// 곡 재생
    /// </summary>
    private void SetPitch()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            BgSound.pitch = Mathf.Clamp(BgSound.pitch + 0.01f, 0f, 2f);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            BgSound.pitch = Mathf.Clamp(BgSound.pitch - 0.01f, 0f, 2f);
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            BgSound.pitch = 1;
        }
    }

    /// <summary>
    /// 노래 재생
    /// </summary>
    private void Playing()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (BgSound.isPlaying)
                Pause();
            else
                Play();
        }
    }
}
