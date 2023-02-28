using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class UIScript : MonoBehaviour
{
    [Header("Public Menu Panels")]
    public static bool IsPlayingIngame = false;
    public static bool isPlaying = false;
    public GameObject pausePanel;
    public GameObject StartPanel;
    public GameObject ingamePanel;
    public GameObject successPanel;
    public GameObject loadingPanel;
    public GameObject failPanel;
    public GameObject failanim;
    public static UIScript Instance;
    GameInitializer gameInitializer;
    //for game success animation and stuff
    public GameObject[] stars;
    public GameObject winPanel;
    public GameObject[] Submarines;
    private int totalFundsAtStart = 0;
    private ulong gameScore = 0;
    private int totalFundsEarned;
    public Text fundsEarnedIngameText;
    public Text ingameLevelText;
    //public Text fundsEarnedSuccessText;
    private bool hasDataBeenUpdated = false;
    public static int funds;
    //level number
    public static int levelNumber = 1;
    public static int currentLevel = 1;
    //coin animation
    [SerializeField] GameObject animatedCoinPrefab;
    [SerializeField] Transform target;

    [Space]
    [Header("Available coins : coins to pool")]
    [SerializeField] int maxCoins;
    Queue<GameObject> coinsQueue = new Queue<GameObject>();

    [Space]
    [Header("Animation Settings")]
    [SerializeField] [Range(0.5f,0.9f)] float minAnimDuration;
    [SerializeField] [Range(0.9f, 2f)] float maxAnimDuration;
    [SerializeField] Ease easeType;
    [SerializeField] float spread;
    Vector3 targetPosition;
    public bool levelSuccess = false;
    public Button pauseButton;
    // Start is called before the first frame update
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
        targetPosition = target.position;
        PrepareCoins();
    }
    void PrepareCoins()
    {
        GameObject coin;
        for (int i = 0; i < maxCoins; i++)
        {
            coin = Instantiate(animatedCoinPrefab);
            coin.transform.parent = transform;
            coin.SetActive(false);
            coinsQueue.Enqueue(coin);
        }
    }

   void Animate(Vector3 collectedCoinPosition, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            //check if there's coins in the pool
            if (coinsQueue.Count > 0)
            {
                //extract a coin from the pool
                GameObject coin = coinsQueue.Dequeue();
                coin.SetActive(true);

                //move coin to the collected coin pos
                coin.transform.position = collectedCoinPosition + new Vector3(Random.Range(-spread, spread), 0f, 0f);

                //animate coin to target position
                float duration = Random.Range(minAnimDuration, maxAnimDuration);
                coin.transform.DOMove(targetPosition, duration)
                .SetEase(easeType)
                .SetUpdate(true)
                .OnComplete(() => {
                    //executes whenever coin reach target position
                    coin.SetActive(false);
                    coinsQueue.Enqueue(coin);

                    funds++;
                });
            }
        }
    }
