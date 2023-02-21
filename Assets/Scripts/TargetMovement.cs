using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    [HideInInspector] public Animator animatorRef;
    [HideInInspector] public float speed;
    private GunScript gun;
    private int life;
    public GameObject kontroler;
    public Transform destroyQueue;
    private Transform child;
    private GameObject shards;
    private Animator shardAnimator;
    public List<string> animations = new();

    void Start()
    {
        animatorRef = GetComponent<Animator>();
        life = animations.Count + 1;
        kontroler = GameObject.Find("GameController");
        destroyQueue = GameObject.Find("DestroyQueue").transform;
        gun = kontroler.GetComponent<GunScript>();
        shards = transform.parent.GetChild(1).gameObject;
        shardAnimator = shards.GetComponent<Animator>();
    }

    public void FinishedMovement()
    {
        // W tym miejscu cel skoñczy³ siê pojawiaæ, wiêc zmienia prêdkoœæ na docelow¹ - zale¿n¹ od poziomu trudnoœci
        animatorRef.speed = speed;
        life--;
        if(life <= 0)
        {
            // Tutaj cel znika: nie zostal zestrzelony, nie liczy sie do puli punktow
            // Animacja niszczenia nie ma byc zalezna od poziomu trudnosci, zmieniamy prêdkoœæ na domyœln¹ przed zniknieciem
            animatorRef.speed = 1;
            StartCoroutine(Disappear());
            return;
        }
        animatorRef.SetTrigger(animations[animations.Count - life]);
    }


    private void OnMouseDown()
    {
        Debug.Log("klik");
        if (gun.readyToFire)
        {
            gun.ShotFired();
            // To samo co wyzej; zmieniamy prêdkoœæ na domyœln¹ przed zniszczeniem
            animatorRef.speed = 1;
            WinCheck.Instance.Clicked();
            life = 0;
            StartCoroutine(DestroyMe());
        }
    }

    // Tutaj wlaczana jest animacja znikniecia i zniszczenia (wygasniecia) celu. Funkcja odpowiadzialna za zestrzelenie jest wyzej
    IEnumerator Disappear()
    {
        animatorRef.SetTrigger("Disappear");
        float animationLength = animatorRef.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSecondsRealtime(animationLength/5);
        Destroy(transform.parent.gameObject);
    }

    // Cel zosta³ klikniêty, wiêc gra animacjê "zniszczenia"
    IEnumerator DestroyMe()
    {
        shards.transform.SetPositionAndRotation(transform.position, transform.rotation);
        shards.SetActive(true);
        shardAnimator.SetTrigger("shard_fade");

        shards.transform.parent = destroyQueue;
        for (int i = 0; i < 9; i++)
        {
            child = shards.transform.GetChild(i);
            child.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            child.GetComponent<Rigidbody2D>().AddForce(new Vector2(UnityEngine.Random.Range(-250, 250), 300));
        }

        transform.GetComponent<SpriteRenderer>().enabled = false;
        transform.GetComponent<CircleCollider2D>().enabled = false;
        shards.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSecondsRealtime(0);
        Destroy(transform.parent.gameObject);
    }
}
