using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScript : MonoBehaviour
{
    private GunScript gun;
    public Animator enemyAnimator;
    public GameObject gunController;
    private SpriteRenderer rendererRef;
    public WinCheck winCheckRef;
    public Slider finisherBar;
    public Sprite enemyDeadSprite;
    public Sprite enemyPunched1;
    public Sprite enemyPunched2;
    public Sprite enemyPunched3;
    public AudioSource enemyDeathSound;
    public AudioSource punchSource1;
    public AudioSource punchSource2;
    public AudioSource punchSource3;
    

    public bool dominated;
    public bool canPunch;
    int i;
    int j;
    int n;
    int previousAnim;

    void Start()
    {
        rendererRef = winCheckRef.enemySprite;
        gun = gunController.GetComponent<GunScript>();
        dominated = false;
    }

    private void OnMouseDown()
    {
        // Jako ze mozna nie trafic celu, klikniecie w tlo powoduje wystrzelenie (i zmarnowanie) pocisku
        if (!dominated)
        {
            gun.ShotFired();
            winCheckRef.Missed();
        }
        else
        {
            // W etapie bonusowym, z zalozenia mozna klikac nieskonczonosc razy, dlatego nie ma koniecznosci podpinania (i konfigurowania) go pod system strzelania
            if (canPunch)
            {
                EnemyPunched();              
            }
        }
    }


    // Przy uderzeniu, losowo wybierz animacje pobicia. Nie moze byc ona taka sama jak poprzednia
    public void EnemyPunched()
    {
        winCheckRef.DominationPunch();
        enemyAnimator.SetTrigger("hurt");

        while (i.Equals(previousAnim))
        {
            i = Random.Range(1, 4);
        }
        j = Random.Range(1, 3);
        n = Random.Range(1, 3); 

        if (i.Equals(1))
        {
            rendererRef.sprite = enemyPunched1;
            punchSource1.Play();
            previousAnim = i;
        }
        else if (i.Equals(2))
        {
            rendererRef.sprite = enemyPunched2;
            punchSource2.Play();
            previousAnim = i;
        }
        else
        {
            rendererRef.sprite = enemyPunched3;
            punchSource3.Play();
            previousAnim = i;
        }

        if (j.Equals(1))
        {
            rendererRef.flipX = true;
        }
        else
        {
            rendererRef.flipX = false;
        }

        if (n.Equals(1))
        {
            enemyAnimator.SetTrigger("recoil1");
        }
        else
        {
            enemyAnimator.SetTrigger("recoil2");
        }
    }
    public void PunchOut()
    {
        // Zamiana broni na piesci, zaczecie odliczania czasu etapu bonusowego
        dominated = true;
        canPunch = true;
        StartCoroutine(Timer());
        StartCoroutine(DrainBar());
    }
    public void KillEnemy()
    {
        // Zagraj animacje zabicia przeciwnika
        enemyAnimator.SetTrigger("dead");
        enemyDeathSound.Play();
        rendererRef.sprite = enemyDeadSprite;
    }
    IEnumerator Timer()
    {
        // Odmierz 5 sekund etapu bonusowego, po tym czasie zablokuj mozliwosc bicia
        yield return new WaitForSeconds(5);
        canPunch = false;
    }
    IEnumerator DrainBar()
    {
        // Zaktualizuj wizualnie stan poziomu bonusowego na pasku
        for (int i=0; i<5; i++)
        {
            yield return new WaitForSeconds(1);
            finisherBar.value -= 0.2f;
        }
    }
}
