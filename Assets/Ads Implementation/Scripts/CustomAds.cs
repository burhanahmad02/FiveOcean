using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomAds : MonoBehaviour
{
    private enum AD
    {
        panel,
        banner,
        interstitial
    }
    [SerializeField] private AD adType;
    [SerializeField] private GameObject adParent;
    [SerializeField] private RawImage adImage;
    [SerializeField] private Text adText;
    [SerializeField] private int adTime;

    [SerializeField] private Texture[] images;
    [SerializeField] private string[] texts;
    [SerializeField] private string[] links;


    private bool continueRoutine;
    private string linkToOpen;
    private int adsArrayLength;
    void Awake()
    {
        if (adTime < 1)
            adTime = 1;

        if (images.Length == 0)
        {
            images = new Texture[1];
        }
        adsArrayLength = images.Length;
        if (texts.Length == 0 || texts.Length != images.Length)
        {
            texts = new string[adsArrayLength];
        }
        if (links.Length == 0 || links.Length != images.Length)
        {
            links = new string[adsArrayLength];
        }
    }
    private void OnEnable()
    {
        if (adType == AD.banner)
        {
            AdManager.customAdsBannerOff += CloseAd;
            AdManager.customAdsBannerOn += OpenAd;
            
            if (AdManager.Instance)
            {
                if (AdManager.Instance.IsBannerOnScreen())
                {
                    CloseAd();
                }
            }
        }
        StartTheADRoutine();
    }
    private void StartTheADRoutine()
    {
        foreach (var item in images)
        {
            if (item == null)
            {
                Utility.ErrorLog("One of the image is not assigned in CustomAds.cs of " + this.gameObject.name, 1);
                CloseAd();
                return;
            }
        }
        if (adType != AD.interstitial)
        {
            OpenAd();
            StartCoroutine(DelayRoutine());
        }
        else
        {
            AssignThings();
        }
    }
    IEnumerator DelayRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(adTime);
        while(continueRoutine)
        {
            AssignThings();
            yield return wait;
        }
    }
    private void AssignThings()
    {
        int randomIndex = Random.Range(0, adsArrayLength);
        if (adImage)
        {
            adImage.texture = images[randomIndex];
        }
        else
            Utility.ErrorLog("Ad Image is not assigned in CustomAds.cs of " + this.gameObject.name, 1);

        if (adType != AD.interstitial)
        {
            if (adText)
            {
                adText.text = texts[randomIndex];
            }
            else
                Utility.ErrorLog("Ad Text is not assigned in CustomAds.cs of " + this.gameObject.name, 1);
        }
        linkToOpen = links[randomIndex];
    }
    private void OnDisable()
    {
        if (adType == AD.banner)
        {
            AdManager.customAdsBannerOff -= CloseAd;
            AdManager.customAdsBannerOn -= OpenAd;
        }
    }
    public void OpenLink()
    {
        Application.OpenURL(linkToOpen);
    }
    public void OpenAd()
    {
        if (adParent)
        {
            if (AdManager.Instance)
            {
                if (!AdManager.Instance.IsBannerOnScreen())
                {
                    adParent.SetActive(true);
                }
            }
            else
            {
                adParent.SetActive(true);
            }
        }
        else
            Utility.ErrorLog("Ad Parent is not assigned in CustomAds.cs of " + this.gameObject.name, 1);

        continueRoutine = true;
    }
    public void CloseAd()
    {
        if (adParent)
        {
            adParent.SetActive(false);
        }
        else
            Utility.ErrorLog("Ad Parent is not assigned in CustomAds.cs of " + this.gameObject.name, 1);

        continueRoutine = false;
    }
}
