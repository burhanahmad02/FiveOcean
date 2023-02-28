using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using I2.Loc;

public class IngameUI : MonoBehaviour
{
    [Header("Sound And Music")]
    public SoundManager soundSource;
    public MusicManager musicSource;
    public static SoundManager _soundSource;
    public static MusicManager _musicSource;

    public static bool IsPlayingIngame = false;
    public static bool isPlaying = false;
    
    [Header("Public Menu Panels")]
    public GameObject StartPanel;
    public GameObject ingamePanel;
    public GameObject pausePanel;
    public GameObject failPanel;
    public GameObject successPanel;
    public GameObject ReSpawnPanel;
    public GameObject itemShopPanel;
    public GameObject InsufficientFundsPanel;
    public GameObject loadingPanel;
    public GameObject RewardPanel;
    public GameObject rewardNotAvailable;
    public GameObject rewardLost;
    public GameObject tutorialPanel;

    [Header("Public Text Fields")]
    public Text levelNumberText;
    public Text LevelTextText;
    public Text ingameLevelText;
    public Text timerText;
    public Text timeAvailableStartPanel;
    public Text rewardText;
    public Text fundsEarnedSuccessText;
    public Text fundsEarnedFailText;
    public Text fundsEarnedIngameText;
    public Text timeTakenSuccessText;
    public Text timeTakenFailText;
    public Text gameScoreText;

    [Header("Public Image Fields")]
    public Image timeFillBar;
    public Image lineBombImage;
    public Image radiusBombImage;
    public GameObject bg_WallLight;
    public GameObject bg_PipesLight;

    [Header("Vibration Sprites")]
    [SerializeField] private Sprite vibrationOn;
    [SerializeField] private Sprite vibrationOff;
    [SerializeField] private Image vibrationObj;

    public float musicLowValue = 0.25f;

    public static IngameUI Instance;

    private int totalFundsEarned;
    private int lineBombCount = 0;
    private int radiusBombCount = 0;
    private int timeBombCount = 0;
    private ulong gameScore = 0;

    private ItemController itemController;
    private GameObject panelToOpenNext = null;

    public delegate void ApplicationOutOfFocusOperations();
    public static ApplicationOutOfFocusOperations applicationUnfocus;
    public delegate void ApplicationInFocusOperations();
    public static ApplicationInFocusOperations applicationfocus;

