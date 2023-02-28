using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    //public Slider slider;

    public float timeToFakeLoad;
    public Image progressBar;
    public Text percentageText;
    public Text loadingDots;

    void Awake()
    {
        //if (!slider)
        //{
        //    slider = GetComponentInChildren<Slider>();

        //    if (!slider)
        //    {
        //        Utility.ErrorLog("Could not found slider component on " + this.gameObject.name, 2);
        //    }
        //}

        //if (slider)
        //{
        //    slider.value = 0;
        //}
    }

    private void ResetValues()
    {
        if (loadingDots)
        {
            loadingDots.text = ".";
        }
        else
            Utility.ErrorLog("Loading Dots is not assigned on loading panel script LoadScene.cs", 1);

        if (percentageText)
        {
            percentageText.text = "0 %";
        }
        else
            Utility.ErrorLog("Percentage Text is not assigned on loading panel script LoadScene.cs", 1);

        if (progressBar)
        {
            progressBar.fillAmount = 0.0f;
        }
        else
            Utility.ErrorLog("Progress Bar is not assigned on loading panel script LoadScene.cs", 1);

        //if (slider)
        //{
        //    slider.value = 0;
        //}

    }
    public void LoadTheSceneAgain()
    {
        ResetValues();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void StartLoadingScene(string sceneName)
    {
        ResetValues();
        StartCoroutine(LoadYourAsyncScene(sceneName));
    }
    IEnumerator LoadYourAsyncScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            //if (slider)
            //{
            //    slider.value = asyncLoad.progress;
            //}
            //else
            if (progressBar)
            {
                progressBar.fillAmount = asyncLoad.progress;
            }
            else
                Utility.ErrorLog("Progress Bar is not assigned on loading panel script LoadScene.cs", 1);

            if (percentageText)
            {
                percentageText.text = ((int)(asyncLoad.progress * 100)).ToString() + " %";
            }
            else
                Utility.ErrorLog("Percentage Text is not assigned on loading panel script LoadScene.cs", 1);


            SetLoadingDots();
            yield return null;
        }
    }
    public void StartFakeLoading()
    {
        ResetValues();
        Time.timeScale = 1f;
        StartCoroutine(LoadFake());
    }
    IEnumerator LoadFake()
    {
        float timeProgress = 0f;
        float progress = 0f;
        while (timeProgress <= timeToFakeLoad)
        {
            timeProgress += Time.deltaTime;
            progress = (timeProgress / timeToFakeLoad);
            progress = Mathf.Round(progress * 100f) / 100f;

            if (progress >= 1)
                progress = 1;

            //if (slider)
            //{
            //    slider.value = progress;
            //}
            //else
            if (progressBar)
            {
                progressBar.fillAmount = progress;
            }
            else
            Utility.ErrorLog("Progress Bar is not assigned on loading panel script LoadScene.cs", 1);

            if (percentageText)
            {
                percentageText.text = ((int)(progress * 100)).ToString() + " %";
            }
            else
                Utility.ErrorLog("Percentage Text is not assigned on loading panel script LoadScene.cs", 1);


            SetLoadingDots();
            yield return null;
        }
        if (MainMenuUI.Instance)
        {
            MainMenuUI.Instance.ActivateMainMenuPanel();
        }
        this.gameObject.SetActive(false);
    }
    void SetLoadingDots()
    {
        if (loadingDots)
        {
            if (loadingDots.text == ".")
            {
                loadingDots.text = "..";
            }
            else if (loadingDots.text == "..")
            {
                loadingDots.text = "...";
            }
            else if (loadingDots.text == "...")
            {
                loadingDots.text = ".";
            }
        }
        else
            Utility.ErrorLog("Loading Dots is not assigned on loading panel script LoadScene.cs", 1);
    }
}
