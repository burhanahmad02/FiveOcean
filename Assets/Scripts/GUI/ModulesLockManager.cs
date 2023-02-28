using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModulesLockManager : MonoBehaviour
{
    public GameObject[] modulesLockedObjects;
    public GameObject[] modulesUnlockedObjects;
   
    private void OnEnable()
    {
        UnlockModules();
    }
    void UnlockModules()
    {
        foreach (var item in modulesLockedObjects)
        {
            if (item)
            {
                item.SetActive(false);
            }
            else
                Utility.ErrorLog("Locked Objects of " + this.gameObject.name + " in ModulesLockManager.cs is not assigned", 1);
        }
        foreach (var item in modulesUnlockedObjects)
        {
            if (item)
            {
                item.SetActive(false);
            }
            else
                Utility.ErrorLog("Unlocked Objects of " + this.gameObject.name + " in ModulesLockManager.cs is not assigned", 1);
        }

        int numberOfUnlockedModules = EncryptedPlayerPrefs.GetInt("LevelsUnocked") / GameManager.Instance.levelsInAModule;
        
        for (int i = 0; i < modulesUnlockedObjects.Length; i++)
        {
            if (i <= numberOfUnlockedModules)
            {
                if (modulesUnlockedObjects[i])
                {
                    modulesUnlockedObjects[i].SetActive(true);
                }
                else
                    Utility.ErrorLog("Unlocked Objects at index " + i + " in ModulesLockManager.cs is not assigned", 1);
            }
            else
            {
                if (i < modulesLockedObjects.Length)
                {
                    if (modulesLockedObjects[i])
                    {
                        modulesLockedObjects[i].SetActive(true);
                    }
                    else
                        Utility.ErrorLog("Locked Objects at index " + i + " in ModulesLockManager.cs is not assigned", 1);
                }
                else
                    Utility.ErrorLog("Locked Objects array is out of bound in ModulesLockManager.cs", 4);
            }
        }
    }
}
