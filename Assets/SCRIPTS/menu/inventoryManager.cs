using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class inventoryManager : MonoBehaviour
{
	[HideInInspector] public List<item> caveInventory;
	[HideInInspector] public List<item> labInventory;
	[HideInInspector] public List<item> equippedItems;
	[HideInInspector] public List<item> missionInventory;

	public inventoryDefinitions inventoryDefinitions;

	void beforeTutorialLabInv()
	{
		// shoes scrap + materials for them
		labInventory = new List<item>
		{
			inventoryDefinitions.scrapShoes
		};
		labInventory.AddRange(inventoryDefinitions.scrapShoes.wholeGear.cost);
	}
	public void m_afterTutorialInv()
	{
		// crafted shoes
		labInventory = new List<item>
		{
			inventoryDefinitions.shoes
		};
	}

	public bool checkEquipment(item it)
	{
		if (equippedItems.Contains(it))
			return true;
		else return false;
	}
	public bool checkResources(List<item> bigList, List<item> doesTheBigListContainThis)
	{
		List<item> checkInv = new List<item>(bigList);
		bool hasResources = true;
		while (hasResources)
		{
			foreach (item it in doesTheBigListContainThis)
			{
				if (checkInv.Contains(it))
					checkInv.Remove(it);
				else hasResources = false;
			}
			break;
		}
		return hasResources;
	}

	public void sortInventory(ref List<item> inv)
	{
		if (inv != null)
			inv = inv.OrderBy(i => i.itemId).ToList();
	}

	public void printInventory(List<item> inv)
	{
		if (inv.Count > 0)
			Debug.Log("items in inventory: " + string.Join(", ", inv));
		else Debug.Log("there are no items in the inventory");
	}

	public void addItem(item it, List<item> inventory)
	//* unnecessary
	{
		inventory.Add(it);
	}
	public void removeItem(item it, List<item> inventory)
	//* unnecessary
	{
		if (inventory.Contains(it))
			inventory.Remove(it);
	}

	public void resetInventories()
	{
		// Debug.LogError($"clearing inventories");

		beforeTutorialLabInv();

		equippedItems.Clear();
		missionInventory.Clear();
	}
}
