using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSelector : MonoBehaviour
{
    [Header("Environment Backgrounds")]
    public GameObject[] envBackgrounds;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (var item in envBackgrounds)
        {
            item.SetActive(false);
        }
        envBackgrounds[0].SetActive(true);
    }
}
