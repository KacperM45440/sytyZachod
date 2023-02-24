using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMusicScript : MonoBehaviour
{
    public AudioSource musicSource;
    bool inGame;

    private void Awake()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (!currentScene.buildIndex.Equals(0) || !currentScene.buildIndex.Equals(4))
        {
            DontDestroyOnLoad(gameObject);
        }
        if (currentScene.buildIndex.Equals(0) || currentScene.buildIndex.Equals(4))
        {
            inGame = false;
        }
    }
    public void PlayMusic()
    {
        musicSource.Play();
        inGame = true;
    }

    void Update()
    {
        if (!musicSource.isPlaying && inGame)
        {
            musicSource.Play();
        }
    }
}
