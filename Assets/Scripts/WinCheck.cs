using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinCheck : MonoBehaviour
{
    // Z klasy WinCheck tworzony jest singleton, poniewaz sprawdzac czy wygralismy na pewno bedziemy w jednym miejscu.
    // Nie ma tez potrzeby tworzenia zadnych kopii tej klasy. Dzieki temu ulatwic mozna odwolywanie sie do niej z innych miejsc w programie.
    private static WinCheck _instance;
    public static WinCheck Instance { get { return _instance; } }
    [HideInInspector] public int targetCounter;
    public SpawnTarget spawnRef;
    public int maxScore;
    public Slider scoreBar;
    private float progress;

    private void Start()
    {
        maxScore = spawnRef.iloscCelow*2;
    }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    // W przypadku trafienia, zwieksz na ekranie ilosc trafionych celow o jeden.
    public void Clicked()
    { 
        targetCounter++;
        progress = (float)targetCounter / (float)maxScore;
        scoreBar.value = progress;
    }

    // Sprawdz, czy gracz wygral w gre. 
    //
    // Todo: 
    // 1. Nie trzeba zestrzelic wszystkich celow, ustanowic progi typu 70%, 80%, 90% zaleznie od poziomu
    // 2. Aby wygrac w gre, poziom musi sie zakonczyc, nawet jesli prog zostal juz wczesniej przekroczony
    // 3. Po wygranej, przekierowac na animacje zwyciestwa nad przeciwnikiem, nastepnie zmiana sceny/poziomu
    public void Checker()
    {
        if (targetCounter >= maxScore * 0.6f)
        {
            GameObject.FindGameObjectWithTag("DebugTag").GetComponent<TMP_Text>().text = "Wygrales!";
        }
        else
        {
            GameObject.FindGameObjectWithTag("DebugTag").GetComponent<TMP_Text>().text = "Przegrales!";
        }
    }
}
