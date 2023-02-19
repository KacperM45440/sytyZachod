using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class GunScript : MonoBehaviour
{
    [HideInInspector] public Animator animatorRef;
    private GameObject currentBullet;
    private GameObject currentMagazine;
    private Transform cylinderBody;
    private SpriteRenderer spriteRef;
    private bool isReloading;
    public GameObject cylinder;
    public GameObject magazinePrefab;
    public Sprite firedBullet;
    public Transform destroyQueue;
    private Vector3 cameraPos;
    public bool readyToFire;
    int currentAmmo;
    int maxAmmo = 6;
    float reloadTime = 1.25f;

    void Start()
    {
        currentMagazine = GameObject.Find("Magazine(Clone)");
        currentAmmo = maxAmmo;
        readyToFire = true;
    }

    private void Awake()
    {
        cylinderBody = cylinder.transform.GetChild(0);
        animatorRef = cylinderBody.GetComponent<Animator>();
    }

    void Update()
    {
        // Bron domyslnie przeladowywana jest w momencie wystrzelenia wszystkich posiadanych pociskow, natomiast mozna to zrobic recznie w dowolnym momencie
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ReloadGun();
        }
        // Zniszcz pociski, ktore wylecialy juz poza ekran
        if (destroyQueue.childCount > 0)
        {
            cameraPos = Camera.main.WorldToScreenPoint(destroyQueue.GetChild(destroyQueue.childCount - 1).transform.position);
            bool outOfBounds = !Screen.safeArea.Contains(cameraPos);
            if (outOfBounds)
            {
                Destroy(destroyQueue.GetChild(destroyQueue.childCount - 1).gameObject);
            }
        }
    }

    public void ShotFired()
    {
        // Zaktualizuj przy wystrzale ilosc obecnie posiadanej amunicji.
        // Funckja ma charakter nadzorczy (samej broni), niszczeniem celu zajmuje sie juz jego klasa "TargetMovement"
        if (readyToFire)
        {
            currentAmmo--;
            animatorRef.SetTrigger("Rotate Single");
            DestroyBullet();
            if (currentAmmo <= 0)
            {
                ReloadGun();
                return;
            }
        }
    }


    public void DestroyBullet()
    {
        // Pociski sa odpinane z magazynka po jednym, wylatuja za ekran a nastepnie sa niszczone.
        // Zmieniana im jest rowniez waga, kierunek odrzutu oraz obrazek w celu uatrakcyjnienia procesu przeladowania.
        currentBullet = currentMagazine.GetComponent<Transform>().GetChild(0).gameObject;
        currentBullet.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        currentBullet.transform.parent = destroyQueue;

        currentBullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(UnityEngine.Random.Range(-100, 100), 0));
        spriteRef = currentBullet.GetComponent<SpriteRenderer>();
        spriteRef.sprite = firedBullet;
    }
    public void ReloadGun()
    {
        // Jak w prawdziwym zyciu, podczas przeladowywania broni nie mozemy z niej jednoczesnie korzystac.
        // Blokujac mozliwosc strzalu unikamy sytuacji w ktorej przeladowujemy np. z 4 pociskami, i w trakcie trwania animacji przeladowania strzelamy dalej.
        // Dodajac pociski na koncu procesu zamiast na poczatku wizualnie sugeruje graczowi, ze jego bron jest juz gotowa do strzalu.
        if (!isReloading)
        {
            isReloading = true;
            readyToFire = false;
            if (currentAmmo > 0)
            {
                int x = currentMagazine.transform.childCount;
                for (int i = 0; i < x; i++)
                {
                    DestroyBullet();
                }
            }
            StartCoroutine(TimeOut());
            currentAmmo = maxAmmo;
        }
    }
    IEnumerator TimeOut()
    {
        // Zaczekaj na przeladowanie, nastepnie dodaj pociski na koncu przeladowania.       
        // Obecnie timeout czeka okreslona recznie ilosc sekund, natomiast lepiej byloby zamienic ponizsza linijke jakims sygnalem, ze animacja zostala zakonczona.
        // Ponowna inicjalizacja magazynka; przeladowanie graficzne ma charakter wklejenie prefabu na nowo, wiec nie moze zostac odwolan do starych obiektow
        Destroy(currentMagazine);
        yield return new WaitUntil(() => currentMagazine.Equals(null));

        cylinderBody.localEulerAngles = SpawnMag.Instance.originalRotation;
        animatorRef.SetTrigger("Full Rotate");
        
        yield return new WaitForSeconds(reloadTime);
        SpawnMag.Instance.NewMagazine();
        currentMagazine = GameObject.Find("Magazine(Clone)");
        isReloading = false;
        readyToFire = true;
    }
}
