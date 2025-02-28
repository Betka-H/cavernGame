using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class inventory : MonoBehaviour
{
	[HideInInspector]
	public List<item> inventoryItems;
	[HideInInspector]
	public List<item> equippedItems;

	[HideInInspector]
	public inventoryContents inventoryContents;

	/*//* private string savePath = "inventory-save.txt";
	File.WriteAllText(savePath, string.Join(",", inventoryIds));
	string[] invRead = File.ReadAllText(savePath).ToString().Split(',');
	 foreach (string s in invRead)
	{
		inventoryIds.Add(int.Parse(s));
	} */

	void Start()
	{
		inventoryContents = FindObjectOfType<inventoryContents>();
	}

	public bool checkEquipment(item it)
	{
		//! tmp lol
		int rnd = new System.Random().Next(1);
		if (rnd == 0) return true;
		else return false;

		/* if (equippedItems.Contains(it))
			return true;
		else return false; */
	}

	public void sortInventory()
	{
		inventoryItems = inventoryItems.OrderBy(i => i.itemName).ToList();
	}

	public void clearInventory()
	{
		inventoryItems.Clear();
	}
	public void printInventory()
	{
		sortInventory();

		if (inventoryItems.Count > 0)
		{
			Debug.Log("items in inventory: " + string.Join(", ", inventoryItems));
			//// bro why dont i use string.join more often this is way easier than setting up a hugeass for loop
		}
		else { Debug.Log("there are no items in the inventory"); }
	}

	public void addItem(item it)
	{
		inventoryItems.Add(it);
	}
	public void removeItem(item it)
	{
		if (!inventoryItems.Contains(it))
		{
			Debug.Log($"cannot remove item {it} cause its not in the inventory"); // dumbass     <<< excuse me???
		}
		else
		{
			inventoryItems.Remove(it);
			Debug.Log($"removed item {it} from inventory");
		}
	}
}
