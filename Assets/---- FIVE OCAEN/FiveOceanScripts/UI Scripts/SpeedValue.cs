using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedValue : MonoBehaviour
{
    private Slider slider;
    public delegate void AllSensitivityScripts(float value);
    public static AllSensitivityScripts changeSensitivity;
    public ParticleSystem bubbleParticle;
    // Use this for initialization
    void Awake()
    {
        if (!slider)
        {
            slider = GetComponentInChildren<Slider>();

            if (!slider)
            {
                Utility.ErrorLog("Could not found slider component on " + this.gameObject.name, 2);
            }
        }
        
    }
    private void Start()
    {
        //slider.value = 10.0f;
    }
    void OnEnable()
    {
        if (slider)
        {
            slider.value = EncryptedPlayerPrefs.GetFloat("SensitivityValue");
        }
        else
            Utility.ErrorLog("Could not found slider component on " + this.gameObject.name, 2);
    }

    public void OnSliderChange()
    {
        if (slider)
        {
           
            GameManager.Instance.sensitivityValue = Mathf.Clamp(slider.value, 10f, 20f);
            slider.value = GameManager.Instance.sensitivityValue;
            EncryptedPlayerPrefs.SetFloat("SensitivityValue", GameManager.Instance.sensitivityValue);

            if (changeSensitivity != null)
            {
               
                changeSensitivity(GameManager.Instance.sensitivityValue);
            }
        }
        else
            Utility.ErrorLog("Could not found slider component on " + this.gameObject.name, 2);
    }
}
