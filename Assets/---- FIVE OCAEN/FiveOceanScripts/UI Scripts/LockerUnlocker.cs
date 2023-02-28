using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerUnlocker : MonoBehaviour
{
    public int myRoverNumber;
    public GameObject lockedObject;
    public GameObject unlockedObject;
    // Start is called before the first frame update
    private void OnEnable()
    {
        if (GetIfLocked() == false)
        {
            lockedObject.SetActive(false);
            unlockedObject.SetActive(true);
        }
        else if (GetIfLocked() == true)
        {
            unlockedObject.SetActive(false);
            lockedObject.SetActive(true);
        }
    }
    public bool GetIfLocked()
    {
        if (EncryptedPlayerPrefs.GetInt("RoverUnlocked" + myRoverNumber) == 1)
        {
            return false;
        }
        else if (EncryptedPlayerPrefs.GetInt("RoverUnlocked" + myRoverNumber) == 0)
        {
            return true;
        }
        return true;
    }
}
