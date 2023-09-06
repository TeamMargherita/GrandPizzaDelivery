using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmManager : MonoBehaviour
{
    public static RhythmManager Instance = null;
    private double StartTime = 0d;
    private void Awake()
    {
        if (Instance != null)
            Destroy(this);

        Instance = this;
        Init();
        DontDestroyOnLoad(Instance);
    }

    public  void Init()
    {
        StartTime = AudioSettings.dspTime;
    }

    public double CurrentTime()
    {
        return AudioSettings.dspTime - StartTime;
    }
}
