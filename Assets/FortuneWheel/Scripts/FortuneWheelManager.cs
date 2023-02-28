using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.AI;

public class FortuneWheelManager : MonoBehaviour
{
    public GameObject rewardAnimationPanel;
    public Text rewardCount;
    public Image rewardImage;
    public Sprite[] rewardsImages;
    public Button TurnButton;
    public Button freeVideo;
    public GameObject Circle; 			// Rotatable Object with rewards
    //public Text CoinsDeltaText; 		// Pop-up text with wasted or rewarded coins amount
    public Text CostText; 		// Pop-up text with wasted or rewarded coins amount
    public int BasicTurnCost;
    public float rotationTime = 4f;
    [HideInInspector]
    public bool isFreeSpin = false;
    private int CurrentCoinsAmount; // Started coins amount. In your project it can be set up from CoinsManager or from EncryptedPlayerPrefs and so on
                                    //private int PreviousCoinsAmount;        // For wasted coins animation

    private int TurnCost;          // How much coins user waste when turn whe wheel
    private bool _isStarted;
    private float[] _sectorsAngles;
    private float _finalAngle;
    private float _startAngle = 0;
    private float currentRotationTime;
    private float half;
    private void Awake()
    {
        _sectorsAngles = new float[] { 40, 80, 120, 160, 200, 240, 280, 320, 360 };
    }
    void OnEnable()
    {
        CloseRewardPanel();
        CurrentCoinsAmount = EncryptedPlayerPrefs.GetInt("Funds");
        _startAngle = _sectorsAngles[UnityEngine.Random.Range(0, _sectorsAngles.Length)];
        half = _startAngle - (GetHalf());
        Circle.transform.localEulerAngles = new Vector3(0, 0, _startAngle);

        int timesPressed = EncryptedPlayerPrefs.GetInt("FortuneWheelPressedTimes");

        TurnCost = GetTurnCost(timesPressed);

        if (timesPressed == 0)
        {
            CostText.text = I2.Loc.LocalizationManager.GetTranslation("FREE");
            TurnButton.gameObject.SetActive(true);
            freeVideo.gameObject.SetActive(false);
        }
        else
        {
            CheckForFreeSpinVideo();
            CostText.text = TurnCost.ToString();
        }
    }
    void CheckForFreeSpinVideo()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            TurnButton.gameObject.SetActive(true);
            freeVideo.gameObject.SetActive(false);
        }
        if (AdManager.Instance)
        {
            if (!AdManager.Instance.IsRewardVideoAvailable())
            {
                TurnButton.gameObject.SetActive(true);
                freeVideo.gameObject.SetActive(false);
            }
            else
            {
                TurnButton.gameObject.SetActive(false);
                freeVideo.gameObject.SetActive(true);
            }
        }
        else
        {
            TurnButton.gameObject.SetActive(true);
            freeVideo.gameObject.SetActive(false);
        }
    }
    int GetTurnCost(int timesPressed)
    {
        return timesPressed * (BasicTurnCost + (timesPressed * 25));
    }
    public static bool CheckTime()
    {
        if (!EncryptedPlayerPrefs.HasKey(GetFortuneWheelTimerKey()))
        {
            EncryptedPlayerPrefs.SetString(GetFortuneWheelTimerKey(), Utility.ParseDateAndTimeInString(DateTime.UtcNow.AddDays(-1)));
            return true;
        }
        else
        {
            return Utility.CompareDataAndTimes(EncryptedPlayerPrefs.GetString(GetFortuneWheelTimerKey()), Utility.ParseDateAndTimeInString(DateTime.UtcNow));
        }
    }
    
    private static string GetFortuneWheelTimerKey()
    {
        return "FortuneWheelTimer";
    }
    public void Pass24Hours()
    {
        SetPlayerPrefForTime(Utility.ParseDateAndTimeInString(DateTime.UtcNow.AddYears(-1)));
    }
    public void CloseRewardPanel()
    {
        rewardAnimationPanel.SetActive(false);
        _startAngle = _sectorsAngles[UnityEngine.Random.Range(0, _sectorsAngles.Length)];
        half = _startAngle - (GetHalf());
        Circle.transform.localEulerAngles = new Vector3(0, 0, _startAngle);
        Utility.MakeClickSound();
    }
    void SetPlayerPrefForTime(string DateAndTime)
    {
        char split = ' ';
        string[] currentDateAndTime = DateAndTime.Split(split);

        split = '/';

        string[] currentDateThings = currentDateAndTime[0].Split(split);

        split = ':';

        string[] currentTimeThings = currentDateAndTime[1].Split(split);

        int previousMonth = int.Parse(currentDateThings[0]);
        int previousDay = int.Parse(currentDateThings[1]);
        int previousYear = int.Parse(currentDateThings[2]);

        int previousHour = int.Parse(currentTimeThings[0]);
        int previousMinute = int.Parse(currentTimeThings[1]);
        int previousSecond = int.Parse(currentTimeThings[2]);

        string stringToForm = previousMonth + "/" + previousDay + "/" + previousYear + " " + previousHour + ":" + previousMinute + ":" + previousSecond;
        
        EncryptedPlayerPrefs.SetString(GetFortuneWheelTimerKey(), stringToForm);
    }
    public void TurnWheel()
    {
        Utility.ShowHeaderValues();
        CurrentCoinsAmount = EncryptedPlayerPrefs.GetInt("Funds");
        // Player has enough money to turn the wheel
        if ((CurrentCoinsAmount >= TurnCost) || isFreeSpin)
        {
            int timesPressed = EncryptedPlayerPrefs.GetInt("FortuneWheelPressedTimes");

            if (!isFreeSpin)
            {
                // Decrease money for the turn
                CurrentCoinsAmount -= TurnCost;
                EncryptedPlayerPrefs.SetInt("Funds", CurrentCoinsAmount);

                Utility.ShowHeaderValues();
            }

            if (timesPressed == 0)
            {
                //MainMenuUI.Instance.fortuneWheelMarkBlinker.SetActive(false);
                
                SetPlayerPrefForTime(Utility.ParseDateAndTimeInString(DateTime.UtcNow));
            }

            if (isFreeSpin == false)
            {
                timesPressed++;
            }

            else if (isFreeSpin)
            {
                isFreeSpin = false;
            }

            EncryptedPlayerPrefs.SetInt("FortuneWheelPressedTimes", timesPressed);
            TurnCost = GetTurnCost(timesPressed);

            currentRotationTime = 0f;
            
            int fullCircles = 6;
            float randomFinalAngle = _sectorsAngles[UnityEngine.Random.Range(0, _sectorsAngles.Length)];
            
            while (randomFinalAngle == 80 || randomFinalAngle == 160 || randomFinalAngle == 200 || randomFinalAngle == 240 || randomFinalAngle == 320)
            {
                randomFinalAngle = _sectorsAngles[UnityEngine.Random.Range(0, _sectorsAngles.Length)];
            }
            // Here we set up how many circles our wheel should rotate before stop
            _finalAngle = -(fullCircles * 360 + randomFinalAngle);
            _isStarted = true;
        }
    }
    private void GiveAwardByAngle()
    {
        // Here you can set up rewards for every sector of wheel
        switch ((int)_startAngle)
        {
            case 0:
                GiveCoins();
                //Debug.Log("ANGLE 360");
                break;
            case -320:
                GiveDoge();
                break;
            case -280:
                GiveLineBomb();
                break;
            case -240:
                GiveBtc();
                break;
            case -200:
                GiveEth();
                break;
            case -160:
                GiveBnb();
                break;
            case -120:
                GiveRadiusBomb();
                break;
            case -80:
                GiveUsdt();
                break;
            case -40:
                GiveTimeBomb();
                break;
        }
    }
    void AfterRewards(int spriteIndex, int rewardAmount)
    {
        int timesPressed = EncryptedPlayerPrefs.GetInt("FortuneWheelPressedTimes");
        if (timesPressed == 1)
        {
            CheckForFreeSpinVideo();
        }
        rewardImage.sprite = rewardsImages[spriteIndex];
        rewardCount.text = rewardAmount.ToString();
        rewardAnimationPanel.SetActive(true);

        SoundManager.Instance.PlayRewardPanelSound();

        Utility.ShowHeaderValues();
    }
    void GiveCoins()
    {
        int value = EncryptedPlayerPrefs.GetInt("Funds");
        int random = UnityEngine.Random.Range(1000, 1501);
        value = value + random;
        EncryptedPlayerPrefs.SetInt("Funds", value);
        AfterRewards(0, random);
    }
    void GiveDoge()
    {
        int value = EncryptedPlayerPrefs.GetInt("DOGECollected");
        int random = UnityEngine.Random.Range(1, 4);
        value = value + random;
        EncryptedPlayerPrefs.SetInt("DOGECollected", value);
        AfterRewards(1, random);
    }
    void GiveLineBomb()
    {
        int value = EncryptedPlayerPrefs.GetInt("LineBomb");
        int random = UnityEngine.Random.Range(2, 4);
        value = value + random;
        EncryptedPlayerPrefs.SetInt("LineBomb", value);
        AfterRewards(2, random);
    }
    void GiveBtc()
    {
        int value = EncryptedPlayerPrefs.GetInt("BTCCollected");
        int random = UnityEngine.Random.Range(1, 2);
        value = value + random;
        EncryptedPlayerPrefs.SetInt("BTCCollected", value);
        AfterRewards(3, random);
    }
    void GiveEth()
    {
        int value = EncryptedPlayerPrefs.GetInt("ETHCollected");
        int random = UnityEngine.Random.Range(1, 3);
        value = value + random;
        EncryptedPlayerPrefs.SetInt("ETHCollected", value);
        AfterRewards(4, random);
    }
    void GiveBnb()
    {
        int value = EncryptedPlayerPrefs.GetInt("BNBCollected");
        int random = UnityEngine.Random.Range(1, 2);
        value = value + random;
        EncryptedPlayerPrefs.SetInt("BNBCollected", value);
        AfterRewards(5, random);
    }
    void GiveRadiusBomb()
    {
        int value = EncryptedPlayerPrefs.GetInt("RadiusBomb");
        int random = UnityEngine.Random.Range(1, 3);
        value = value + random;
        EncryptedPlayerPrefs.SetInt("RadiusBomb", value);
        AfterRewards(6, random);
    }
    void GiveUsdt()
    {
        int value = EncryptedPlayerPrefs.GetInt("USDTCollected");
        int random = UnityEngine.Random.Range(1, 2);
        value = value + random;
        EncryptedPlayerPrefs.SetInt("USDTCollected", value);
        AfterRewards(7, random);
    }
    void GiveTimeBomb()
    {
        int value = EncryptedPlayerPrefs.GetInt("TimeBomb");
        int random = UnityEngine.Random.Range(1, 3);
        value = value + random;
        EncryptedPlayerPrefs.SetInt("TimeBomb", value);
        AfterRewards(8, random);
    }
    void Update()
    {
        // Make turn button non interactable if user has not enough money for the turn
        if (_isStarted || CurrentCoinsAmount < TurnCost)
        {
            TurnButton.interactable = false;
            TurnButton.GetComponent<Image>().color = new Color(255, 255, 255, 0.5f);
        }
        else
        {
            TurnButton.interactable = true;
            TurnButton.GetComponent<Image>().color = new Color(255, 255, 255, 1);
        }

        if (!_isStarted)
            return;


        // increment timer once per frame
        currentRotationTime += Time.deltaTime;
        if (currentRotationTime > rotationTime || Circle.transform.localEulerAngles.z == _finalAngle)
        {
            currentRotationTime = rotationTime;
            _isStarted = false;
            _startAngle = _finalAngle % 360;

            CostText.text = TurnCost.ToString();
            GiveAwardByAngle();
            //StartCoroutine(HideCoinsDelta());
            //half = Circle.transform.localEulerAngles.z - (GetHalf());
            //Debug.Log(half +"   " +Circle.transform.localEulerAngles.z);
        }

        // Calculate current position using linear interpolation
        float t = currentRotationTime / rotationTime;

        // This formulae allows to speed up at start and speed down at the end of rotation.
        // Try to change this values to customize the speed        x
        t = t * t * t * (t * (6f * t - 15f) + 10f);

        float angle = Mathf.Lerp(_startAngle, _finalAngle, t);
        Circle.transform.localEulerAngles = new Vector3(0, 0, angle);
        CheckHalf(angle);
    }
    void CheckHalf(float angle)
    {
        if ((angle - half) < 0.1f)
        {
            half = angle - GetHalf();
            SoundManager.Instance.PlayTikTik();
        }
    }
    float GetHalf()
    {
        return (360 / _sectorsAngles.Length) / 2;
    }
}
