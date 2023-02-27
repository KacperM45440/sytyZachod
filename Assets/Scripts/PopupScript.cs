using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopupScript : MonoBehaviour
{
    public WinCheck winRef;
    public AudioSource mumbleSource;
    public AudioSource clapSource;
    public AudioSource cheerSource;

    // Ta klasa odpowiedzialna jest za kontrolowanie zestawu dzwiekow i animacji dla elementow pop-up (takich jak np. rundy lub wygrywanie)
    public void CloseMenu()
    {
        winRef.Unpause();
    }

    public void AnimationVersus()
    {
        FadeIn();
    }
    public void AnimationWin()
    {
        clapSource.Play();
    }
    public void AnimationFinisher()
    {
        cheerSource.Play();
    }

    public void StopMusic()
    {
        if (SceneManager.GetActiveScene().buildIndex.Equals(3))
        {
            GameMusicScript.Instance.FadeOut();
        }
    }    

    public void FadeIn()
    {
        mumbleSource.Play();
        mumbleSource.volume = 0;
        StartCoroutine(FadeInRoutine());
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutRoutine());
    }

    IEnumerator FadeInRoutine()
    {
        while (mumbleSource.volume < 1)
        {
            mumbleSource.volume += 0.05f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator FadeOutRoutine()
    {
        while (mumbleSource.volume > 0)
        {
            mumbleSource.volume -= 0.05f;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
