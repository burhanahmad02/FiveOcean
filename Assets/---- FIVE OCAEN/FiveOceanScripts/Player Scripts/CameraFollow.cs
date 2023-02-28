using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance;
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    //enable and set the maximum Y value
    public bool yMaxEnabled;
    public float yMaxValue = 0;
    //enable and set the minimum Y value
    public bool yMinEnabled;
    public float yMinValue = 0;
    //enable and set the maximum X value
    public bool xMaxEnabled = false;
    public float xMaxValue = 0;
    //enable and set the minimum X value
    public bool xMinEnabled = false;
    public float xMinValue = 0;
    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
        // PlayerController.isInputing = false;
        //PlayerController.isRestricting = false;

    }
    
    void FixedUpdate()
    {
       
        Vector3 desiredPosition = target.position + offset;

        //vertical
        if(yMinEnabled && yMaxEnabled)
        {
            desiredPosition.y = Mathf.Clamp(target.position.y, yMinValue, yMaxValue);
        }
        else if (yMinEnabled)
        {
            desiredPosition.y = Mathf.Clamp(target.position.y, yMinValue, target.position.y);
            
        }
        else if (yMaxEnabled)
        {
            desiredPosition.y = Mathf.Clamp(target.position.y,target.position.y, yMaxValue);
        }
        //horizontal
        if (xMinEnabled && xMaxEnabled)
        {
            desiredPosition.x = Mathf.Clamp(target.position.x, xMinValue, xMaxValue);
            //PlayerController.Instance.FindBoundaries();
        }
        else if (xMinEnabled)
        {
            desiredPosition.x = Mathf.Clamp(target.position.x, xMinValue, target.position.x);
            //PlayerController.Instance.FindBoundaries();
        }
        else if (xMaxEnabled)
        {
            desiredPosition.x = Mathf.Clamp(target.position.x, target.position.x, xMaxValue);
            
            //PlayerController.Instance.FindBoundaries();
        }

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        //transform.LookAt(target);
    }
}
