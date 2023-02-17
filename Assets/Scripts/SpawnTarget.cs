using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnTarget : MonoBehaviour
{
    private GameObject newTarget;
    private LevelData chosenLevel;
    private Vector2 targetPosition;
    private int roundNumber;
    // Prefab celu i animacje przypisywane sa w edytorze 
    public List<GameObject> targets = new List<GameObject>();
    public GameObject target;
    public Transform enemies;
    public Animator popupAnimator;
    public Animator fadeAnimator;
    public bool canPunch;
    public float levelSpeed;
    public int targetAmount;

    void Start()
    {
        InitialiseLevel();
    }

    private void RoundPopup()
    {
        // Wyswietl animacje przebiegu rundy
        if (roundNumber.Equals(0))
        {
            popupAnimator.SetTrigger("round1");
            StartCoroutine(FadeOut());
        }
        else
        {
            fadeAnimator.SetTrigger("fade_in");
            popupAnimator.SetTrigger("round2");
            StartCoroutine(FadeOut());
        }
    }
    public void InitialiseLevel()
    {
        RoundPopup();

        // Za³aduj dane celów z poziomu, nadaj odpowiednia im predkosc a nastepnie rozpocznij proces tworzenia celów w grze
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
        // Przejscie przez animacje rund zajmuje (okolo) trzy sekundy, wiec tyle czeka program
        yield return new WaitForSeconds(3);
        for (int i = 0; i < targetAmount; i++)
        {
            // Pozycja celu okreslana jest recznie poprzez wpis do tabeli znajdujacej sie w klasie LevelData.cs
            // Stworz cel: numer prefabu (animacji), pozycja, obrot
            targetPosition = new Vector2((chosenLevel.finishedTable[i].locationX), (chosenLevel.finishedTable[i].locationY));
            newTarget = Instantiate(targets[chosenLevel.finishedTable[i].targetType], targetPosition, Quaternion.identity);
            newTarget.transform.parent = enemies;
            
            // Dodanie przerwy pomiedzy tworzeniem celow umozliwia sekwencyjnie ulozyc poziomy
            yield return new WaitForSeconds(chosenLevel.finishedTable[i].delay);
        }
        yield return new WaitUntil(() => enemies.childCount.Equals(1));
        
        roundNumber++;
        if (roundNumber < 2)
        {
            // Pod koniec rundy 1 zaczyna runde druga, pod koniec rundy drugiej sprawdza czy gracz wygral
            InitialiseLevel();
        }
        else
        {
            WinCheck.Instance.Checker();
        }
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(2);
        fadeAnimator.SetTrigger("fade_out");
    }
}
