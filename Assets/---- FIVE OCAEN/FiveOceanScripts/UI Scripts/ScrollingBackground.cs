using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
[RequireComponent(typeof(Image))]
public class ScrollingBackground : MonoBehaviour
{
    [SerializeField]
    private float tweenDuration;
    // Start is called before the first frame update
    void Awake()
    {
        var image = GetComponent<Image>();
        var rectTransform = GetComponent<RectTransform>();
        var posTween = DOTween.To( 
            () => rectTransform.anchoredPosition, x => rectTransform.anchoredPosition = x,
            new Vector2(-image.sprite.texture.width , -image.sprite.texture.height ), tweenDuration);
        posTween.SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);


       var sizeTween = DOTween.To(
            () => rectTransform.sizeDelta, x => rectTransform.sizeDelta = x,
            new Vector2(image.sprite.texture.width, image.sprite.texture.height), tweenDuration);
        sizeTween.SetEase(Ease.Linear)
          .SetLoops(-1, LoopType.Restart);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
