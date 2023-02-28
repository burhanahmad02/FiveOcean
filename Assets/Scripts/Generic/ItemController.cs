using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public static ItemController Instance;

    private UIScript ingameUiScript;
    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }
    void Start()
    {
        if (UIScript.Instance)
        {
            ingameUiScript = UIScript.Instance;
            //ingameUiScript.UpdateItemValuesCount();
        }
        else
            Utility.ErrorLog("ingame Ui Script could not be found in ItemController.cs of " + this.gameObject, 1);
    }
    public void UseLineBomb()
    {
        //int quantity = EncryptedPlayerPrefs.GetInt("LineBomb");
        //quantity--;
        //EncryptedPlayerPrefs.SetInt("LineBomb", quantity);
        //HeadSpawner.Instance.UseLineBomb();
        //ingameUiScript.UpdateItemValuesCount();
    }
    public void UseRadiusBomb()
    {
        //int quantity = EncryptedPlayerPrefs.GetInt("RadiusBomb");
        //quantity--;
        //EncryptedPlayerPrefs.SetInt("RadiusBomb", quantity);
        //HeadSpawner.Instance.UseRadiusBomb();
        //ingameUiScript.UpdateItemValuesCount();
    }
    public void UseTimeBomb()
    {
        //int quantity = EncryptedPlayerPrefs.GetInt("TimeBomb");
        //quantity--;
        //if (quantity < 0)
        //    quantity = 0;
        //EncryptedPlayerPrefs.SetInt("TimeBomb", quantity);
        //HeadSpawner.Instance.UseTimeBomb();
        //ingameUiScript.UpdateItemValuesCount();
    }
}
