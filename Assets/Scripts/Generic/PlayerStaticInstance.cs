using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStaticInstance : MonoBehaviour
{
    public static PlayerStaticInstance Instance;
    // Use this for initialization
    void Awake ()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }
}
