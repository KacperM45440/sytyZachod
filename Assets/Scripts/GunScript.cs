using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using TMPro;

public class GunScript : MonoBehaviour
{
    public bool readyToFire;
    int maxAmmo = 6;
    int currentAmmo;
    float reloadTime = 1.25f;

    public GameObject magazinePrefab;
    public GameObject cylinder;
    private Transform cylinderBody;
    private GameObject currentMagazine;
    private GameObject currentBullet;
    private SpriteRenderer spriteRef;
    public Sprite firedBullet;
    public Transform destroyQueue;
    private Vector3 cameraPos;

    [HideInInspector] public Animator animatorRef;
    void Start()
    {
        currentMagazine = GameObject.Find("Magazine(Clone)");
        GameObject.FindGameObjectWithTag("DebugTag").GetComponent<TMP_Text>().text += currentMagazine;
        GameObject.FindGameObjectWithTag("DebugTag").GetComponent<TMP_Text>().text += "i get up";

        currentAmmo = maxAmmo;
        readyToFire = true;
    }

    private void Awake()
    {
        Application.targetFrameRate = 60;
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
   
        if (destroyQueue.childCount > 0)
        {
            cameraPos = Camera.main.WorldToScreenPoint(destroyQueue.GetChild(0).transform.position);
            bool outOfBounds = !Screen.safeArea.Contains(cameraPos);
            if (outOfBounds)
            {
                Destroy(destroyQueue.GetChild(0).gameObject);
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
        // Todo: usprawnic proces
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
        // Dodajac pocisku na koncu procesu zamiast na poczatku wizualnie sugeruje graczowi, ze jego bron jest juz gotowa do strzalu.
        if (currentAmmo > 0)
        {
            int x = currentMagazine.transform.childCount;
            for (int i=0; i < x; i++)
            {
                DestroyBullet();
            }
        }
        readyToFire = false;
        StartCoroutine(TimeOut());
        currentAmmo = maxAmmo;
    }
    IEnumerator TimeOut()
    {
        // Zaczekaj na przeladowanie, nastepnie dodaj pociski na koncu przeladowania.       
        // Obecnie timeout czeka okreslona recznie ilosc sekund, natomiast lepiej byloby zamienic ponizsza linijke jakims sygnalem, ze animacja zostala zakonczona.
        // Ponowna inicjalizacja magazynka; przeladowanie graficzne ma charakter wklejenie prefabu na nowo, wiec nie moze zostac odwolan do starych obiektow
        Destroy(currentMagazine);
        yield return new WaitUntil(() => currentMagazine == null);
        cylinderBody.localEulerAngles = SpawnMag.Instance.originalRotation;
        animatorRef.SetTrigger("Full Rotate");
        yield return new WaitForSeconds(reloadTime);
        SpawnMag.Instance.NewMagazine();
        currentMagazine = GameObject.Find("Magazine(Clone)");
        readyToFire = true;
    }
}
