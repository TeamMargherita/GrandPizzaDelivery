using UnityEngine;

/// <summary>
/// 배경 노래와 관련된 클래스
/// </summary>
public class BGSound : MonoBehaviour
{
    private AudioSource source;
    private RhythmManager manager;
    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    private void Start()
    {
        manager = RhythmManager.Instance;
        source.clip = manager.AudioClip;
        source.Play();
    }

    private void Update()
    {
        source.volume = manager.MusicSound;
        // 노래 재생 시간 동기화
        manager.CurrentTime = (decimal)source.time;
    }
}
