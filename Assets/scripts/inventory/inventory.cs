using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class inventory : MonoBehaviour
{
	// public Transform slotsParent;
	// List<Transform> slots;

	// [Range(1, 8)]
	// public int slotLimit;

	[HideInInspector]
	public List<item> inventoryItems;
	[HideInInspector]
	public List<item> equippedItems;
	// List<int> inventoryIds;

	[Header("assorted list of all existing items by id")]
	public item[] allItemList;

	private string savePath = "inventory-save.txt";

	void Start()
	{
		loadInventory();

		inventoryItems = allItemList.ToList();
		inventoryItems.Remove(allItemList[8]);
		equippedItems = new List<item> { allItemList[8] };
		// getSlots();
	}

	/* void getSlots()
	{
		slots = slotsParent.GetComponentsInChildren<Transform>().ToList();
		slots.Remove(slotsParent); // fuck yoy
	} */

	public bool checkEquipment(item it)
	{
		// equippedItems.Any(item => inventory.allGearList[item].itemKind == itemKind.gear);
		if (equippedItems.Contains(it))
			return true;
		else return false;
	}

	public void sortInventory()
	{
		// inventoryIds.Sort();
		Debug.LogWarning("sort the inventory!");
	}

	public void saveInventory()
	{
		Debug.LogWarning("sav the inventory!");
		sortInventory();

		// save inv);
		//* File.WriteAllText(savePath, string.Join(",", inventoryIds));

		// Debug.Log("inventory saved");
		// printInventory();
	}
	void loadInventory()
	{
		clearInventory();

		// load inv
		//* string[] invRead = File.ReadAllText(savePath).ToString().Split(',');
		//* foreach (string s in invRead)
		{
			//* inventoryIds.Add(int.Parse(s));
		}

		// Debug.Log("inventory loaded");
		printInventory();
	}
	public void clearInventory()
	{
		// clear inv
		// inventoryIds.Clear();
		inventoryItems.Clear();

		// Debug.LogWarning("inventory cleared; remember to save!");
	}
	public void printInventory()
	{
		sortInventory();

		if (inventoryItems.Count > 0)
		{
			// Debug.Log("items in inventory: " + string.Join(", ", inventoryIds)); // bro why dont i use string.join more often this is way easier than setting up a hugeass for loop
		}
		else { Debug.Log("there are no items in the inventory"); }
	}

	public void addItem(item it)
	{
		// add item
		inventoryItems.Add(it);

		Debug.Log($"added item {it} to inventory");
	}

	public void removeItem(item it)
	{
		// remove item
		if (!inventoryItems.Contains(it))
		{
			Debug.Log($"cannot remove item {it} cause its not in the inventory"); // dumbass
		}
		else
		{
			inventoryItems.Remove(it);
			Debug.Log($"removed item {it} from inventory");
		}
	}
}





/* using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class inventoryWrapper
{
	public List<item> Witems;
}

public class inventory : MonoBehaviour
{
	[Header("assorted list of all existing items by id")]
	public item[] itemList;

	public static List<item> items;

	public Transform slotsParent;
	List<Transform> slots;

	void Start()
	{
		items.Add(itemList[0]);

		getSlots();
		loadInventory();
	}

	void getSlots()
	{
		slots = slotsParent.GetComponentsInChildren<Transform>().ToList();
		slots.Remove(slotsParent); // fuck yoy
	}

	void loadInventory()
	{
		clearInventory();

		string readText = File.ReadAllText("inventory-save.txt");
		inventoryWrapper loadedWrapper = JsonUtility.FromJson<inventoryWrapper>(readText);
		items = loadedWrapper?.Witems ?? new List<item>(); // god bless chagpt i fucking guess

		Debug.Log("inventory loaded");
		printInventory();
	}
	public static void saveInventory()
	{
		File.WriteAllText("inventory-save.txt", JsonUtility.ToJson(items)); // writes the wrapped list (as json)

		Debug.Log("inventory saved");
		printInventory();
	}
	public static void clearInventory()
	{
		;
		Debug.LogWarning("inventory cleared; remember to save!");
	}
	public static void printInventory()
	{
		if (items.Count > 0)
		{
			Debug.Log("items in inventory;: " + string.Join(", ", items)); // bro why dont i use string.join more often this is way easier than setting up a hugeass for loop
		}
		else { Debug.Log("there are no items in the inventory"); }
	}

	public static void addItem(item it)
	{
		items.Add(it);
		Debug.Log($"added item {it.itemName} to inventory");
	}
	public static void removeItem(item it)
	{
		if (!items.Contains(it))
		{
			Debug.Log($"cannot remove item {it.itemName} cause its not there dumbass");
		}
		else
		{
			items.Remove(it);
			Debug.Log($"removed item {it.itemName} from inventory");
		}
	}

} */

/* public void displayItems()
{
	for (int i = 0; i < items.Count; i++)
	{
		// !!!!!!!!!!!!!! items are null becuase they are gettign destryoed
		Debug.Log($"item: {items[i]}, slot: {slots[i]}");
		// GameObject iobj = Instantiate(itemSprites[i].objPrefab, slots[i]);
		// iobj.transform.localScale = new Vector3(3, 3, 0);
	}

	Debug.Log("items: ");
	foreach (Sprite it in items)
	{
		Debug.Log(it + ", ");
	}
}*/

/* 
public List<item> items;
	
	static List<int> itemIds;

	public Transform slotsParent;
	List<Transform> slots;

	void Start()
	{
		getSlots();
		loadInventory();
	}

	void loadInventory()
	{
		itemIds = new List<int>(); // clears inv just in case

		string readText = File.ReadAllText("inventory-save.txt");
		inventoryWrapper loadedWrapper = JsonUtility.FromJson<inventoryWrapper>(readText);
		itemIds = loadedWrapper?.itemIds ?? new List<int>(); // god bless chagpt i fucking guess

		Debug.Log("inventory loaded");
		printInventory();
	}
	public static void saveInventory()
	{
		inventoryWrapper wrapper = new inventoryWrapper { itemIds = itemIds }; // from chatgpt. allows the int list to be json serialised
		File.WriteAllText("inventory-save.txt", JsonUtility.ToJson(wrapper)); // writes the wrapped list (as json)

		Debug.Log("inventory saved");
		printInventory();
	}
	public static void clearInventory()
	{
		itemIds = new List<int>();
		Debug.LogWarning("inventory cleared; remember to save!");
	}
	public static void printInventory()
	{
		Debug.Log("items in inventory;: " + string.Join(", ", itemIds)); // bro why dont i use string.join more often this is way easier than setting up a hugeass for loop
	}

	public static void addItemId(int id)
	{
		itemIds.Add(id);
		Debug.Log($"added id {id} to inventory");
	}
	public static void removeItemId(int id)
	{
		if (!itemIds.Contains(id))
		{
			Debug.Log($"cannot remove id {id} cause its not there dumbass");
		}
		else
		{
			itemIds.Remove(id);
			Debug.Log($"removed id {id} from inventory");
		}
	}

	void getSlots()
	{
		slots = slotsParent.GetComponentsInChildren<Transform>().ToList();
		slots.Remove(slotsParent); // fuck yoy
	}
 */

