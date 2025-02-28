using UnityEngine;

public class traderHand : MonoBehaviour
{
	[HideInInspector]
	public item assignedItem;
	public SpriteRenderer slotSpriteRenderer;

	public void displayItem()
	{
		if (assignedItem != null)
		{
			Debug.Log($"{name}'s item is: {assignedItem.name} ({assignedItem})");
			slotSpriteRenderer.sprite = assignedItem.itemSprite;
		}
		else
		{
			Debug.Log($"{name}'s item is null");
			slotSpriteRenderer.sprite = null;
		}
	}

	public virtual void assignItem(item it)
	{
		assignedItem = it;
	}
}