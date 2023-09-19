using UnityEngine;

public class BGSound : MonoBehaviour
{
    bool isPlay = false;

    void Update()
    {
        if (!isPlay)
        {
            GetComponent<AudioSource>().Play();
            isPlay = true;
        }
    }
}
