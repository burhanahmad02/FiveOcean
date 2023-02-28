﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

// Deriving the Purchaser class from IStoreListener enables it to receive messages from Unity Purchasing.
public class Purchaser : MonoBehaviour, IStoreListener
{
    private static IStoreController m_StoreController;          // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

    // Product identifiers for all products capable of being purchased: 
    // "convenience" general identifiers for use with Purchasing, and their store-specific identifier 
    // counterparts for use with and outside of Unity Purchasing. Define store-specific identifiers 
    // also on each platform's publisher dashboard (iTunes Connect, Google Play Developer Console, etc.)

    // General product identifiers for the consumable, non-consumable, and subscription products.
    // Use these handles in the code to reference which product to purchase. Also use these values 
    // when defining the Product Identifiers on the store. Except, for illustration purposes, the 
    // kProductIDSubscription - it has custom Apple and Google identifiers. We declare their store-
    // specific mapping to Unity Purchasing's AddProduct, below.
    public static string FUNDS_PACK_1 = "com.studiogameever.zombiejustice_coins_pack1";
    public static string FUNDS_PACK_2 = "com.studiogameever.zombiejustice_coins_pack2";
    public static string FUNDS_PACK_3 = "com.studiogameever.zombiejustice_coins_pack3";
    public static string FUNDS_PACK_4 = "com.studiogameever.zombiejustice_coins_pack4";
    public static string ADS_DESTROY = "com.studiogameever.zombiejustice_destroy_ads";
    //public static string kProductIDSubscription = "subscription";

    // Apple App Store-specific product identifier for the subscription product.
    //private static string kProductNameAppleSubscription = "com.unity3d.subscription.new";

    // Google Play Store-specific product identifier subscription product.
    //private static string kProductNameGooglePlaySubscription = "com.unity3d.subscription.original";


    internal GameObject callBackGameobject = null;
    internal int fundsToAdd = 0;
    internal int gemsToAdd = 0;

    void Start()
    {
        // If we haven't set up the Unity Purchasing reference
        if (m_StoreController == null)
        {
            // Begin to configure our connection to Purchasing
            InitializePurchasing();
        }
    }

    public void InitializePurchasing()
    {
        // If we have already connected to Purchasing ...
        if (IsInitialized())
        {
            // ... we are done here.
            return;
        }

        // Create a builder, first passing in a suite of Unity provided stores.
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        // Add a product to sell / restore by way of its identifier, associating the general identifier
        // with its store-specific identifiers.
        builder.AddProduct(FUNDS_PACK_1, ProductType.Consumable);
        builder.AddProduct(FUNDS_PACK_2, ProductType.Consumable);
        builder.AddProduct(FUNDS_PACK_3, ProductType.Consumable);
        builder.AddProduct(FUNDS_PACK_4, ProductType.Consumable);
        // Continue adding the non-consumable product.
        builder.AddProduct(ADS_DESTROY, ProductType.NonConsumable);
        // And finish adding the subscription product. Notice this uses store-specific IDs, illustrating
        // if the Product ID was configured differently between Apple and Google stores. Also note that
        // one uses the general kProductIDSubscription handle inside the game - the store-specific IDs 
        // must only be referenced here. 
        //builder.AddProduct(kProductIDSubscription, ProductType.Subscription, new IDs(){
        //        { kProductNameAppleSubscription, AppleAppStore.Name },
        //        { kProductNameGooglePlaySubscription, GooglePlay.Name },
        //    });

        // Kick off the remainder of the set-up with an asynchrounous call, passing the configuration 
        // and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
        UnityPurchasing.Initialize(this, builder);
    }


    private bool IsInitialized()
    {
        // Only say we are initialized if both the Purchasing references are set.
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }
    public void AssignGameobject(GameObject callbackObj)
    {
        callBackGameobject = callbackObj;
    }
    public void AssignFunds(int funds)
    {
        fundsToAdd = funds;
    }

    public void AssignGems(int gems)
    {
        gemsToAdd = gems;
    }

    public void BuyFundsPack(int index)
    {
        switch(index)
        {
            case 0:
                BuyPack1();
                break;
            case 1:
                BuyPack2();
                break;
            case 2:
                BuyPack3();
                break;
            case 3:
                BuyPack4();
                break;
        }
    }

