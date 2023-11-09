using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 리듬게임에 사용되는 변수를 세팅해주는 UI 클래스
/// </summary>
public class SpeedSetting : MonoBehaviour
{
    public Slider SpeedSlider;

    private RhythmManager manager;          // 리듬 매니저 캐싱

    private void Start()
    {
        // 리듬 매니저 캐싱
        manager = RhythmManager.Instance;

        SpeedSlider.value = manager.Speed / 5f;
        SpeedSlider.onValueChanged.AddListener(SetSpeed);
    }

    /// <summary>
    /// Slider로 속도를 설정하기 위한 함수
    /// </summary>
    /// <param name="volume">슬라이더 값</param>
    public void SetSpeed(float volume)
    {
        manager.Speed = volume * 5f;
    }
}
