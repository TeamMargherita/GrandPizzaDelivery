using UnityEngine;

public class BGSound : MonoBehaviour
{
    public AudioSource bgSound;
    private void Start()
    {
        if (bgSound == null)
            bgSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (bgSound.isPlaying)
            RhythmManager.Instance.CurrentTime = (decimal)bgSound.time;

        Debug.Log(bgSound.clip.length);
    }

    public void Play()
    {
        if (bgSound.isPlaying)
            bgSound.time = 0;
        bgSound.Play();
    }
    public void Pause()
    {
        bgSound.Pause();
    }
    public void Stop()
    {
        bgSound.Stop();
        bgSound.time = 0;
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
}
