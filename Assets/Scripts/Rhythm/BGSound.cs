using UnityEngine;
using System.Collections;

/// <summary>
/// 배경 노래와 관련된 클래스
/// </summary>
public class BGSound : MonoBehaviour
{
    public float Timer = 3f;
    private float timer;
    private AudioSource source;
    private RhythmManager manager;
    
    private void Awake()
    {
        source = GetComponent<AudioSource>();
        timer = Timer;
    }
    private void Start()
    {
        manager = RhythmManager.Instance;
        source.clip = manager.AudioClip;
        StartCoroutine(WaitPlaying());
    }

    private void Update()
    {
        source.volume = manager.MusicSound;
        if(timer > 0f)
        {

            timer -= Time.deltaTime;
            manager.CurrentTime = -(decimal)timer;
        }
        else
            // 노래 재생 시간 동기화
            manager.CurrentTime = (decimal)source.time;
    }

    private IEnumerator WaitPlaying()
    {
        yield return new WaitForSeconds(Timer);
        source.Play();
    }
}
