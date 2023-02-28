using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateWithAcc : MonoBehaviour
{
    [SerializeField]
    private float LimiteSx;
    [SerializeField]
    private float LimiteDx;

    [SerializeField]
    private float Speed = 15f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Input.acceleration.x * Time.deltaTime * Speed, 0, 0);


        //transform.position = new Vector3(Mathf.Clamp(transform.position.x,-2.82f, 2.82f), transform.position.y,  transform.position.z);

        Vector3 ClampedPosition = transform.position;
        ClampedPosition.x = Mathf.Clamp(transform.position.x, LimiteSx, LimiteDx);
        transform.position = ClampedPosition;
    }
}