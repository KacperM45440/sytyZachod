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
    public Animator enemyAnimatorRef;
    public BackgroundScript backgroundRef;
    public GameObject progressUI;
    public GameObject finisherUI;
    public GunScript gunRef;
    public Slider scoreBar;
    public Sprite enemyTired;
    public Sprite enemyDominated;
    public SpriteRenderer enemySprite;
    public SpawnTarget spawnRef;
    public TMP_Text comboCounter;
    public TMP_Text scoreCounter;
    public TransitionScript changeScene;
    public static bool isPaused;
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
        score = PlayerPrefs.GetInt("currentScore");
        scoreCounter.text = score.ToString("D6");
        isPaused = false;
        changeScene.ChooseCursor("crosshairShooting");

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
        // W dowolnym miejscu w grze mozna wyjsc do menu glownego
        // Zarowno wyjscie do menu jak zresetowanie poziomu wiaze sie z wyzerowaniem wyniku
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CheckPause();
        }
    }

    public void CheckPause()
    {
        if(isPaused.Equals(false))
        {
            PauseGame();
        }
        else
        {
            Unpause();
        }
    }
    public void PauseGame()
    {
        // W menu pauzy zabierana jest mozliwosc strzalu aby nie marnowac pociskow przy kliknieciu
        isPaused = true;
        gunRef.readyToFire = false;
        fadeAnimatorRef.SetTrigger("fade_in");
        popupAnimatorRef.SetTrigger("pause_menu");
    }    
    
    public void Clicked()
    {
        // W przypadku trafienia, zwieksz ilosc trafionych celow o jeden, oraz popchnij do przodu pasek postepu.
        // Zwykle zestrzelenie celu przyznaje 100 punktow, natomiast trafianie wielu celow z rzedu podbija mnoznik combo.
        targetCounter++;
        EnemyCheck();
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

    public void EnemyCheck()
    {
        // Gdy zestrzelony jest cel, przeciwnik gra animacje bolu
        // Sprawdzane jest rowniez, czy zostalo zestrzelone wystarczajaco celow aby podmienic mu obrazek na bardziej zmeczony, zaznaczajac tym samym postep poziomu
        enemyAnimatorRef.SetTrigger("hurt");
        if (targetCounter >= maxScore * 0.6f && targetCounter < maxScore * 0.8f)
        {
            enemySprite.sprite = enemyTired;
        }
        else if (targetCounter >= maxScore * 0.8f)
        {
            enemySprite.sprite = enemyDominated;
        }
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
    public void Checker() 
    {
        if (targetCounter >= maxScore * 0.6f && targetCounter < maxScore * 0.8f)
        {
            StartCoroutine(Completed());
        }
        else if (targetCounter >= maxScore * 0.8f)
        {
            // Zestrzelenie wiekszosci celow wynagradza etapem bonusowym przed zmiana poziomu
            StartCoroutine(Finisher());
        }
        else
        {
            // W przypadku przegranej, przeciwnik nie ginie
            gunRef.readyToFire = false;
            Unpause();
            fadeAnimatorRef.SetTrigger("fade_in");
            popupAnimatorRef.SetTrigger("defeat");
        }
    }
    
    IEnumerator NextLevel()
    {
        PlayerPrefs.SetInt("currentScore", score);
        yield return new WaitForSeconds(2);
        changeScene.NextLevel();
    }

    public void Retry()
    {
        score = 0;
        PlayerPrefs.SetInt("currentScore", score);
        popupAnimatorRef.SetTrigger("idle");
        changeScene.ThisLevel();
    }

    public void ExitToMenu()
    {
        score = 0;
        PlayerPrefs.SetInt("currentScore", score);
        GameMusicScript.Instance.FadeOut();
        changeScene.MainMenu();
    }

    public void Unpause()
    {
        if (isPaused)
        {
            if (!fadeAnimatorRef.GetCurrentAnimatorStateInfo(0).IsName("fade_out"))
            {
                fadeAnimatorRef.SetTrigger("fade_out");
            }
            popupAnimatorRef.SetTrigger("idle");
            gunRef.readyToFire = true;
            isPaused = false;
        }
    }
    IEnumerator Completed()
    {
        // W przypadku zwyklej wygranej nie przechodzimy do etapu bonusowego, a bezposrednio do nastepnego poziomu
        score += 5000;
        scoreCounter.text = score.ToString("D6");
        gunRef.readyToFire = false;

        backgroundRef.KillEnemy();
        Unpause();
        yield return new WaitForSeconds(1f);
        fadeAnimatorRef.SetTrigger("fade_in");
        popupAnimatorRef.SetTrigger("win_regular");
        StartCoroutine(NextLevel());
    }
    IEnumerator Finisher()
    {
        // Podmien pasek postepu na pasek czasu etapu bonusowego
        uiAnimatorRef.SetTrigger("roll_back");
        yield return new WaitForSeconds(0.5f);
        progressUI.SetActive(false);
        finisherUI.SetActive(true);
        uiAnimatorRef.SetTrigger("roll_up");
        changeScene.ChooseCursor("fistCrosshair");

        // Poczekaj na zakonczenie etapu, a nastepnie przyznaj dodatkowe punkty za pokonanie przeciwnika
        yield return new WaitForSeconds(0.25f);
        backgroundRef.PunchOut();
        yield return new WaitUntil(() => backgroundRef.canPunch.Equals(false));
        score += 10000;
        scoreCounter.text = score.ToString("D6");
        gunRef.readyToFire = false;

        // Zagraj animacje pokonanego przeciwnika, animacje wygrania etapu a nastepnie zmien poziom na nastepny
        backgroundRef.KillEnemy();
        changeScene.ChooseCursor("crosshairShooting");
        Unpause();
        yield return new WaitForSeconds(1f);
        fadeAnimatorRef.SetTrigger("fade_in");
        popupAnimatorRef.SetTrigger("win_domination");

        StartCoroutine(NextLevel());
    }
}
