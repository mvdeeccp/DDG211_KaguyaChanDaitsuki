using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioMixer audioMixer;
    public Slider volumeSlider;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    void Start()
    {
        if (volumeSlider != null)
        {
            float volume = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
            volumeSlider.value = volume;
            SetVolume(volume);
        }
    }

    public void SetVolume(float value)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20); // dB scale
        PlayerPrefs.SetFloat("MasterVolume", value);
    }
}
