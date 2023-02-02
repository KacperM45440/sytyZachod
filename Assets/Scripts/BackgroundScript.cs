using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    private GunScript gun;
    public GameObject kontroler;

    void Start()
    {
        gun = kontroler.GetComponent<GunScript>();
    }

    private void OnMouseDown()
    {
        //Jako ze mozna nie trafic celu, klikniecie w tlo powoduje wystrzelenie (i zmarnowanie) pocisku
        gun.ShotFired();
    }
}
