using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 평균 출력해주는 클래스
/// </summary>
public class AccuracyText : MonoBehaviour
{
    private Text accuracy;              // 출력할 텍스트
    private RhythmManager manager;      // 리듬 매니저 캐싱

    private void Start()
    {
        // 리듬 매니저 캐싱
        manager = RhythmManager.Instance;

        // 자기 자신의 Text 컴포넌트
        accuracy = GetComponent<Text>();
    }

    void Update()
    {
        // 정확도를 텍스트로 전환
        accuracy.text = manager.Judges.Accuracy.ToString("00.0") + "%";
    }
}
