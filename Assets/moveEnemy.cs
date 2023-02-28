using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveEnemy : MonoBehaviour
{
    public float speed = 2;
void Update()
{
    float x = Input.GetAxis("Horizontal");
    float y = Input.GetAxis("Vertical");
    Vector3 movement = new Vector3(x, y, 0);
    transform.Translate(movement * speed * Time.deltaTime);
}
}
