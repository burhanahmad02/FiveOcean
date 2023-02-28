using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class FadingPanel : MonoBehaviour
{
    public static FadingPanel Instance;
    [SerializeField] private CanvasGroup canvasGroup;
    private Tween fadeTween;
    public void Awake()
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
    public void FadeIn(float duration)
    {
        Fade(1f, duration, () =>
        {
            canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true; });
            

    }
    public void FadeOut(float duration)
    {
        Fade(0f, duration, () =>
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        });


    }
    private void Fade(float endValue,float duration, TweenCallback onEnd)
    {
        if(fadeTween!=null)
        {
            fadeTween.Kill(false);
        }
        fadeTween = canvasGroup.DOFade(endValue, duration);
        fadeTween.onComplete += onEnd;
    }
}
