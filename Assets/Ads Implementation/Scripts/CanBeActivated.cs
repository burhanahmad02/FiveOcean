using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanBeActivated : MonoBehaviour
{
    private enum Type
    {
        adGameobject,
        dateDependentadAdGameobject,
        removeAdsGameobject,
        otherServerDependentObject
    }

    public GameObject Tick;
    [SerializeField] private Type typeOfObject;

    private bool customEnableRun = false;

    private void OnEnable()
    {
        if (!customEnableRun)
        {
            customEnableRun = true;
            return;
        }
        PerformOperations();
    }

    private void Start()
    {
        if (typeOfObject == Type.removeAdsGameobject)
        {
            Tick.SetActive(false);
            this.GetComponent<Button>().interactable = true;
        }
        OnEnable();
    }

    void PerformOperations()
    {
        if (AdManager.Instance)
        {
            if (AdManager.Instance.HasAdsBeenRemoved())
            {
                if (typeOfObject == Type.removeAdsGameobject)
                {
                    if (Tick)
                        Tick.SetActive(true);
                    if(this.GetComponent<Button>())
                        this.GetComponent<Button>().interactable = false;
                    //this.gameObject.SetActive(false);
                }
                else if (typeOfObject == Type.adGameobject || typeOfObject == Type.dateDependentadAdGameobject)
                {
                    this.gameObject.SetActive(false);
                }

                if (AdManager.Instance.CanDeliverAds() == false)
                {
                    if (typeOfObject == Type.otherServerDependentObject)
                    {
                        this.gameObject.SetActive(false);
                    }
                }
            }
            else if (AdManager.Instance.CanDeliverAds() == false)
            {
                if (typeOfObject == Type.adGameobject || typeOfObject == Type.otherServerDependentObject)
                {
                    this.gameObject.SetActive(false);
                }
            }
            else
            {
                if (typeOfObject == Type.dateDependentadAdGameobject)
                {
                    if (!ReleaseDate.released)
                    {
                        this.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
    public void RemoveAllAds()
    {
        if (typeOfObject == Type.removeAdsGameobject)
        {
            Tick.SetActive(true);
            this.GetComponent<Button>().interactable = false;
            //this.gameObject.SetActive(false);
        }
        else if (typeOfObject != Type.otherServerDependentObject)
        {
            this.gameObject.SetActive(false);
        }
    }
}
