using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private GameManager gameManager { get { return Instance; } }

    private float _sensitivityValue = 0.5f;
    public float sensitivityValue { get { return _sensitivityValue; } set { _sensitivityValue = value; } }

    private float _soundValue = 0.5f;
    public float soundValue { get { return _soundValue; } set { _soundValue = value; } }

    private float _musicValue = 0.5f;
    public float musicValue { get { return _musicValue; } set { _musicValue = value; } }

    private int _moduleNumber = 0;
    public int moduleNumber { get { return _moduleNumber; } set { _moduleNumber = value; } }
    private int _roverNumber = 1;
    public int roverNumber { get { return _roverNumber; } set { _roverNumber = value; } }
    private int _levelNumber = 1;
    public int levelNumber { get { return _levelNumber; } set { _levelNumber = value; } }
    
    private int _levelsInAModule = 1;
    public int levelsInAModule { get { return _levelsInAModule; } set { _levelsInAModule = value; } }

    private bool _comingFromIngame = false;
    public bool comingFromIngame { get { return _comingFromIngame; } set { _comingFromIngame = value; } }

    private int _loadoutCount = 1;
    public int loadoutCount { get { return _loadoutCount; } set { _loadoutCount = value; } }

    private int _environmentNumber = 0;
    public int environmentNumber { get { return _environmentNumber; } set { _environmentNumber = value; } }

    private int _environmentType = -1;
    public int environmentType { get { return _environmentType; } set { _environmentType = value; } }

    private bool _gameOver;
    public bool gameOver { get { return _gameOver; } set { _gameOver = value; } }

    private float _vibrationValue = 1f;
    public float vibrationValue { get { return _vibrationValue; } set { _vibrationValue = value; } }

    private void OnEnable()
    {
        //levelNumber = 18;
    }

    public void GameWon()
    {
        if (IngameUI.Instance && !gameOver)
        {
            IngameUI.Instance.GameComplete(true);
        }
    }

    public void GameLost()
    {
        if (IngameUI.Instance)
        {
            IngameUI.Instance.GameComplete(false);
        }
    }

    public int GetLoadoutInfo (int i)
    {
        if (i == 0)
            i = 1;

        string _prefEquip = "Loadout" + i + "Equipped";
        //Debug.Log(_prefEquip + " = " + EncryptedPlayerPrefs.GetInt(_prefEquip));
        return EncryptedPlayerPrefs.GetInt(_prefEquip);
    }

    public int GetLoadoutPrefInfo(int loadout, int weapon, string prefKey)
    {
        if (loadout == 0)
            loadout = 1;
        if (weapon == 0)
            weapon = 1;

        string _prefEquip = "Loadout" + loadout + "Weapon" + weapon + prefKey;
        //Debug.Log(_prefEquip + " = " + EncryptedPlayerPrefs.GetInt(_prefEquip));
        return EncryptedPlayerPrefs.GetInt(_prefEquip);
    }
    public int GetRoverNumber()
    {
        roverNumber = EncryptedPlayerPrefs.GetInt("RoverSelected");
        return roverNumber;
    }
    /*    public void SetRoverNumber(int number)
        {
            EncryptedPlayerPrefs.SetInt("RoverSelected", number);
        }
        public static void DisplayValuesOfItems()
        {
            DisplayItemValues[] itemValues = FindObjectsOfType<DisplayItemValues>() as DisplayItemValues[];
            foreach (var item in itemValues)
            {
                item.ShowCount();
            }
        }*/
}