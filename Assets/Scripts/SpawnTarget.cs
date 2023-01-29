using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTarget : MonoBehaviour
{
    public List<GameObject> targets = new List<GameObject>();
    private LevelData chosenLevel;
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
        chosenLevel = new();
        StartCoroutine(SpawnerCoroutine());
    }

    IEnumerator SpawnerCoroutine()
    {
        chosenLevel.Level1();
        yield return new WaitForSeconds(1);
        for (int i = 0; i < iloscCelow; i++)
        {
            //pozycja celu okreslana jest recznie poprzez wpis do tabeli znajdujacej sie w klasie LevelData.cs
            pozycja = new Vector2((chosenLevel.finishedTable[i].locationX), (chosenLevel.finishedTable[i].locationY));
            //Stworz cel: numer prefabu (animacji), pozycja, obrot
            Instantiate(targets[chosenLevel.finishedTable[i].targetType], pozycja, Quaternion.identity);
            yield return new WaitForSeconds(1);
        }
    }

}
