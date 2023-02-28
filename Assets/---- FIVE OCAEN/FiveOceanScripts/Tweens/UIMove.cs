using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float duration;
    public float endValue;
    public Ease animEase;
    void Start()
    {
        var image = GetComponent<Image>();
        var rectTransform = GetComponent<RectTransform>();
        /*transform.DOMoveX(endValue, duration)*/
        var posTween = DOTween.To(
            () => rectTransform.anchoredPosition, x => rectTransform.anchoredPosition = x,
            new Vector2(image.sprite.texture.width, image.sprite.texture.height), duration)
              .SetEase(animEase)
            .SetLoops(-1, LoopType.Yoyo)
              .OnStepComplete(FlipSprite);
    }
    private void FlipSprite()
    {

        if (transform.localScale.x == 1)
        {
            transform.DOScaleX(-1,0);
            
        }
        else
        {
            transform.DOScaleX(1, 0);
        }
    }
}
