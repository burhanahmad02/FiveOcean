/***************************************************************************\
Project:      Daily Rewards
Copyright (c) Niobium Studios.
Author:       Guilherme Nunes Barbosa (gnunesb@gmail.com)
\***************************************************************************/
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using I2.Loc;

namespace NiobiumStudios
{
    /**
     * The UI Logic Representation of the Daily Rewards
     **/
    public class DailyRewardsInterface : MonoBehaviour
    {
        public GameObject canvas;
        public GameObject dailyRewardPrefab;        // Prefab containing each daily reward

        [Header("Panel Debug")]
		public bool isDebug;
        public GameObject panelDebug;
		public Button buttonAdvanceDay;
		public Button buttonAdvanceHour;
		public Button buttonReset;
		public Button buttonReloadScene;

        [Header("Panel Reward Message")]
        public GameObject panelReward;              // Rewards panel
        public Text textReward;                     // Reward Text to show an explanatory message to the player
        public Button buttonCloseReward;            // The Button to close the Rewards Panel
        public Image imageReward;                   // The image of the reward

        [Header("Panel Reward")]
        public Button buttonClaim;                  // Claim Button
        public Button buttonClose;                  // Close Button
        public Button buttonCloseWindow;            // Close Button on the upper right corner
        public Text textTimeDue;                    // Text showing how long until the next claim
        public GridLayoutGroup dailyRewardsGroup;   // The Grid that contains the rewards
        public ScrollRect scrollRect;               // The Scroll Rect

        private bool readyToClaim;                  // Update flag
        private List<DailyRewardUI> dailyRewardsUI = new List<DailyRewardUI>();
        public GameObject content;
		private DailyRewards dailyRewards;          // DailyReward Instance      

        public GameObject dailyRewardPanel;
        public GameObject dailyRewardCanvasPanel;
        public GameObject notification;
        public ParticleSystem coinsReward;
        public static DailyRewardsInterface instance;
        void Awake()
        {
            if (!instance)
            {
                instance = this;
            }
            else
            {
                return;
            }
            MainMenuUI.Instance.TurnParticlesOn();
           
            dailyRewards = GetComponent<DailyRewards>();
            dailyRewardCanvasPanel.SetActive(true);
        }

        void Start()
        {
            dailyRewardCanvasPanel.SetActive(true);
            InitializeDailyRewardsUI();

            if (panelDebug)
                panelDebug.SetActive(isDebug);

            //buttonClose.gameObject.SetActive(false);

            buttonClaim.onClick.AddListener(() =>
            {
				dailyRewards.ClaimPrize();
                readyToClaim = false;
                UpdateUI();
            });

            buttonCloseReward.onClick.AddListener(() =>
            {
				var keepOpen = dailyRewards.keepOpen;
                panelReward.SetActive(false);
                canvas.gameObject.SetActive(keepOpen);
                if (keepOpen == true)
                {
                    MainMenuUI.Instance.TurnParticlesOff();
                }
                else
                {
                    MainMenuUI.Instance.TurnParticlesOn();
                }
            });

            buttonClose.onClick.AddListener(() =>
            {
                canvas.gameObject.SetActive(true);
                MainMenuUI.Instance.TurnParticlesOn();
                Utility.MakeClickSound();
            });

            buttonCloseWindow.onClick.AddListener(() =>
            {
                canvas.gameObject.SetActive(true);
                MainMenuUI.Instance.TurnParticlesOn();
                Utility.MakeClickSound();
            });

            // Simulates the next Day
            if (buttonAdvanceDay)
				buttonAdvanceDay.onClick.AddListener(() =>
				{
                    dailyRewards.debugTime = dailyRewards.debugTime.Add(new TimeSpan(1, 0, 0, 0));
                    UpdateUI();
				});

			// Simulates the next hour
			if(buttonAdvanceHour)
				buttonAdvanceHour.onClick.AddListener(() =>
              	{
                      dailyRewards.debugTime = dailyRewards.debugTime.Add(new TimeSpan(1, 0, 0));
                      UpdateUI();
				});

			if(buttonReset)
				// Resets Daily Rewards from Player Preferences
				buttonReset.onClick.AddListener(() =>
				{
					dailyRewards.Reset();
                    dailyRewards.debugTime = new TimeSpan();
                    dailyRewards.lastRewardTime = System.DateTime.MinValue;
					readyToClaim = false;
				});

			// Reloads the same scene
			if(buttonReloadScene)
				buttonReloadScene.onClick.AddListener(() =>
				{
					Application.LoadLevel (Application.loadedLevel);
				});


			UpdateUI();
        }

