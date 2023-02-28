using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStars : MonoBehaviour
{
    public GameObject[] stars;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in stars)
        {
            item.SetActive(false);
        }
        string pref = "Level_" + GameManager.Instance.levelNumber + "_stars";
        int starsToUnlock = EncryptedPlayerPrefs.GetInt(pref, 0);

        for(int i = 0; i < starsToUnlock; i++)
        {
            stars[i].SetActive(true);
        }
    }
}
