using UnityEngine;

public class worldItem : MonoBehaviour
{
	private item assignedItem;
	public SpriteRenderer spriteRenderer;

	public void updateItem(item it)
	{
		assignedItem = it;
		if (it == null)
		{
			// gameObject.SetActive(false);
		}
		else gameObject.SetActive(true);
		updateSprite();
	}
	void updateSprite()
	{
		// Debug.Log($"sr: {spriteRenderer}");
		// Debug.Log($"ass item: {assignedItem}");
		if (assignedItem != null)
			spriteRenderer.sprite = assignedItem.itemSprite;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		// Debug.Log($"item collide with tag {other.tag}");
		if (other.gameObject.CompareTag("player"))
		{
			Debug.Log("item collide");
		}
	}
}