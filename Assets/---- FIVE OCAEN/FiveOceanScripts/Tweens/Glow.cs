using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Glow : MonoBehaviour
{
    public float duration;
    public float endValue;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.DOScale(endValue,duration)
           .SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