        void OnEnable()
        {
            dailyRewards.onClaimPrize += OnClaimPrize;
            dailyRewards.onInitialize += OnInitialize;
        }

        void OnDisable()
        {
            if (dailyRewards != null)
            {
                dailyRewards.onClaimPrize -= OnClaimPrize;
                dailyRewards.onInitialize -= OnInitialize;
            }
        }

        // Initializes the UI List based on the rewards size
        private void InitializeDailyRewardsUI()
        {
            for (int i = 0; i < dailyRewards.rewards.Count; i++)
            {
                int day = i + 1;
                var reward = dailyRewards.GetReward(day);

                GameObject dailyRewardGo = GameObject.Instantiate(dailyRewardPrefab) as GameObject;

                DailyRewardUI dailyRewardUI = dailyRewardGo.GetComponent<DailyRewardUI>();
                dailyRewardUI.transform.SetParent(dailyRewardsGroup.transform);
                dailyRewardGo.transform.localScale = Vector2.one;

                dailyRewardUI.day = day;
                dailyRewardUI.reward = reward;
                dailyRewardUI.Initialize();

                dailyRewardsUI.Add(dailyRewardUI);
            }
        }

        public void UpdateUI()
        {
            dailyRewards.CheckRewards();

            bool isRewardAvailableNow = false;

            var lastReward = dailyRewards.lastReward;
            var availableReward = dailyRewards.availableReward;

            foreach (var dailyRewardUI in dailyRewardsUI)
            {
                var day = dailyRewardUI.day;

                if (day == availableReward)
                {
                    dailyRewardUI.state = DailyRewardUI.DailyRewardState.UNCLAIMED_AVAILABLE;

                    isRewardAvailableNow = true;
                }
                else if (day <= lastReward)
                {
                    dailyRewardUI.state = DailyRewardUI.DailyRewardState.CLAIMED;
                }
                else
                {
                    dailyRewardUI.state = DailyRewardUI.DailyRewardState.UNCLAIMED_UNAVAILABLE;
                }

                dailyRewardUI.Refresh();
            }

           // buttonClaim.gameObject.SetActive(isRewardAvailableNow);
            //buttonClose.gameObject.SetActive(!isRewardAvailableNow);
            if (isRewardAvailableNow)
            {
                SnapToReward();
                //textTimeDue.text = "You can claim your reward!";
                textTimeDue.text = LocalizationManager.GetTermTranslation("YOU CAN CLAIM YOUR REWARD!");
            }
            readyToClaim = isRewardAvailableNow;
        }

        // Snap to the next reward
        public void SnapToReward()
        {
            Canvas.ForceUpdateCanvases();

            var lastRewardIdx = dailyRewards.lastReward;

            // Scrolls to the last reward element
            if (dailyRewardsUI.Count - 1 < lastRewardIdx)
                lastRewardIdx++;

			if(lastRewardIdx > dailyRewardsUI.Count - 1)
				lastRewardIdx = dailyRewardsUI.Count - 1;

            var target = dailyRewardsUI[lastRewardIdx].GetComponent<RectTransform>();

            var content = scrollRect.content;

            //content.anchoredPosition = (Vector2)scrollRect.transform.InverseTransformPoint(content.position) - (Vector2)scrollRect.transform.InverseTransformPoint(target.position);

            float normalizePosition = (float)target.GetSiblingIndex() / (float)content.transform.childCount;
            scrollRect.verticalNormalizedPosition = normalizePosition;
        }