public void AddCoins(Vector3 collectedCoinPosition, int amount)
    {
        Animate(collectedCoinPosition, amount);
    }

    // Update is called once per frame
    private void Update()
    {
        fundsEarnedIngameText.text = PlayerPrefs.GetInt("Funds").ToString();
        if (SpawnEnemy.Instance.enemies.transform.childCount > 0)
        {
            //reduce timer here
         /*   Debug.Log("Timer place");*/
        }
        else if(SpawnEnemy.Instance.enemies.transform.childCount < 1 && SpawnEnemy.Instance.enemyCount <= 0 && levelSuccess == false)
        {
            Debug.Log("We here");
            StartCoroutine(UIScript.Instance.ShowStarCo());
            levelSuccess = true;
        }
        /* if (ClockUI.Instance.hours >= 45)
         {
             StartCoroutine(ShowStarCo());
         }*/
    }
    void Start()
    {
        //PlayerPrefs.SetInt("LevelNumber", levelNumber);
        totalFundsAtStart = PlayerPrefs.GetInt("Funds");
        fundsEarnedIngameText.text = totalFundsAtStart.ToString();
        //healthBarBehaviour.SetHealth(health, startHealth);
        pauseButton.enabled = true;
        if (StartPanel)
        {
            StartPanel.SetActive(true);

            SoundManager.Instance.PlayTaptoplaySound();
            Time.timeScale = 0f;
        }
        else
            Utility.ErrorLog("Start Panel is not assigned in IngameUI.cs", 1);
        
    }
     public void FailOperations()
    {
        //ShowInterstitialAd(false);
        ScoreCalculation(false);
        hasDataBeenUpdated = true;
        IsPlayingIngame = false;
        failanim.SetActive(true);
        failPanel.SetActive(true);
        
    }
    public void UpdateDataOfGame()
    {
        if (hasDataBeenUpdated == false)
        {
           // PlayerPrefs.SetInt("Funds", totalFundsAtStart);
        }
    }
    private void ScoreCalculation(bool gameWon)
    {
        UpdateDataOfGame();
        if (gameWon)
        {
            LevelCompleteMusic.Instance.PlaySuccessMusic();

            int totalUnlockedLevels = PlayerPrefs.GetInt("LevelsUnocked");
            if (GameManager.Instance.levelNumber >= totalUnlockedLevels)
            {
                totalUnlockedLevels++;
            }
            EncryptedPlayerPrefs.SetInt("LevelsUnocked", totalUnlockedLevels);

            funds = PlayerPrefs.GetInt("Funds");
            int tempFunds = Random.Range(10, 25);
            funds = tempFunds + funds;
            PlayerPrefs.SetInt("Funds", funds);

            fundsEarnedIngameText.text = PlayerPrefs.GetInt("Funds").ToString();

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
        /*    if (GameServerData.Instance)
                GameServerData.Instance.SetData(gameHighScore.ToString(), gameHighScoreWeekly.ToString(), 1);*/

          /*  if (fundsEarnedSuccessText)
            {
                fundsEarnedSuccessText.text = totalFundsEarned.ToString();
            }
            else
                Utility.ErrorLog("fundsEarnedSuccessText is not assigned in IngameUI.cs", 1);
*/
            // timeTakenSuccessText.text = TimeController.Instance.GetTime();
        }
       /* else
        {
            LevelCompleteMusic.Instance.PlayFailMusic();
*//*
            if (fundsEarnedFailText)
            {
                fundsEarnedFailText.text = totalFundsEarned.ToString();
            }
            else
                Utility.ErrorLog("fundsEarnedFailText is not assigned in IngameUI.cs", 1);*//*

            //  timeTakenFailText.text = TimeController.Instance.GetTime();
        }*/
    }
    public void GameStart()
    {
       
        isPlaying = true;
        IsPlayingIngame = true;
        GameManager.Instance.gameOver = false;
        SoundManager.Instance.PlayTappedSound();

        GameIsInOFInGameOperations();

        if (StartPanel)
        {
            StartPanel.SetActive(false);
            //play random engine sound here
            SoundManager.Instance.PlaySubSound(1f);
        }
            
        else
            Utility.ErrorLog("Start Panel is not assigned in IngameUI.cs", 1);

        if (ingamePanel)
            ingamePanel.SetActive(true);
        else
            Utility.ErrorLog("Ingame Panel is not assigned in IngameUI.cs", 1);

        Time.timeScale = 1.0f;/*
        ingameLevelText.text = PlayerPrefs.GetInt("LevelNumber").ToString();*/
        /*  if (!TutorialManager.isTutorialRunning)
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
          }*/

        /* if (GameManager.Instance.levelNumber == 1)
         {
         }*/

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
    public void GameIsOutOfIngameOperations(bool dateCheckDependencyForInterstitial)
    {
        //ShowInterstitialAd(dateCheckDependencyForInterstitial);
        //ShowBanner();
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
        //UpdateDataOfGame();
        //GameManager.Instance.comingFromIngame = true;
        Screen.orientation = ScreenOrientation.LandscapeRight;
        Utility.MakeClickSound();
        StartLoading();
    }
    public void GameIsInOFInGameOperations()
    {
        // HideBanner();
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
    IEnumerator ShowPanelWait(bool gameStatus)
    {
        // AdjustLowVolume();

        // yield return new WaitForSeconds(2f);

        // ShowInterstitialAd(false);
        yield return new WaitForSeconds(1f);

        //Time.timeScale = 0f;

        hasDataBeenUpdated = true;
        IsPlayingIngame = false;
        SoundManager.Instance.PlaySubSound(0f);
        SoundManager.Instance.PlayHullSound(0f);
        if (gameStatus)
        {

            if (successPanel)
            {
                
                /* levelNumber++;
                 Debug.Log(levelNumber);
                 PlayerPrefs.SetInt("LevelNumber", levelNumber);*/
                successPanel.SetActive(true);
            }
            else
                Utility.ErrorLog("Success Panel is not assigned in IngameUI.cs", 1);
        }
        else
        {
            if (failPanel)
            {
                
                failanim.SetActive(true);
                failPanel.SetActive(true);
                //StartCoroutine(GiveExtraTimeCoins());
            }
            else
                Utility.ErrorLog("Fail Panel is not assigned in IngameUI.cs", 1);
        }
    }
    public void GameComplete(bool gameStatus)
    {
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
                   // ReSpawnPanel.SetActive(true);
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
    public void GameNext()
    {
        //ShowInterstitialAd(false);
        UpdateDataOfGame();
        PlayerPrefs.GetInt("LevelNumber");

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
    public void GameRestart()
    {
        ScoreScript.scoreNumber = 0;
        //ShowInterstitialAd(false);
        // UpdateDataOfGame();
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

    public IEnumerator ShowStarCo()
    {
        SoundManager.Instance.PlaySubSound(0f);
        SoundManager.Instance.PlayHullSound(0f);
        Time.timeScale = 0f;
        isPlaying = false;
        if (currentLevel >=25)
        {
            currentLevel++;
            PlayerPrefs.SetInt("LevelNumber",Random.Range(2, 22));
            winPanel.SetActive(true);
            successPanel.SetActive(true);
            pauseButton.enabled = false;
        }
        else if(currentLevel <25)
        {
            currentLevel++;
            levelNumber++;
            Debug.Log(levelNumber);
            PlayerPrefs.SetInt("LevelNumber", levelNumber);
            winPanel.SetActive(true);
            successPanel.SetActive(true);
            pauseButton.enabled = false;
        }
        if (ScoreScript.scoreNumber < 40)
        {
            yield return new WaitForSecondsRealtime(1.0f);
            stars[0].SetActive(true);
            //anim.clip = success;
            //anim.Play();
        }
        else if(ScoreScript.scoreNumber < 50)
        {
            yield return new WaitForSecondsRealtime(1.0f);
            stars[0].SetActive(true);
            yield return new WaitForSecondsRealtime(1.2f);
            stars[1].SetActive(true);
          /*  anim.clip = success;
            anim.Play();*/
        }
        else
        {
            yield return new WaitForSecondsRealtime(1.0f);
            stars[0].SetActive(true);
            yield return new WaitForSecondsRealtime(1.2f);
            stars[1].SetActive(true);
            yield return new WaitForSecondsRealtime(1.4f);
            stars[2].SetActive(true);
        }
      
        
    }
}
