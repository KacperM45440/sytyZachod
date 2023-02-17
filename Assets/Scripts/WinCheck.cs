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
    private int maxScore;
    private float progress;
    private static WinCheck _instance;
    public static WinCheck Instance { get { return _instance; } }
    public Animator fadeAnimatorRef;
    public Animator popupAnimatorRef;
    public Animator uiAnimatorRef;
    public BackgroundScript backgroundRef;
    public GameObject progressUI;
    public GameObject finisherUI;
    public Slider scoreBar;
    public SpawnTarget spawnRef;
    public TMP_Text comboCounter;
    public TMP_Text scoreCounter;
    public int combo;
    public int score;

    private void Start()
    {
        // Maksymalny mozliwy wynik jest suma tarcz z obu rund
        maxScore = spawnRef.targetAmount * 2;
    }
    private void Awake()
    {
        Application.targetFrameRate = 60;

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

    
    public void Clicked()
    {
        // W przypadku trafienia, zwieksz ilosc trafionych celow o jeden, oraz popchnij do przodu pasek postepu.
        // Zwykle zestrzelenie celu przyznaje 100 punktow, natomiast trafianie wielu celow z rzedu podbija mnoznik combo.
        targetCounter++;
        score += (100 * combo);
        // Wynik zawsze wyswietlany jest szesciocyfrowo, w przypadku mniejszych sum uzupelniany jest brakujacymi zerami
        // 125 = 000125, 68 - 000068 itp.
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
        // Zresetuj combo w przypadku trafienia w tlo
        combo = 1;
        comboCounter.text = "X"+combo;
    }

    public void DominationPunch()
    {
        // Funkcja odpowiedzialna za przyznawanie punktow w etapie bonusowym
        // Te punkty nie podlegaja premiowaniu za combo
        score += 100;
        scoreCounter.text = score.ToString("D6");
    }    

    // Sprawdz, czy gracz wygral w gre. 
    //
    // Todo: 
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
            // Zestrzelenie wiekszosci celow wynagradza etapem bonusowym przed zmiana poziomu
            StartCoroutine(Finisher());
        }
        else
        {
            // W przypadku przegranej, przeciwnik nie ginie
            fadeAnimatorRef.SetTrigger("fade_in");
            popupAnimatorRef.SetTrigger("defeat");
        }
    }

    IEnumerator Finisher()
    {
        // Podmien pasek postepu na pasek czasu etapu bonusowego
        uiAnimatorRef.SetTrigger("roll_back");
        yield return new WaitForSeconds(0.5f);
        progressUI.SetActive(false);
        finisherUI.SetActive(true);
        uiAnimatorRef.SetTrigger("roll_up");

        // Poczekaj na zakonczenie etapu, a nastepnie przyznaj dodatkowe punkty za pokonanie przeciwnika
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
