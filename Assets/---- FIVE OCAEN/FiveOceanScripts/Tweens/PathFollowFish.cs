using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PathFollowFish : MonoBehaviour
{
    // Start is called before the first frame update
    SpriteRenderer _spriteRenderer;
    public float duration;
    public float endValue;
    public Ease animEase;
    void Start()
    {
        transform.DOMoveX(endValue, duration)
              .SetEase(animEase)
              .SetLoops(-1, LoopType.Yoyo)
              .OnStepComplete(FlipSprite);
    }
    private void FlipSprite()
    {
       
        if(transform.eulerAngles.y == 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
        else
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
    }
}
