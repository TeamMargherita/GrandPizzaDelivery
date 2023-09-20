using UnityEngine;
using System.Collections.Generic;

public class RhythmManager : MonoBehaviour
{
    public static RhythmManager Instance = null;
    public decimal CurrentTime;
    public string Title;
    public float Offset;
    public AudioSource Metronome;
    public AudioData Data;         // 곡 데이터
    public Note NotePrefab;        // 노트
    public Bar BarPrefab;          // 마디

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);

        Instance = this;
        DontDestroyOnLoad(Instance);
    }

    public void SaveData()
    {
        JsonManager<AudioData>.Save(Data, Title);
    }

    public void LoadData()
    {
        Data = new AudioData(Title);
    }
}
