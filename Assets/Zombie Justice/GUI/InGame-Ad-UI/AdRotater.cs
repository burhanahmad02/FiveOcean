using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdRotater : MonoBehaviour
{
    public GameObject [] panels;
    private int index = 0;
    private void Awake()
    {
        index = 0;
    }
    void OnEnable()
    {
        StartRotating();
    }
    void OnDisable()
    {
        StopAllCoroutines();
    }
    void StartRotating()
    {
        StartCoroutine(RotatePanels());
    }
    IEnumerator RotatePanels()
    {
        Rotate();
        yield return new WaitForSeconds(3f);
        StopAllCoroutines();
        StartRotating();
    }
    void Rotate()
    {
        foreach (var item in panels)
        {
            item.SetActive(false);
        }
        if (index < panels.Length)
        {
            panels[index].SetActive(true);
            index++;
        }
        if (index >= panels.Length)
        {
            index = 0;
        }
    }
}
