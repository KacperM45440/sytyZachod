using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMag : MonoBehaviour
{
    private static SpawnMag _instance;
    public static SpawnMag Instance { get { return _instance; } }

    public GameObject magazinePrefab;
    private GameObject cylinder;
    private GameObject currentMagazine;

    void Start()
    {
        cylinder = GameObject.Find("Cylinder");
        NewMagazine();
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

    public void NewMagazine()
    {
        currentMagazine = Instantiate(magazinePrefab);
        currentMagazine.transform.parent = cylinder.transform;
        currentMagazine.transform.position = cylinder.transform.position;
        currentMagazine.transform.rotation = cylinder.transform.rotation;
        currentMagazine.transform.localScale = new Vector3(1, 1, 1);
    }
}
