using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicValue : MonoBehaviour 
{
    private SoundAndMusic[] audioScript;
    private Slider slider;

    void Awake ()
    {
        audioScript = GameObject.FindObjectsOfType<SoundAndMusic>() as SoundAndMusic[];
        slider = GetComponent<Slider>();

        if (!slider)
        {
            slider = GetComponentInChildren<Slider>();

            if (!slider)
            {
                Utility.ErrorLog("Could not found slider component on " + this.gameObject.name, 2);
            }
        }
    }

    void OnEnable()
    {
        if (slider)
        {
            slider.value = PlayerPrefs.GetFloat("MusicValue");
        }
        else
            Utility.ErrorLog("Could not found slider component on " + this.gameObject.name, 2);
    }

    public void OnSliderChange()
    {
        if (slider)
        {
            GameManager.Instance.musicValue = Mathf.Clamp(slider.value, 0f, 1f);
            PlayerPrefs.SetFloat("MusicValue", GameManager.Instance.musicValue);

            foreach (var item in audioScript)
            {
                if (item)
                {
                    item.ChangeAudioSetting();
                }
                //else
                //    Utility.ErrorLog("Audio Script of GameObject " + item.gameObject.name + " is null", 2);
            }
        }
        else
            Utility.ErrorLog("Could not found slider component on " + this.gameObject.name, 2);
    }

    void SimpleLog(string log)
    {
        Debug.Log(log);
    }
}