using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TransitionScript transitionRef;
    public AudioSource menuSource;
    // Efekty dzwiekowe i muzyka rozdzielone sa na dwa rozne kanaly ktorymi mozna sterowac, dzieki czemu mozemy zdecydowac ktore z nich chcemy miec glosniej
    public AudioMixer musicMixer;
    public AudioMixer soundFXMixer;
    float decibels;
    private void Awake()
    {
        Application.targetFrameRate = 60;
        transitionRef.ChooseCursor("menuCrosshair");
    }
    public void PlayGame()
    {
        transitionRef.PlayGame();
        MenuMusicScript.Instance.FadeOut();
    }
    
    // Wydaj dzwiek przy kliknieciu guzika
    public void PlaySound()
    {
        menuSource.Play();
    }

    // Ustaw domyslne wartosci dzwieku przy uruchamianiu gry, nastepnie zmien je na wartosci decybeliczne
    private void Start()
    {  
        if (PlayerPrefs.HasKey("musicValue").Equals(false))
        {
            PlayerPrefs.SetFloat("musicValue", 5f);
        }
        if (PlayerPrefs.HasKey("sfxValue").Equals(false))
        {
            PlayerPrefs.SetFloat("sfxValue", 5f);
        }

        decibels = 30f * Mathf.Log10(PlayerPrefs.GetFloat("musicValue") / 10f);
        musicMixer.SetFloat("MusicVolume", decibels);

        decibels = 30f * Mathf.Log10(PlayerPrefs.GetFloat("sfxValue") / 10f);
        soundFXMixer.SetFloat("SoundFXVolume", decibels);
    }

    // Wyjdz z gry, zamknij aplikacje
    public void QuitGame()
    {
        Application.Quit();
    }
}
