using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadoutManager : MonoBehaviour {

    [Header("Loadout Types")]
    public Loadouts[] loadouts;

    [Header("Loadout Attachment EncryptedPlayerPrefs")]
    public string[] attachementPrefKeys;
    
    private int loadoutIndex = 1;
    
    private void OnEnable()
    {
        OpenLoadout(loadoutIndex);
    }
    public void OpenLoadout(int index)
    {
        foreach (var item in loadouts)
        {
            if (item)
            {
                item.gameObject.SetActive(false);
            }
            else
                Utility.ErrorLog("Loadouts Objects of " + this.gameObject.name + " in LoadoutManager.cs is not assigned", 1);
        }

        if (index < loadouts.Length)
        {
            if (loadouts[index])
            {
                loadoutIndex = index;
                loadouts[loadoutIndex].gameObject.SetActive(true);

                foreach (var item in loadouts[loadoutIndex].weapon)
                {
                    if (item)
                    {
                        item.gameObject.SetActive(false);
                    }
                    else
                        Utility.ErrorLog("Weapons Objects of " + loadouts[loadoutIndex].gameObject.name + " in Loadouts.cs is not assigned", 1);
                }

                int indexEquipped = EncryptedPlayerPrefs.GetInt("Loadout" + index + "Equipped");
                loadouts[loadoutIndex].currentIndex = indexEquipped;

                if (loadouts[loadoutIndex].weapon[indexEquipped].gameObject)
                {
                    loadouts[loadoutIndex].weapon[indexEquipped].gameObject.SetActive(true);
                }
                else
                    Utility.ErrorLog("Weapons Object of " + loadouts[loadoutIndex].gameObject.name + " at index " + indexEquipped + " in Loadouts.cs is not assigned", 1);

            }
            else
                Utility.ErrorLog("Loadouts Object of " + this.gameObject.name + " at index " + index + " in LoadoutManager.cs is not assigned", 1);
        }
        else
            Utility.ErrorLog("Loadout Objects array is out of bound in LoadoutManager.cs", 4);
        
        Utility.MakeClickSound();
    }
    public void NextWeapon (Loadouts loadout)
    {
        if(loadout)
        {
            loadout.currentIndex++;
            if (loadout.currentIndex >= loadout.weapon.Length)
            {
                loadout.currentIndex = 1;
            }
            foreach (var item in loadout.weapon)
            {
                if (item)
                {
                    item.gameObject.SetActive(false);
                }
                else
                    Utility.ErrorLog("Weapons Objects of " + loadout.gameObject.name + " in Loadouts.cs is not assigned", 1);
            }
            if (loadout.weapon[loadout.currentIndex])
            {
                loadout.weapon[loadout.currentIndex].SetActive(true);
            }
            else
                Utility.ErrorLog("Weapons Object of " + loadout.gameObject.name + " at index " + loadout.currentIndex + " in Loadouts.cs is not assigned", 1);
        }
        else
            Utility.ErrorLog("Loadout Object in parameter of NextWeapon() in LoadoutManager.cs is not assigned", 1);

        Utility.MakeClickSound();
    }
    public void PreviousWeapon(Loadouts loadout)
    {
        if (loadout)
        {
            loadout.currentIndex--;
            if (loadout.currentIndex <= 0)
            {
                loadout.currentIndex = loadout.weapon.Length - 1;
            }
            foreach (var item in loadout.weapon)
            {
                if (item)
                {
                    item.gameObject.SetActive(false);
                }
                else
                    Utility.ErrorLog("Weapons Objects of " + loadout.gameObject.name + " in Loadouts.cs is not assigned", 1);
            }
            if (loadout.weapon[loadout.currentIndex])
            {
                loadout.weapon[loadout.currentIndex].SetActive(true);
            }
            else
                Utility.ErrorLog("Weapons Object of " + loadout.gameObject.name + " at index " + loadout.currentIndex + " in Loadouts.cs is not assigned", 1);
        }
        else
            Utility.ErrorLog("Loadout Object in parameter of PreviousWeapon() in LoadoutManager.cs is not assigned", 1);

        Utility.MakeClickSound();
    }
    public void BuyLoadout(Text price)
    {
        if (price)
        {
            int loadoutPrice = int.Parse(price.text);
            
            int totalFunds = EncryptedPlayerPrefs.GetInt("Funds");
            
            if (loadoutPrice <= totalFunds)
            {
                LoadoutLockAndEquipStatus loadoutStatus = price.gameObject.GetComponentInParent<LoadoutLockAndEquipStatus>() as LoadoutLockAndEquipStatus;
                if (loadoutStatus)
                {
                    if (loadoutStatus.prefKey != null)
                    {
                        EncryptedPlayerPrefs.SetInt(loadoutStatus.prefKey, 1);
                        EncryptedPlayerPrefs.SetInt(loadoutStatus.prefEquip, loadoutStatus.weaponNumber);
                        if (loadoutStatus.gameObject.GetComponentInParent<Loadouts>())
                        {
                            OnEnable();
                        }
                        else
                            Utility.ErrorLog("Loadout Component not found in parent of " + loadoutStatus.gameObject.name, 2);

                        int finalFunds = totalFunds - loadoutPrice;
                        EncryptedPlayerPrefs.SetInt("Funds", finalFunds);
                        Utility.ShowHeaderValues();
                    }
                }
                else
                    Utility.ErrorLog("Loadout Locked Status Component not found in parent of " + price.gameObject.name, 2);
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
            Utility.ErrorLog("Price Text in parameter of BuyLoadout() in LoadoutManager.cs is not assigned", 1);
        
    }
    public void EquipWeapon(LoadoutLockAndEquipStatus loadoutStatus)
    {
        if (loadoutStatus)
        {
            if (EncryptedPlayerPrefs.GetInt(loadoutStatus.prefKey) == 1)
            {
                EncryptedPlayerPrefs.SetInt(loadoutStatus.prefEquip, loadoutStatus.weaponNumber);

                if (loadoutStatus.gameObject.GetComponentInParent<Loadouts>())
                {
                    OnEnable();
                }
                else
                    Utility.ErrorLog("Loadout Component not found in parent of " + loadoutStatus.gameObject.name, 2);
            }
        }
        else
            Utility.ErrorLog("Loadout Status Object in parameter of EquipWeapon() in LoadoutManager.cs is not assigned", 1);

    }
}
