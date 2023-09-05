using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSound : MonoBehaviour
{
    double currentTime;
    bool isPlay = false;
    void Start()
    {
        currentTime = AudioSettings.dspTime;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(AudioSettings.dspTime - currentTime);
        if (AudioSettings.dspTime - currentTime >= 10d && !isPlay)
        {
            GetComponent<AudioSource>().Play();
            isPlay = true;
        }
    }
}
