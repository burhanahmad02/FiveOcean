using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Shake : MonoBehaviour
{
    public float duration;
    public float strength;
    public int vibrato;
    public float randomness;
    public bool fadeOut;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.DOShakeScale(duration, strength, vibrato, randomness,fadeOut)
            .SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
