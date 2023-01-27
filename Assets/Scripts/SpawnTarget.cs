using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTarget : MonoBehaviour
{
    public List<GameObject> targets = new List<GameObject>();
    public GameObject target;
    private Vector2 pozycja;
    public int iloscCelow;
    void Start()
    {
        /*
        for (int i=0; i<iloscCelow; i++)
        {
            pozycja = new Vector2(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f));

            //Stworz cel: prefab, pozycja, obrot
            int randomTarget = Random.Range(0, targets.Count);
            Instantiate(targets[randomTarget], pozycja, Quaternion.identity);
        }*/
        StartCoroutine(SpawnerCoroutine());
    }

    IEnumerator SpawnerCoroutine()
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < iloscCelow; i++)
        {

            //zamienic losowe generowanie na liste z pozycjami (na zasadzie leveli)
            pozycja = new Vector2(Random.Range(4.0f, 12.0f), Random.Range(-1.25f, 1.0f));
            //Stworz cel: prefab, pozycja, obrot
            int randomTarget = Random.Range(0, targets.Count);
            Instantiate(targets[randomTarget], pozycja, Quaternion.identity);
            yield return new WaitForSeconds(1);
        }
    }

}
