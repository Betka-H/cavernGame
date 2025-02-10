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

	void Start()
	{
		gameObject.SetActive(false);
		getSlots();
	}

	void getSlots()
	{
		invScreenItemSlots = inventoryScreenContent.GetComponentsInChildren<invItem>();
	}

	void OnEnable()
	{
		refreshItems();
	}

	void refreshItems()
	{
		if (invScreenItemSlots != null)
		{
			for (int i = 0; i < invScreenItemSlots.Length; i++)
			{
				invItem currentItem = invScreenItemSlots[i]; // current item slot
				if (i < inventoryScript.inventoryIds.Count) // only if there are itemids left
				{
					currentItem.assignItem(inventoryScript.allItemList[inventoryScript.inventoryIds[i]]); // assign ith item
				}
				else currentItem.assignItem(null); // if there are no more itemids, assign null
			}
		}
	}

	public void showTooltip(item item)
	{
		invTxt_itemName.SetText(item.itemName);
		invSprite_itemSprite.sprite = item.itemSprite;
		invTxt_itemDescription.SetText(item.itemDescription);
	}
}
