using UnityEngine;

public class worldItem : MonoBehaviour
{
	audioManager audioManager;
	inventoryManager inventoryManager;
	caveItemMenu caveItemMenu;

	public SpriteRenderer spriteRenderer;
	loot assignedItem;

	[HideInInspector] public Transform assignedSpawnPoint;
	[HideInInspector] public room_cavern assignedRoomSO;

	void Awake()
	{
		audioManager = FindObjectOfType<audioManager>();

		inventoryManager = FindObjectOfType<inventoryManager>();
		caveItemMenu = FindObjectOfType<caveItemMenu>(true);
	}

	public loot checkItem()
	{
		return assignedItem;
	}

	public void updateItem(loot it)
	{
		if (it != null)
		{
			assignedItem = it;
			gameObject.SetActive(true);
			spriteRenderer.sprite = assignedItem.itemSprite;
		}
		else Destroy(gameObject);
	}


	public void pickUp()
	{
		if (inventoryManager.caveInventory.Count < caveItemMenu.slotLimit)
		// if there are less items in the inventory than the max amount
		{
			audioManager.playSfx(audioManager.worldSfxSource, audioManager.itemPickup);

			// collect and destroy
			inventoryManager.addItem(assignedItem, inventoryManager.caveInventory);
			assignedRoomSO.removeItemSpawn(assignedItem, assignedSpawnPoint);
			updateItem(null);
		}
		else FindObjectOfType<announcerManager>().announceMessage("inventory full!");
	}
}