using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 타이머 UI 표시 클래스
/// </summary>
public class RhythmTimer : MonoBehaviour
{
    Text text;      // 타이머 텍스트
    int minute;     // 분
    int second;     // 초

    int endMinute;
    int endSecond;
    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        second = (int)RhythmManager.Instance.CurrentTime % 60;
        minute = (int)RhythmManager.Instance.CurrentTime / 60;

        endSecond = (int)RhythmManager.Instance.Data.Length % 60;
        endMinute = (int)RhythmManager.Instance.Data.Length / 60;
        text.text = minute.ToString("00") + ":" + second.ToString("00") + "/"
            + endMinute.ToString("00") + ":" + endSecond.ToString("00");
    }
}
