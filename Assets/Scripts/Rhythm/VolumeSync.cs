using UnityEngine;

public class VolumeSync : MonoBehaviour
{
    private AudioSource source;     // 음악 재생을 위한 오디오 소스 캐싱
    private RhythmManager manager;  // 매니저 캐싱

    // Start is called before the first frame update
    private void Start()
    {
        // 오디오 소스 캐싱
        source = GetComponent<AudioSource>();

        // 매니저 캐싱
        manager = RhythmManager.Instance;
    }

    private void Update()
    {
        // 볼륨을 매니저 값으로 동기화
        source.volume = manager.MusicSound;
    }
}
