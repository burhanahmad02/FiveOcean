using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InsufficientFundsManager : MonoBehaviour
{
    [SerializeField]
    private bool openDirectInappPanel = false;

    public GameObject managerPanel;
    public GameObject inappPanel;
    public GameObject mainMenuPanel;
    public GameObject SubmarineSelectionPanel;
    public bool cameFromMainMenu = false;
    public bool cameFromSubmarines = false;

    public string[] purchasePrices;
    public int[] coins;
    public Text[] coinsTexts;
    public Text[] priceTexts;

    private GameObject previousPanelToOpen = null;
    private bool openDirectInapp = false;
    public bool OpenDirectInapp { get => openDirectInapp; set => openDirectInapp = value; }
    private GameObject adManager;

    //public 
    void Awake()
    {
        if (AdManager.Instance)
        {
            adManager = AdManager.Instance.gameObject;

            if (adManager.GetComponent("Purchaser"))
            {
                adManager.SendMessage("AssignGameobject", this.gameObject);
            }
        }
        AssignStringsToTexts();
        if (openDirectInappPanel)
        {
            openDirectInapp = true;
        }
    }
    private void OnEnable()
    {
        if (openDirectInapp)
        {
            OpenInappPanel();
        }
        else
        {
            if (inappPanel)
                inappPanel.SetActive(false);
            else
                Utility.ErrorLog("Inapp Panel is not assigned in InsufficientCurrencyManager.cs " + " of " + this.gameObject.name, 1);
            if (managerPanel)
                managerPanel.SetActive(true);
            else
                Utility.ErrorLog("Manager Panel is not assigned in InsufficientCurrencyManager.cs " + " of " + this.gameObject.name, 1);
        }
    }
    void AssignStringsToTexts()
    {
        for (int i = 0; i < coinsTexts.Length; i++)
        {
            if (i < coins.Length)
            {
                coinsTexts[i].text = coins[i].ToString();
            }
            else
                Utility.ErrorLog("Array out of bound of coins in InsufficientCurrencyManager.cs " + " of " + this.gameObject.name, 4);
        }
        for (int i = 0; i < priceTexts.Length; i++)
        {
            if (i < purchasePrices.Length)
            {
                priceTexts[i].text = purchasePrices[i].ToString();
            }
            else
                Utility.ErrorLog("Array out of bound of prices in InsufficientCurrencyManager.cs " + " of " + this.gameObject.name, 4);
        }
    }

    public void BuyFunds(int pack)
    {
        if (adManager)
        {
            if (adManager.GetComponent("Purchaser"))
            {
                if (pack < coins.Length)
                {
                    adManager.SendMessage("AssignFunds", coins[pack]);
                    adManager.SendMessage("BuyFundsPack", pack);
                }
                else
                    Utility.ErrorLog("Array out of bound of pack index in InsufficientCurrencyManager.cs " + " of " + this.gameObject.name, 4);
            }
            else
                Utility.ErrorLog("Purchaser is not found in InsufficientCurrencyManager.cs " + " of " + this.gameObject.name, 2);
        }
        else
            Utility.ErrorLog("Ad Manager is not found in InsufficientCurrencyManager.cs " + " of " + this.gameObject.name, 1);

        Utility.MakeClickSound();
    }
    public void RemoveAds()
    {
        if (adManager)
        {
            if (adManager.GetComponent("Purchaser"))
            {
                adManager.GetComponent("Purchaser").SendMessage("BuyDestroyAds");
            }
            else
                Utility.ErrorLog("Purchaser is not found in InsufficientCurrencyManager.cs " + " of " + this.gameObject.name, 2);
        }
        else
            Utility.ErrorLog("Ad Manager is not found in InsufficientCurrencyManager.cs " + " of " + this.gameObject.name, 1);

        Utility.MakeClickSound();
    }
    public void CloseManager()
    {
        if (managerPanel)
            managerPanel.SetActive(false);
        else
            Utility.ErrorLog("Manager Panel is not assigned in InsufficientCurrencyManager.cs " + " of " + this.gameObject.name, 1);

        Utility.MakeClickSound();
    }
    public void OpenInappPanel()
    {
        CloseManager();
        if (inappPanel)
            inappPanel.SetActive(true);
        else
            Utility.ErrorLog("Inapp Panel is not assigned in InsufficientCurrencyManager.cs " + " of " + this.gameObject.name, 1);

        Utility.MakeClickSound();
    }
    public void CloseInappPanel()
    {
        if (cameFromMainMenu)
        {
            if (mainMenuPanel)
            {
                cameFromMainMenu = false;
                mainMenuPanel.SetActive(true);
            }
        }
        if (inappPanel)
            inappPanel.SetActive(false);
        else
            Utility.ErrorLog("Inapp Panel is not assigned in InsufficientCurrencyManager.cs " + " of " + this.gameObject.name, 1);
        if (managerPanel)
            managerPanel.SetActive(true);
        else
            Utility.ErrorLog("Manager Panel is not assigned in InsufficientCurrencyManager.cs " + " of " + this.gameObject.name, 1);
       

        if (previousPanelToOpen)
        {
            previousPanelToOpen.gameObject.SetActive(true);
            previousPanelToOpen = null;
        }

        if (!openDirectInappPanel)
        {
            openDirectInapp = false;
        }

        //Utility.MakeClickSound();

        SoundManager.Instance.PlayUICloseSound();
        this.gameObject.SetActive(false);

    }

}
