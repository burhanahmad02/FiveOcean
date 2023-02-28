/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineSelector : MonoBehaviour
{
	public AudioSource upgradeSource;
	public AudioSource buySource;
	public GameObject[] rovers;
	private int totalRovers;
	private int currentRover;

	//public GameObject []firstRoverObjects;
	//public GameObject []nextRoverObjects;

	private static bool firstTimeRunning = true;
	public void OnEnable()
	{
		if (firstTimeRunning)
		{
			firstTimeRunning = false;
		}
		else
		{
			totalRovers = rovers.Length - 1;
			DisableAll();
			ActivateRover();
		}
	}

	void Start()
	{
	}
	public void ActivateRover()
	{
		int roverNumber = GameManager.Instance.GetRoverNumber();

		if (TutorialManager.isTutorialRunning)
		{
			if (EncryptedPlayerPrefs.GetInt("Tutorial") == 6)
			{
				roverNumber = 1;
			}
		}

		for (int i = 1; i < rovers.Length; i++)
		{
			if (i == roverNumber)
			{
				string prefKey = "RoverUnlocked" + i;

				if (roverNumber < rovers.Length)
				{
					rovers[roverNumber].SetActive(true);
				}
				else
				{
					roverNumber = 1;
					rovers[roverNumber].SetActive(true);
				}
			}
			else
			{
				rovers[i].SetActive(false);
			}
		}
		currentRover = roverNumber;

		//if (currentRover == 1)
		//{
		//	foreach (var item in nextRoverObjects)
		//	{
		//		item.SetActive(false);
		//	}
		//	foreach (var item in firstRoverObjects)
		//	{
		//		item.SetActive(true);
		//	}
		//}
		//else
		//{
		//	foreach (var item in firstRoverObjects)
		//	{
		//		item.SetActive(false);
		//	}
		//	foreach (var item in nextRoverObjects)
		//	{
		//		item.SetActive(true);
		//	}
		//}
	}
	public void DisableAll()
	{
		for (int i = 1; i < rovers.Length; i++)
		{
			if (rovers[i] != null)
			{
				rovers[i].SetActive(false);
			}
		}
	}
	public void NextRoverButttonClick()
	{
		rovers[currentRover].SetActive(false);
		currentRover++;
		if (currentRover > totalRovers)
		{
			currentRover = 1;
		}
		rovers[currentRover].SetActive(true);

		//if (currentRover == 1)
		//{
		//	foreach (var item in nextRoverObjects)
		//	{
		//		item.SetActive(false);
		//	}
		//	foreach (var item in firstRoverObjects)
		//	{
		//		item.SetActive(true);
		//	}
		//}
		//else
		//{
		//	foreach (var item in firstRoverObjects)
		//	{
		//		item.SetActive(false);
		//	}
		//	foreach (var item in nextRoverObjects)
		//	{
		//		item.SetActive(true);
		//	}
		//}
		Utility.MakeClickSound();
	}
	public void PreviousRoverButttonClick()
	{
		rovers[currentRover].SetActive(false);
		currentRover--;
		if (currentRover <= 0)
		{
			currentRover = totalRovers;
		}
		rovers[currentRover].SetActive(true);
		//if (currentRover == 1)
		//{
		//	foreach (var item in nextRoverObjects)
		//	{
		//		item.SetActive(false);
		//	}
		//	foreach (var item in firstRoverObjects)
		//	{
		//		item.SetActive(true);
		//	}
		//}
		//else
		//{
		//	foreach (var item in firstRoverObjects)
		//	{
		//		item.SetActive(false);
		//	}
		//	foreach (var item in nextRoverObjects)
		//	{
		//		item.SetActive(true);
		//	}
		//}
		Utility.MakeClickSound();
	}
	public void BuyRover(PriceDecider deciderScript)
	{
		bool isGems = deciderScript.isGemsPrice;

		int totalRequirement = 0;

		if (isGems)
		{
			totalRequirement = EncryptedPlayerPrefs.GetInt("Gems");
		}
		else
		{
			totalRequirement = EncryptedPlayerPrefs.GetInt("Funds");
		}
		int needed = deciderScript.price;
		if (totalRequirement >= needed)
		{

			EncryptedPlayerPrefs.SetInt("RoverUnlocked" + currentRover.ToString(), 1);

			GameManager.Instance.SetRoverNumber(currentRover);

			GameServerData.Instance.SetData("0", "0", currentRover);

			DisableAll();
			ActivateRover();


			totalRequirement = totalRequirement - needed;
			if (isGems)
			{
				EncryptedPlayerPrefs.SetInt("Gems", totalRequirement);

			}
			else
			{
				EncryptedPlayerPrefs.SetInt("Funds", totalRequirement);
			}

			*//*if (rovers[currentRover].GetComponentInChildren<LockerUnlocker>().unlockedObject.GetComponentInChildren<UpgradeDot>())
			{
				UpgradeDot[] upgradeDots = rovers[currentRover].GetComponentInChildren<LockerUnlocker>().unlockedObject.GetComponentsInChildren<UpgradeDot>();

				foreach (var item in upgradeDots)
				{
					item.enabled = true;
				}
			}*//*

			//GetComponentInChildren<LockerUnlocker>().unlockedObject.GetComponent<RoverEquip>().EquipRover();
			GameManager.DisplayValuesOfItems();
			buySource.Play();

		}
		else
		{
			Utility.MakeClickSound();
			MainMenuUI.Instance.OpenInAppPanel();
			MainMenuUI.Instance.InsufficientFundsPanel.GetComponent<InsufficientFundsManager>().SetPreviousPanel(GetComponentInParent<RoverSelector>().gameObject);
			GetComponentInParent<RoverSelector>().gameObject.SetActive(false);
		}
	}
	public void BuyRoverFromInapp()
	{
		MainMenuUI.Instance.OpenInAppPanel();
		MainMenuUI.Instance.InsufficientFundsPanel.GetComponent<InsufficientFundsManager>().SetPreviousPanel(GetComponentInParent<RoverSelector>().gameObject);
		GetComponentInParent<RoverSelector>().gameObject.SetActive(false);
		Utility.MakeClickSound();
	}
}
*/