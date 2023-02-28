/***************************************************************************\
Project:      Daily Rewards
Copyright (c) Niobium Studios.
Author:       Guilherme Nunes Barbosa (gnunesb@gmail.com)
\***************************************************************************/
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using I2.Loc;

/* 
 * Timed Rewards Canvas is the User interface to show Timed rewards
 */
namespace NiobiumStudios
{
    public class TimedRewardsInterface : MonoBehaviour
    {
        //public GameObject canvas;

        [Header("Panel Debug")]
        public bool isDebug;
        public GameObject panelDebug;
        public Button buttonReset;
        public Button buttonReloadScene;

        [Header("Panel Reward")]
        public Button buttonClaim;                  // The button to claim the reward
        public Text textInfo;                       // Informative text about the reward
        public GameObject boxAnim;
        public GameObject rewardPanelAnim;
        public GameObject alertObj;

        [HideInInspector]
        public bool isRewardAvailable = false;

        [Header("Panel Reward Message")]
        public GameObject panelReward;              // Rewards panel
        public int rewardID;
        public Text textReward;                     // Reward Text to show an explanatory message to the player
        public Button buttonCloseReward;            // The Button to close the Rewards Panel
        public Image imageRewardMessage;            // The Image of the reward

        [Header("Panel Available Rewards")]
        public GameObject panelAvailableRewards;    // Available Rewards panel
        public GameObject rewardPrefab;             // The prefab that contains the reward
        public Button buttonCloseAvailable;         // The Button to close the Available rewards Panel
        public GridLayoutGroup rewardsGroup;        // The Grid that contains the rewards
        public ScrollRect scrollRect;               // The Scroll Rect

        private TimedRewards timedRewards;          // TimedReward Instance

        private List<TimedRewardUI> rewardsUI = new List<TimedRewardUI>();

        void Awake()
        {
            //canvas.SetActive(false);

            if (!isDebug)
                panelDebug.SetActive(false);

            timedRewards = GetComponent<TimedRewards>();
            alertObj.SetActive(false);
            //boxAnim.GetComponent<Animator>().SetBool("active", true);
        }

        void Start()
        {
            InitializeAvailableRewardsUI();
            buttonClaim.interactable = false;
            panelAvailableRewards.SetActive(false);

            buttonCloseAvailable.onClick.AddListener(() =>
            {
                //boxAnim.GetComponent<Animator>().SetBool("active", true);
                panelAvailableRewards.SetActive(false);
                buttonClaim.interactable = true;
                alertObj.SetActive(true);
            });

            buttonClaim.onClick.AddListener(() =>
            {
                isRewardAvailable = false;
                buttonClaim.interactable = false;
                alertObj.SetActive(false);
                boxAnim.GetComponent<Animator>().SetBool("active", false);
                // On single rewards automatically claims
                if (timedRewards.rewards.Count == 1)
                {
                    ClaimReward(0);
                }
                else if (timedRewards.isRewardRandom)
                {
                    // If the reward is random, player claims a random reward from the list
                    ClaimReward(UnityEngine.Random.Range(0, timedRewards.rewards.Count));
                }
                else
                {
                    // On multiple rewards shows the Available Rewards panel
                    panelAvailableRewards.SetActive(true);
                }
            });

            buttonCloseReward.onClick.AddListener(() =>
            {
                rewardPanelAnim.GetComponent<RewardPanel>().CloseRewardPanel();
                panelAvailableRewards.SetActive(false);
                panelReward.SetActive(false);
            });

            // Resets timed rewards preferences. Debug Purposes
            if (buttonReset)
                buttonReset.onClick.AddListener(() =>
                {
                    timedRewards.Reset();
                    buttonClaim.interactable = true;
                    alertObj.SetActive(true);
                });

            // Reloads the same scene
            if (buttonReloadScene)
                buttonReloadScene.onClick.AddListener(() =>
                {
                    Application.LoadLevel(Application.loadedLevel);
                });
        }

        void OnEnable()
        {
            timedRewards.onCanClaim += OnCanClaim;
            timedRewards.onInitialize += OnInitialize;
        }

        void OnDisable()
        {
            if (timedRewards != null)
            {
                timedRewards.onCanClaim -= OnCanClaim;
                timedRewards.onInitialize -= OnInitialize;
            }
        }

        private void UpdateTextInfo()
        {
            if (timedRewards.timer.TotalSeconds > 0)
                textInfo.text = timedRewards.GetFormattedTime();
        }

        // Initializes the UI List based on the rewards size
        private void InitializeAvailableRewardsUI()
        {
            // Initializes only when there is more than one Reward
            if (timedRewards.rewards.Count > 1)
            {
                for (int i = 0; i < timedRewards.rewards.Count; i++)
                {
                    var reward = timedRewards.GetReward(i);

                    GameObject dailyRewardGo = GameObject.Instantiate(rewardPrefab) as GameObject;

                    TimedRewardUI rewardUI = dailyRewardGo.GetComponent<TimedRewardUI>();

                    rewardUI.index = 0;

                    rewardUI.transform.SetParent(rewardsGroup.transform);
                    dailyRewardGo.transform.localScale = Vector2.one;

                    rewardUI.button.onClick.AddListener(OnClickReward(i));

                    rewardUI.reward = reward;
                    rewardUI.Initialize();

                    rewardsUI.Add(rewardUI);
                }
            }
        }

        // The action on the button when claiming the reward
        private UnityEngine.Events.UnityAction OnClickReward(int index)
        {
            return () =>
            {
                panelAvailableRewards.SetActive(false);
                ClaimReward(index);
            };
        }

        // Claimed the reward
        private void ClaimReward(int index)
        {
            alertObj.SetActive(false);
            boxAnim.GetComponent<Animator>().SetBool("active", false);
            rewardPanelAnim.GetComponent<RewardPanel>().ShowReward(rewardID);

            timedRewards.ClaimReward(index);


            panelReward.SetActive(true);
            

            var reward = timedRewards.GetReward(index);
            var unit = reward.unit;
            var rewardQt = reward.reward;

            if (reward.unit == "Coins")
            {
                int rewardValue = EncryptedPlayerPrefs.GetInt("Funds");
                rewardValue = rewardValue + reward.reward;
                EncryptedPlayerPrefs.SetInt("Funds", rewardValue);
            }
            else if (reward.unit == "Repair Kit")
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

            //GameManager.DisplayValuesOfItems();


            Utility.MakeClickSound();
            imageRewardMessage.sprite = reward.sprite;

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
            //if (rewardQt > 0)
            //    textReward.text = string.Format("You got {0} {1}!", reward.reward, unit);
            //else
            //    textReward.text = string.Format("You got {0}!", unit);
        }

        // Delegate
        // Updates the UI
        private void OnCanClaim()
        {
            boxAnim.GetComponent<Animator>().SetBool("active", true);
            isRewardAvailable = true;
            buttonClaim.interactable = true;
            alertObj.SetActive(true);
            textInfo.text = LocalizationManager.GetTermTranslation("REWARD READY!");
            alertObj.SetActive(true);
            if (!buttonClaim.gameObject.activeInHierarchy)
                buttonClaim.gameObject.SetActive(true);
        }

        private void OnInitialize(bool error, string errorMessage)
        {
            if (!error)
            {
                // canvas.gameObject.SetActive(true);
                StartCoroutine(TickTime());
            }
        }

        private IEnumerator TickTime()
        {
            for (; ; )
            {
                // Updates the timer UI
                timedRewards.TickTime();
                UpdateTextInfo();
                yield return null;
            }
        }
    }
}