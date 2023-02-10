using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnMag : MonoBehaviour
{
    private static SpawnMag _instance;
    public static SpawnMag Instance { get { return _instance; } }

    public GameObject magazinePrefab;
    private Transform cylinder;
    private GameObject currentMagazine;
    public Vector3 originalRotation;

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

        cylinder = transform;
        originalRotation = cylinder.localEulerAngles;
        NewMagazine();

    }

    public void NewMagazine()
    {
        currentMagazine = Instantiate(magazinePrefab);
        currentMagazine.transform.parent = cylinder;
        currentMagazine.transform.SetPositionAndRotation(cylinder.position, cylinder.rotation);
        currentMagazine.transform.localScale = new Vector3(1, 1, 1);
    }
}
