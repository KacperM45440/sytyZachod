using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinCheck : MonoBehaviour
{
    // Z klasy WinCheck tworzony jest singleton, poniewaz sprawdzac czy wygralismy na pewno bedziemy w jednym miejscu.
    // Nie ma tez potrzeby tworzenia zadnych kopii tej klasy. Dzieki temu ulatwic mozna odwolywanie sie do niej z innych miejsc w programie.
    [HideInInspector] public int targetCounter;
    public BackgroundScript backgroundRef;
    public Animator popupAnimatorRef;
    public Animator fadeAnimatorRef;
    public Animator uiAnimatorRef;
    public Slider scoreBar;
    public SpawnTarget spawnRef;
    private static WinCheck _instance;
    public static WinCheck Instance { get { return _instance; } }
    public TMP_Text comboCounter;
    public TMP_Text scoreCounter;
    public GameObject progressUI;
    public GameObject finisherUI;

    public int combo;
    public int score;

    private int maxScore;
    private float progress;

    private void Start()
    {
        maxScore = spawnRef.iloscCelow*2;
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    // W przypadku trafienia, zwieksz na ekranie ilosc trafionych celow o jeden.
    public void Clicked()
    { 
        targetCounter++;
        score += (100 * combo);
        scoreCounter.text = score.ToString("D6");
        if (combo < 5)
        {
            combo++;
        }
        comboCounter.text = "X" + combo;
        progress = (float)targetCounter / (float)maxScore;
        scoreBar.value = progress;
    }

    public void Missed()
    {
        combo = 1;
        comboCounter.text = "X"+combo;
    }

    public void DominationPunch()
    {
        score += 100;
        scoreCounter.text = score.ToString("D6");
    }    
    // Sprawdz, czy gracz wygral w gre. 
    //
    // Todo: 
    // 1. Nie trzeba zestrzelic wszystkich celow, ustanowic progi typu 70%, 80%, 90% zaleznie od poziomu
    // 2. Aby wygrac w gre, poziom musi sie zakonczyc, nawet jesli prog zostal juz wczesniej przekroczony
    // 3. Po wygranej, przekierowac na animacje zwyciestwa nad przeciwnikiem, nastepnie zmiana sceny/poziomu
    public void Checker() 
    {
        if (targetCounter >= maxScore * 0.6f && targetCounter < maxScore * 0.95f)
        {
            score += 5000;
            scoreCounter.text = score.ToString("D6");

            backgroundRef.KillEnemy();
            fadeAnimatorRef.SetTrigger("fade_in");
            popupAnimatorRef.SetTrigger("win_regular");
        }
        else if (targetCounter >= maxScore * 0.95f)
        {
            StartCoroutine(Finisher());
        }
        else
        {
            fadeAnimatorRef.SetTrigger("fade_in");
            popupAnimatorRef.SetTrigger("defeat");
        }
    }

    IEnumerator Finisher()
    {
        uiAnimatorRef.SetTrigger("roll_back");
        yield return new WaitForSeconds(0.5f);
        progressUI.SetActive(false);
        finisherUI.SetActive(true);
        uiAnimatorRef.SetTrigger("roll_up");

        yield return new WaitForSeconds(0.25f);
        backgroundRef.PunchOut();
        yield return new WaitUntil(() => backgroundRef.canPunch.Equals(false));
        score += 10000;
        scoreCounter.text = score.ToString("D6");

        backgroundRef.KillEnemy();
        fadeAnimatorRef.SetTrigger("fade_in");
        popupAnimatorRef.SetTrigger("win_domination");
    }
}
