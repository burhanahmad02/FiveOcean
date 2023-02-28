using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DisplayItemValues : MonoBehaviour
{
    public enum Items
    {
        Funds,
        gems,
        Hearts,
        airstrike,
        shield,
        Lightning,
        HealthKit,
        points,
        PlayerName,
        LevelNumber,
        SelectedRover,
        repairKit,
        PrizeCards,
        BTC,
        ETH,
        BNB,
        USDT,
        XRP,
        DOGE,
        LevelNumberSurvival
    }

    [Header("Select Item")]
    public Items item;
    [Header("Item Text")]
    public Text itemText;

    public GameObject[] SubmarineSprites;

    void OnEnable()
    {
        ShowCount();
        ShowCryptoValues();
    }
    public void ShowCount()
    {
        if (itemText)
        {
            if (item == Items.Funds)
            {
                itemText.text = PlayerPrefs.GetInt("Funds").ToString();
            }
            else
            if (item == Items.points)
            {
                itemText.text = PlayerPrefs.GetInt("FiveOceanHighScore").ToString();
            }
            else
            if (item == Items.airstrike)
            {
                itemText.text = EncryptedPlayerPrefs.GetInt("air_strike_count").ToString();
            }
            else
            if (item == Items.shield)
            {
                itemText.text = EncryptedPlayerPrefs.GetInt("shield_pack_count").ToString();
            }
            else
            if (item == Items.Lightning)
            {
                itemText.text = EncryptedPlayerPrefs.GetInt("Lightning_Count").ToString();
            }
            else
            if (item == Items.HealthKit)
            {
                itemText.text = EncryptedPlayerPrefs.GetInt("HealthKit").ToString();
            }
            else
            if (item == Items.PlayerName)
            {
                itemText.text = EncryptedPlayerPrefs.GetString("PlayerName").ToString();
            }
            else
            if (item == Items.Hearts)
            {
                itemText.text = EncryptedPlayerPrefs.GetInt("Hearts").ToString();
            }
            else
            if (item == Items.LevelNumber)
            {
                itemText.text = EncryptedPlayerPrefs.GetInt("LevelsUnocked").ToString();
            }
            else
            if (item == Items.repairKit)
            {
                itemText.text = EncryptedPlayerPrefs.GetInt("Hearts").ToString();
            }
            else
            if (item == Items.PrizeCards)
            {
                itemText.text = EncryptedPlayerPrefs.GetInt("PrizeCards").ToString();
            }
            else
            if (item == Items.LevelNumberSurvival)
            {
                itemText.text = EncryptedPlayerPrefs.GetInt("LevelOfSurvivalLevel").ToString();
            }
            else
            {
                //Debug.Log("select your type" + this.gameObject.name);
            }
        }
        else
        {
            if (item == Items.SelectedRover)
            {
                foreach (var item in SubmarineSprites)
                {
                    if (item)
                        item.SetActive(false);
                }

                if (SubmarineSprites[PlayerPrefs.GetInt("Submarine")])
                    SubmarineSprites[MainMenuUI.submarine_number].SetActive(true);
            }


            if (this.gameObject.GetComponent<UnityEngine.UI.Text>())
            {
                itemText = this.gameObject.GetComponent<UnityEngine.UI.Text>();
                ShowCount();
            }
            else
                MainMenuUI.ErrorLog("Text is not assigned of " + this.gameObject.name + " in DisplayItemValues.cs", 1);
        }

    }
    public void ShowCryptoValues()
    {
        if (!itemText)
        {
            if (this.gameObject.GetComponent<UnityEngine.UI.Text>())
            {
                itemText = this.gameObject.GetComponent<UnityEngine.UI.Text>();
                ShowCount();
            }
        }

        if (item == Items.BTC)
        {
            itemText.text = EncryptedPlayerPrefs.GetInt("BTCCollected").ToString();
        }
        else
        if (item == Items.ETH)
        {
            itemText.text = EncryptedPlayerPrefs.GetInt("ETHCollected").ToString();
        }
        else
        if (item == Items.BNB)
        {
            itemText.text = EncryptedPlayerPrefs.GetInt("BNBCollected").ToString();
        }
        else
        if (item == Items.USDT)
        {
            itemText.text = EncryptedPlayerPrefs.GetInt("USDTCollected").ToString();
        }
        else
        if (item == Items.XRP)
        {
            itemText.text = EncryptedPlayerPrefs.GetInt("XRPCollected").ToString();
        }
        else
        if (item == Items.DOGE)
        {
            itemText.text = EncryptedPlayerPrefs.GetInt("DOGECollected").ToString();
        }
    }
}
