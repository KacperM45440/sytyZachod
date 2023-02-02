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

    
    void Update()
    {
        // Bron domyslnie przeladowywana jest w momencie wystrzelenia wszystkich posiadanych pociskow, natomiast mozna to zrobic recznie w dowolnym momencie
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ReloadGun();
        }
    }

    public void ShotFired()
    {
        // Zaktualizuj przy wystrzale ilosc obecnie posiadanej amunicji.
        // Funckja ma charakter nadzorczy (samej broni), niszczeniem celu zajmuje sie juz jego klasa "TargetMovement"
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
        // Jak w prawdziwym zyciu, podczas przeladowywania broni nie mozemy z niej jednoczesnie korzystac.
        // Blokujac mozliwosc strzalu unikamy sytuacji w ktorej przeladowujemy np. z 4 pociskami, i w trakcie trwania animacji przeladowania strzelamy dalej.
        // Dodajac pocisku na koncu procesu zamiast na poczatku wizualnie sugeruje graczowi, ze jego bron jest juz gotowa do strzalu.
        readyToFire = false;
        StartCoroutine(TimeOut());
        currentAmmo = maxAmmo;
    }

    IEnumerator TimeOut()
    {
        // Zaczekaj na przeladowanie, nastepnie dodaj pociski na koncu przeladowania.       
        // Obecnie timeout czeka okreslona recznie ilosc sekund, natomiast lepiej byloby zamienic ponizsza linijke jakims sygnalem, ze animacja zostala zakonczona.
        yield return new WaitForSeconds(reloadTime);
        ammoCounter.text = "CURRENT AMMO: " + currentAmmo;
        readyToFire = true;
    }
}
