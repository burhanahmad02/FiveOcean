using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeController : MonoBehaviour
{
    public static float totalTimePassed;
    public static float totalDistanceCovered;
    public static TimeController Instance;
    private PlayerController playerController;

    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }

    void Update()
    {
        /* if (IngameUI._isPlaying)
         {
             if (playerController == null)
             {
                 *//*if (PlayerStaticInstance.Instance)
                 {
                     playerController = PlayerStaticInstance.Instance.gameObject.GetComponent<PlayerController>();
                 }*//*
             }
             totalTimePassed = totalTimePassed + Time.deltaTime;

            *//* if (playerController.isFreeFalling)
             {
                 totalDistanceCovered = totalDistanceCovered + (Time.deltaTime * 4);
             }*//*
            // else
           //  {
                 totalDistanceCovered = totalDistanceCovered + (Time.deltaTime * 2);
            // }

            // IngameUI.Instance.UpdateScore((int)totalTimePassed);
         }*/
    }
    public string GetTimeInFormat()
    {
        float totalSeconds = totalTimePassed;

        int minutes = Mathf.FloorToInt(totalSeconds / 60f);
        int seconds = Mathf.RoundToInt(totalSeconds % 60f);

        if (seconds == 60)
        {
            seconds = 0;
            minutes += 1;
        }
        if (minutes <= 0)
        {
            minutes = 0;
        }
        return minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}