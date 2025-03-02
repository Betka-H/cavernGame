using System;
using System.Collections.Generic;
using System.IO;
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
		sortInventory(ref allItems);
	}

	public bool checkEquipment(item it)
	{
		//! tmp lol
		if (new System.Random().Next(1) == 0) return true;
		else return false;

		/* if (equippedItems.Contains(it))
			return true;
		else return false; */
	}

	public void sortInventory(ref List<item> inv)
	{
		inv = inv.OrderBy(i => i.itemId).ToList();
	}

	public void printInventory(List<item> inv)
	{
		// sortInventory(inv);

		if (inv.Count > 0)
		{
			Debug.Log("items in inventory: " + string.Join(", ", inv));
			//// bro why dont i use string.join more often this is way easier than setting up a hugeass for loop
		}
		else Debug.Log("there are no items in the inventory");
	}

	void addItem(item it, List<item> inventory)
	{
		inventory.Add(it);
	}
	void removeItem(item it, List<item> inventory)
	{
		if (!inventory.Contains(it))
		{
			Debug.Log($"cannot remove item {it} cause its not in the inventory"); // dumbass     <<< excuse me???
		}
		else
		{
			inventory.Remove(it);
			Debug.Log($"removed item {it} from inventory");
		}
	}

	public void addCaveItem(item it)
	{
		addItem(it, caveInventory);
	}
	public void addLabItem(item it)
	{
		addItem(it, labInventory);
	}

	public void removeCaveItem(item it)
	{
		removeItem(it, caveInventory);
	}
	public void removeLabItem(item it)
	{
		removeItem(it, labInventory);
	}
}
