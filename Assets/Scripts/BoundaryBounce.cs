using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryBounce : MonoBehaviour
{
    public bool isHorizontal;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TargetTag"))
        {
            Debug.Log("porownalem");
            GameObject target = collision.gameObject;
            target.GetComponent<TargetMovement>().Bounce(isHorizontal);
        }
    }
}
