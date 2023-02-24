using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TransitionScript transitionRef;
    public void PlayGame()
    {
        transitionRef.PlayGame();
    }

    private void Awake()
    {
        Application.targetFrameRate = 60;
        transitionRef.ChooseCursor("menuCrosshair");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
