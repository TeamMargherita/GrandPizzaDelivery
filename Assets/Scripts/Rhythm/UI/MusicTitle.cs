using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicTitle : MonoBehaviour
{
    void Start()
    {
        GetComponent<Text>().text = RhythmManager.Instance.Title;
    }
}