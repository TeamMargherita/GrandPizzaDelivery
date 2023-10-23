using UnityEngine;
using UnityEngine.UI;

public class RhythmSetting : MonoBehaviour
{
    public Slider MusicSlider;
    public Slider KeySlider;
    public Slider SpeedSlider;

    public InputField MusicInputField;
    public InputField KeyInputField;
    public InputField SpeedInputField;

    private RhythmManager manager;

    private void Start()
    {
        manager = RhythmManager.Instance;

        MusicSlider.value = manager.MusicSound;
        KeySlider.value = manager.KeySound;
        SpeedSlider.value = manager.Speed / 5f;

        MusicInputField.text = manager.MusicSound.ToString("0.0");
        KeyInputField.text = manager.KeySound.ToString("0.0");
        SpeedInputField.text = manager.Speed.ToString("0.0");
    }

    public void SetMusicVolume(float volume)
    {
        manager.MusicSound = volume;
        MusicVolumeSync();
    }
    public void SetMusicVolume(string volume)
    {
        float value = float.Parse(volume);
        value = Mathf.Clamp(value, 0, 10) / 10f;
        manager.MusicSound = value;
        MusicVolumeSync();
    }
    private void MusicVolumeSync()
    {
        MusicSlider.value = manager.MusicSound;
        MusicInputField.text = (manager.MusicSound * 10f).ToString("0.0");
    }

    public void SetKeyVolume(float volume)
    {
        manager.KeySound = volume;
        KeyVolumeSync();
    }
    public void SetKeyVolume(string volume)
    {
        float value = float.Parse(volume);
        value = Mathf.Clamp(value, 0, 10) / 10f;
        manager.KeySound = value;
        KeyVolumeSync();
    }
    private void KeyVolumeSync()
    {
        KeySlider.value = manager.KeySound;
        KeyInputField.text = (manager.KeySound * 10f).ToString("0.0");
    }

    public void SetSpeed(float volume)
    {
        volume = Mathf.Clamp(volume, 0.02f, 1);
        manager.Speed = volume * 5f;
        SpeedSync();
    }
    public void SetSpeed(string volume)
    {
        float value = float.Parse(volume);
        value = Mathf.Clamp(value, 0.1f, 5);
        manager.Speed = value;
        SpeedSync();
    }
    private void SpeedSync()
    {
        SpeedSlider.value = manager.Speed / 5f;
        SpeedInputField.text = manager.Speed.ToString("0.0");
    }
}
