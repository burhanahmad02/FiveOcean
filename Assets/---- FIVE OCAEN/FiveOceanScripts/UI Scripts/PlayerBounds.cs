using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounds : MonoBehaviour
{
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x+objectWidth, screenBounds.x * -1);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y+objectHeight, screenBounds.y * -1);
        transform.position = viewPos;
    }
}
