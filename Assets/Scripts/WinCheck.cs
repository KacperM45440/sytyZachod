using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinCheck : MonoBehaviour
{
    // Z klasy WinCheck tworzony jest singleton, poniewaz sprawdzac czy wygralismy na pewno bedziemy w jednym miejscu.
    // Nie ma tez potrzeby tworzenia zadnych kopii tej klasy. Dzieki temu ulatwic mozna odwolywanie sie do niej z innych miejsc w programie.
    private static WinCheck _instance;
    public static WinCheck Instance { get { return _instance; } }
    [HideInInspector] public int targetCounter;
    public SpawnTarget spawnRef;
    public TMP_Text score;

    // ???
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

    // W przypadku trafienia, zwieksz na ekranie ilosc trafionych celow o jeden.
    public void Clicked()
    { 
        targetCounter++;
        score.text = "TARGETS HIT: " + targetCounter;
        Checker();
    }

    // Sprawdz, czy gracz wygral w gre. 
    //
    // Todo: 
    // 1. Nie trzeba zestrzelic wszystkich celow, ustanowic progi typu 70%, 80%, 90% zaleznie od poziomu
    // 2. Aby wygrac w gre, poziom musi sie zakonczyc, nawet jesli prog zostal juz wczesniej przekroczony
    // 3. Po wygranej, przekierowac na animacje zwyciestwa nad przeciwnikiem, nastepnie zmiana sceny/poziomu
    public void Checker()
    {
        if (targetCounter >= spawnRef.iloscCelow)
        {
            Debug.Log("Wygrales w gre!");
        }
    }
}
