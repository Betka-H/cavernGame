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

	void Awake()
	{
		// Debug.Log("starting invenmanager");
		inventoryDefinitions = FindObjectOfType<inventoryDefinitions>();
	}

	void beforeTutorialLabInv()
	{
		Debug.LogError($"li: {labInventory}");

		labInventory = new List<item>
	{
	inventoryDefinitions.gemGreen,
	inventoryDefinitions.scrapShoes,
	};
	}
	public void m_afterTutorialInv()
	{
		Debug.LogWarning("after tutorial inv");
		labInventory = new List<item>
	{
	inventoryDefinitions.shoes,
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
		Debug.LogError($"clearing inventories");
		// labInventory.Clear();
		beforeTutorialLabInv();

		equippedItems.Clear();
		missionInventory.Clear();
	}
}
