using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScript : MonoBehaviour
{
    private float transitionTime = 1.25f;
    private int sceneNumber;
    public Animator animatorRef;
    public bool startRn;

    // Ta klasa odpowiedzialna jest za przyciemnienie oraz rozjasnienie ekranu kiedy gra przelacza pomiedzy dwoma scenami

    private void Start()
    {
        if (startRn)
        {
            animatorRef.SetTrigger("Ended");
        }
    }

    public void PlayGame()
    {
        // Zmien scene
        sceneNumber = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(LoadLevel(sceneNumber));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        // Rozpocznij przyciemnianie
        animatorRef.SetTrigger("Started");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
