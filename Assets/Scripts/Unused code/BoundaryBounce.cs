using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryBounce : MonoBehaviour
{
    public bool isHorizontal;

    // Ten fragment kodu odpowiedzialny by� za odbicie kierunku celu w drug� stron�, w przypadku wyjechania poza granice ekranu.
    // Nie jest juz wykorzystywany poniewa� cele nie s� ju� ustawiane losowo, a w dodatku nie dzia�a� ze statycznym Rigidbody.
    // Funkcja "bounce" wykonywana by�a w skrypcie "TargetMovement" wykrytego celu.
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TargetTag"))
        {
            Debug.Log("porownalem");
            GameObject target = collision.gameObject;
            //target.GetComponent<TargetMovement>().Bounce(isHorizontal);
        }
    }
}
