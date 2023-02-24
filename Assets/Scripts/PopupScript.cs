using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupScript : MonoBehaviour
{
    public WinCheck winRef;
    public AudioSource mumbleSource;
    public AudioSource clapSource;
    public AudioSource cheerSource;

    public void CloseMenu()
    {
        winRef.Unpause();
    }

    public void AnimationVersus()
    {
        mumbleSource.Play();
    }
    public void AnimationWin()
    {
        clapSource.Play();
    }
    public void AnimationFinisher()
    {
        cheerSource.Play();
    }
}
