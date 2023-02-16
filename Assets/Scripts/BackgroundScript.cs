using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScript : MonoBehaviour
{
    public Animator enemyAnimator;
    private GunScript gun;
    public Slider finisherBar;
    public GameObject kontroler;
    public bool dominated;
    public bool canPunch;

    void Start()
    {
        gun = kontroler.GetComponent<GunScript>();
        dominated = false;
    }

    private void OnMouseDown()
    {
        //Jako ze mozna nie trafic celu, klikniecie w tlo powoduje wystrzelenie (i zmarnowanie) pocisku
        if (!dominated)
        {
            gun.ShotFired();
            WinCheck.Instance.Missed();
        }
        else
        {
            if (canPunch)
            {
                WinCheck.Instance.DominationPunch();
                enemyAnimator.SetTrigger("hurt");
            }
        }
    }

    public void PunchOut()
    {
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