    private bool isOutOfFocus = false;
    private bool hasDataBeenUpdated = false;
    private int totalFundsAtStart = 0;
    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
        _soundSource = soundSource;
        _musicSource = musicSource;
        PlayMusic();
    }

    void Start ()
    {
        Time.timeScale = 0.0f;

        ChangeVibrationSprite();

        if (StartPanel)
        {
           // StartPanel.SetActive(true);
           // SoundManager.Instance.PlayTaptoplaySound();
        }
        else
            Utility.ErrorLog("Start Panel is not assigned in IngameUI.cs", 1);

        if (ItemController.Instance)
        {
            itemController = ItemController.Instance;
        }
        else
            Utility.ErrorLog("ItemController could not be found in IngameUI.cs of " + this.gameObject, 1);        

        totalFundsAtStart = EncryptedPlayerPrefs.GetInt("Funds");
        fundsEarnedIngameText.text = totalFundsAtStart.ToString();

        if (levelNumberText)
        {
            levelNumberText.text = GameManager.Instance.levelNumber.ToString();
        }
        else
            Utility.ErrorLog("Level Number Text is not assigned in IngameUI.cs of " + this.gameObject, 1);

        isPlaying = false;
        IsPlayingIngame = false;
        GameIsOutOfIngameOperations(true);
        totalFundsEarned = 0;
    }
    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            //if (ManageNotifications.Instance)
            //{
            //    ManageNotifications.Instance.CancelNotifications();
            //}
            if (applicationfocus != null)
            {
                applicationfocus();
            }
            //if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
            //{
            //    if (Share.shareStarted)
            //    {
            //        Share.shareStarted = false;
            //        CheckForShareRewards();
            //    }
            //}
            if (IsPlayingIngame)
            {
                if (isOutOfFocus)
                {
                    isOutOfFocus = false;
                }
            }
        }
        else
        {
            //if (ManageNotifications.Instance)
            //{
            //    ManageNotifications.Instance.ScheduleNotifications();
            //}
            if (applicationUnfocus != null)
            {
                applicationUnfocus();
            }
            if (IsPlayingIngame)
            {
                if (!isOutOfFocus)
                {
                    isOutOfFocus = true;
                    UpdateDataOfGame();
                }
            }
        }
    }
    public void UpdateDataOfGame()
    {
        if (hasDataBeenUpdated == false)
        {
            EncryptedPlayerPrefs.SetInt("Funds", totalFundsAtStart);
            EncryptedPlayerPrefs.SetInt("LineBomb", lineBombCount);
            EncryptedPlayerPrefs.SetInt("RadiusBomb", radiusBombCount);
            EncryptedPlayerPrefs.SetInt("TimeBomb", timeBombCount);
        }
    }
    public void playLightningEffect()
    {
        bg_WallLight.GetComponent<Animator>().SetTrigger("light");
        bg_PipesLight.GetComponent<Animator>().SetTrigger("light");
    }
    public void GameStart()
    {
        isPlaying = true;
        IsPlayingIngame = true;

        GameManager.Instance.gameOver = false;
        SoundManager.Instance.PlayTappedSound();

        GameIsInOFInGameOperations();

        if (StartPanel)
            StartPanel.SetActive(false);
        else
            Utility.ErrorLog("Start Panel is not assigned in IngameUI.cs", 1);

        if (ingamePanel)
            ingamePanel.SetActive(true);
        else
            Utility.ErrorLog("Ingame Panel is not assigned in IngameUI.cs", 1);

        Time.timeScale = 1.0f;
        
        if (!TutorialManager.isTutorialRunning)
        {
            //newHeadsPanel.SetActive(true);
            Time.timeScale = 0.0f;
        }

        ingameLevelText.text = GameManager.Instance.levelNumber.ToString();

        if (TutorialManager.isTutorialRunning)
        {
            tutorialPanel.SetActive(true);
            if (EncryptedPlayerPrefs.GetInt("Tutorial") == 0)
            {
                if (TutorialManager.Instance) 
                    TutorialManager.Instance.OpenPanel(0);
                Time.timeScale = 0f;
            }
        }

        if (GameManager.Instance.levelNumber == 1)
        {
        }

        Utility.MakeClickSound();
    }
    public void GamePause()
    {
        isPlaying = false;
        IsPlayingIngame = false;
        GameIsOutOfIngameOperations(false);

        Time.timeScale = 0.0f;
        
        if (pausePanel)
            pausePanel.SetActive(true);
        else
            Utility.ErrorLog("Pause Panel is not assigned in IngameUI.cs", 1);

        SoundManager.Instance.PlayPauseSound();
    }

    public void GameResume()
    {
        isPlaying = true;
        IsPlayingIngame = true;
        GameIsInOFInGameOperations();

        if (pausePanel)
            pausePanel.SetActive(false);
        else
            Utility.ErrorLog("Pause Panel is not assigned in IngameUI.cs", 1);

        Time.timeScale = 1.0f;

        Utility.MakeClickSound();
    }

    public void BackToMainMenu()
    {
        //ShowInterstitialAd(false);
        UpdateDataOfGame();
        GameManager.Instance.comingFromIngame = true;

        Screen.orientation = ScreenOrientation.Portrait;

        StartLoading();
    }

    public void GameRestart()
    {
        ShowInterstitialAd(false);
        UpdateDataOfGame();
        if (loadingPanel)
        {
            loadingPanel.SetActive(true);

            if (loadingPanel.GetComponent<LoadScene>())
            {
                loadingPanel.GetComponent<LoadScene>().LoadTheSceneAgain();
            }
            else
                Utility.ErrorLog("LoadScene Component not found on Loading Panel", 2);
        }
        else
            Utility.ErrorLog("Loading Panel Panel is not assigned in IngameUI.cs", 1);

        Utility.MakeClickSound();
    }

    public void GameNext()
    {
        //ShowInterstitialAd(false);
        UpdateDataOfGame();
        GameManager.Instance.levelNumber += 1;

        if (loadingPanel)
        {
            loadingPanel.SetActive(true);

            if (loadingPanel.GetComponent<LoadScene>())
            {
                loadingPanel.GetComponent<LoadScene>().LoadTheSceneAgain();
            }
            else
                Utility.ErrorLog("LoadScene Component not found on Loading Panel", 2);
        }
        else
            Utility.ErrorLog("Loading Panel Panel is not assigned in IngameUI.cs", 1);

        Utility.MakeClickSound();
    }

    public void GameComplete(bool gameStatus)
    {
        isPlaying = false;
        GameIsOutOfIngameOperations(false);
        
        if (gameStatus)
        {
          //  TimeController.Instance.StopTimer();

            ScoreCalculation(gameStatus);
            StartCoroutine(ShowPanelWait(gameStatus));
        }
        else
        {
            if (AdManager.Instance)
            {
                if ((AdManager.Instance.IsRewardVideoAvailable() && RewardVideo.pressed != 2))
                {
                    ReSpawnPanel.SetActive(true);
                }
                else
                {
                    ScoreCalculation(gameStatus);
                    StartCoroutine(ShowPanelWait(gameStatus));
                }
            }
            else
            {
                ScoreCalculation(gameStatus);
                StartCoroutine(ShowPanelWait(gameStatus));
            }
        }
    }
    public void RevivePlayer()
    {
        ReSpawnPanel.GetComponent<RespawnPanel>().RespawnByVideo();
        Time.timeScale = 1;
        isPlaying = true;
        GameManager.Instance.gameOver = false;
      //  TimeController.Instance.AddTimeInMinutes(2);
      //  TimeController.Instance.ResetTimeCompleted();

    }
    public void FailOperations()
    {
        ShowInterstitialAd(false);
        ScoreCalculation(false);
        hasDataBeenUpdated = true;
        IsPlayingIngame = false;
        failPanel.SetActive(true);
    }
    public void UpdateHeaderValues()
    {
        DisplayItemValues[] items = GameObject.FindObjectsOfType<DisplayItemValues>() as DisplayItemValues[];

        foreach (var item in items)
        {
            item.ShowCount();
        }
    }

    IEnumerator ShowPanelWait(bool gameStatus)
    {
        AdjustLowVolume();

        yield return new WaitForSeconds(2f);

        ShowInterstitialAd(false);
        yield return new WaitForSeconds(1f);

        //Time.timeScale = 0f;

        hasDataBeenUpdated = true;
        IsPlayingIngame = false;

        if (gameStatus)
        {

            if (successPanel)
            {
                successPanel.SetActive(true);
            }
            else
                Utility.ErrorLog("Success Panel is not assigned in IngameUI.cs", 1);
        }
        else
        {
            if (failPanel)
            {
                failPanel.SetActive(true);
                //StartCoroutine(GiveExtraTimeCoins());
            }
            else
                Utility.ErrorLog("Fail Panel is not assigned in IngameUI.cs", 1);
        }
    }
    public void StartLoading()
    {
        if (loadingPanel)
        {
            loadingPanel.SetActive(true);

            if (loadingPanel.GetComponent<LoadScene>())
            {
                loadingPanel.GetComponent<LoadScene>().StartLoadingScene("MainMenu");
            }
            else
                Utility.ErrorLog("LoadScene Component not found on Loading Panel", 2);
        }
        else
            Utility.ErrorLog("Loading Panel is not assigned in IngameUI.cs", 1);

        Utility.MakeClickSound();
    }

    public void GameIsInOFInGameOperations()
    {
        HideBanner();
    }

    public void GameIsOutOfIngameOperations(bool dateCheckDependencyForInterstitial)
    {
        //ShowInterstitialAd(dateCheckDependencyForInterstitial);
        //ShowBanner();
    }

    private static int adCounter = 0;
    public void ShowInterstitialAd(bool dateCheckDependency)
    {
        if (adCounter != 2)
        {
            adCounter++;
            return;
        }
        else
        {
            adCounter = 0;
        }
        if (AdManager.Instance)
        {
            if (AdManager.Instance.IsInterstitialAvailable())
            {
                AdManager.Instance.ShowInterstitialAd(dateCheckDependency);
            }
        }
    }

    public void ShowBanner()
    {
        if (AdManager.Instance)
        {
            AdManager.Instance.ShowBanner();
        }
    }

    public void HideBanner()
    {
        if (AdManager.Instance)
        {
            AdManager.Instance.HideBanner();
        }
    }
    public void ShareGame()
    {
        if (AdManager.Instance)
        {
            if (AdManager.Instance.gameObject.GetComponent<Share>())
            {
                AdManager.Instance.gameObject.GetComponent<Share>().ShareGame();
            }
            else
                Utility.ErrorLog("Share Component not found in on " + AdManager.Instance.gameObject, 2);
        }

        Utility.MakeClickSound();
    }

    public void RateUs()
    {
        string url = "https://play.google.com/store/apps/details?id=" + Application.identifier;
        Application.OpenURL(url);
    }

    public void DoubleTheCoins()
    {
        int totalFunds = EncryptedPlayerPrefs.GetInt("Funds", 0);
        totalFunds = totalFunds + totalFundsEarned;
        EncryptedPlayerPrefs.SetInt("Funds", totalFunds);
        //int totalFunds =  EncryptedPlayerPrefs.GetInt("Funds");
        //totalFundsEarned = totalFundsEarned * 2;
        //totalFunds = totalFunds + totalFundsEarned;
        //EncryptedPlayerPrefs.SetInt("Funds", totalFunds);
        if (fundsEarnedSuccessText)
        {
            fundsEarnedSuccessText.text = (totalFundsEarned * 2).ToString();
        }
        if (rewardText)
        {
            rewardText.text = (totalFundsEarned * 2).ToString();
        }
        else
            Utility.ErrorLog("rewardText is not assigned in IngameUI.cs", 1);

        if (RewardPanel)
        {
            RewardPanel.SetActive(true);
        }
        else
            Utility.ErrorLog("RewardPanel is not assigned in IngameUI.cs", 1);

        Utility.MakeClickSound();
    }

    public void RewardPanelClose()
    {
        if (RewardPanel)
        {
            RewardPanel.SetActive(false);
        }
        else
            Utility.ErrorLog("RewardPanel is not assigned in IngameUI.cs", 1);

        Utility.MakeClickSound();
    }

    public void RewardNotAvailablePanelClose()
    {
        if (rewardNotAvailable)
        {
            rewardNotAvailable.SetActive(false);
        }
        else
            Utility.ErrorLog("rewardNotAvailable is not assigned in IngameUI.cs", 1);

        Utility.MakeClickSound();
    }

    public void RewardLostPanelClose()
    {
        if (rewardLost)
        {
            rewardLost.SetActive(false);
        }
        else
            Utility.ErrorLog("rewardLost is not assigned in IngameUI.cs", 1);

        Utility.MakeClickSound();
    }

    private void ScoreCalculation(bool gameWon)
    {
        UpdateDataOfGame();
        if (gameWon)
        {
            LevelCompleteMusic.Instance.PlaySuccessMusic();

            int totalUnlockedLevels = EncryptedPlayerPrefs.GetInt("LevelsUnocked");
            if (GameManager.Instance.levelNumber >= totalUnlockedLevels)
            {
                totalUnlockedLevels++;
            }
            EncryptedPlayerPrefs.SetInt("LevelsUnocked", totalUnlockedLevels);

            int funds = EncryptedPlayerPrefs.GetInt("Funds");
            int tempFunds = Random.Range(10, 25);
            funds = tempFunds + funds;
            EncryptedPlayerPrefs.SetInt("Funds", funds);

            fundsEarnedIngameText.text = funds.ToString();

            totalFundsEarned += tempFunds;

            if (EncryptedPlayerPrefs.GetString("HighScore") == "")
            {
                EncryptedPlayerPrefs.SetString("HighScore", "0");
            }

            ulong gameHighScore = ulong.Parse(EncryptedPlayerPrefs.GetString("HighScore"));
            ulong gameHighScoreWeekly = ulong.Parse(EncryptedPlayerPrefs.GetString("HighScoreWeekly"));

            //int scoreLevel = (totalFundsEarned * 2) + (100 * GameManager.Instance.eyesPopped) + 500;
            //gameScore += (ulong)scoreLevel;
            gameHighScore += gameScore;
            gameHighScoreWeekly += gameScore;

            //if (EncryptedPlayerPrefs.GetInt("Highscore", 0) < gameScore)
            //{
            EncryptedPlayerPrefs.SetString("Highscore", gameHighScore.ToString());
            EncryptedPlayerPrefs.SetString("HighScoreWeekly", gameHighScoreWeekly.ToString());
            //}

           /* if (GameServerData.Instance)
                GameServerData.Instance.SetData(gameHighScore.ToString(), gameHighScoreWeekly.ToString(), 1);*/

            if (fundsEarnedSuccessText)
            {
                fundsEarnedSuccessText.text = totalFundsEarned.ToString();
            }
            else
                Utility.ErrorLog("fundsEarnedSuccessText is not assigned in IngameUI.cs", 1);

           // timeTakenSuccessText.text = TimeController.Instance.GetTime();
        }
        else
        {
            LevelCompleteMusic.Instance.PlayFailMusic();

            if (fundsEarnedFailText)
            {
                fundsEarnedFailText.text = totalFundsEarned.ToString();
            }
            else
                Utility.ErrorLog("fundsEarnedFailText is not assigned in IngameUI.cs", 1);

          //  timeTakenFailText.text = TimeController.Instance.GetTime();
        }
    }

    public void RemoveAds()
    {
        if (AdManager.Instance)
        {
            GameObject adManager = AdManager.Instance.gameObject;
            if (adManager.GetComponent("Purchaser"))
            {
                adManager.GetComponent("Purchaser").SendMessage("RemoveAdsPurchaseCompleted");
            }
            else
                Utility.ErrorLog("Purchaser is not found in IngameUI.cs " + " of " + this.gameObject.name, 2);
        }
        Utility.MakeClickSound();
    }

    public void MakeClickSound()
    {
        if (_soundSource)
        {
            _soundSource.PlayUIClickedSound();
        }
        else
            Utility.ErrorLog("Sound Source is not assigned in IngameUI.cs", 1);
    }

    public static void PlayMusic()
    {
        if (_musicSource)
        {
            _musicSource.PlayIngameMusic();
        }
        else
            Utility.ErrorLog("Music Source is not assigned in IngameUI.cs", 1);
    }

    public void ControlViberation()
    {
        if (GameManager.Instance.vibrationValue == 1)
        {
            GameManager.Instance.vibrationValue = 0;
            EncryptedPlayerPrefs.SetInt("vibrationValue", 0);
        }
        else 
        if (GameManager.Instance.vibrationValue == 0)
        {
            //Vibration.Vibrate(50);
            GameManager.Instance.vibrationValue = 1;
            EncryptedPlayerPrefs.SetInt("vibrationValue", 1);
        }
        ChangeVibrationSprite();
        Utility.MakeClickSound();
    }

    public void ChangeVibrationSprite()
    {
        if (GameManager.Instance.vibrationValue == 1)
        {
            vibrationObj.sprite = vibrationOn;
        }
        else
        if (GameManager.Instance.vibrationValue == 0)
        {
            vibrationObj.sprite = vibrationOff;
        }
    }

    public void AdjustLowVolume()
    {
        musicSource.GetComponent<SoundAndMusic>().MusicLowAdjust(musicLowValue);
    }

    public void AdjustHighVolume()
    {
        musicSource.GetComponent<SoundAndMusic>().MusicHighAdjust();
    }
}