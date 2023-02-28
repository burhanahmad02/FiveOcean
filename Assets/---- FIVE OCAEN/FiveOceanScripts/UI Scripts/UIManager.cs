using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UIManager : MonoBehaviour
{
    public RectTransform play, submarine, shop, store, dailyReward, settings;
    // Start is called before the first frame update
    void Start()
    {
        //play.DOAnchorPos(new Vector2(-155.04f, -463f), 5f);
        play.DOAnchorPos(new Vector2(-594f, -463f), 5f);
        submarine.DOAnchorPos(new Vector2(147f, -216.1f), 5f);
        //shop.DOAnchorPos(new Vector2(147f, -460f), 5f);
        shop.DOAnchorPos(new Vector2(147f, -330f), 5f);
        //store.DOAnchorPos(new Vector2(147f, -330f), 5f);
        dailyReward.DOAnchorPos(new Vector2(-155.0399f, -332f), 5f);
        settings.DOAnchorPos(new Vector2(-159f, -218f), 5f);
    }
}
