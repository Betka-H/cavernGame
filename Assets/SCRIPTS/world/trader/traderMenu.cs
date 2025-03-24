using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class traderMenu : MonoBehaviour
{
	public traderHand handL;
	public traderHand handR;
	public traderHand bagHand;

	public menuManager menuManager;
	public inventoryManager inventoryManager;
	public announcerManager announcerManager;

	private List<item> traderInv;
	item mainItem;

	public TMP_Text timesTradedInfoDisp;

	void OnEnable()
	{
		menuManager.itemInfoDisplay.setInfo(null);

		clearLHand();
		updateHands();
	}

	void updateHands()
	{
		handL.displayItem();
		handR.displayItem();
		bagHand.displayItem();
	}
	void clearLHand()
	{
		handL.assignedItem = null;
	}

	item offeredItem;
	public void offer()
	{
		offeredItem = menuManager.itemInfoDisplay.selectedItem;
		handL.assignItem(offeredItem);
		updateHands();
	}

	int invLength = -1;
	int timesTraded = 0;
	public void trade()
	{
		if (offeredItem != null && traderInv.Count > 0)
		{
			if (invLength == -1)
				invLength = traderInv.Count;

			item currentlyTradedItem = traderInv[0];
			if (offeredItem != mainItem)
			{
				timesTraded++;
				timesTraded += UnityEngine.Random.Range(0, timesTraded);

				showTradeCount();

				//  Debug.Log("attempting trade");

				inventoryManager.removeItem(offeredItem, inventoryManager.caveInventory);

				offeredItem = null;
				handL.assignItem(offeredItem);

				inventoryManager.addItem(currentlyTradedItem, inventoryManager.caveInventory);
				menuManager.caveItemGrid.refreshItems(menuManager.caveItemGrid.regularSlots, inventoryManager.caveInventory);
				traderInv.RemoveAt(0);
				if (traderInv.Count > 0)
				{
					currentlyTradedItem = traderInv[0];
					handR.assignItem(currentlyTradedItem);
					if (traderInv.Count > 1)
						bagHand.assignItem(traderInv[1]);
					else
						bagHand.assignItem(null);
				}
				else
					handR.assignItem(null);

				updateHands();
				clearTooltip();
			}
			else announcerManager.announceMessage("you cannot trade the same item the trader is selling!");
		}
		else announcerManager.announceMessage("no offer!");

		Debug.LogWarning($"times traded: {timesTraded}, trader inv: {invLength}");
		if (timesTraded > invLength)
			StartCoroutine(flee());
	}
	IEnumerator flee()
	{
		yield return new WaitForSecondsRealtime(0.25f);

		// Debug.LogWarning("fleeing!");
		FindObjectOfType<menuManager>().hideMenus();
		FindObjectOfType<npcTrader>().gameObject.SetActive(false);
		int stealRnd = UnityEngine.Random.Range(0, timesTraded / 2);
		int stoleAmount = 0;
		for (int i = 0; i < stealRnd; i++)
		{
			inventoryManager.removeItem(inventoryManager.caveInventory[UnityEngine.Random.Range(0, inventoryManager.caveInventory.Count - 1)], inventoryManager.caveInventory);
			stoleAmount++;
		}
		if (stoleAmount > 0)
			announcerManager.announceMessage($"the trader has fled, taking {stoleAmount} of your items with it!");
		else announcerManager.announceMessage($"the trader has fled! your wallet remains intact.");

		FindObjectOfType<roomController>().killTrader();
	}

	private void clearTooltip()
	{
		menuManager.itemInfoDisplay.setInfo(null);
	}

	void showTradeCount()
	{
		timesTradedInfoDisp.text = $"times traded: {timesTraded}";
	}

	public void setInventory(item[] inv, item mainItem)
	{
		timesTraded = 0;
		this.mainItem = mainItem;
		showTradeCount();
		traderInv = inv.ToList();
		handR.assignItem(traderInv[0]);
		bagHand.assignItem(traderInv[1]);
		Debug.LogWarning("trader inv:");
		inventoryManager.printInventory(traderInv);
	}
}