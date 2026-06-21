using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource engineSource;
    public AudioSource sfxSource;
    public AudioSource ambientSource;

    [Header("Sliders")]
    public Slider musicSlider;
    public Slider engineSlider;
    public Slider sfxSlider;
    public Slider ambientSlider;

    void Start()
    {
        // Load volume yang tersimpan
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        engineSlider.value = PlayerPrefs.GetFloat("EngineVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
        ambientSlider.value = PlayerPrefs.GetFloat("AmbientVolume", 1f);

        // Terapkan volume
        SetMusicVolume(musicSlider.value);
        SetEngineVolume(engineSlider.value);
        SetSFXVolume(sfxSlider.value);
        SetAmbientVolume(ambientSlider.value);

        // Event slider
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        engineSlider.onValueChanged.AddListener(SetEngineVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        ambientSlider.onValueChanged.AddListener(SetAmbientVolume);
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetEngineVolume(float volume)
    {
        engineSource.volume = volume;
        PlayerPrefs.SetFloat("EngineVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void SetAmbientVolume(float volume)
    {
        ambientSource.volume = volume;
        PlayerPrefs.SetFloat("AmbientVolume", volume);
    }
}