using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSound : MonoBehaviour
{
    bool isPlay = false;

    void Update()
    {
        if (RhythmManager.Instance.CurrentTime() >= 10d && !isPlay)
        {
            GetComponent<AudioSource>().Play();
            isPlay = true;
        }
    }
}
