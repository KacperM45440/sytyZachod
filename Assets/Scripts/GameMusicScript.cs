using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMusicScript : MonoBehaviour
{
    private static GameMusicScript _instance;
    public static GameMusicScript Instance { get { return _instance; } }
    public AudioSource gameMusicSource;
    Scene currentScene;
    bool inGame;

    private void Awake()
    {
        // Muzyka powinna grac przechodzac z jednej sceny do drugiej, oraz nie powinna grac kiedy znajdujemy sie w menu.
        // Moze wystepowac tylko raz, zeby sie na siebie nie nakladala
        currentScene = SceneManager.GetActiveScene();
        if (currentScene.buildIndex.Equals(0))
        {
            DontDestroyOnLoad(gameObject);
        }
        if (currentScene.buildIndex.Equals(0) || currentScene.buildIndex.Equals(4))
        {
            inGame = false;
        }
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void PlayMusic()
    {
        // Wlacz muzyke, jezeli jest wyciszona to ja odcisz
        gameMusicSource.Play();
        if (gameMusicSource.volume.Equals(0))
        {
            gameMusicSource.volume = 1;
        }
        inGame = true;
    }

    void Update()
    {
        // Jezeli muzyka skonczy sie, pusc petle od nowa
        if (!gameMusicSource.isPlaying && inGame)
        {
            gameMusicSource.Play();
        }
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutRoutine());
    }

    IEnumerator FadeOutRoutine()
    {
        // Wycisz stopniowo muzyke, nastepnie zniszcz powiazany z nia obiekt
        while (gameMusicSource.volume > 0)
        {
            gameMusicSource.volume -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        Destroy(gameObject);
    }
}
