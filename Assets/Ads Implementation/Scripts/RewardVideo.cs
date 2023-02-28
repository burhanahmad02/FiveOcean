using UnityEngine;
using UnityEngine.UI;

public class RewardVideo : MonoBehaviour
{
    public enum RewardType
    {
        doubleReward,
        fixedReward,
        randomReward,
        reviveReward,
        freeSpinReward
    }
    public RewardType rewardType;

    public int totalReward;
    public int minimumRandomReward;
    public int maximumRandomReward;

    public Object rewardToAssign;
    public Object rewardPanel;
    public Object rewardNotAvailable;
    public Object rewardLost;
    // Use this for initialization

    public static int pressed = 0;

    void OnEnable()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            this.gameObject.SetActive(false);
            return;
        }
        if (AdManager.Instance)
        {
            if (!AdManager.Instance.IsRewardVideoAvailable())
            {
                this.gameObject.SetActive(false);
                return;
            }
        }
        else
        {
            this.gameObject.SetActive(false);
            return;
        }
        if (rewardType == RewardType.reviveReward)
        {
            if (pressed == 2)
            {
                pressed = 0;
                this.gameObject.SetActive(false);
                return;
            }
        }
    }
    public void RewardPressed()
    {
        if (AdManager.Instance)
        {
            if (!AdManager.Instance.IsRewardVideoAvailable())
            {
                GameObject noRewardAvailibility = (GameObject)rewardNotAvailable;
                noRewardAvailibility.SetActive(true);
            }
            else
            {
                AdManager.Instance.ShowRewardAd(RewardVideoResult);
            }
        }

        if (rewardType == RewardType.doubleReward || rewardType == RewardType.reviveReward || rewardType == RewardType.randomReward || rewardType == RewardType.fixedReward)
        {
            if (rewardType == RewardType.reviveReward)
            {
                pressed++;
            }
            this.gameObject.SetActive(false);
        }
        else if (rewardType == RewardType.freeSpinReward)
        {
            MainMenuUI.Instance.fortuneWheelPanel.GetComponent<FortuneWheelManager>().freeVideo.gameObject.SetActive(false);
            MainMenuUI.Instance.fortuneWheelPanel.GetComponent<FortuneWheelManager>().TurnButton.gameObject.SetActive(true);
        }
    }
    public void RewardVideoResult(bool status)
    {
        if (rewardType == RewardType.reviveReward)
        {
            if (!status)
            {
                IngameUI.Instance.FailOperations();
            }
            else
            {
                IngameUI.Instance.RevivePlayer();
            }
        }
        if (status)
        {
            if (rewardType == RewardType.doubleReward)
            {
                if (IngameUI.Instance)
                {
                    IngameUI.Instance.DoubleTheCoins();
                }
            }
            else if (rewardType == RewardType.freeSpinReward)
            {
                if (MainMenuUI.Instance)
                {
                    MainMenuUI.Instance.fortuneWheelPanel.GetComponent<FortuneWheelManager>().isFreeSpin = true;
                    MainMenuUI.Instance.fortuneWheelPanel.GetComponent<FortuneWheelManager>().TurnWheel();
                }
            }
            //if (rewardType == RewardType.reviveReward)
            //{
            //    if (MainMenuUI.Instance)
            //    {
            //        IngameUI.Instance.RevivePlayer();
            //    }
            //}
            else if (rewardType == RewardType.fixedReward)
            {
                //if (isGems)
                //{
                //    int totalFunds = EncryptedPlayerPrefs.GetInt("Gems", 0);
                //    totalFunds = totalFunds + totalReward;
                //    EncryptedPlayerPrefs.SetInt("Gems", totalFunds);
                //}
                //else
                {
                    int totalFunds = EncryptedPlayerPrefs.GetInt("Funds", 0);
                    totalFunds = totalFunds + totalReward;
                    EncryptedPlayerPrefs.SetInt("Funds", totalFunds);
                }

                if (rewardToAssign)
                {
                    Text text = (Text)rewardToAssign;
                    text.text = totalReward.ToString();
                }
                else
                    Utility.ErrorLog("rewardToAssign is not assigned in RewardVideo of " + this.gameObject.name, 1);
                
                if (rewardPanel)
                {
                    GameObject panel = (GameObject)rewardPanel;
                    panel.SetActive(true);
                }
                else
                    Utility.ErrorLog("rewardPanel is not assigned in RewardVideo of " + this.gameObject.name, 1);

                
            }
            else if (rewardType == RewardType.randomReward)
            {
                int rewardResult = Random.Range(minimumRandomReward, maximumRandomReward);
                //if (isGems)
                //{
                //    int totalFunds = EncryptedPlayerPrefs.GetInt("Gems", 0);
                //    totalFunds = totalFunds + rewardResult;
                //    EncryptedPlayerPrefs.SetInt("Gems", totalFunds);
                //}
                //else
                {

                    int totalFunds = EncryptedPlayerPrefs.GetInt("Funds", 0);
                    totalFunds = totalFunds + rewardResult;
                    EncryptedPlayerPrefs.SetInt("Funds", totalFunds);
                }
                if (rewardToAssign)
                {
                    Text text = (Text)rewardToAssign;
                    text.text = rewardResult.ToString();
                }
                else
                    Utility.ErrorLog("rewardToAssign is not assigned in RewardVideo of " + this.gameObject.name, 1);

                if (rewardPanel)
                {
                    GameObject panel = (GameObject)rewardPanel;
                    panel.SetActive(true);
                }
                else
                    Utility.ErrorLog("rewardPanel is not assigned in RewardVideo of " + this.gameObject.name, 1);
            }
        }
        else
        {
            if (rewardLost)
            {
                GameObject panel = (GameObject)rewardLost;
                panel.SetActive(true);
            }
        }

        Utility.ShowHeaderValues();
    }
    public void RewardPanelClose()
    {
        if (rewardPanel)
        {
            GameObject panel = (GameObject)rewardPanel;
            panel.SetActive(false);
            Utility.MakeClickSound();
        }
        else
            Utility.ErrorLog("rewardPanel is not assigned in RewardVideo of " + this.gameObject.name, 1);
    }
    public void RewardNotAvailablePanelClose()
    {
        if (rewardPanel)
        {
            GameObject panel = (GameObject)rewardNotAvailable;
            panel.SetActive(false);
        }
        else
            Utility.ErrorLog("rewardNotAvailable is not assigned in RewardVideo of " + this.gameObject.name, 1);
    }
    public void RewardLostPanelClose()
    {
        if (rewardPanel)
        {
            GameObject panel = (GameObject)rewardLost;
            panel.SetActive(false);
        }
        else
            Utility.ErrorLog("rewardLost is not assigned in RewardVideo of " + this.gameObject.name, 1);
    }
}
