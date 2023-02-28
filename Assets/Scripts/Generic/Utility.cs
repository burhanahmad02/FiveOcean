using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public static class Utility
{
    /// <summary>
    /// Get Optimized Distance Between 2 Vectors
    /// </summary>
    /// 

    internal static string Clipboard
    {
        get
        {
            TextEditor _textEditor = new TextEditor();
            _textEditor.Paste();
            return _textEditor.text;
        }
        set
        {
            TextEditor _textEditor = new TextEditor
            { text = value };

            _textEditor.OnFocus();
            _textEditor.Copy();
        }
    }

    public static float Distance(Vector3 vectorA, Vector3 vectorB)
    {
        return Mathf.Sqrt((vectorA - vectorB).sqrMagnitude);
    }

    public static void MakeClickSound()
    {
        if (MainMenuUI.Instance)
        {
            MainMenuUI.Instance.MakeClickSound();
        }
        else if (IngameUI.Instance)
        {
            IngameUI.Instance.MakeClickSound();
        }
        else
            Utility.ErrorLog("Sound Source is not assigned", 1);
    }
    public static void ShowHeaderValues()
    {
        DisplayItemValues[] items = GameObject.FindObjectsOfType<DisplayItemValues>() as DisplayItemValues[];
        foreach (var item in items)
        {
            item.ShowCount();
        }
    }
    /// <summary>
    /// error type 1 is null variable
    /// error type 2 is component not found
    /// error type 3 is tag not found
    /// error type 4 is Array out of bound
    /// </summary>

    public static void ErrorLog(string error, int errorType)
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            Debug.LogError(error);
        }
        else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (errorType == 1)
                Debug.Log("Debugging: Unassigned Variable Exception");
            else if (errorType == 2)
                Debug.Log("Debugging: Component Not Found Exception");
            else if (errorType == 3)
                Debug.Log("Debugging: Tag Not Found Exception");
            else if (errorType == 4)
                Debug.Log("Debugging: Array Out Of Bound Exception");
        }
    }
    public static void SimpleLog(string log)
    {
        if (Debug.isDebugBuild)
            Debug.Log(log);
    }

    public static string ParseDateAndTimeInString(DateTime dateAndTime)
    {
        int currentDay = dateAndTime.Day;
        int currentMonth = dateAndTime.Month;
        int currentYear = dateAndTime.Year;

        int currentHour = dateAndTime.Hour;
        int currentMinute = dateAndTime.Minute;
        int currentSecond = dateAndTime.Second;

        return currentMonth + "/" + currentDay + "/" + currentYear + " " + currentHour + ":" + currentMinute + ":" + currentSecond;
    }
    public static bool CompareDataAndTimes(string _previousDateAndTime, string _currentDateAndTime)
    {
        char split = ' ';
        string[] previousDateAndTime = _previousDateAndTime.Split(split);

        split = '/';

        string[] previousDateThings = previousDateAndTime[0].Split(split);

        split = ':';

        string[] previousTimeThings = previousDateAndTime[1].Split(split);

        int previousMonth = int.Parse(previousDateThings[0]);
        int previousDay = int.Parse(previousDateThings[1]);
        int previousYear = int.Parse(previousDateThings[2]);

        int previousHour = int.Parse(previousTimeThings[0]);
        int previousMinute = int.Parse(previousTimeThings[1]);
        int previousSecond = int.Parse(previousTimeThings[2]);


        split = ' ';
        string[] currentDateAndTime = _currentDateAndTime.Split(split);

        split = '/';

        string[] currentDateThings = currentDateAndTime[0].Split(split);

        split = ':';

        string[] currentTimeThings = currentDateAndTime[1].Split(split);

        int currentMonth = int.Parse(currentDateThings[0]);
        int currentDay = int.Parse(currentDateThings[1]);
        int currentYear = int.Parse(currentDateThings[2]);

        int currentHour = int.Parse(currentTimeThings[0]);
        int currentMinute = int.Parse(currentTimeThings[1]);
        int currentSecond = int.Parse(currentTimeThings[2]);

        if (currentYear == previousYear)
        {
            if (currentMonth == previousMonth)
            {
                if (currentDay > (previousDay + 1))
                {
                    return true;
                }
                else if (currentDay == (previousDay + 1))
                {
                    if (currentHour == previousHour)
                    {
                        if (currentMinute == previousMinute)
                        {
                            if (currentSecond >= previousSecond)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else if (currentMinute > previousMinute)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (currentHour > previousHour)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else if (currentMonth > previousMonth)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (currentYear > previousYear)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static void SetTimeStringForPref(string prefKey, DateTime dateTime)
    {
        EncryptedPlayerPrefs.SetString(prefKey, dateTime.ToBinary().ToString());
    }
    public static int GetMinutesDifference(string prefKey)
    {
        DateTime oldDate = DateTime.FromBinary(Convert.ToInt64(EncryptedPlayerPrefs.GetString(prefKey)));
        TimeSpan difference = DateTime.UtcNow.Subtract(oldDate);

        return (int)difference.TotalMinutes;
    }
}