        private void CheckTimeDifference ()
        {
            if (!readyToClaim)
            {
                notification.SetActive(false);
                content.SetActive(false);
                canvas.SetActive(true);
                TimeSpan difference = dailyRewards.GetTimeDifference();

                // If the counter below 0 it means there is a new reward to claim
                if (difference.TotalSeconds <= 0)
                {
                    readyToClaim = true;
                    UpdateUI();
                    SnapToReward();
                    return;
                }

                string formattedTs = dailyRewards.GetFormattedTime(difference);
                textTimeDue.text = LocalizationManager.GetTermTranslation("COME BACK IN") + " " + formattedTs + " " + LocalizationManager.GetTermTranslation("FOR YOUR NEXT REWARD");
                //textTimeDue.text = string.Format("Come back in {0} for your next reward", formattedTs);
            }
            if(readyToClaim)
            {
               
                notification.SetActive(true);
            }
        }
        public void CloseDailyyRewardPanel()
        {
            dailyRewardPanel.SetActive(false);
            MainMenuUI.Instance.mainMenuPanel.SetActive(true);
            Utility.MakeClickSound();
        }
        // Delegate
        private void OnClaimPrize(int day)
        {
            panelReward.SetActive(true);

            var reward = dailyRewards.GetReward(day);
            var unit = reward.unit;
            var rewardQt = reward.reward;

            if (reward.unit == "Coins")
            {
                int rewardValue = EncryptedPlayerPrefs.GetInt("Funds");
                rewardValue = rewardValue + reward.reward;
                EncryptedPlayerPrefs.SetInt("Funds", rewardValue);
            }
            else if (reward.unit == "Air Strikes")
            {
                int rewardValue = EncryptedPlayerPrefs.GetInt("air_strike_count");
                rewardValue = rewardValue + reward.reward;
                EncryptedPlayerPrefs.SetInt("air_strike_count", rewardValue);
            }
            else if (reward.unit == "Health Kits")
            {
                int rewardValue = EncryptedPlayerPrefs.GetInt("HealthKit");
                rewardValue = rewardValue + reward.reward;
                EncryptedPlayerPrefs.SetInt("HealthKit", rewardValue);
            }
            else if (reward.unit == "Repair Kits")
            {
                int rewardValue = EncryptedPlayerPrefs.GetInt("Hearts");
                rewardValue = rewardValue + reward.reward;
                EncryptedPlayerPrefs.SetInt("Hearts", rewardValue);
            }
            else
            {
                int rewardValue = EncryptedPlayerPrefs.GetInt(reward.unit);
                rewardValue = rewardValue + reward.reward;
                EncryptedPlayerPrefs.SetInt(reward.unit, rewardValue);
            }


            Utility.MakeClickSound();

            //GameManager.DisplayValuesOfItems();

            imageReward.sprite = reward.sprite;
            //if (rewardQt > 0)
            //{
            //    textReward.text = string.Format("You got {0} {1}!", reward.reward, unit);
            //}
            //else
            //{
            //    textReward.text = string.Format("You got {0}!", unit);
            //}
            if (rewardQt > 0)
            {
                string finalText = LocalizationManager.GetTermTranslation("YOU GOT");
                finalText += " ";
                finalText += reward.reward.ToString();
                finalText += " ";
                finalText += LocalizationManager.GetTermTranslation(reward.unit.ToUpper());
                textReward.text = finalText.ToString();
            }
            else
            {
                string finalText = LocalizationManager.GetTermTranslation("YOU GOT");
                finalText += " ";
                finalText += LocalizationManager.GetTermTranslation(reward.unit.ToUpper());
                textReward.text = finalText.ToString();
            }
        }

        private void OnInitialize(bool error, string errorMessage)
        {
            if (!error)
            {
                var showWhenNotAvailable = dailyRewards.keepOpen;
                var isRewardAvailable = dailyRewards.availableReward > 0;

                UpdateUI();
                bool check = showWhenNotAvailable || (!showWhenNotAvailable && isRewardAvailable);
                canvas.gameObject.SetActive(check);
                if (check == true)
                {
                    MainMenuUI.Instance.TurnParticlesOff();
                }
                else
                {
                    MainMenuUI.Instance.TurnParticlesOn();
                }
                SnapToReward();
                CheckTimeDifference();

				StartCoroutine(TickTime());
            }
        }

		private IEnumerator TickTime() {
			for(;;){
				dailyRewards.TickTime();
				// Updates the time due
				CheckTimeDifference();
				yield return null;
			}
		}
    }
}