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

    //zosta� klikni�ty, wi�c gra animacj� "zniszczenia"
    private void OnMouseDown()
    {
        //zmienia pr�dko�� na domy�ln� dla animacji niszczenia
        animatorRef.speed = 1;

        Debug.Log("kliku klik");
        WinCheck.Instance.Clicked();
        life = 0;
        animatorRef.SetTrigger("Destroy");
    }

    //ten kod odpala animacja zniszczenia i znikni�cia
    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}
