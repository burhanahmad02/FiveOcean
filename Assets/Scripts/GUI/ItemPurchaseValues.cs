using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPurchaseValues : MonoBehaviour
{
    public UnityEngine.UI.Text priceText;
    public int quantity;
    public int price;

    void Awake()
    {
        if (priceText)
        {
            priceText.text = price.ToString();
        }
        else
            Utility.ErrorLog("Price Text Funds Panel is not assigned in ItemPurchaseValues.cs of " + this.gameObject, 1);
    }
}
