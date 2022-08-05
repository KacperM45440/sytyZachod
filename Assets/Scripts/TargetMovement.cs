using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    public List<string> animations = new List<string>();
    public float speed = 1;
    
    private int life;
    [HideInInspector] public Animator animatorRef;

    void Start()
    {
        animatorRef = GetComponent<Animator>();
        life = animations.Count + 1;   
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

    //zosta³ klikniêty, wiêc gra animacjê "zniszczenia"
    private void OnMouseDown()
    {
        //zmienia prêdkoœæ na domyœln¹ dla animacji niszczenia
        animatorRef.speed = 1;

        Debug.Log("kliku klik");
        WinCheck.Instance.Clicked();
        life = 0;
        animatorRef.SetTrigger("Destroy");
    }

    //ten kod odpala animacja zniszczenia i znikniêcia
    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}
