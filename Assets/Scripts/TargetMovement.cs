using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    public List<string> animations = new List<string>();
    private GunScript gun;
    public GameObject kontroler;
    private int life;
    [HideInInspector] public Animator animatorRef;
    [HideInInspector] public float speed;

    void Start()
    {
        animatorRef = GetComponent<Animator>();
        life = animations.Count + 1;
        kontroler = GameObject.Find("GameController");
        gun = kontroler.GetComponent<GunScript>();
    }

    public void FinishedMovement()
    {
        // W tym miejscu cel sko�czy� si� pojawia�, wi�c zmienia pr�dko�� na docelow� - zale�n� od poziomu trudno�ci
        animatorRef.speed = speed;
        life--;
        if(life <= 0)
        {
            // Tutaj cel znika: nie zostal zestrzelony, nie liczy sie do puli punktow
            // Animacja niszczenia nie ma byc zalezna od poziomu trudnosci, zmieniamy pr�dko�� na domy�ln� przed zniknieciem
            animatorRef.speed = 1;
            animatorRef.SetTrigger("Disappear");
            return;
        }
        animatorRef.SetTrigger(animations[animations.Count - life]);
    }

    // Cel zosta� klikni�ty, wi�c gra animacj� "zniszczenia"
    //
    // Todo: zamienic animacje niszczenia na particle?
    private void OnMouseDown()
    {
        if (gun.readyToFire)
        {
            gun.ShotFired();
            // To samo co wyzej; zmieniamy pr�dko�� na domy�ln� przed zniszczeniem
            animatorRef.speed = 1;
            WinCheck.Instance.Clicked();
            life = 0;
            animatorRef.SetTrigger("Destroy");
        }
    }

    // Tutaj wlaczana jest animacja znikniecia i zniszczenia (wygasniecia) celu. Funkcja odpowiadzialna za zestrzelenie jest wyzej

    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}
