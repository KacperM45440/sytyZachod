using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public Transform enemies;
    private int roundNumber;
    
    void Start()
    {
        InitialiseLevel();
    }

    public void InitialiseLevel()
    {
        // Za�aduj dane cel�w z poziomu, nadaj odpowiednia im predkosc a nastepnie rozpocznij proces tworzenia cel�w w grze
        chosenLevel = new();
        foreach (GameObject t in targets)
        {
            t.GetComponentInChildren<TargetMovement>().speed = (levelSpeed + roundNumber);
        }
        chosenLevel.Level1();
        StartCoroutine(TargetSpawnerCoroutine());
    }
    IEnumerator TargetSpawnerCoroutine()
    {
        GameObject.FindGameObjectWithTag("DebugTag").GetComponent<TMP_Text>().text = "Runda: "+ (roundNumber+1);
        yield return new WaitForSeconds(1);
        for (int i = 0; i < iloscCelow; i++)
        {
            // Pozycja celu okreslana jest recznie poprzez wpis do tabeli znajdujacej sie w klasie LevelData.cs
            // Stworz cel: numer prefabu (animacji), pozycja, obrot
            pozycja = new Vector2((chosenLevel.finishedTable[i].locationX), (chosenLevel.finishedTable[i].locationY));
            newTarget = Instantiate(targets[chosenLevel.finishedTable[i].targetType], pozycja, Quaternion.identity);
            newTarget.transform.parent = enemies;

            yield return new WaitForSeconds(chosenLevel.finishedTable[i].delay);
        }
        yield return new WaitUntil(() => enemies.childCount.Equals(1));
        
        roundNumber++;
        if (roundNumber < 2)
        {
            InitialiseLevel();
        }
        else
        {
            WinCheck.Instance.Checker();
        }
    }
}
