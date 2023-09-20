using UnityEngine;

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
        if (source.isPlaying)
            RhythmManager.Instance.CurrentTime = (decimal)source.time;
    }
}
