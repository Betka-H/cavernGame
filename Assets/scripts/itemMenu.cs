using TMPro;
using UnityEngine;

public class itemMenu : MonoBehaviour
{
	public inventory inventoryMan;
	public TMP_Text invTxt_itemName;
	public SpriteRenderer invSprite_itemSprite;
	public TMP_Text invTxt_itemDescription;
	// public Sprite testsprite;

	void Start()
	{
		gameObject.SetActive(false);
	}

	void OnEnable()
	{
		refreshItems();
	}

	void refreshItems()
	{
		// reload items
		showTooltip(inventoryMan.allItemList[1]);
	}

	public void showTooltip(item item)
	{
		invTxt_itemName.SetText(item.itemName);
		invSprite_itemSprite.sprite = item.itemSprite;
		invTxt_itemDescription.SetText(item.itemDescription);
	}
}
