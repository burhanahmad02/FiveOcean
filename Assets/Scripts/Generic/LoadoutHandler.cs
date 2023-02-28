using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class LoadoutList
{
    [SerializeField] private List<GameObject> loadout = new List<GameObject>();
    public List<GameObject> Loadout { get { return loadout; } set { loadout = value; } }
}
public class LoadoutHandler : MonoBehaviour
{
    [SerializeField] private List<LoadoutList> loadoutList = new List<LoadoutList>();

    private int currentActivated = 0;
    private bool isSpecialWeaponActivated = false;
    public static LoadoutHandler instance;

    // Use this for initialization
    void Awake ()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }
    private void Start()
    {
        SwitchLoadout();
    }
    public void SwitchLoadout()
    {
        currentActivated++;
        if (currentActivated >= loadoutList.Count)
        {
            currentActivated = 1;
        }
        ActivateLoadout(currentActivated);
    }
    public void SwapSpecialLoadout()
    {
        if (isSpecialWeaponActivated)
        {
            ActivateLoadout(currentActivated);
        }
        else
        {
            ActivateLoadout(0);
        }
    }
    public void ActivateLoadout(int indexToLoad)
    {
        if (loadoutList.Count != 0)
        {
            if (GameManager.Instance.loadoutCount == 1)
            {
                GameManager.Instance.loadoutCount = loadoutList.Count - 1;
            }
            if (loadoutList.Count == GameManager.Instance.loadoutCount + 1)
            {
                for (int i = 0; i <= GameManager.Instance.loadoutCount; i++)
                {
                    if (i < loadoutList.Count)
                    {
                        foreach (var item in loadoutList[i].Loadout)
                        {
                            if (item)
                            {
                                item.SetActive(false);
                            }
                            else
                                Utility.ErrorLog("Loadout is not assigned in LoadoutHandler.cs of " + this.gameObject.name, 1);
                        }
                    }
                    else
                        Utility.ErrorLog("Equipped Loadout is greater than the total length of the loadouts in LoadoutHandler.cs of " + this.gameObject.name, 4);
                }
                if (GameManager.Instance.GetLoadoutInfo(indexToLoad) < loadoutList[indexToLoad].Loadout.Count)
                {
                    if (loadoutList[indexToLoad].Loadout[GameManager.Instance.GetLoadoutInfo(indexToLoad)])
                    {
                        isSpecialWeaponActivated = false;
                        loadoutList[indexToLoad].Loadout[GameManager.Instance.GetLoadoutInfo(indexToLoad)].SetActive(true);
                    }
                    else
                        Utility.ErrorLog("Loadout is not assigned in LoadoutHandler.cs of " + this.gameObject.name, 1);
                }
                else if (indexToLoad == 0)
                {
                    if (loadoutList[indexToLoad].Loadout[0])
                    {
                        isSpecialWeaponActivated = true;
                        loadoutList[indexToLoad].Loadout[0].SetActive(true);
                    }
                    else
                        Utility.ErrorLog("Loadout is not assigned in LoadoutHandler.cs of " + this.gameObject.name, 1);
                }
                else
                    Utility.ErrorLog("Equipped Loadout is greater than the total length of the loadouts in LoadoutHandler.cs of " + this.gameObject.name, 4);
            }
            else
                Utility.ErrorLog("Indexer count and total loadout assinged are not equal in LoadoutHandler.cs of " + this.gameObject.name, 4);
        }
        else
            Utility.ErrorLog("Indexer Count in LoadoutHandler.cs of " + this.gameObject.name + " is 0", 4);



    }
}
