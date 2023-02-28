using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsrBehaviour : MonoBehaviour
{
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {

    }
    //call in gameplay panel, pause panel and settings panel
    public void SetSpeed(float h, float mH)
    {
        slider.gameObject.SetActive(h < mH);
        slider.value = h;
        slider.maxValue = mH;
    }
    //call in gameplay panel
    public void SetFireRate(float f, float fr)
    {
        slider.gameObject.SetActive(f < fr);
        slider.value = f;
        slider.maxValue = fr;
    }
}
