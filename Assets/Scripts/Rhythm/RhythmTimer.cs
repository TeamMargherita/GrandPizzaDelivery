using UnityEngine;
using UnityEngine.UI;

public class RhythmTimer : MonoBehaviour
{
    Text text;
    int minute;
    int second;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        second = (int)RhythmManager.Instance.CurrentTime % 60;
        minute = (int)RhythmManager.Instance.CurrentTime / 60;
        text.text = minute.ToString() + ":" + second.ToString("00");
    }
}
