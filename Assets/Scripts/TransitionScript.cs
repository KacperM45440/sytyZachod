using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScript : MonoBehaviour
{
    public Animator animatorRef;
    private float transitionTime = 1.25f;
    private int sceneNumber;
    public bool startRn;

    private void Start()
    {
        if (startRn)
        {
            animatorRef.SetTrigger("Ended");
        }
    }

    public void PlayGame()
    {
        sceneNumber = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(LoadLevel(sceneNumber));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        animatorRef.SetTrigger("Started");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
