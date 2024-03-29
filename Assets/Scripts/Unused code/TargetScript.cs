using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    public float targetSpeed;
    private float directionTime = 3f;
    private float timeLeft;
    private Vector2 targetDirection;
    private Rigidbody2D rbRef;
    void Start()
    {
        rbRef = GetComponent<Rigidbody2D>();
        targetDirection = rbRef.position + new Vector2(1, 1);
    }

    // Stare kawa�ki kodu odpowiedzialne za poruszanie celem.
    // Obecnie cele nie s� poruszane wy��cznie kodem, poniewa� korzystaj� r�wnie� z animacji, kt�re k��ci�y si� z poni�szymi funkcjami.

    private void Update()
    {
        if (Vector2.Distance(rbRef.position,targetDirection) <= 0.1f)
        {
            targetDirection = rbRef.position + new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
            //targetDirection = new Vector2(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f)).normalized;
            timeLeft += directionTime;
        }
    }

    private void FixedUpdate()
    {
        rbRef.position = Vector2.MoveTowards(rbRef.position, targetDirection, targetSpeed * Time.deltaTime);
        //rbRef.position += new Vector2(targetSpeed * targetDirection.x, targetSpeed * targetDirection.y);
    }

    private void OnMouseDown()
    {
        WinCheck.Instance.Clicked();
        Destroy(gameObject);
    }
}
