using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowIntervalKey : MonoBehaviour
{
    [SerializeField] private int MyLine;
    private Text text;
    char k1 = ' ';
    char k2 = ' ';

    private void Start()
    {
        if(text == null)
            text = GetComponent<Text>();

        if (MyLine == 0)
        {
            k1 = (char)RhythmManager.Instance.ClearKeys[0];
            k2 = (char)RhythmManager.Instance.ClearKeys[1];
        }
        else if (MyLine == 1)
        {
            k1 = (char)RhythmManager.Instance.ClearKeys[2];
            k2 = (char)RhythmManager.Instance.ClearKeys[3];
        }
        text.text = "[ " + k1.ToString().ToUpper() + " ] " + " [ " + k2.ToString().ToUpper() + " ]";
    }

    public void Refresh()
    {
        if (text == null)
            text = GetComponent<Text>();

        if (MyLine == 0)
        {
            k1 = (char)RhythmManager.Instance.ClearKeys[0];
            k2 = (char)RhythmManager.Instance.ClearKeys[1];
        }
        else if (MyLine == 1)
        {
            k1 = (char)RhythmManager.Instance.ClearKeys[2];
            k2 = (char)RhythmManager.Instance.ClearKeys[3];
        }
        text.text = "[ " + k1.ToString().ToUpper() + " ] " + " [ " + k2.ToString().ToUpper() + " ]";
    }
}
