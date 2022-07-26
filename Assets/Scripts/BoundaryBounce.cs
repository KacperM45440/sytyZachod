using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryBounce : MonoBehaviour
{
    public bool isHorizontal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TargetTag"))
        {
            collision.GetComponent<TargetScript>().Bounce(isHorizontal);
        }
    }
}
