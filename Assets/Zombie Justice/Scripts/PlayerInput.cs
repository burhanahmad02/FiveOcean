using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private bool isMobilePlatform = false;

    void Start()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            isMobilePlatform = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isMobilePlatform)
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);

                Ray ray = Camera.main.ScreenPointToRay(touch.position);

                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("hit with " + hit.collider.name);
                }
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                if (hit.collider != null)
                {
                    Debug.Log(hit.collider.gameObject.name);
                    //hit.collider.attachedRigidbody.AddForce(Vector2.up);
                    Destroy(hit.collider);
                }
                else
                {
                    Debug.Log("null");
                }
            }
        }
    }
}