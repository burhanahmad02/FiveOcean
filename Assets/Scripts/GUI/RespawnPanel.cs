using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnPanel : MonoBehaviour
{
    public Image fillImage;

    public float timeAmount = 5;
    float time;
    private bool canFill = false;
    public GameObject rewardBtn;
    void Start()
    {
        time = timeAmount;
    }
    private void OnEnable()
    {
        rewardBtn.SetActive(true);
        canFill = true;
    }
    void Update()
    {
        if (canFill)
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
                fillImage.fillAmount = time / timeAmount;
            }
            if (fillImage.fillAmount <= 0)
            {
                IngameUI.Instance.FailOperations();
                this.gameObject.SetActive(false);
            }
        }
    }
    public void RespawnByVideo()
    {
        ResetEveryThing();
        this.gameObject.SetActive(false);
    }
    public void ResetEveryThing()
    {
        canFill = false;
        fillImage.fillAmount = 1;
        time = timeAmount;
    }
}