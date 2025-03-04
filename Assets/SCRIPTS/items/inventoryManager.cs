using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class inventoryManager : MonoBehaviour
{
	[HideInInspector] public List<item> labInventory;
	[HideInInspector] public List<item> caveInventory;
	[HideInInspector] public List<item> equippedItems;

	[HideInInspector] public inventoryDefinitions inventoryContents;
	public List<item> allItems;

	/*//* private string savePath = "inventory-save.txt";
	File.WriteAllText(savePath, string.Join(",", inventoryIds));
	string[] invRead = File.ReadAllText(savePath).ToString().Split(',');
	 foreach (string s in invRead)
	{
		inventoryIds.Add(int.Parse(s));
	} */

	void Start()
	{
		inventoryContents = FindObjectOfType<inventoryDefinitions>();
		orderInventory(ref allItems);

		tempInv();
		printInventory(labInventory);
	}

	void tempInv()
	{
		labInventory = new List<item>
	{
	inventoryContents.gemBlue,
	inventoryContents.gemBlue,
	inventoryContents.gemBlue,
	inventoryContents.gemBlue,
	inventoryContents.gemBlue,
	inventoryContents.gemBlue,
	inventoryContents.gemYellow,
	inventoryContents.gemYellow,
	inventoryContents.gemOrange,
	inventoryContents.gemOrange,
	inventoryContents.gemClear,
	inventoryContents.gemClear,
	inventoryContents.gemClear,
	inventoryContents.gemClear,
	inventoryContents.gemClear,
	inventoryContents.gemClear,
	inventoryContents.gemClear,
	inventoryContents.scrapGoggles,
	inventoryContents.scrapGoggles,
	inventoryContents.scrapGoggles,
	inventoryContents.scrapPickaxe,
	inventoryContents.scrapPickaxe,
	};
	}

	public bool checkEquipment(item it)
	{
		/* //! tmp lol
		int rnd = new System.Random().Next(2);
		if (rnd == 0) return true;
		else return false; */

		if (equippedItems.Contains(it))
			return true;
		else return false;
	}

	public void orderInventory(ref List<item> inv)
	{
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
			// Debug.Log($"removed item {it} from inventory");
		}
	}
}
