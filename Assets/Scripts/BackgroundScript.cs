using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    private GunScript gun;
    public GameObject kontroler;
    // Start is called before the first frame update
    void Start()
    {
        gun = kontroler.GetComponent<GunScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        gun.ShotFired();
    }
}
