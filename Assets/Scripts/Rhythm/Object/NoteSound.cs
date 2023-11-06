using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSound : MonoBehaviour
{
    private AudioSource source;     // 음악 재생을 위한 오디오 소스 캐싱
    private RhythmManager manager;  // 매니저 캐싱
    // Start is called before the first frame update
    void Start()
    {
        // 오디오 소스 캐싱
        source = GetComponent<AudioSource>();
        manager = RhythmManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        source.volume = manager.KeySound;
    }
}
