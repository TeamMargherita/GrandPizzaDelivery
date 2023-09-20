using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteEditor : MonoBehaviour
{
    public AudioData data;
    private decimal culc;

    void Start()
    {
        data = RhythmManager.Instance.Data;
    }

    void Update()
    {
        if(data == null)
        {
            Debug.Log("Null");
            data = RhythmManager.Instance.Data;
        }
        if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.D))
        {
            culc = (RhythmManager.Instance.CurrentTime - (decimal)NoteSpawner.Offset) / NoteSpawner.bitSlice;
            int index = (culc % 1 < 0.5m) ? (int)culc : (int)culc + 1;

            data.IsNote[index] = true;
        }
        if (Input.GetKey(KeyCode.V))
        {
            culc = (RhythmManager.Instance.CurrentTime - (decimal)NoteSpawner.Offset) / NoteSpawner.bitSlice;
            int index = (culc % 1 < 0.5m) ? (int)culc : (int)culc + 1;
            if (data.IsNote[index])
            {
                if (NoteSpawner.NoteLoad.Count > 0)
                    NoteSpawner.NoteClear();
                data.IsNote[index] = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            FButton.onClick.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            BButton.onClick.Invoke();
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            bgSound.pitch = Mathf.Clamp(bgSound.pitch + 0.01f, 0f, 2f);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            bgSound.pitch = Mathf.Clamp(bgSound.pitch - 0.01f, 0f, 2f);
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            bgSound.pitch = 1;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (bgSound.isPlaying)
                Pause();
            else
                Play();
        }
    }
    public void Front(int second)
    {
        bgSound.time = Mathf.Clamp(bgSound.time - second, 0f, bgSound.clip.length);
        RhythmManager.Instance.CurrentTime = (decimal)bgSound.time;
    }

    public void Back(int second)
    {
        bgSound.time = Mathf.Clamp(bgSound.time + second, 0f, (int)bgSound.clip.length);
        RhythmManager.Instance.CurrentTime = (decimal)bgSound.time;
    }
    public void Play()
    {
        bgSound.Play();
    }
    public void Pause()
    {
        bgSound.Pause();
    }
    public void Stop()
    {
        bgSound.Stop();
    }
}
