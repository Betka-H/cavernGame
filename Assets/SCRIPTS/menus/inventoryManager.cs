using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class inventoryManager : MonoBehaviour
{
	[HideInInspector] public List<item> caveInventory;
	[HideInInspector] public List<item> labInventory;
	[HideInInspector] public List<item> equippedItems;
	[HideInInspector] public List<item> missionInventory;

	[HideInInspector] public inventoryDefinitions inventoryDefinitions;

	/*//* private string savePath = "inventory-save.txt";
	File.WriteAllText(savePath, string.Join(",", inventoryIds));
	string[] invRead = File.ReadAllText(savePath).ToString().Split(',');
	 foreach (string s in invRead)
	{
		inventoryIds.Add(int.Parse(s));
	} */

	void Start()
	{
		// Debug.Log("starting invenmanager");
		inventoryDefinitions = FindObjectOfType<inventoryDefinitions>();

		tempInv();
	}

	void tempInv()
	{
		labInventory = new List<item>
	{
	inventoryDefinitions.gemBlue,
	inventoryDefinitions.gemBlue,
	inventoryDefinitions.gemBlue,
	inventoryDefinitions.gemBlue,
	inventoryDefinitions.gemBlue,
	inventoryDefinitions.gemBlue,
	inventoryDefinitions.gemYellow,
	inventoryDefinitions.gemYellow,
	inventoryDefinitions.gemOrange,
	inventoryDefinitions.gemOrange,
	inventoryDefinitions.gemClear,
	inventoryDefinitions.gemClear,
	inventoryDefinitions.gemClear,
	inventoryDefinitions.gemClear,
	inventoryDefinitions.gemClear,
	inventoryDefinitions.gemClear,
	inventoryDefinitions.gemClear,
	inventoryDefinitions.scrapGoggles,
	inventoryDefinitions.scrapGoggles,
	inventoryDefinitions.scrapPickaxe,
	inventoryDefinitions.scrapPickaxe,
	inventoryDefinitions.scrapBackpack,
	inventoryDefinitions.backpack,
	inventoryDefinitions.shoes,
	inventoryDefinitions.shoes,
	inventoryDefinitions.harderHat,
	inventoryDefinitions.harderHat,
	inventoryDefinitions.harderHat,
	inventoryDefinitions.scrapHarderHat,
	inventoryDefinitions.scrapHarderHat,
	inventoryDefinitions.scrapHarderHat,
	inventoryDefinitions.scrapHarderHat,
	inventoryDefinitions.scrapShoes,
	inventoryDefinitions.scrapShoes,
	inventoryDefinitions.scrapShoes,
	inventoryDefinitions.backpack,
	inventoryDefinitions.backpack,
	inventoryDefinitions.pickaxe,
	inventoryDefinitions.pickaxe,
	inventoryDefinitions.nightVisionGoggles,
	inventoryDefinitions.nightVisionGoggles,
	inventoryDefinitions.nightVisionGoggles,
	};
	}

	public bool checkEquipment(item it)
	{
		if (equippedItems.Contains(it))
		{
			// Debug.Log($"{it} is equipped");
			return true;
		}
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
				// Debug.Log($"checking {it}");
				if (checkInv.Contains(it))
				{
					// Debug.Log($"has {it}");
					checkInv.Remove(it);
				}
				else
				{
					hasResources = false;
					// Debug.Log($"has not {it}");
				}
			}
			break;
		}
		// Debug.Log($"resources? {hasResources}");
		return hasResources;
	}

	public void sortInventory(ref List<item> inv)
	{
		// Debug.Log($"SORTING!?!??!?");
		// printInventory(inv);
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
	{
		inventory.Add(it);
	}
	public void removeItem(item it, List<item> inventory)
	{
		if (!inventory.Contains(it))
		{
			Debug.Log($"cannot remove item {it} cause its not in the inventory"); // dumbass     <<< excuse me???
		}
		else
		{
			inventory.Remove(it);
			// Debug.Log($"removed item {it} from {nameof(inventory)}");
		}
	}

	public void resetInventories()
	{
		labInventory.Clear();
		equippedItems.Clear();
		missionInventory.Clear();
	}
}
