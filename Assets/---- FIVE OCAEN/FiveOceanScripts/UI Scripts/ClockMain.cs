
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClockMain : MonoBehaviour
{

    private const float REAL_SECONDS_PER_INGAME_DAY = 60f;

    private Transform clockHourHandTransform;
    private Transform clockMinuteHandTransform;
    public TextMeshProUGUI timeText;
    private float day;
    public int hours;
    public static ClockMain Instance;

    private void Awake()
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
        clockHourHandTransform = transform.Find("hourHand");
        clockMinuteHandTransform = transform.Find("minuteHand");
        timeText = transform.Find("timeText").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        day += Time.deltaTime / REAL_SECONDS_PER_INGAME_DAY;

        float dayNormalized = day % 1f;

        float rotationDegreesPerDay = 360f;
        clockHourHandTransform.eulerAngles = new Vector3(0, 0, -dayNormalized * rotationDegreesPerDay);

        float hoursPerDay = 60;
        clockMinuteHandTransform.eulerAngles = new Vector3(0, 0, -dayNormalized * rotationDegreesPerDay * hoursPerDay);
        hours = (int)(dayNormalized * hoursPerDay);
        string hoursString = Mathf.Floor(dayNormalized * hoursPerDay).ToString("00");

        int minutesPerHour = 60;
        string minutesString = Mathf.Floor(((dayNormalized * hoursPerDay) % 1f) * minutesPerHour).ToString("00");

        timeText.text = hoursString + ":" + minutesString;

    }


}
