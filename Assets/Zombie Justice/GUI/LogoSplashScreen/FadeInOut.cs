using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    public Image FadeInImage;
    public float fadeSpeed = 0.75f;

    private Color imageColor;
    private float mark;

    bool fadein = false;
    bool fadeout = false;
    // Start is called before the first frame update
    void Start()
    {
        imageColor = FadeInImage.color;
//        FadeInImage.canvasRenderer.SetAlpha(0.0f);
    }

    private void Update()
    {
        if (fadein)
        {
            mark += Time.deltaTime * fadeSpeed;
            imageColor.a = Mathf.Lerp(1.0f, 0.0f, mark);
            FadeInImage.color = imageColor;
        }

        else
            if (fadeout)
        {
            mark += Time.deltaTime * fadeSpeed;
            imageColor.a = Mathf.Lerp(0.0f, 1.0f, mark);
            FadeInImage.color = imageColor;
        }
    }

    public void FadeIn()
    {
        mark = 0.0f;
        fadeout = true;
       // FadeInImage.CrossFadeAlpha(1, 2, false);
    }

    public void FadeOut()
    {
        mark = 0.0f;
        fadein = true;
        //FadeInImage.CrossFadeAlpha(0, 2, false);
    }
}
