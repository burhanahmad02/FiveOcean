using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    public float destroyAfter = 2f;

    void Start()
    {
        Invoke("Destroy", destroyAfter);
    }

    void Destroy()
    {
        Destroy(this.gameObject);
    }
}