using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadoutAttachements : MonoBehaviour
{
    [Header("Attachement Assignments")]
    public UnityEngine.UI.Image fillBar;
    public UnityEngine.UI.Text upgradePrice;
    public GameObject upgradeButton;
    public GameObject trayButton;

    private LoadoutLockAndEquipStatus myLoadout;
    [Header("Attachement Pref Key Index in LoadoutManager.cs")]
    [SerializeField] private int myIndex;
    [Header("Attachement Pref Key Minimum And Maximum Values")]
    [SerializeField] private int myMinimumValue = 1;
    [SerializeField] private int myMaximumValue = 1;
    [Header("Attachement Pref Key Prices")]
    [SerializeField] private int [] prices = new int[10];
    void Awake ()
    {
        if (prices.Length < 10)
        {
            prices = new int[10];
        }

        for (int i = 0; i < prices.Length; i++)
        {
            if (prices[i] == 0)
            {
                prices[i] = (i + 1) * 100;
            }
        }
        if (myMinimumValue > myMaximumValue)
        {
            int temp = myMinimumValue;
            myMinimumValue = myMaximumValue;
            myMaximumValue = temp;
        }

        if (myMinimumValue < 1 || myMinimumValue > 10)
            myMinimumValue = 1;

        if (myMaximumValue < 1 || myMaximumValue > 10)
            myMaximumValue = 1;


        myLoadout = GetComponentInParent<LoadoutLockAndEquipStatus>() as LoadoutLockAndEquipStatus;

        if (myLoadout == null)
        {
            Utility.ErrorLog("LoadoutLockAndEquipStatus Component not found in parent of " + this.gameObject.name, 2);
        }
    }
    void OnEnable()
    {
        StartCoroutine(OnEnableFunctionalities());
    }
    IEnumerator OnEnableFunctionalities()
    {
        yield return null;
        if (myLoadout)
        {
            if (myLoadout.lockedPanel.gameObject.activeInHierarchy && !myLoadout.unlockedPanel.gameObject.activeInHierarchy)
            {
                if (upgradeButton)
                {
                    upgradeButton.SetActive(false);
                }
                else
                    Utility.ErrorLog("Upgrade Button of " + this.gameObject.name + " in LoadoutAttachements.cs is not assigned", 1);

                if (trayButton)
                {
                    trayButton.SetActive(false);
                }
                else
                    Utility.ErrorLog("Tray Button of " + this.gameObject.name + " in LoadoutAttachements.cs is not assigned", 1);
            }
            else if (myLoadout.unlockedPanel.gameObject.activeInHierarchy)
            {
                if (upgradeButton)
                {
                    if (!upgradeButton.gameObject.activeInHierarchy)
                        upgradeButton.SetActive(true);
                }
                else
                    Utility.ErrorLog("Upgrade Button of " + this.gameObject.name + " in LoadoutAttachements.cs is not assigned", 1);

                if (trayButton)
                {
                    if (!trayButton.gameObject.activeInHierarchy)
                        trayButton.SetActive(true);
                }
                else
                    Utility.ErrorLog("Tray Button of " + this.gameObject.name + " in LoadoutAttachements.cs is not assigned", 1);
            }
        }
        else
            Utility.ErrorLog("LoadoutLockAndEquipStatus Component not found in parent of " + this.gameObject.name, 2);
        FillBar();
    }
    void FillBar()
    {
        if (myLoadout)
        {
            if (myLoadout.prefKey != null)
            {
                LoadoutManager loadoutManager = myLoadout.gameObject.GetComponentInParent<LoadoutManager>() as LoadoutManager;

                if (loadoutManager)
                {
                    if (myIndex < loadoutManager.attachementPrefKeys.Length && myIndex >= 0)
                    {
                        int prefValue = EncryptedPlayerPrefs.GetInt((myLoadout.prefKey + loadoutManager.attachementPrefKeys[myIndex]));

                        if (prefValue == 0)
                        {
                            EncryptedPlayerPrefs.SetInt((myLoadout.prefKey + loadoutManager.attachementPrefKeys[myIndex]), myMinimumValue);
                            prefValue = myMinimumValue;
                        }

                        if (prefValue < myMinimumValue)
                        {
                            prefValue = myMinimumValue;
                            EncryptedPlayerPrefs.SetInt((myLoadout.prefKey + loadoutManager.attachementPrefKeys[myIndex]), prefValue);
                        }

                        if (prefValue >= myMaximumValue)
                        {
                            upgradeButton.SetActive(false);
                            upgradePrice.text = "MAXED OUT";
                        }
                        else
                        {
                            upgradePrice.text = prices[prefValue - 1].ToString();
                        }

                        if (fillBar)
                        {
                            fillBar.fillAmount = (float)prefValue / prices.Length;
                        }
                        else
                            Utility.ErrorLog("Fill Bar of " + this.gameObject.name + " in LoadoutAttachements.cs is not assigned", 1);
                    }
                    else
                        Utility.ErrorLog("My index of " + this.gameObject.name + " is out of bound of array of LoadoutManager.cs", 4);
                }
                else
                    Utility.ErrorLog("Loadout Manager Component not found in parent of " + this.gameObject.name, 2);
            }
            else
                Utility.ErrorLog("Loadout pref key is null of " + this.gameObject.name, 1);
        }
        else
            Utility.ErrorLog("LoadoutLockAndEquipStatus Component not found in parent of " + this.gameObject.name, 2);
    }
    public void UpgradeAttachment ()
    {
        
        if (myLoadout)
        {
            if (myLoadout.prefKey != null)
            {
                LoadoutManager loadoutManager = myLoadout.gameObject.GetComponentInParent<LoadoutManager>() as LoadoutManager;

                if (loadoutManager)
                {
                    if (myIndex < loadoutManager.attachementPrefKeys.Length && myIndex >= 0)
                    {
                        int prefValue = EncryptedPlayerPrefs.GetInt((myLoadout.prefKey + loadoutManager.attachementPrefKeys[myIndex]));

                        if ((prefValue - 1) >= 0 && (prefValue - 1) < prices.Length)
                        {

                            if (prices[prefValue - 1] <= EncryptedPlayerPrefs.GetInt("Funds"))
                            {
                                EncryptedPlayerPrefs.SetInt("Funds", (EncryptedPlayerPrefs.GetInt("Funds") - prices[prefValue - 1]));
                                Utility.ShowHeaderValues();
                                prefValue++;

                                EncryptedPlayerPrefs.SetInt((myLoadout.prefKey + loadoutManager.attachementPrefKeys[myIndex]), prefValue);
                                
                                if (prefValue >= myMaximumValue)
                                {
                                    upgradeButton.SetActive(false);
                                    upgradePrice.text = "MAXED OUT";
                                }
                                else
                                {
                                    upgradePrice.text = prices[prefValue - 1].ToString();
                                }

                                if (fillBar)
                                {
                                    fillBar.fillAmount = (float)prefValue / prices.Length;
                                }
                                else
                                    Utility.ErrorLog("Fill Bar of " + this.gameObject.name + " in LoadoutAttachements.cs is not assigned", 1);
                            }
                        }
                        else
                        {
                            if (MainMenuUI.Instance)
                            {
                                if (MainMenuUI.Instance.InsufficientFundsPanel)
                                {
                                    MainMenuUI.Instance.InsufficientFundsPanel.SetActive(true);
                                }
                                else
                                    Utility.ErrorLog("Insufficient Funds Panel is not assigned in MainMenuUI.cs", 1);
                            }
                            else if (IngameUI.Instance)
                            {
                                if (IngameUI.Instance.InsufficientFundsPanel)
                                {
                                    IngameUI.Instance.InsufficientFundsPanel.SetActive(true);
                                }
                                else
                                    Utility.ErrorLog("Insufficient Funds Panel is not assigned in IngameUI.cs", 1);
                            }
                        }
                    }
                    else
                        Utility.ErrorLog("My index of " + this.gameObject.name + " is out of bound of array of LoadoutManager.cs", 4);
                }
                else
                    Utility.ErrorLog("Loadout Manager Component not found in parent of " + this.gameObject.name, 2);
            }
        }
        else
            Utility.ErrorLog("LoadoutLockAndEquipStatus Component not found in parent of " + this.gameObject.name, 2);

        Utility.MakeClickSound();
    }
}
