using UnityEngine;

public class BGSound : MonoBehaviour
{
    bool isPlay = false;

    void Update()
    {
        if (RhythmManager.Instance.CurrentTime() >= 6m && !isPlay)
        {
            GetComponent<AudioSource>().Play();
            isPlay = true;
        }
    }
}