    public void BuySpecialMegaPack()
    {
        //fundsToAdd = 100000;
        //gemsToAdd = 70000;
        //BuySpecialPackMegaPack();

    }
    void FundsPurchaseCompleted()
    {
        int totalFunds = EncryptedPlayerPrefs.GetInt("Funds");
        totalFunds += fundsToAdd;
        EncryptedPlayerPrefs.SetInt("Funds", totalFunds);
        fundsToAdd = 0;

        DisplayItemValues[] items = GameObject.FindObjectsOfType<DisplayItemValues>() as DisplayItemValues[];
        foreach (var item in items)
        {
            item.ShowCount();
        }
        if (callBackGameobject)
        {
            if (callBackGameobject.GetComponent<InsufficientFundsManager>())
            {
                callBackGameobject.GetComponent<InsufficientFundsManager>().CloseInappPanel();
            }
            else
                Utility.ErrorLog("Purchaser is not found in InsufficientCurrencyManager.cs " + " of " + this.gameObject.name, 2);
        }
        //else
        //    Utility.ErrorLog("Insufficient Funds Manager Panel is not assigned in Purchaser.cs " + " of " + this.gameObject.name, 1);
    }

    void GemsPurchaseCompleted()
    {
        int totalGems = EncryptedPlayerPrefs.GetInt("Gems");
        totalGems += gemsToAdd;
        EncryptedPlayerPrefs.SetInt("Gems", totalGems);
        gemsToAdd = 0;

        DisplayItemValues[] items = GameObject.FindObjectsOfType<DisplayItemValues>() as DisplayItemValues[];
        foreach (var item in items)
        {
            item.ShowCount();
        }
        if (callBackGameobject)
        {
            if (callBackGameobject.GetComponent<InsufficientFundsManager>())
            {
                callBackGameobject.GetComponent<InsufficientFundsManager>().CloseInappPanel();
            }
            else
                Utility.ErrorLog("Purchaser is not found in InsufficientCurrencyManager.cs " + " of " + this.gameObject.name, 2);
        }
        //else
        //    Utility.ErrorLog("Insufficient Funds Manager Panel is not assigned in Purchaser.cs " + " of " + this.gameObject.name, 1);
    }
    void SpecialPackPurchaseCompleted()
    {
        int totalGems = EncryptedPlayerPrefs.GetInt("Gems");
        totalGems += gemsToAdd;
        EncryptedPlayerPrefs.SetInt("Gems", totalGems);
        gemsToAdd = 0;
        int totalFunds = EncryptedPlayerPrefs.GetInt("Funds");
        totalFunds += fundsToAdd;
        EncryptedPlayerPrefs.SetInt("Funds", totalFunds);
        fundsToAdd = 0;

        DisplayItemValues[] items = GameObject.FindObjectsOfType<DisplayItemValues>() as DisplayItemValues[];
        foreach (var item in items)
        {
            item.ShowCount();
        }
        if (callBackGameobject)
        {
            if (callBackGameobject.GetComponent<InsufficientFundsManager>())
            {
                callBackGameobject.GetComponent<InsufficientFundsManager>().CloseInappPanel();
            }
            else
                Utility.ErrorLog("Purchaser is not found in InsufficientCurrencyManager.cs " + " of " + this.gameObject.name, 2);
        }
        //else
        //    Utility.ErrorLog("Insufficient Funds Manager Panel is not assigned in Purchaser.cs " + " of " + this.gameObject.name, 1);
    }

    void RemoveAdsPurchaseCompleted()
    {
        Advertisements.Instance.RemoveAds(true);

        CanBeActivated[] activators = GameObject.FindObjectsOfType<CanBeActivated>() as CanBeActivated[];
        foreach (var item in activators)
        {
            item.RemoveAllAds();
        }
        if (callBackGameobject)
        {
            if (callBackGameobject.GetComponent<InsufficientFundsManager>())
            {
                callBackGameobject.GetComponent<InsufficientFundsManager>().CloseInappPanel();
            }
            else
                Utility.ErrorLog("Purchaser is not found in InsufficientCurrencyManager.cs " + " of " + this.gameObject.name, 2);
        }
        else
            Utility.ErrorLog("Insufficient Funds Manager Panel is not assigned in Purchaser.cs " + " of " + this.gameObject.name, 1);
    }
    public void BuyPack1()
    {
        // Buy the consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        BuyProductID(FUNDS_PACK_1);
    }
    public void BuyPack2()
    {
        // Buy the consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        BuyProductID(FUNDS_PACK_2);
    }
    public void BuyPack3()
    {
        // Buy the consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        BuyProductID(FUNDS_PACK_3);
    }
    public void BuyPack4()
    {
        // Buy the consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        BuyProductID(FUNDS_PACK_4);
    }
    
