using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTarget : MonoBehaviour
{
    // Prefab celu i animacje przypisywane sa w edytorze 
    public List<GameObject> targets = new List<GameObject>();
    private LevelData chosenLevel;
    public float levelSpeed;
    public GameObject target;
    private GameObject newTarget;
    private Vector2 pozycja;
    public int iloscCelow;
    public Transform Enemies;
    
    void Start()
    {
        // Za³aduj dane celów z poziomu, nadaj odpowiednia im predkosc a nastepnie rozpocznij proces tworzenia celów w grze
        chosenLevel = new();
        foreach (GameObject t in targets)
        {
            t.GetComponentInChildren<TargetMovement>().speed = levelSpeed;
        }
        StartCoroutine(SpawnerCoroutine());
    }

    IEnumerator SpawnerCoroutine()
    {
        chosenLevel.Level1();
        yield return new WaitForSeconds(1);
        for (int i = 0; i < iloscCelow; i++)
        {
            // Pozycja celu okreslana jest recznie poprzez wpis do tabeli znajdujacej sie w klasie LevelData.cs
            pozycja = new Vector2((chosenLevel.finishedTable[i].locationX), (chosenLevel.finishedTable[i].locationY));
            // Stworz cel: numer prefabu (animacji), pozycja, obrot
            newTarget = Instantiate(targets[chosenLevel.finishedTable[i].targetType], pozycja, Quaternion.identity);
            newTarget.transform.parent = Enemies;
            yield return new WaitForSeconds(chosenLevel.finishedTable[i].delay);
        }
    }

}
