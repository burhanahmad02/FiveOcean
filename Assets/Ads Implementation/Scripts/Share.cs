using UnityEngine;
using System.Collections;
//using System.IO;
//using System.Runtime.InteropServices;


public class Share : MonoBehaviour
{
    public static bool shareStarted;
    private string subject = "Subject text";
    private string body = "Actual text (Link)";

    void Start()
    {
        subject = Application.productName;

        if (Application.platform == RuntimePlatform.Android)
        {
            body = "Hey there! They are offering Real Cryptocurrency! Earn it and try to beat my score. I am sure you will have a hard time competing with me\n" + "https://play.google.com/store/apps/details?id=" + Application.identifier;
        }
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            body = "Hey there! They are offering Real Cryptocurrency! Earn it and try to beat my score. I am sure you will have a hard time competing with me\n" + MainMenuUI.Instance.iosAppURL;
        }
    }
    public void ShareGame()
    {
		StartCoroutine(ShareMessage());
    }

	IEnumerator ShareMessage()
	{
		yield return new WaitForEndOfFrame();

        //Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        //ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        //ss.Apply();

        //string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
        //File.WriteAllBytes(filePath, ss.EncodeToPNG());

        //// To avoid memory leaks
        //Destroy(ss);

        new NativeShare()/*.AddFile(filePath)*/.SetSubject(subject).SetText(body).SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget)).Share();

        //if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
        //{
        //    shareStarted = true;
        //}
        //else if (Application.platform == RuntimePlatform.IPhonePlayer)
        //{
        //    yield return new WaitForSeconds(2);
        //    MainMenuUI.Instance.CheckForShareRewards();
        //}
    }
}
