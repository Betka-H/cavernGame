using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class traderMenu : MonoBehaviour
{
	public traderHand handL;
	public traderHand handR;

	public caveItemMenu itemMenu;
	public inventoryManager inventory;
	private List<item> traderInv;

	public TMP_Text timesTradedInfoDisp;

	void OnEnable()
	{
		updateHands();
		clearLHand();
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
		offeredItem = itemMenu.selectedItem;
		handL.assignItem(offeredItem);
		updateHands();
	}
	int timesTraded = 0;
	public void trade()
	{
		if (offeredItem != null && traderInv.Count() > 0)
		{
			item tradedItem = traderInv[0];
			if (offeredItem != tradedItem)
			{
				timesTraded++;
				showTradeCount();

				Debug.Log("attempting trade");

				// itemMenu.remItem(itemMenu.selectedItem);
				inventory.removeItem(offeredItem, inventory.caveInventory);

				// offerMade = false;
				offeredItem = null;
				handL.assignItem(offeredItem);

				// itemMenu.addItem(tradedItem);
				inventory.addItem(tradedItem, inventory.caveInventory);
				itemMenu.refreshItems(itemMenu.regularSlots, inventory.caveInventory);
				traderInv.RemoveAt(0);
				if (traderInv.Count() > 0)
				{
					tradedItem = traderInv[0];
					handR.assignItem(tradedItem);
				}
				else
					handR.assignItem(null);

				// chance stuff

				updateHands();
				clearTooltip();
			}
			else Debug.Log("cannot trade the same item");
		}
		else
		{
			Debug.Log("no offer or trader inv empty");
			// chance stuff? make it more agitated when you offer nothing
		}
	}

	private void clearTooltip()
	{
		itemMenu.selectedItem = null;
		Debug.Log("clear item tooltip");
	}

	void showTradeCount()
	{
		timesTradedInfoDisp.text = $"times traded: {timesTraded}";
	}

	public void setInventory(item[] inv)
	{
		timesTraded = 0;
		showTradeCount();
		traderInv = inv.ToList();
		handR.assignItem(traderInv[0]);
	}
}