using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    public List<string> animations = new List<string>();
    public float speed = 1;
    private GunScript gun;
    public GameObject kontroler;
    private int life;
    private Vector2 targetDirection;
    //private Rigidbody2D rbRef;
    [HideInInspector] public Animator animatorRef;

    void Start()
    {
        animatorRef = GetComponent<Animator>();
        life = animations.Count + 1;
        kontroler = GameObject.Find("GameController");
        gun = kontroler.GetComponent<GunScript>();
        //rbRef = GetComponent<Rigidbody2D>();
        //targetDirection = rbRef.position + new Vector2(1, 1);
    }

    public void FinishedMovement()
    {
        //tutaj skoñczy³ siê pojawiaæ wiêc zmienia prêdkoœæ na docelow¹ targetu - czyli poziom trudnoœci
        animatorRef.speed = speed;

        life--;
        if(life <= 0)
        {
            //zmienia prêdkoœæ na domyœln¹ dla animacji niszczenia
            animatorRef.speed = 1;
            animatorRef.SetTrigger("Disappear");
            return;
        }
        animatorRef.SetTrigger(animations[animations.Count - life]);
    }
    public void Bounce(bool horizontally)
    {
        if (horizontally)
        {
            //targetDirection = rbRef.position + new Vector2(rbRef.position.x - targetDirection.x, 0);
        }
        else
        {
            //targetDirection = rbRef.position + new Vector2(0, rbRef.position.y - targetDirection.y);
        }
    }

    //zosta³ klikniêty, wiêc gra animacjê "zniszczenia"
    private void OnMouseDown()
    {
        if (gun.readyToFire)
        {
            gun.ShotFired();
            //zmienia prêdkoœæ na domyœln¹ dla animacji niszczenia
            animatorRef.speed = 1;
            WinCheck.Instance.Clicked();
            life = 0;
            animatorRef.SetTrigger("Destroy");
        }
    }

    //ten kod odpala animacja zniszczenia i znikniêcia
    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}
