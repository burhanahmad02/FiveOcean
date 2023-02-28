using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAndMusic : MonoBehaviour
{
    public float minClampValueForSound = 0;
    public float maxClampValueForSound = 1;
    public enum Type
    {
        sound,
        music
    };

    public Type audioType;

    void Start()
    {
        ChangeAudioSetting();
    }

    public void ChangeAudioSetting()
    {
        if (audioType == Type.sound)
        {
            float clamped = PlayerPrefs.GetFloat("SoundValue");
            clamped = Mathf.Clamp(clamped, minClampValueForSound, maxClampValueForSound);
            
            if (minClampValueForSound != 0 && maxClampValueForSound != 1)
                GameManager.Instance.soundValue = clamped;
            //GameManager.Instance.soundValue = PlayerPrefs.GetFloat("SoundValue");

            if (GetComponent<AudioSource>())
            {
                if (minClampValueForSound != 0 && maxClampValueForSound != 1)
                {
                    GetComponent<AudioSource>().volume = GameManager.Instance.soundValue;
                }
                else
                {
                    GetComponent<AudioSource>().volume = clamped;
                }
            }
        }
        else if (audioType == Type.music)
        {
            GameManager.Instance.musicValue = PlayerPrefs.GetFloat("MusicValue");

            if (GetComponent<AudioSource>())
            {
                GetComponent<AudioSource>().volume = GameManager.Instance.musicValue;
            }
        }
    }

    public void MusicLowAdjust(float adjustValue)
    {
        if (audioType == Type.music)
        {
            float volumeRightNow = PlayerPrefs.GetFloat("MusicValue");

            if (volumeRightNow > adjustValue)
            {
                StartCoroutine(AdjustVolumeDown(adjustValue));
                //if (GetComponent<AudioSource>())
                //{
                //    GetComponent<AudioSource>().volume = adjustValue;
                //}
            }
        }
    }

    public void MusicHighAdjust()
    {
        if (audioType == Type.music)
        {
            float volumeRightNow = PlayerPrefs.GetFloat("MusicValue");

            if (GetComponent<AudioSource>().volume < volumeRightNow)
            {
                StartCoroutine(AdjustVolumeUp(volumeRightNow));
                //if (GetComponent<AudioSource>())
                //{
                //    GetComponent<AudioSource>().volume = volumeRightNow;
                //}
            }
        }
    }

    IEnumerator AdjustVolumeUp(float value)
    {
        while (GetComponent<AudioSource>().volume < value)
        {
            GetComponent<AudioSource>().volume += Time.unscaledDeltaTime;
            yield return null;
        }
        GetComponent<AudioSource>().volume = value;
    }

    IEnumerator AdjustVolumeDown(float value)
    {
        while (GetComponent<AudioSource>().volume > value)
        {
            GetComponent<AudioSource>().volume -= Time.unscaledDeltaTime;
            yield return null;
        }
        GetComponent<AudioSource>().volume = value;
    }

    void SimpleLog(string log)
    {
        Debug.Log(log);
    }
}