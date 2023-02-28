using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SetOnCorners : MonoBehaviour
{
  /*  public GameObject left;
    public GameObject right;*/
   
    public enum Corner
    {
        right,
        left
    }
    public Corner cornerName;
    public float offset;
    public void Awake()
    {
        /*if (cornerName == Corner.right)
        {
            transform.position = new Vector2(20.23f, 25.82f);
        }
        else if (cornerName == Corner.left)
        {
            transform.position = new Vector2(-21.6f, 20.47f);
        }*/
        // Corner.left.transform.position = 
        // left.transform.position = 
        //right.transform.position = new Vector2(20.23f, 25.82f);
        /*       left.transform.position = new Vector2(-59.7f, -34.6f);
               right.transform.position = new Vector2(-88.4f, -35.5f);*/
    }
    private void Start()
    {
        Set();
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
        {
            StartCoroutine(UpdateMyPosition());
        }
    }
    IEnumerator UpdateMyPosition()
    {
        while (true)
        {
            yield return null;
            Set();
        }
    }
    private void Set()
    {
        float halfCamHeight = Camera.main.orthographicSize;
        float halfCamWidth = halfCamHeight * Camera.main.aspect;

      /* if (cornerName == Corner.right)
        {
            transform.position = new Vector3(halfCamWidth + offset, transform.position.y, transform.position.z);
        }
        else if (cornerName == Corner.left)
        {
            transform.position = new Vector3(-halfCamWidth + offset, transform.position.y, transform.position.z);
        }*/
    }
}
