using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.Analytics;
using System.Collections.Generic;

public class GeoData : MonoBehaviour
{
    void Awake()
    {
        StartCoroutine(GetRequest());
    }
    IEnumerator GetRequest()
    {
        string url = "https://get.geojs.io/v1/ip/geo.js";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string response = webRequest.downloadHandler.text;

            if (webRequest.isNetworkError)
            {
                Debug.Log("Done with Error: " + webRequest.error);
            }
            else
            {
                char[] split = { ',', ':', '"' };
                string[] pages = response.Split(split);

                string country = pages[66];
                string city = pages[60];
#if UNITY_EDITOR
                Debug.Log("Country name is " + country + " and city is " + city);
#endif
                EncryptedPlayerPrefs.SetString("country", country);
                EncryptedPlayerPrefs.SetString("city", city);
                PushCountryNameInAnalytics(country);
            }
        }
    }
    public void PushCountryNameInAnalytics(string country)
    {
        if (EncryptedPlayerPrefs.GetInt("playerNameSet") == 0)
        {
            AnalyticsResult result = AnalyticsEvent.Custom("country", new Dictionary<string, object>
        {
            { "country_is", country }
        });
#if UNITY_EDITOR
            Debug.Log("Result of Custom Analytics named country is " + result);
#endif
        }
        else if (EncryptedPlayerPrefs.GetInt("playerNameSet") == 1)
        {

            string name = EncryptedPlayerPrefs.GetString("PlayerName");
            AnalyticsResult result = AnalyticsEvent.Custom("country", new Dictionary<string, object>
        {
            { "country_of_" + name + "_is", country }
        });
#if UNITY_EDITOR
            Debug.Log("Result of Custom Analytics named country is " + result);
#endif
        }
    }
}
