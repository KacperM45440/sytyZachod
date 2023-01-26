using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundariesKeeper : MonoBehaviour
{
    private Vector2 screenBounds;
    private float targetRadius;
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        targetRadius = transform.GetComponent<SpriteRenderer>().bounds.size.x*2;
    }

    // Update is called once per frame
    void Update()
    {
        //    Vector3 viewPos = transform.position;
        //  viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 - targetRadius, screenBounds.x + targetRadius);
        // viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 - targetRadius, screenBounds.y + targetRadius);
        // if transform.position = viewPos;

        var pos = Camera.main.WorldToScreenPoint(transform.position);

        bool outOfBounds = !Screen.safeArea.Contains(pos);
        Debug.Log(Screen.safeArea);
        if (outOfBounds)
        {
            Debug.Log("Malpa w kosmosie");
        }
    }
}
