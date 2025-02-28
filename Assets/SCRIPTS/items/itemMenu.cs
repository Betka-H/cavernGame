using TMPro;
using UnityEngine;

public class itemMenu : MonoBehaviour
{
	public inventory inventoryScript;

	private invItem[] gridSlots;

	[Header("item info section")]
	public SpriteRenderer invSprite_itemSprite;
	public TMP_Text invTxt_itemName;
	public TMP_Text invTxt_itemDescription;

	public Sprite placeholderItemSprite;
	public string placeholderItemName;
	[TextArea]
	public string placeholderItemDescription;

	[HideInInspector]
	public item selectedItem;

	void Awake()
	{
		getSlots();
	}

	void getSlots()
	{
		gridSlots = GetComponentsInChildren<invItem>();
	}

	void OnEnable()
	{
		selectedItem = null;
		showInfo();
		refreshItems();
	}

	public void refreshItems()
	{
		inventoryScript.sortInventory();

		if (gridSlots != null)
		{
			for (int i = 0; i < gridSlots.Length; i++)
			{
				invItem currentItem = gridSlots[i];
				if (i < inventoryScript.inventoryItems.Count)
				{
					currentItem.assignItem(inventoryScript.inventoryItems[i]);
				}
				else currentItem.assignItem(null);
			}
		}
	}

	public void showInfo()
	{
		if (selectedItem != null)
		{
			invSprite_itemSprite.sprite = selectedItem.itemSprite;
			invTxt_itemName.SetText(selectedItem.itemName);
			invTxt_itemDescription.SetText(selectedItem.itemDescription);
		}
		else
		{
			invSprite_itemSprite.sprite = placeholderItemSprite;
			invTxt_itemName.SetText(placeholderItemName);
			invTxt_itemDescription.SetText(placeholderItemDescription);
		}
	}
}
