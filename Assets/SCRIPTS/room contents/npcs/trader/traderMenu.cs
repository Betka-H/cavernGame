using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class traderMenu : MonoBehaviour
{
	public traderHand handL;
	public traderHand handR;

	menuManager menuManager;
	inventoryManager inventoryManager;
	private List<item> traderInv;
	item mainItem;

	public TMP_Text timesTradedInfoDisp;

	void Awake()
	{
		menuManager = FindObjectOfType<menuManager>();
		inventoryManager = FindObjectOfType<inventoryManager>();
	}

	void OnEnable()
	{
		clearLHand();
		updateHands();
	}

	void updateHands()
	{
		handL.displayItem();
		handR.displayItem();
	}
	void clearLHand()
	{
		handL.assignedItem = null;
	}

	// bool offerMade;
	item offeredItem;
	public void offer()
	{
		// offerMade = true;
		offeredItem = menuManager.itemInfoDisplay.selectedItem;
		handL.assignItem(offeredItem);
		updateHands();
	}
	int timesTraded = 0;
	public void trade()
	{
		if (offeredItem != null && traderInv.Count() > 0)
		{
			item currentlyTradedItem = traderInv[0];
			if (offeredItem != mainItem)
			{
				timesTraded++;
				showTradeCount();

				Debug.Log("attempting trade");

				// itemMenu.remItem(itemMenu.selectedItem);
				inventoryManager.removeItem(offeredItem, inventoryManager.caveInventory);

				// offerMade = false;
				offeredItem = null;
				handL.assignItem(offeredItem);

				// itemMenu.addItem(tradedItem);
				inventoryManager.addItem(currentlyTradedItem, inventoryManager.caveInventory);
				menuManager.caveItemMenu.refreshItems(menuManager.caveItemMenu.regularSlots, inventoryManager.caveInventory);
				traderInv.RemoveAt(0);
				if (traderInv.Count() > 0)
				{
					currentlyTradedItem = traderInv[0];
					handR.assignItem(currentlyTradedItem);
				}
				else
					handR.assignItem(null);

				// chance stuff

				updateHands();
				clearTooltip();
			}
			else Debug.Log("cannot trade the main item");
		}
		else
		{
			Debug.Log("no offer or trader inv empty");
			// chance stuff? make it more agitated when you offer nothing
		}
	}

	private void clearTooltip()
	{
		// menuManager.itemInfoDisplay.selectedItem = null;
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
	}
}