using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drive1 : MonoBehaviour
{
    public float speed = 5.0f;
    int moveCount = 2;
    public Vector3 traveller;
    public Vector3 destination;
    public Vector3 newDestination;
    private void Start()
    {

      

    }
    void Update()
    {
        float Step = 20.5f;
        transform.position = Vector3.MoveTowards(traveller, destination, Step);

    }
}
