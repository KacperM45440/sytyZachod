using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTarget : MonoBehaviour
{
    public GameObject target;
    private Vector2 pozycja;
    public int iloscCelow;
    void Start()
    {
        for (int i=0; i<iloscCelow; i++)
        {
            pozycja = new Vector2(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f));

            //Stworz cel: prefab, pozycja, obrot
            Instantiate(target, pozycja, Quaternion.identity);
        }
    }
}
