using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroll : MonoBehaviour
{

    public float scrollSpeed;
    public Image img;

    void Update()
    {
        img.material.mainTextureOffset = img.material.mainTextureOffset + new Vector2(Time.deltaTime * (-scrollSpeed / 10), 0f );

    }
}