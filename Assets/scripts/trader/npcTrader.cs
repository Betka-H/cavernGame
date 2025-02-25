using System.Collections.Generic;
using UnityEngine;

public class npcTrader : MonoBehaviour
{
	traderMenu traderMenu;
	// tooltipCaller tooltipCaller;

	[Space]
	public item[] traderItemPool;
	public item[] traderScrapPool;

	private item[] traderInventory;

	[Space]
	[Tooltip("chance of the trader offering a piece of scrap")]
	[Range(0, 100)]
	public int scrapChance;

	void Start()
	{
		traderMenu = FindObjectOfType<traderMenu>(true);
		// Debug.Log($"the trader menu is {traderMenu}");
		// tooltipCaller = gameObject.GetComponent<tooltipCaller>();
		rollTraderInventory();
	}

	/* void Update()
	{
		if (Input.GetKeyDown(tooltipCaller.keyToInteract))
		{
			Debug.Log("hi im the trader npc. kys unity");
			toggleMenu();
		}
	} */

	void rollTraderInventory()
	{
		System.Random rnd = new System.Random();

		traderInventory = new item[5];

		item tradedItem = traderItemPool[rnd.Next(traderItemPool.Length)];
		for (int i = 0; i < traderInventory.Length; i++)
		{
			traderInventory[i] = tradedItem;
		}

		if (scrapChance > rnd.Next(100))
		{
			int scrapPosition = rnd.Next(traderInventory.Length);
			// traderInventory[scrapPosition] = traderScrapPool[rnd.Next(traderItemPool.Length - 1)];
			traderInventory[scrapPosition] = traderScrapPool[rnd.Next(traderScrapPool.Length - 1)];
			Debug.Log($"trader sells scrap ({scrapPosition})");
		}

		logTraderInventory();

		traderMenu.setInventory(traderInventory);
	}

	void logTraderInventory()
	{
		List<string> msg = new List<string>();
		for (int i = 0; i < traderInventory.Length; i++)
		{
			msg.Add($"{traderInventory[i].itemName} ({i})");
		}
		Debug.Log($"the trader inventory contains {string.Join(", ", msg)}");
	}

	public void toggleMenu()
	{
		traderMenu.toggleTraderMenu();
		// Debug.Log("opening trader menu");
	}

	/* public item[] getInventory()
	{
		return traderInventory;
	} */
}
