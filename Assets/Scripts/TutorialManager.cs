using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] panels;
    private int panelIndex = 0;

    public static TutorialManager Instance;
    public static bool isTutorialRunning = false;
   
    private void Awake()
    {
        if (GameManager.Instance.moduleNumber == 0)
        {
            if (EncryptedPlayerPrefs.GetInt("TutorialFinished", 0) == 1)
            {
                this.gameObject.SetActive(false);
                return;
            }
            else
            {
                EncryptedPlayerPrefs.SetInt("TutorialFinished", 1);
            }

            if (!Instance)
            {
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
                return;
            }
            isTutorialRunning = true;
            panelIndex = EncryptedPlayerPrefs.GetInt("Tutorial");

        }
    }

    public void startTutorial()
    {
        Time.timeScale = 1.0f;
        
        foreach (var item in panels)
        {
            if (item)
            {
                item.SetActive(false);
            }
        }

        //HeadPositionsArray.Instance.headsList[11].gameObject.GetComponent<HeadScript>().Glow();
        //HeadPositionsArray.Instance.headsList[12].gameObject.GetComponent<HeadScript>().Glow();
        //HeadPositionsArray.Instance.headsList[19].gameObject.GetComponent<HeadScript>().Glow();

        ShowPanelAfterTime(2f);
    }

    public void ShowPanelAfterTime(float Time)
    {
        StartCoroutine(ShowPanelAfter(Time));
    }

    public IEnumerator ShowPanelAfter(float waitDuration)
    {
        yield return new WaitForSeconds(waitDuration);
        NextPanel();
    }

    public void CloseAlPanels()
    {
        foreach (var item in panels)
        {
            if (item)
            {
                item.SetActive(false);
            }
        }
    }

    public void CloseCurrentPanel()
    {
        panels[panelIndex].SetActive(false);
    }

    public void NextPanel()
    {
        panelIndex++;
        EncryptedPlayerPrefs.SetInt("Tutorial", panelIndex);

        //if (panelIndex < panels.Length)
        //{
            OpenPanel(panelIndex);
        //}
        //else
        //{
            //FinishTutorial();
        //}
    }

    public void IncreasePrefValue()
    {
        panelIndex++;
        EncryptedPlayerPrefs.SetInt("Tutorial", panelIndex);
    }

    public void OpenPanel(int index)
    {
        foreach (var item in panels)
        {
            if (item)
            {
                item.SetActive(false);
            }
        }
        if (panels[panelIndex])
            panels[panelIndex].SetActive(true);
    }

    private void OnDisable()
    {
        //isTutorialRunning = false;
    }

    public void MakeTimeScale(float scale)
    {
        Time.timeScale = scale;
    }

    public void FinishTutorial()
    {
        StartCoroutine(waitFor());
    }

    IEnumerator waitFor()
    {
        yield return new WaitForSeconds(2f);

        isTutorialRunning = false;
        EncryptedPlayerPrefs.SetInt("TutorialFinished", 1);
        this.gameObject.SetActive(false);

        IngameUI.Instance.BackToMainMenu();

    }
}