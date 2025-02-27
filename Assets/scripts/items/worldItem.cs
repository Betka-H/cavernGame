using System;
using System.Linq;
using UnityEngine;

public class worldItem : MonoBehaviour
{
	[HideInInspector]
	public loot assignedItem;
	[HideInInspector]
	public Transform assignedTransform;
	public SpriteRenderer spriteRenderer;
	private inventory inventory;
	[HideInInspector]
	public room_cavern roomSO;

	void Start()
	{
		inventory = FindObjectOfType<inventory>();
	}

	public void updateItem(loot it)
	{
		assignedItem = it;
		if (it != null)
		{
			gameObject.SetActive(true);
			updateSprite();
		}
		else gameObject.SetActive(false);
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
			// Debug.Log("item collide");

			addItemToInventory();
			removeItemFromSpawnPool();
			killItem();
		}
	}

	void addItemToInventory()
	{
		inventory.addItem(assignedItem);
	}
	void killItem()
	{
		updateItem(null);
	}
	void removeItemFromSpawnPool()
	{
		// Debug.Log($"ie length: {room_cavern.itemsForThisRoom.Count()}, index: {Array.IndexOf(room_cavern.itemsForThisRoom.ToArray(), assignedItem)}");
		// room_cavern.indexedItemsForThisRoom[Array.IndexOf(room_cavern.indexedItemsForThisRoom.ToArray(), assignedItem)] = null;
		// Debug.Log($"items in room: {string.Join(", ", room_cavern.lootForThisRoom)}, assigned item:{assignedItem}");
		roomSO.lootForThisRoom.Remove(assignedItem);
		// Debug.Log($"items now in room: {string.Join(", ", room_cavern.lootForThisRoom)}, assigned item:{assignedItem}");
		roomSO.chosenLootSpawnLocations.Remove(assignedTransform);
	}
}