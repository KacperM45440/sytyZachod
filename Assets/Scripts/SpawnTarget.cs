using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTarget : MonoBehaviour
{
    public GameObject target;
    void Start()
    {
        //Stworz cel: prefab, pozycja, obrot
        Instantiate(target, new Vector2 (0,0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
