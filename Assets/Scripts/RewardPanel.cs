using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardPanel : MonoBehaviour
{
    public GameObject background;
    public GameObject box;
    public GameObject box2;
    public GameObject particles;
    public GameObject panel;

    public void ShowReward(int id)
    {
        if (id == 1)
        {
            box2.SetActive(true);
            this.GetComponent<Animator>().SetTrigger("box2");
        }
        else
        if (id == 2)
        {
            box.SetActive(true);
            this.GetComponent<Animator>().SetTrigger("box1");
        }

        panel.SetActive(false);
        background.SetActive(true);
        
        
        this.GetComponent<Animator>().SetBool("close", false);
    }
    public void disableBox()
    {
        panel.SetActive(true);
        this.GetComponent<Animator>().SetBool("panel", true);
        SoundManager.Instance.PlayRewardPanelSound();
        box.SetActive(false);
        box2.SetActive(false);
        particles.SetActive(true);
    }

    public void CloseRewardPanel()
    {
        Utility.ShowHeaderValues();
        Utility.MakeClickSound();
        this.GetComponent<Animator>().SetBool("panel", false);
        particles.SetActive(false);
        background.SetActive(false);
        box.SetActive(false);
        this.GetComponent<Animator>().SetBool("close", true);
        //MainMenuUI.Instance.CheckForGiftsIndicatorFunction();
    }
}