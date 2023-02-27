using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsScript : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;
    public AudioMixer musicMixer;
    public AudioMixer soundFXMixer;
    float decibels;

    private void Start()
    {
        // Przedstaw graficznie domyslne wartosci dzwieku
        musicSlider.value = PlayerPrefs.GetFloat("musicValue");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxValue");
    }
    private void Awake()
    {
        // W momencie przesuniecia paska, zaktualizuj wartosci dzwieku i zapisz je
        musicSlider.onValueChanged.AddListener((musicValue) =>
        {
            decibels = 30f * Mathf.Log10(musicValue / 10);
            musicMixer.SetFloat("MusicVolume", decibels);
            PlayerPrefs.SetFloat("musicValue", musicValue);
        }
        );
        sfxSlider.onValueChanged.AddListener((sfxValue) =>
        {
            decibels = 30f * Mathf.Log10(sfxValue / 10);
            soundFXMixer.SetFloat("SoundFXVolume", decibels);
            PlayerPrefs.SetFloat("sfxValue", sfxValue);
        }
        );
    }
}
