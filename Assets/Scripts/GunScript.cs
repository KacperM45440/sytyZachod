using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunScript : MonoBehaviour
{
    public bool readyToFire;
    int maxAmmo = 6;
    int currentAmmo;
    float reloadTime = 1.5f;

    public TMP_Text ammoCounter;

    void Start()
    {
        currentAmmo = maxAmmo;
        readyToFire = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ReloadGun();
        }
    }

    public void ShotFired()
    {
        if (readyToFire)
        {
            if (currentAmmo <= 0)
            {
                ReloadGun();
                return;
            }
            currentAmmo--;
            ammoCounter.text = "CURRENT AMMO: " + currentAmmo;
        }
    }

    public void ReloadGun()
    {
        readyToFire = false;
        StartCoroutine(TimeOut());
        currentAmmo = maxAmmo;
    }

    IEnumerator TimeOut()
    {
        yield return new WaitForSeconds(reloadTime);
        readyToFire = true;
    }
}
