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

	void Start()
	{
		/* inventoryManager.addItem(inventoryManager.inventoryDefinitions.gemBlue, inventoryManager.caveInventory);
		inventoryManager.addItem(inventoryManager.inventoryDefinitions.gemBlue, inventoryManager.caveInventory);
		inventoryManager.addItem(inventoryManager.inventoryDefinitions.gemBlue, inventoryManager.caveInventory);
		inventoryManager.addItem(inventoryManager.inventoryDefinitions.gemPink, inventoryManager.caveInventory);
		inventoryManager.addItem(inventoryManager.inventoryDefinitions.gemPink, inventoryManager.caveInventory);
		inventoryManager.addItem(inventoryManager.inventoryDefinitions.gemPink, inventoryManager.caveInventory);
		inventoryManager.addItem(inventoryManager.inventoryDefinitions.gemYellow, inventoryManager.caveInventory);
		inventoryManager.addItem(inventoryManager.inventoryDefinitions.gemYellow, inventoryManager.caveInventory);
		inventoryManager.addItem(inventoryManager.inventoryDefinitions.gemYellow, inventoryManager.caveInventory);
		inventoryManager.addItem(inventoryManager.inventoryDefinitions.gemYellow, inventoryManager.caveInventory); */
	}

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

	// bool offerMade;
	item offeredItem;
	public void offer()
	{
		// offerMade = true;
		offeredItem = menuManager.itemInfoDisplay.selectedItem;
		handL.assignItem(offeredItem);
		updateHands();
	}

	// bool hasFled = false;
	int invLength = -1;
	int timesTraded = 0;
	public void trade()
	{
		// if (!hasFled)
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

				// itemMenu.remItem(itemMenu.selectedItem);
				inventoryManager.removeItem(offeredItem, inventoryManager.caveInventory);

				// offerMade = false;
				offeredItem = null;
				handL.assignItem(offeredItem);

				// itemMenu.addItem(tradedItem);
				inventoryManager.addItem(currentlyTradedItem, inventoryManager.caveInventory);
				menuManager.caveItemMenu.refreshItems(menuManager.caveItemMenu.regularSlots, inventoryManager.caveInventory);
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

				// chance stuff

				updateHands();
				clearTooltip();
			}
			else
			{
				// Debug.Log("cannot trade the main item");
				FindObjectOfType<announcerManager>().announceMessage("you cannot trade the same item the trader is selling!");
				/* timesTraded++;
				showTradeCount(); */
			}
		}
		else
		{
			// Debug.Log("no offer or trader inv empty");
			//! chance stuff? make it more agitated when you offer nothing
			FindObjectOfType<announcerManager>().announceMessage("no offer!");
			/* timesTraded++;
			showTradeCount(); */
		}

		Debug.LogWarning($"times traded: {timesTraded}, trader inv: {invLength}");
		if (timesTraded > invLength)
		{
			// Debug.LogWarning("flee?");
			StartCoroutine(flee());
		}
	}
	IEnumerator flee()
	{
		yield return new WaitForSecondsRealtime(0.25f);

		// Debug.LogWarning("fleeing!");
		// hasFled = true;
		FindObjectOfType<menuManager>().hideMenus();
		FindObjectOfType<npcTrader>().gameObject.SetActive(false);
		int stealRnd = UnityEngine.Random.Range(0, timesTraded / 2);
		int stoleAmount = 0;
		for (int i = 0; i < stealRnd; i++)
		{
			//! what if no cave inv?
			inventoryManager.removeItem(inventoryManager.caveInventory[UnityEngine.Random.Range(0, inventoryManager.caveInventory.Count - 1)], inventoryManager.caveInventory);
			stoleAmount++;
		}
		if (stoleAmount > 0)
			FindObjectOfType<announcerManager>().announceMessage($"the trader has fled, taking {stoleAmount} of your items with it!");
		else
			FindObjectOfType<announcerManager>().announceMessage($"the trader has fled! your wallet remains intact.");

		FindObjectOfType<roomController>().killTrader();
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
		bagHand.assignItem(traderInv[1]);
		Debug.LogWarning("trader inv:");
		inventoryManager.printInventory(traderInv);
	}
}