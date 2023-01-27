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
        //tutaj sko�czy� si� pojawia� wi�c zmienia pr�dko�� na docelow� targetu - czyli poziom trudno�ci
        animatorRef.speed = speed;

        life--;
        if(life <= 0)
        {
            //zmienia pr�dko�� na domy�ln� dla animacji niszczenia
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

    //zosta� klikni�ty, wi�c gra animacj� "zniszczenia"
    private void OnMouseDown()
    {
        if (gun.readyToFire)
        {
            gun.ShotFired();
            //zmienia pr�dko�� na domy�ln� dla animacji niszczenia
            animatorRef.speed = 1;
            WinCheck.Instance.Clicked();
            life = 0;
            animatorRef.SetTrigger("Destroy");
        }
    }

    //ten kod odpala animacja zniszczenia i znikni�cia
    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}
