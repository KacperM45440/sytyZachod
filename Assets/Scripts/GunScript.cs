using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunScript : MonoBehaviour
{
    public bool readyToFire;
    int maxAmmo = 6;
    int currentAmmo;
    float reloadTime = 1.25f;

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
            currentAmmo--;
            ammoCounter.text = "CURRENT AMMO: " + currentAmmo;
            if (currentAmmo <= 0)
            {
                ReloadGun();
                return;
            }
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
        ammoCounter.text = "CURRENT AMMO: " + currentAmmo;
        readyToFire = true;
    }
}
