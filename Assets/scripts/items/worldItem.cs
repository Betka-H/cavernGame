using UnityEngine;

public class worldItem : MonoBehaviour
{
	SpriteRenderer spriteRenderer;
	private inventoryManager inventoryManager;
	private caveItemMenu caveItemMenu;

	loot assignedItem;
	[HideInInspector] public Transform assignedSpawnTransform;
	[HideInInspector] public room_cavern assignedRoomSO;

	void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		inventoryManager = FindObjectOfType<inventoryManager>();
		caveItemMenu = FindObjectOfType<caveItemMenu>(true);
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

	/* void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("player"))
		{
			pickUp();
		}
	} */
	public void pickUp()
	{
		// Debug.Log($"hs: {caveItemMenu.hasAllSlots}");
		Debug.Log("check backpack elsewhere");
		if (inventoryManager.checkEquipment(inventoryManager.inventoryDefinitions.backpack))
			caveItemMenu.hasAllSlots = true;
		else caveItemMenu.hasAllSlots = false;

		// Debug.Log($"{inventoryManager.caveInventory.Count}, {caveItemMenu.slotLimit}");
		if (inventoryManager.caveInventory.Count < caveItemMenu.slotLimit)
		{
			inventoryManager.addItem(assignedItem, inventoryManager.caveInventory);
			assignedRoomSO.removeItemSpawn(assignedItem, assignedSpawnTransform);
			updateItem(null);
		}
		else Debug.Log("inv full!");
	}
}