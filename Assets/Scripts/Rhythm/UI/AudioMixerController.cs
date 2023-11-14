using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixerController : MonoBehaviour
{
    [SerializeField] private AudioMixer m_AudioMixer;
    [SerializeField] private Slider m_MusicMasterSlider;
    [SerializeField] private Slider m_MusicBGMSlider;
    [SerializeField] private Slider m_MusicSFXSlider;

    private void Awake()
    {
        float bolume = 0;
        m_AudioMixer.GetFloat("Master", out bolume);
        // Mathf.Log10(volume) * 20 = value
        // value / 20 = Mathf.Log10(volume)
        // Mathf.Pow(10, value / 20) = volume
        bolume = Mathf.Pow(10, bolume / 20);
        m_MusicMasterSlider.value = bolume;

        m_AudioMixer.GetFloat("BGM", out bolume);
        bolume = Mathf.Pow(10, bolume / 20);
        m_MusicBGMSlider.value = bolume;

        m_AudioMixer.GetFloat("SFX", out bolume);
        bolume = Mathf.Pow(10, bolume / 20);
        m_MusicSFXSlider.value = bolume;

        m_MusicMasterSlider.onValueChanged.AddListener(SetMasterVolume);
        m_MusicBGMSlider.onValueChanged.AddListener(SetMusicVolume);
        m_MusicSFXSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SetMasterVolume(float volume)
    {
        m_AudioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }

    public void SetMusicVolume(float volume)
    {
        m_AudioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        m_AudioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }

    public void Closebutton()
    {
        gameObject.SetActive(false);
    }
}