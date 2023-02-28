using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWorldPosition : MonoBehaviour
{
    public Transform pos;
    private void OnEnable()
    {
        transform.position = pos.position;
    }
}