    public void BuyDestroyAds()
    {
        // Buy the non-consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        BuyProductID(ADS_DESTROY);
    }


    //public void BuySubscription()
    //{
    //    // Buy the subscription product using its the general identifier. Expect a response either 
    //    // through ProcessPurchase or OnPurchaseFailed asynchronously.
    //    // Notice how we use the general product identifier in spite of this ID being mapped to
    //    // custom store-specific identifiers above.
    //    BuyProductID(kProductIDSubscription);
    //}


    void BuyProductID(string productId)
    {
        // If Purchasing has been initialized ...
        if (IsInitialized())
        {
            // ... look up the Product reference with the general product identifier and the Purchasing 
            // system's products collection.
            Product product = m_StoreController.products.WithID(productId);

            // If the look up found a product for this device's store and that product is ready to be sold ... 
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                // asynchronously.
                m_StoreController.InitiatePurchase(product);
            }
            // Otherwise ...
            else
            {
                // ... report the product look-up failure situation  
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        // Otherwise ...
        else
        {
            // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
            // retrying initiailization.
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }


    // Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google. 
    // Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
    public void RestorePurchases()
    {
        // If Purchasing has not yet been set up ...
        if (!IsInitialized())
        {
            // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        // If we are running on an Apple device ... 
        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            // ... begin restoring purchases
            Debug.Log("RestorePurchases started ...");

            // Fetch the Apple store-specific subsystem.
            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            // Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
            // the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
            apple.RestoreTransactions((result) => {
                // The first phase of restoration. If no more responses are received on ProcessPurchase then 
                // no purchases are available to be restored.
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }
        // Otherwise ...
        else
        {
            // We are not running on an Apple device. No work is necessary to restore purchases.
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }


    //  
    // --- IStoreListener
    //

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        // Purchasing has succeeded initializing. Collect our Purchasing references.
        Debug.Log("OnInitialized: PASS");

        // Overall Purchasing system, configured with products for this application.
        m_StoreController = controller;
        // Store specific subsystem, for accessing device-specific store features.
        m_StoreExtensionProvider = extensions;
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }


    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        // A consumable product has been purchased by this user.
        if (String.Equals(args.purchasedProduct.definition.id, FUNDS_PACK_1, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            FundsPurchaseCompleted();
            // The consumable item has been successfully purchased.
        }
        if (String.Equals(args.purchasedProduct.definition.id, FUNDS_PACK_2, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            FundsPurchaseCompleted();
            // The consumable item has been successfully purchased.
        }
        if (String.Equals(args.purchasedProduct.definition.id, FUNDS_PACK_3, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            FundsPurchaseCompleted();
            // The consumable item has been successfully purchased.
        }
        if (String.Equals(args.purchasedProduct.definition.id, FUNDS_PACK_4, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            FundsPurchaseCompleted();
            // The consumable item has been successfully purchased.
        }
        
        // Or ... a non-consumable product has been purchased by this user.
        else if (String.Equals(args.purchasedProduct.definition.id, ADS_DESTROY, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            RemoveAdsPurchaseCompleted();
            // TODO: The non-consumable item has been successfully purchased.
        }
        // Or ... a subscription product has been purchased by this user.
        //else if (String.Equals(args.purchasedProduct.definition.id, kProductIDSubscription, StringComparison.Ordinal))
        //{
        //    Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
        //    // TODO: The subscription item has been successfully purchased, grant this to the player.
        //}
        // Or ... an unknown product has been purchased by this user. Fill in additional products here....
        else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
        }

        // Return a flag indicating whether this product has completely been received, or if the application needs 
        // to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
        // saving purchased products to the cloud, and when that save is delayed. 
        return PurchaseProcessingResult.Complete;
    }


    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
        // this reason with the user to guide their troubleshooting actions.
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
}