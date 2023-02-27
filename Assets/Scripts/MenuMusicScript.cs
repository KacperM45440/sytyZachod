using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMusicScript : MonoBehaviour
{
    private static MenuMusicScript _instance;
    public static MenuMusicScript Instance { get { return _instance; } }
    public AudioSource menuMusicSource;

    private void Awake()
    {
        // Muzyka z menu nie powinna przenosic sie do gry
        // Moze wystepowac tylko raz, zeby sie na siebie nie nakladala
        menuMusicSource = transform.GetComponent<AudioSource>();
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.buildIndex.Equals(4))
        {
            DontDestroyOnLoad(gameObject);
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

    void Update()
    {
        // Wlacz muzyke, jezeli jest wyciszona to ja odcisz
        if (!menuMusicSource.isPlaying)
        {
            menuMusicSource.volume = 1;
            menuMusicSource.Play();
        }
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutRoutine());
    }

    IEnumerator FadeOutRoutine()
    {
        // Wycisz stopniowo muzyke
        while (menuMusicSource.volume > 0)
        {
            menuMusicSource.volume -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
