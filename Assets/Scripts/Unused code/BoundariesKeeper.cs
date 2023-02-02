using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundariesKeeper : MonoBehaviour
{
    //private Vector2 screenBounds;
    //private float targetRadius;

    // Update is called once per frame
    void Update()
    {
        // Ten kawalek kodu odpowiedzialny byl za sprawdzenie, czy cel aktualnie znajduje sie w widocznym fragmencie ekranu.
        // Jako ze obecnie cele sa ustawiane recznie zamiast losowo, nie jest on juz potrzebny

        var pos = Camera.main.WorldToScreenPoint(transform.position);

        bool outOfBounds = !Screen.safeArea.Contains(pos);
        if (outOfBounds)
        {
            Debug.Log("Malpa w kosmosie");
        }
    }
}
