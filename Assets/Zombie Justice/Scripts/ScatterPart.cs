using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatterPart : MonoBehaviour
{
    void Start()
    {
        if (!GetComponent<Rigidbody2D>())
            this.gameObject.AddComponent<Rigidbody2D>();
        Scatter();
    }

    public void Scatter()
    {
        Vector2 temp = new Vector2(Random.Range(-5,5), Random.Range(5,10));
        this.GetComponent<Rigidbody2D>().AddForce(temp, ForceMode2D.Impulse );
       // rigidbody.AddForce(0, 100, 0);
    }
}
