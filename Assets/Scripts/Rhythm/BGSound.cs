using UnityEngine;

/// <summary>
/// 배경 노래와 관련된 클래스
/// </summary>
public class BGSound : MonoBehaviour
{
    private AudioSource source;
    private void Start()
    {
        if (source == null)
            source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // 노래 재생 시간 동기화
        RhythmManager.Instance.CurrentTime = (decimal)source.time;
    }
}
