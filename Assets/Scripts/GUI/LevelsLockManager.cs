using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsLockManager : MonoBehaviour
{
    public GameObject[] levelsLockedObjects;
    public GameObject[] levelsUnlockedObjects;

    private void OnEnable()
    {
        UnlockLevels();
    }

    void UnlockLevels ()
    {
        foreach (var item in levelsLockedObjects)
        {
            if (item)
            {
                item.SetActive(false);
            }
            else
                Utility.ErrorLog("Locked Objects of " + this.gameObject.name + " in LevelsLockManager.cs is not assigned", 1);
        }
        foreach (var item in levelsUnlockedObjects)
        {
            if (item)
            {
                item.SetActive(false);
            }
            else
                Utility.ErrorLog("Unlocked Objects of " + this.gameObject.name + " in LevelsLockManager.cs is not assigned", 1);
        }

        int levelsUnlockedLimit = EncryptedPlayerPrefs.GetInt("LevelsUnocked") % GameManager.Instance.levelsInAModule;

        //if (GameManager.Instance.moduleNumber != (EncryptedPlayerPrefs.GetInt("LevelsUnocked") / GameManager.Instance.levelsInAModule))
        //{
        //    levelsUnlockedLimit = GameManager.Instance.levelsInAModule;
        //}

        for (int i = 1; i < levelsUnlockedObjects.Length; i++)
        {
            if (i <= levelsUnlockedLimit)
            {
                if (levelsUnlockedObjects[i])
                {
                    levelsUnlockedObjects[i].SetActive(true);
                }
                else
                    Utility.ErrorLog("Unlocked Objects at index " + i + " in LevelsLockManager.cs is not assigned", 1);
            }
            else
            {
                if (i < levelsLockedObjects.Length)
                {
                    if (levelsLockedObjects[i])
                    {
                        levelsLockedObjects[i].SetActive(true);
                    }
                    else
                        Utility.ErrorLog("Locked Objects at index " + i + " in LevelsLockManager.cs is not assigned", 1);
                }
                else
                    Utility.ErrorLog("Locked Objects array is out of bound in LevelsLockManager.cs", 4);
            }
        }
    }
}