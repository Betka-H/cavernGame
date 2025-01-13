using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class inventoryWrapper
{
	public List<int> itemIds;
}

public class inventory : MonoBehaviour
{
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
}