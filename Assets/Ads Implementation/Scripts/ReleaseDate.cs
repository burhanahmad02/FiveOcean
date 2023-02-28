using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseDate : MonoBehaviour
{
    [Header("Date To Check")]
    [SerializeField] private int day;
    [SerializeField] private int month;
    [SerializeField] private int year;

    public static bool released = false;

    // Use this for initialization
    void Awake ()
    {
        if (EncryptedPlayerPrefs.GetInt("ReleaseDate") == 0)
        {
            day = Mathf.Clamp(day, 1, 31);
            month = Mathf.Clamp(month, 1, 12);
            year = Mathf.Clamp(year, 2000, 3000);

            int currentDay = int.Parse(System.DateTime.UtcNow.ToString("dd"));
            int currentMonth = int.Parse(System.DateTime.UtcNow.ToString("MM"));
            int currentYear = int.Parse(System.DateTime.UtcNow.ToString("yyyy"));

            released = HasDateReached(currentDay, currentMonth, currentYear);
            
            if (released)
            {
                EncryptedPlayerPrefs.SetInt("ReleaseDate", 1);
            }
        }
        else if (EncryptedPlayerPrefs.GetInt("ReleaseDate") == 1)
        {
            released = true;
        }
    }
    bool HasDateReached(int currentDay, int currentMonth, int currentYear)
    {
        if (currentYear == year)
        {
            if (currentMonth == month)
            {
                if (currentDay >= day)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (currentMonth > month)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (currentYear > year)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
