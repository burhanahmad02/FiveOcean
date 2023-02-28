using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class rotateObject : MonoBehaviour
{
    public float duration;
    public Vector3 endValue;
    // Start is called before the first frame update
    void Start()
    {
        
             
        gameObject.transform.DORotate(endValue,duration,RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
