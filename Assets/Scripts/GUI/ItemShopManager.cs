using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShopManager : MonoBehaviour
{
    [Header("Buttons Activators")]
    public GameObject[] panelButtonActivators;
    [Header("Actived Panels")]
    public GameObject[] panelToActivate;

    [HideInInspector]
    public GameObject previousPanelToOpen = null;

    private bool isGamePlay = false;

    void Awake()
    {
        if (IngameUI.Instance)
        {
            isGamePlay = true;
        }
    }
    void OnEnable()
    {
        if (isGamePlay)
        {
            Time.timeScale = 0f;
        }
    }
    void Start()
    {
        if (panelButtonActivators.Length > 1 && panelToActivate.Length > 1)
        {
            OpenPanelNumber(1);
        }
    }
    public void OpenPanelNumber(int number)
    {
        foreach (var item in panelButtonActivators)
        {
            if (item)
            {
                item.SetActive(false);
            }
            else
                Utility.ErrorLog("Panel Button Activators is not assigned in ItemShopManager.cs of " + this.gameObject, 1);
        }
        foreach (var item in panelToActivate)
        {
            if (item)
            {
                item.SetActive(false);
            }
            else
                Utility.ErrorLog("Panel To Activate is not assigned in ItemShopManager.cs of " + this.gameObject, 1);
        }
        if (number < panelButtonActivators.Length)
        {
            if (panelButtonActivators[number])
            {
                panelButtonActivators[number].SetActive(true);
            }
            else
                Utility.ErrorLog("Panel Button Activators is not assigned in ItemShopManager.cs of " + this.gameObject, 1);
        }
        else
            Utility.ErrorLog("Array out of bound of Panel Button Activators index in ItemShopManager.cs " + " of " + this.gameObject.name, 4);

        if (number < panelToActivate.Length)
        {
            if (panelToActivate[number])
            {
                panelToActivate[number].SetActive(true);
            }
            else
                Utility.ErrorLog("Panel To Activate is not assigned in ItemShopManager.cs of " + this.gameObject, 1);
        }
        else
            Utility.ErrorLog("Array out of bound of Panel To Activate index in ItemShopManager.cs " + " of " + this.gameObject.name, 4);

        Utility.MakeClickSound();
    }
    public void BackOfItemShop()
    {
        if (isGamePlay)
        {
            IngameUI.isPlaying = true;
            Time.timeScale = 1f;
        }
        if (previousPanelToOpen)
        {
            previousPanelToOpen.gameObject.SetActive(true);
            previousPanelToOpen = null;
        }
        this.gameObject.SetActive(false);
        //Utility.MakeClickSound();
        SoundManager.Instance.PlayUICloseSound();
    }

    public void BuyLineBomb(ItemPurchaseValues purchaser)
    {
        if (purchaser)
        {
            int quantity = purchaser.quantity;
            int price = purchaser.price;

            int totalFunds = EncryptedPlayerPrefs.GetInt("Funds");

            if (price <= totalFunds)
            {
                totalFunds -= price;
                EncryptedPlayerPrefs.SetInt("Funds", totalFunds);

                int totalItemValue = EncryptedPlayerPrefs.GetInt("LineBomb");
                totalItemValue += quantity;
                EncryptedPlayerPrefs.SetInt("LineBomb", totalItemValue);

                if (isGamePlay)
                {
                    //IngameUI.Instance.UpdateItemValuesCount();
                }
                Utility.ShowHeaderValues();
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
            Utility.ErrorLog("Purchaser Object in parameter of BuyHealthKit() in ItemShopManager.cs is not assigned", 1);

        Utility.MakeClickSound();
    }

    public void BuyRadiusBomb(ItemPurchaseValues purchaser)
    {
        if (purchaser)
        {
            int quantity = purchaser.quantity;
            int price = purchaser.price;

            int totalFunds = EncryptedPlayerPrefs.GetInt("Funds");

            if (price <= totalFunds)
            {
                totalFunds -= price;
                EncryptedPlayerPrefs.SetInt("Funds", totalFunds);

                int totalItemValue = EncryptedPlayerPrefs.GetInt("RadiusBomb");
                totalItemValue += quantity;
                EncryptedPlayerPrefs.SetInt("RadiusBomb", totalItemValue);

                if (isGamePlay)
                {
                    //IngameUI.Instance.UpdateItemValuesCount();
                }
                Utility.ShowHeaderValues();
            }
            else
            {
                if (MainMenuUI.Instance)
                {
                    if (MainMenuUI.Instance.InsufficientFundsPanel)
                    {
                        MainMenuUI.Instance.mainMenuPanel.SetActive(false);
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
            Utility.ErrorLog("Purchaser Object in parameter of BuyHealthKit() in ItemShopManager.cs is not assigned", 1);

        Utility.MakeClickSound();
    }

    public void BuyTimeBomb(ItemPurchaseValues purchaser)
    {
        if (purchaser)
        {
            int quantity = purchaser.quantity;
            int price = purchaser.price;

            int totalFunds = EncryptedPlayerPrefs.GetInt("Funds");

            if (price <= totalFunds)
            {
                totalFunds -= price;
                EncryptedPlayerPrefs.SetInt("Funds", totalFunds);

                int totalItemValue = EncryptedPlayerPrefs.GetInt("TimeBomb");
                totalItemValue += quantity;
                EncryptedPlayerPrefs.SetInt("TimeBomb", totalItemValue);

                if (isGamePlay)
                {
                    //IngameUI.Instance.UpdateItemValuesCount();
                }
                Utility.ShowHeaderValues();
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
            Utility.ErrorLog("Purchaser Object in parameter of BuyHealthKit() in ItemShopManager.cs is not assigned", 1);

        Utility.MakeClickSound();
    }
}
