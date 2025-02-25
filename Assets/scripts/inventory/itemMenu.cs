using System;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class itemMenu : MonoBehaviour
{
	public inventory inventoryScript;
	public GameObject inventoryScreenContent;
	private invItem[] invScreenItemSlots;
	[Space]
	public TMP_Text invTxt_itemName;
	public SpriteRenderer invSprite_itemSprite;
	public TMP_Text invTxt_itemDescription;
	[HideInInspector]
	public item selectedItem;

	public Sprite placeholderItemSprite;
	public string placeholderItemName;
	[TextArea]
	public string placeholderItemDescription;

	void Start()
	{
		gameObject.SetActive(false);
		getSlots();
		showTooltip(null);
	}

	void getSlots()
	{
		invScreenItemSlots = inventoryScreenContent.GetComponentsInChildren<invItem>();
	}

	void OnEnable()
	{
		showTooltip(null);
		refreshItems();
	}

	public void refreshItems()
	{
		inventoryScript.sortInventory();

		if (invScreenItemSlots != null)
		{
			for (int i = 0; i < invScreenItemSlots.Length; i++)
			{
				invItem currentItem = invScreenItemSlots[i]; // current item slot
				if (i < inventoryScript.inventoryItems.Count) // only if there are itemids left
				{
					// currentItem.assignItem(inventoryScript.allItemList[inventoryScript.inventoryIds[i]]); // assign ith item
					currentItem.assignItem(inventoryScript.inventoryItems[i]); // assign ith item
				}
				else currentItem.assignItem(null); // if there are no more itemids, assign null
			}
		}
	}

	public void showTooltip(item item)
	{
		if (item != null)
		{
			invTxt_itemName.SetText(item.itemName);
			invSprite_itemSprite.sprite = item.itemSprite;
			invTxt_itemDescription.SetText(item.itemDescription);
		}
		else
		{
			invTxt_itemName.SetText(placeholderItemName);
			invSprite_itemSprite.sprite = placeholderItemSprite;
			invTxt_itemDescription.SetText(placeholderItemDescription);
		}
	}

	/* public void remItem(item it)
	{
		inventoryScript.removeItem(it);
	} */

	/* public void addItem(item it)
	{
		inventoryScript.addItem(it);
	} */
}
