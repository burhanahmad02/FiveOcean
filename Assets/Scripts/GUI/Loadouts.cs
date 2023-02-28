using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loadouts : MonoBehaviour
{
    [Header("Loadouts")]
    public GameObject[] weapon;

    private int _currentIndex = 1;
    public int currentIndex { get { return _currentIndex; } set { _currentIndex = value; } }
}
