using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Events;

public class AdManager : MonoBehaviour
{
    public int admobWaitTimeInMinutes;
    public int unityWaitTimeInMinutes;
    public int facebookWaitTimeInMinutes;
    public int admobWaitTimeInMinutesInterstitial;
    public int unityWaitTimeInMinutesInterstitial;
    public int facebookWaitTimeInMinutesInterstitial;

    public static AdManager Instance;

    public delegate void LinkCustomAdsBannerOff();
    public static LinkCustomAdsBannerOff customAdsBannerOff;
    public delegate void LinkCustomAdsBannerOn();
    public static LinkCustomAdsBannerOn customAdsBannerOn;

    // Use this for initialization
    private void Awake()
    {
        if (!Instance) { Instance = this; DontDestroyOnLoad(this.gameObject); }
        else { Destroy(this.gameObject); return; }
    }
    private void Start()
    {
        //GameServerData.Instance.CheckAdDelivery();
    }
    private void OnLevelWasLoaded(int level)
    {
        StopAllCoroutines();
    }
    public void SetGDRPConsent(bool consentValue)
    {
        Advertisements.Instance.SetUserConsent(consentValue);
        Advertisements.Instance.Initialize();
    }
    public void GDRPConsentTakenAlready()
    {
        Advertisements.Instance.Initialize();
    }
    public void ShowInterstitialAd(bool dateCheckDependency)
    {
        if (dateCheckDependency)
        {
            if (!ReleaseDate.released)
            {
                return;
            }
        }
        if (!CanDeliverAds())
            return;
#if USE_ADMOB
        if (CheckTimeAvailability(GetAdmobLastTimeKeyInterstitial(), admobWaitTimeInMinutesInterstitial))
        {
            if (Advertisements.Instance.IsInterstitialVideoAvailableOfSelectedAdvertiser(GleyMobileAds.SupportedAdvertisers.Admob))
            {
                Advertisements.Instance.ShowInterstitialOfSelectedAdvertiser(GleyMobileAds.SupportedAdvertisers.Admob);
                Utility.SetTimeStringForPref(GetAdmobLastTimeKeyInterstitial(), DateTime.UtcNow);
                return;
            }
        }
#endif
#if USE_UNITYADS
        if (CheckTimeAvailability(GetUnityLastTimeKeyInterstitial(), unityWaitTimeInMinutesInterstitial))
        {
            if (Advertisements.Instance.IsInterstitialVideoAvailableOfSelectedAdvertiser(GleyMobileAds.SupportedAdvertisers.Unity))
            {
                Advertisements.Instance.ShowInterstitialOfSelectedAdvertiser(GleyMobileAds.SupportedAdvertisers.Unity);
                Utility.SetTimeStringForPref(GetUnityLastTimeKeyInterstitial(), DateTime.UtcNow);
                return;
            }
        }
#endif
#if USE_FACEBOOKADS
        if (CheckTimeAvailability(GetFacebookLastTimeKeyInterstitial(), facebookWaitTimeInMinutesInterstitial))
        {
            if (Advertisements.Instance.IsInterstitialVideoAvailableOfSelectedAdvertiser(GleyMobileAds.SupportedAdvertisers.Facebook))
            {
                Advertisements.Instance.ShowInterstitialOfSelectedAdvertiser(GleyMobileAds.SupportedAdvertisers.Facebook);
                Utility.SetTimeStringForPref(GetFacebookLastTimeKeyInterstitial(), DateTime.UtcNow);
                return;
            }
        }
#endif
    }
    public void ShowRewardAd(UnityAction<bool> CompleteMethod)
    {
#if USE_ADMOB
        if (CheckTimeAvailability(GetAdmobLastTimeKey(), admobWaitTimeInMinutes))
        {
            if (Advertisements.Instance.IsRewardVideoAvailableOfSelectedAdvertiser(GleyMobileAds.SupportedAdvertisers.Admob))
            {
                Advertisements.Instance.ShowRewardedVideoOfSelectedAdvertiser(CompleteMethod, GleyMobileAds.SupportedAdvertisers.Admob);
                Utility.SetTimeStringForPref(GetAdmobLastTimeKey(), DateTime.UtcNow);
                return;
            }
        }
#endif
#if USE_UNITYADS
        if (CheckTimeAvailability(GetUnityLastTimeKey(), unityWaitTimeInMinutes))
        {
            if (Advertisements.Instance.IsRewardVideoAvailableOfSelectedAdvertiser(GleyMobileAds.SupportedAdvertisers.Unity))
            {
                Advertisements.Instance.ShowRewardedVideoOfSelectedAdvertiser(CompleteMethod, GleyMobileAds.SupportedAdvertisers.Unity);
                Utility.SetTimeStringForPref(GetUnityLastTimeKey(), DateTime.UtcNow);
                return;
            }
        }
#endif
#if USE_FACEBOOKADS
        if (CheckTimeAvailability(GetFacebookLastTimeKey(), facebookWaitTimeInMinutes))
        {
            if (Advertisements.Instance.IsRewardVideoAvailableOfSelectedAdvertiser(GleyMobileAds.SupportedAdvertisers.Facebook))
            {
                Advertisements.Instance.ShowRewardedVideoOfSelectedAdvertiser(CompleteMethod, GleyMobileAds.SupportedAdvertisers.Facebook);
                Utility.SetTimeStringForPref(GetFacebookLastTimeKey(), DateTime.UtcNow);
                return;
            }
        }
#endif
    }
    public void ShowBanner()
    {
        if (!IsBannerOnScreen() && !IfCallHasBeenSentToShowBanner() && CanDeliverAds())
        {
            TurnCustomAdBannerOn();
            StartCoroutine(WaitForBannerToDisplayOnScreen());
            Advertisements.Instance.ShowBanner();
        }
    }
    public void ShowBannerIfNotBeingShownAlready()
    {
        if (!IsBannerOnScreen() && CanDeliverAds())
        {
            Advertisements.Instance.ShowBanner();
        }
    }
    public void HideBanner()
    {
        TurnCustomAdBannerOff();
        Advertisements.Instance.HideBanner();
    }
    IEnumerator WaitForBannerToDisplayOnScreen()
    {
        while (!IsBannerOnScreen())
        {
            yield return null;
        }
        TurnCustomAdBannerOff();
    }
    public void TurnCustomAdBannerOff()
    {
        if (customAdsBannerOff != null)
        {
            customAdsBannerOff();
        }
    }
    public void TurnCustomAdBannerOn()
    {
        if (customAdsBannerOn != null)
        {
            customAdsBannerOn();
        }
    }
    public bool IsRewardVideoAvailable()
    {
        bool videoAvailablity = false;
#if USE_ADMOB
        if (videoAvailablity == false)
        {
            videoAvailablity = CheckTimeAvailability(GetAdmobLastTimeKey(), admobWaitTimeInMinutes);
            videoAvailablity = videoAvailablity && Advertisements.Instance.IsRewardVideoAvailableOfSelectedAdvertiser(GleyMobileAds.SupportedAdvertisers.Admob);
        }
#endif
#if USE_UNITYADS
        if (videoAvailablity == false)
        {
            videoAvailablity = CheckTimeAvailability(GetUnityLastTimeKey(), unityWaitTimeInMinutes);
            videoAvailablity = videoAvailablity && Advertisements.Instance.IsRewardVideoAvailableOfSelectedAdvertiser(GleyMobileAds.SupportedAdvertisers.Unity);
        }
#endif
#if USE_FACEBOOKADS
        if (videoAvailablity == false)
        {
            videoAvailablity = CheckTimeAvailability(GetFacebookLastTimeKey(), facebookWaitTimeInMinutes);
            videoAvailablity = videoAvailablity && Advertisements.Instance.IsRewardVideoAvailableOfSelectedAdvertiser(GleyMobileAds.SupportedAdvertisers.Facebook);
        }
#endif
        return videoAvailablity && CanDeliverAds();
    }
    bool CheckTimeAvailability(string prefKey, int waitTime)
    {
        if (!EncryptedPlayerPrefs.HasKey(prefKey))
        {
            Utility.SetTimeStringForPref(prefKey, DateTime.UtcNow.AddDays(-1));
            return true;
        }
        else
        {
            if (Utility.GetMinutesDifference(prefKey) < waitTime)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
    public bool IsBannerAvailable()
    {
        return Advertisements.Instance.IsBannerAvailable() && CanDeliverAds();
    }
    public bool IsBannerOnScreen()
    {
        return Advertisements.Instance.IsBannerOnScreen();
    }
    public bool IfCallHasBeenSentToShowBanner()
    {
        return Advertisements.Instance.IfCallHasBeenSentToShowBanner();
    }
    public bool IsInterstitialAvailable()
    {
        bool videoAvailablity = false;
#if USE_ADMOB
        if (videoAvailablity == false)
        {
            videoAvailablity = CheckTimeAvailability(GetAdmobLastTimeKeyInterstitial(), admobWaitTimeInMinutesInterstitial);
            videoAvailablity = videoAvailablity && Advertisements.Instance.IsInterstitialVideoAvailableOfSelectedAdvertiser(GleyMobileAds.SupportedAdvertisers.Admob);
        }
#endif
#if USE_UNITYADS
        if (videoAvailablity == false)
        {
            videoAvailablity = CheckTimeAvailability(GetUnityLastTimeKeyInterstitial(), unityWaitTimeInMinutesInterstitial);
            videoAvailablity = videoAvailablity && Advertisements.Instance.IsInterstitialVideoAvailableOfSelectedAdvertiser(GleyMobileAds.SupportedAdvertisers.Unity);
        }
#endif
#if USE_FACEBOOKADS
        if (videoAvailablity == false)
        {
            videoAvailablity = CheckTimeAvailability(GetFacebookLastTimeKeyInterstitial(), facebookWaitTimeInMinutesInterstitial);
            videoAvailablity = videoAvailablity && Advertisements.Instance.IsInterstitialVideoAvailableOfSelectedAdvertiser(GleyMobileAds.SupportedAdvertisers.Facebook);
        }
#endif
        return videoAvailablity && CanDeliverAds();
    }
    public bool HasAdsBeenRemoved()
    {
        return !Advertisements.Instance.CanShowAds();
    }
    public bool CanDeliverAds()
    {
        if (EncryptedPlayerPrefs.GetInt("AdDelivery", 1) == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    private string GetAdmobLastTimeKey()
    {
        return "AdmobLastTime";
    }
    private string GetUnityLastTimeKey()
    {
        return "UnityLastTime";
    }
    private string GetFacebookLastTimeKey()
    {
        return "FacebookLastTime";
    }
    private string GetAdmobLastTimeKeyInterstitial()
    {
        return "AdmobLastTimeInterstitial";
    }
    private string GetUnityLastTimeKeyInterstitial()
    {
        return "UnityLastTimeInterstitial";
    }
    private string GetFacebookLastTimeKeyInterstitial()
    {
        return "FacebookLastTimeInterstitial";
    }
}
