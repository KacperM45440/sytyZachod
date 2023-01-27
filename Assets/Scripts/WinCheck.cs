using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinCheck : MonoBehaviour
{
    //Tworzymy singletona samego siebie w celu mozliwosci odnoszenia sie do skryptu
    private static WinCheck _instance;
    public static WinCheck Instance { get { return _instance; } }
    //przeczytac o serializacji zmiennych
    [HideInInspector] public int targetCounter;
    public SpawnTarget spawnRef;
    public TMP_Text score;


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
    void Start()
    {
    }

    public void Clicked()
    { 
        targetCounter++;
        score.text = "TARGETS HIT: " + targetCounter;
        Checker();
    }
    public void Checker()
    {
        if (targetCounter >= spawnRef.iloscCelow)
        {
            Debug.Log("Wygrales w gre!");
        }
    }
}
