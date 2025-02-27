using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class traderMenu : MonoBehaviour
{
	// public Transform menuContents;

	public traderHand handL;
	public traderHand handR;

	// private item offeredItem;

	public itemMenu itemMenu;
	public inventory inventory;
	private List<item> traderInv;

	public TMP_Text timesTradedInfoDisp;

	/* void Start()
	{
		handSlotL = FindObjectOfType<traderHandLeft>(true);
		handSlotR = FindObjectOfType<traderHandRight>(true);
		Debug.Log($"slot l: {handSlotL}, slot r: {handSlotR}");
	} */

	public void toggleTraderMenu()
	{
		// bool menuEnabled = menuContents.gameObject.activeSelf;
		// toggleItemMenu(menuEnabled);
		// Debug.Log($"contents {menuContents.gameObject.activeSelf}");
		gameObject.SetActive(!gameObject.activeSelf);
		if (!gameObject.activeSelf)
			clearLHand();
		// Debug.Log($"contents now {menuContents.gameObject.activeSelf}");
		toggleItemMenu();
		updateHands();
	}
	void toggleItemMenu()
	{
		// Debug.Log($"item menu is {itemMenu.gameObject.activeSelf}");
		itemMenu.gameObject.SetActive(gameObject.activeSelf);
		// Debug.Log($"item menu is now {itemMenu.gameObject.activeSelf}");
	}

	void OnEnable()
	{
		// updateHands();
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
				inventory.removeItem(offeredItem);

				// offerMade = false;
				offeredItem = null;
				handL.assignItem(offeredItem);

				// itemMenu.addItem(tradedItem);
				inventory.addItem(tradedItem);
				itemMenu.refreshItems();
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
		itemMenu.showTooltip(null);
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