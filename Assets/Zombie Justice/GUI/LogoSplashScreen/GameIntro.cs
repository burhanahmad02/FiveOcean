using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
//using UnityEditor.Rendering;
using UnityEngine;

public class GameIntro : MonoBehaviour
{
    public GameObject background;
    public GameObject planet;
    public GameObject rocket;

    public GameObject explosion;

    public GameObject FadeImage;

    public GameObject SplashScreen;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DoBlast());
    }

    public IEnumerator DoBlast()
    {
        //yield return new WaitForSeconds(4f);
        //background.GetComponent<ScrollUV>().ScrollX = 0.05f;
        //background.GetComponent<ScrollUV>().ScrollY = 0.05f;
        yield return new WaitForSeconds(3.8f);
        planet.SetActive(false);
        rocket.SetActive(false);

        explosion.SetActive(true);
        yield return new WaitForSeconds(0.2f);

        FadeImage.GetComponent<FadeInOut>().FadeIn();
        yield return new WaitForSeconds(1f);
        SplashScreen.SetActive(true);
        FadeImage.GetComponent<FadeInOut>().FadeOut();

        this.gameObject.SetActive(false);
    }
}