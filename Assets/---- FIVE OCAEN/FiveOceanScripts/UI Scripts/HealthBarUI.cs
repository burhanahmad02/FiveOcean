using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Slider slider;
    public Color low;
    public Color high;
    public GameObject healthBar;
    public float waitTime = 0.2f;
    public void Start()
    {
        SetHealth(0, 200);
        slider.value = 200;
        slider.fillRect.GetComponent<Image>().color = Color.green;
        slider.transform.GetChild(0).GetComponent<Image>().fillAmount = slider.value / slider.maxValue;
        
    }
    public void Update()
    {
        healthBar.SetActive(true);
        var tempColor = slider.fillRect.GetComponentInChildren<Image>().color;
        tempColor.a = 1f;
        slider.fillRect.GetComponentInChildren<Image>().color = tempColor;
    }
    public void SetHealth(float h, float mH)
    {
        slider.gameObject.SetActive(h < mH);
        slider.value = h;
        slider.maxValue = mH;
        slider.fillRect.GetComponent<Image>().color = Color.Lerp(low, high, slider.normalizedValue);
        StartCoroutine(BackgroundCo());
    }
    IEnumerator BackgroundCo()
    {
        yield return new WaitForSeconds(0.5f);

        slider.transform.GetChild(0).GetComponent<Image>().fillAmount = slider.value / slider.maxValue;
        slider.transform.GetChild(0).GetComponent<Image>().transform.localEulerAngles = new Vector3(180.0f * (slider.value / slider.maxValue), 0, 0 );
    }
}
