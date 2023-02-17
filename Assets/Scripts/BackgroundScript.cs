using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScript : MonoBehaviour
{
    private GunScript gun;
    public Animator enemyAnimator;
    public GameObject gunController;
    public Slider finisherBar;
    public bool dominated;
    public bool canPunch;

    void Start()
    {
        gun = gunController.GetComponent<GunScript>();
        dominated = false;
    }

    private void OnMouseDown()
    {
        // Jako ze mozna nie trafic celu, klikniecie w tlo powoduje wystrzelenie (i zmarnowanie) pocisku
        if (!dominated)
        {
            gun.ShotFired();
            WinCheck.Instance.Missed();
        }
        else
        {
            // W etapie bonusowym, z zalozenia mozna klikac nieskonczonosc razy, dlatego nie ma koniecznosci podpinania (i konfigurowania) go pod system strzelania
            if (canPunch)
            {
                WinCheck.Instance.DominationPunch();
                enemyAnimator.SetTrigger("hurt");
            }
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
        enemyAnimator.SetTrigger("dead");
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(10);
        canPunch = false;
    }
    IEnumerator DrainBar()
    {
        for (int i=0; i<10; i++)
        {
            yield return new WaitForSeconds(1);
            finisherBar.value -= 0.1f;
        }
    }
}
