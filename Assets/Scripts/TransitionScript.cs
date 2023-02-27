using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScript : MonoBehaviour
{
    public Texture2D fistCrosshair;
    public Texture2D crosshairShooting;
    public Texture2D menuCrosshair;
    private Vector2 cursorHotspot;
    private float transitionTime = 1.25f;
    private int sceneNumber;
    public Animator animatorRef;
    public bool startRn;

    // Ta klasa odpowiedzialna jest za przyciemnienie oraz rozjasnienie ekranu kiedy gra przelacza pomiedzy dwoma scenami.
    // Dodatkowo, zaleznie od sceny, zmieniane sa kursory na atrakcyjniejsze niz systemowy
    private void Start()
    {
        if (startRn)
        {
            animatorRef.SetTrigger("Ended");
        }
    }

    public void PlayGame()
    {
        PlayerPrefs.SetInt("currentScore", 0);
        NextLevel();
    }
    
    public void MainMenu()
    {
        StartCoroutine(LoadLevel(0)); 
    }
    public void NextLevel()
    {
        // Zmien scene
        sceneNumber = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(LoadLevel(sceneNumber));
    }

    public void ThisLevel()
    {
        sceneNumber = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadLevel(sceneNumber));
    }

    public void ChooseCursor(string whichOne)
    {
        if (whichOne.Equals("fistCrosshair"))
        {
            cursorHotspot = new Vector2(fistCrosshair.width / 2, fistCrosshair.height / 2);
            Cursor.SetCursor(fistCrosshair, cursorHotspot, CursorMode.Auto);
        }
        if (whichOne.Equals("crosshairShooting"))
        {
            cursorHotspot = new Vector2(crosshairShooting.width / 2, crosshairShooting.height / 2);
            Cursor.SetCursor(crosshairShooting, cursorHotspot, CursorMode.Auto);
        }
        if (whichOne.Equals("menuCrosshair"))
        {
            cursorHotspot = new Vector2(0,0);
            Cursor.SetCursor(menuCrosshair, cursorHotspot, CursorMode.Auto);
        } 
    }
    IEnumerator LoadLevel(int levelIndex)
    {
        // Rozpocznij przyciemnianie
        animatorRef.SetTrigger("Started");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
