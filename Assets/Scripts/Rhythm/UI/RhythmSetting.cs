using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 리듬게임에 사용되는 변수를 세팅해주는 UI 클래스
/// </summary>
public class RhythmSetting : MonoBehaviour
{
    // Sliders
    public Slider MusicSlider;
    public Slider KeySlider;
    public Slider SpeedSlider;

    // InputFields
    public InputField MusicInputField;
    public InputField KeyInputField;
    public InputField SpeedInputField;

    private RhythmManager manager;          // 리듬 매니저 캐싱

    private void Start()
    {
        // 리듬 매니저 캐싱
        manager = RhythmManager.Instance;

        // 슬라이더들의 값을 매니저의 값으로 초기화
        MusicSlider.value = manager.MusicSound;
        KeySlider.value = manager.KeySound;
        SpeedSlider.value = manager.Speed / 5f;

        // 텍스트들을 매니저의 값으로 초기화
        MusicInputField.text = (manager.MusicSound * 10f).ToString("0.0");
        KeyInputField.text = (manager.KeySound * 10f).ToString("0.0");
        SpeedInputField.text = manager.Speed.ToString("0.0");
    }

    /// <summary>
    /// Slider로 배경음을 설정하기 위한 함수
    /// </summary>
    /// <param name="volume">슬라이더 값</param>
    public void SetMusicVolume(float volume)
    {
        manager.MusicSound = volume;
        MusicVolumeSync();
    }
    /// <summary>
    /// InputField로 배경음을 설정하기 위한 함수
    /// </summary>
    /// <param name="volume">텍스트 값</param>
    public void SetMusicVolume(string volume)
    {
        float value;
        try
        {
            value = float.Parse(volume);
        }
        catch (FormatException e)
        {
            Debug.Log(e);
            MusicVolumeSync();
            return;
        }

        value = Mathf.Clamp(value, 0, 10) / 10f;
        manager.MusicSound = value;
        MusicVolumeSync();
    }
    /// <summary>
    /// 배경음을 통해 Slider와 InputField의 값을 동기화 하는 함수
    /// </summary>
    private void MusicVolumeSync()
    {
        MusicSlider.value = manager.MusicSound;
        MusicInputField.text = (manager.MusicSound * 10f).ToString("0.0");
    }

    /// <summary>
    /// Slider로 키음을 설정하기 위한 함수
    /// </summary>
    /// <param name="volume">슬라이더 값</param>
    public void SetKeyVolume(float volume)
    {
        manager.KeySound = volume;
        KeyVolumeSync();
    }
    /// <summary>
    /// InputField로 키음을 설정하기 위한 함수
    /// </summary>
    /// <param name="volume">텍스트 값</param>
    public void SetKeyVolume(string volume)
    {
        float value;
        try
        {
           value  = float.Parse(volume);
        }
        catch (FormatException e)
        {
            Debug.Log(e);
            KeyVolumeSync();
            return;
        }

        value = Mathf.Clamp(value, 0, 10) / 10f;
        manager.KeySound = value;
        KeyVolumeSync();
    }
    /// <summary>
    /// 키음을 통해 Slider와 InputField의 값을 동기화 하는 함수
    /// </summary>
    private void KeyVolumeSync()
    {
        KeySlider.value = manager.KeySound;
        KeyInputField.text = (manager.KeySound * 10f).ToString("0.0");
    }

    /// <summary>
    /// Slider로 속도를 설정하기 위한 함수
    /// </summary>
    /// <param name="volume">슬라이더 값</param>
    public void SetSpeed(float volume)
    {
        volume = Mathf.Clamp(volume, 0.02f, 1);
        manager.Speed = volume * 5f;
        SpeedSync();
    }
    /// <summary>
    /// InputField로 속도를 설정하기 위한 함수
    /// </summary>
    /// <param name="volume">텍스트 값</param>
    public void SetSpeed(string volume)
    {
        float value;
        try
        {
            value = float.Parse(volume);
        }
        catch (FormatException e)
        {
            Debug.Log(e);
            SpeedSync();
            return;
        }
        value = Mathf.Clamp(value, 0.1f, 5);
        manager.Speed = value;
        SpeedSync();
    }
    /// <summary>
    /// 속도를 통해 Slider와 InputField의 값을 동기화 하는 함수
    /// </summary>
    private void SpeedSync()
    {
        SpeedSlider.value = manager.Speed / 5f;
        SpeedInputField.text = manager.Speed.ToString("0.0");
    }
}
