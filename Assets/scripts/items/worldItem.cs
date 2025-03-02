using UnityEngine;

public class worldItem : MonoBehaviour
{
	SpriteRenderer spriteRenderer;
	private inventoryManager inventory;

	loot assignedItem;
	[HideInInspector] public Transform assignedSpawnTransform;
	[HideInInspector] public room_cavern assignedRoomSO;

	void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		inventory = FindObjectOfType<inventoryManager>();
	}

	public loot checkItem()
	{
		return assignedItem;
	}

	public void updateItem(loot it)
	{
		assignedItem = it;
		if (it != null)
		{
			gameObject.SetActive(true);
			updateSprite();
		}
		else Destroy(gameObject);
	}
	void updateSprite()
	{
		if (assignedItem != null)
			spriteRenderer.sprite = assignedItem.itemSprite;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("player"))
		{
			pickUp();
		}
	}
	void pickUp()
	{
		inventory.addCaveItem(assignedItem);
		assignedRoomSO.removeItemSpawn(assignedItem, assignedSpawnTransform);
		updateItem(null);
	}
}