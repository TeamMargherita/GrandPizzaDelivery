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

    private float CurrentSpeed;
    private void Start()
    {
        SpeedSlider.onValueChanged.AddListener(SetSpeed);
    }

    private void OnEnable()
    {
        if (manager == null)
            manager = RhythmManager.Instance;
        SpeedSlider.value = manager.Speed / 5f;
        CurrentSpeed = manager.Speed;
    }

    private void Update()
    {
        if (CurrentSpeed == manager.Speed)
            return;

        SpeedSlider.value = manager.Speed / 5f;
        CurrentSpeed = manager.Speed;
    }

    /// <summary>
    /// Slider로 속도를 설정하기 위한 함수
    /// </summary>
    /// <param name="volume">슬라이더 값</param>
    public void SetSpeed(float volume)
    {
        float v = Mathf.Round(volume * 50);
        manager.Speed = v / 10;
    }
}
