using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCheck : MonoBehaviour
{
    //Tworzymy singletona samego siebie w celu mozliwosci odnoszenia sie do skryptu
    
    private static WinCheck _instance;
    public static WinCheck Instance { get { return _instance; } }


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
    //przeczytac o serializacji zmiennych
    [HideInInspector] public int targetCounter;
    private SpawnTarget spawnRef;
    void Start()
    {
        spawnRef = GetComponent<SpawnTarget>();
    }

    void Update()
    {
    }

    public void Clicked()
    {
        targetCounter++;
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
