using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SpawnTarget : MonoBehaviour
{
    private GameObject newTarget;
    private LevelData chosenLevel;
    private Vector2 targetPosition;
    private int roundNumber;
    private int currentLevel;
    // Przejscie przez animacje rund zajmuje (okolo) siedem sekund, wiec tyle czeka program
    private int roundCooldown;
    // Prefab celu i animacje przypisywane sa w edytorze 
    public List<GameObject> targets = new();
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
            roundCooldown = 7;
            popupAnimator.SetTrigger("versus");
            StartCoroutine(Round1());
        }
        else
        {
            roundCooldown = 3;
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
        ChooseLevel();
        StartCoroutine(TargetSpawnerCoroutine());
    }

    public void ChooseLevel()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        switch (currentLevel)
        {
            case 1:
                chosenLevel.Level1();
                break;
            case 2:
                chosenLevel.Level2();
                break;
            case 3:
                chosenLevel.Level3();
                break;
            default:
                break;
        }
    }
    IEnumerator TargetSpawnerCoroutine()
    {
        yield return new WaitForSeconds(roundCooldown);
        for (int i = 0; i < targetAmount; i++)
        {
            // Pozycja celu okreslana jest recznie poprzez wpis do tabeli znajdujacej sie w klasie LevelData.cs
            // Stworz cel: numer prefabu (animacji), pozycja, obrot
            targetPosition = new Vector3((chosenLevel.finishedTable[i].locationX), (chosenLevel.finishedTable[i].locationY));
            newTarget = Instantiate(targets[chosenLevel.finishedTable[i].targetType], targetPosition, Quaternion.identity);
            newTarget.transform.position = new Vector3(newTarget.transform.position.x, newTarget.transform.position.y, 100);
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

    IEnumerator Round1()
    {
        yield return new WaitForSeconds(4);
        popupAnimator.SetTrigger("round1");
        StartCoroutine(FadeOut());
    }
    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(2);
        if (!fadeAnimator.GetCurrentAnimatorStateInfo(0).IsName("fade_out"))
        {
            fadeAnimator.SetTrigger("fade_out");
        }
    }
}
