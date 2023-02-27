using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnMag : MonoBehaviour
{
    private Transform cylinder;
    private GameObject currentMagazine;
    private static SpawnMag _instance;
    public static SpawnMag Instance { get { return _instance; } }
    public GameObject magazinePrefab;
    public Vector3 originalRotation;

    private void Awake()
    {
        // Magazynek moze byc tylko jeden
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        // Zapisz i przekaz obrot tak, aby pociski znajdowaly sie w dobrym miejscu w cylindrze
        cylinder = transform;
        originalRotation = cylinder.localEulerAngles;
        NewMagazine();
    }

    public void NewMagazine()
    {
        // Stworz nowy magazynek z prefabu, a nastepnie doklej go do cylindra
        currentMagazine = Instantiate(magazinePrefab);
        currentMagazine.transform.parent = cylinder;
        currentMagazine.transform.SetPositionAndRotation(cylinder.position, cylinder.rotation);
        currentMagazine.transform.localScale = new Vector3(1, 1, 1);
    }
}
