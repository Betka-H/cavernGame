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
		labInventory = new List<item>
		{
			inventoryDefinitions.scrapShoes
		};
		labInventory.AddRange(inventoryDefinitions.scrapShoes.wholeGear.cost);
	}
	public void m_afterTutorialInv()
	{
		Debug.LogWarning("after tutorial inv");
		labInventory = new List<item>
		{
		inventoryDefinitions.shoes,
		inventoryDefinitions.shoes,
		inventoryDefinitions.shoes,
		inventoryDefinitions.shoes,
		inventoryDefinitions.shoes,
		inventoryDefinitions.nightVisionGoggles,
		inventoryDefinitions.nightVisionGoggles,
		inventoryDefinitions.nightVisionGoggles,
		inventoryDefinitions.nightVisionGoggles,
		inventoryDefinitions.nightVisionGoggles,
		inventoryDefinitions.nightVisionGoggles,
		inventoryDefinitions.nightVisionGoggles,
		inventoryDefinitions.nightVisionGoggles,
		inventoryDefinitions.nightVisionGoggles,
		inventoryDefinitions.nightVisionGoggles,
		inventoryDefinitions.nightVisionGoggles,
		inventoryDefinitions.nightVisionGoggles,
		inventoryDefinitions.nightVisionGoggles,
		inventoryDefinitions.nightVisionGoggles,
		inventoryDefinitions.nightVisionGoggles,
		inventoryDefinitions.nightVisionGoggles,
		inventoryDefinitions.nightVisionGoggles,
		inventoryDefinitions.nightVisionGoggles,
		inventoryDefinitions.nightVisionGoggles,
		inventoryDefinitions.nightVisionGoggles,
		inventoryDefinitions.nightVisionGoggles,
		inventoryDefinitions.scrapGoggles,
		inventoryDefinitions.scrapGoggles,
		inventoryDefinitions.scrapGoggles,
		inventoryDefinitions.scrapGoggles,
		inventoryDefinitions.scrapGoggles,
		inventoryDefinitions.scrapGoggles,
		inventoryDefinitions.scrapGoggles,
		inventoryDefinitions.scrapGoggles,
		inventoryDefinitions.scrapGoggles,
		inventoryDefinitions.scrapGoggles,
		inventoryDefinitions.scrapGoggles,
		inventoryDefinitions.scrapGoggles,
		inventoryDefinitions.scrapGoggles,
		inventoryDefinitions.scrapGoggles,
		inventoryDefinitions.scrapGoggles,
		inventoryDefinitions.scrapGoggles,
		inventoryDefinitions.scrapGoggles,
		inventoryDefinitions.scrapGoggles,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemGreen,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemRed,
		inventoryDefinitions.gemBlue,
		inventoryDefinitions.gemBlue,
		inventoryDefinitions.gemBlue,
		inventoryDefinitions.gemBlue,
		inventoryDefinitions.gemBlue,
		inventoryDefinitions.gemBlue,
		inventoryDefinitions.gemBlue,
		inventoryDefinitions.gemBlue,
		inventoryDefinitions.gemBlue,
		};


		/* labInventory = new List<item>
		{
			inventoryDefinitions.shoes
		}; */
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

	void OnApplicationQuit()
	{
		// labInventory.AddRange(equippedItems);
		equippedItems.Clear();
	}
}
