using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMusicScript : MonoBehaviour
{
    public AudioSource musicSource;

    private void Awake()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.buildIndex.Equals(4))
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update()
    {
        if (!musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }
}
