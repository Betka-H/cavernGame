using System.Collections.Generic;
using UnityEngine;

public class npcTrader : MonoBehaviour
{
	[Space]
	public item[] traderItemPool;
	public item[] traderScrapPool;

	private item[] traderInventory;

	menuManager menuManager;

	[Space]
	[Tooltip("chance of the trader offering a piece of scrap")]
	[Range(0, 100)]
	public int scrapChance;

	void Start()
	{
		menuManager = FindObjectOfType<menuManager>();

		rollTraderInventory();
		//* move trader down to nearest ground
	}

	item mainItem;
	void rollTraderInventory()
	{
		System.Random rnd = new System.Random();

		traderInventory = new item[5];

		mainItem = traderItemPool[rnd.Next(traderItemPool.Length)];
		for (int i = 0; i < traderInventory.Length; i++)
		{
			traderInventory[i] = mainItem;
		}

		if (scrapChance > rnd.Next(100))
		{
			int scrapPosition = rnd.Next(traderInventory.Length);
			traderInventory[scrapPosition] = traderScrapPool[rnd.Next(traderScrapPool.Length)];
			// Debug.Log($"trader sells scrap (as {scrapPosition})");
		}

		// logTraderInventory();

		menuManager.traderMenu.setInventory(traderInventory, mainItem);
	}

	void logTraderInventory()
	{
		List<string> msg = new List<string>();
		for (int i = 0; i < traderInventory.Length; i++)
		{
			msg.Add($"{traderInventory[i].itemName} ({i})");
		}
		// Debug.Log($"the trader inventory contains {string.Join(", ", msg)}");
	}
}
