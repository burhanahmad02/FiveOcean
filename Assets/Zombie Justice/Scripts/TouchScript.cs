using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchScript : MonoBehaviour
{
    float speed = 4;

    RaycastHit hit = new RaycastHit();

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            if (Physics.Raycast(ray, out hit))
            {
                Destroy(hit.transform.gameObject);
            }
            else
            {
                Debug.Log("NOTHING IS BEEN HIT");
            }
        }
    }
}