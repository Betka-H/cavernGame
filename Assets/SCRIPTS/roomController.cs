using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public enum leftRight { left, right, entrance };

public class roomController : MonoBehaviour
{
	public TMP_Text roomNumberTMP;

	private darknessOL darknessOverlay;
	private Transform chosenDarkness;
	private int darknessChance; // % out of 100
	[HideInInspector]
	public darknessLevel darknessLvl; // mainly for weather forecast
									  // public bool hasNightVision;

	// private playerEquipment playerEquipment;
	private inventory inventory;

	private gameController.level currentLevel;

	[Space]
	public Transform roomParent;
	public Transform enclosureParent;
	[Header("doorway blocks")]
	public GameObject blockL;
	public GameObject blockR;
	[Header("doorway height")]
	public GameObject enclosure_walls_lab;
	public GameObject enclosure_walls_cave;

	[Space]
	public int minCavernRoomsNr;
	public int maxCavernRoomsNr;
	[Header("rooms // cavern 0 is entry room")]
	public roomSO[] labRooms;
	public room_cavern[] cavernRooms;
	private roomSO[] selectedRooms;
	private roomSO entranceRoom;

	[Space]
	[HideInInspector]
	public int traderSpawnChance;
	public Transform traderParent;
	public GameObject traderPrefab;
	// private Transform traderSpawnPoint;
	private room_cavern traderSpawnRoom;
	private npcTrader npcTrader;

	public Transform itemParent;
	public GameObject itemPrefab;
	private int itemSpawnRate;

	private int currentRoomNr;
	private int roomLeft;
	private int roomRight;

	void Start()
	{
		//! temp value
		Debug.LogWarning("temp trader spawn chance");
		traderSpawnChance = 50;
		//! temp value
		Debug.LogWarning("temp item spawn rate");
		itemSpawnRate = 50;

		//! temp value. revisit later
		Debug.LogWarning("temp max room amount");
		maxCavernRoomsNr = cavernRooms.Length;

		darknessOverlay = FindObjectOfType<darknessOL>();
		// playerEquipment = FindObjectOfType<playerEquipment>();
		inventory = FindObjectOfType<inventory>();
		// chooseDarkness();
	}

	public void generateLevel(gameController.level lvl)
	{
		Debug.Log("=============================... generating new level ...=======================================");

		currentLevel = lvl;

		switch (currentLevel)
		{
			case gameController.level.lab:
				Instantiate(enclosure_walls_lab, enclosureParent);
				generateLab();
				break;
			case gameController.level.cavern:
				Instantiate(enclosure_walls_cave, enclosureParent);
				generateCavern();
				break;
		}

		// hideDarkness();

		chooseDarkness();
		insEntranceR();
		// toggleDarkness();

		// logRooms();
	}

	void logCavernRooms()
	{
		int i = 0;
		string debug = "";
		foreach (room_cavern room in selectedRooms)
		{
			debug += $"{room.orderOnMap}: ";
			if (room == entranceRoom)
				debug += "ENTRANCE ROOM";
			else debug += room.name;

			if (room.isDark == true)
			{
				debug += " (dark)";
			}
			if (room.hasTrader)
			{
				// debug += " (T)";
			}
			debug += ", ";
			i++;
		}
		Debug.Log($"({darknessLvl}) level rooms: {debug}");
	}

	void generateLab()
	{
		entranceRoom = labRooms[2];
		selectedRooms = labRooms;
	}
	void generateCavern()
	{
		// clear console :bwomp:

		// generates array of unique rooms in random order

		entranceRoom = cavernRooms[0]; // determines entrance room

		List<room_cavern> roomPool = cavernRooms.ToList();

		System.Random rndForRoom = new System.Random();
		int roomsNr = rndForRoom.Next(minCavernRoomsNr, maxCavernRoomsNr + 1); // choose a random amount of rooms (+1 to include maxRNr)
		selectedRooms = new room_cavern[roomsNr]; // new empty array for that amount

		for (int i = 0; i < roomsNr; i++)
		{
			room_cavern randomRoom = roomPool[rndForRoom.Next(roomPool.Count())]; // choose random room from all
																				  // randomRoom.roomPrefab.GetComponent<roomObj>().orderOnMap = i;
			selectedRooms[i] = randomRoom; // add the room
			roomPool.Remove(randomRoom); // remove the room from the pool
		}

		// fix missing entrance
		if (!selectedRooms.Contains(entranceRoom)) // if the entrance room isnt there already
		{
			selectedRooms[rndForRoom.Next(selectedRooms.Length)] = entranceRoom; // insert entrance room instead of one random room
																				 // Debug.LogWarning("the impostorrr");
		}

		for (int i = 0; i < selectedRooms.Length; i++)
		{
			selectedRooms[i].orderOnMap = i;
		}

		chooseDarkRooms();

		logCavernRooms();

		chooseItemSpawnLocations();
		chooseTraderSpawnLocation();
	}

	void chooseItemSpawnLocations()
	{
		foreach (room_cavern room in selectedRooms)
		{
			room.setLoot(itemSpawnRate);
		}
	}
	/* void spawnItems(room_cavern room)
	{
		if (room.chosenLootSpawnLocations.Count() > 0)
		{
			for (int i = 0; i < room.chosenLootSpawnLocations.Count(); i++)
			{
				spawnItem();
			}
		}
	}
	void spawnItem()
	{
		worldItem spawnedItem = Instantiate(itemPrefab, itemParent).GetComponent<worldItem>();
		spawnedItem.assignedRoomSO = room;
		spawnedItem.updateItem(room.chosenLoot[i]);
		spawnedItem.assignedSpawnTransform = room.chosenLootSpawnLocations[i];
		if (spawnedItem.checkItem() != null)
		{
			spawnedItem.gameObject.transform.localPosition = spawnedItem.assignedSpawnTransform.position;
		}
		else Destroy(spawnedItem);
	} */

	private void chooseTraderSpawnLocation()
	{
		killTrader();

		System.Random rnd = new System.Random();
		if (traderSpawnChance >= rnd.Next(100))
		{
			// Debug.Log("trader will appear (chance)");
			// Debug.Log("attempting to spawn trader");
			List<room_cavern> traderSpawnRooms = new List<room_cavern>();
			foreach (room_cavern room in selectedRooms) // will have list of all possible spawnpoints
			{
				if (room.isDark && room != entranceRoom) // only spawn trader in dark rooms! thats like its thing AND NOT THE ENTRANCE ROOM. ITS FORBIDDEN THERE. IT ATE THE ELEVATOR CABLES THE LAST TIME
				{
					// rnd = new System.Random();
					room.setTraderSpawn();
					traderSpawnRooms.Add(room);
					// Debug.Log($"adding point {traderSpawnpoints[traderSpawnpoints.Count - 1]} to trader spawn pool");
				}
			}


			//
			/* //! TEMP FOR TESTING. forces trader to spawn in entrance room
			Debug.LogWarning("trader should be spawned elsewhere");
			traderSpawnRooms.Clear();
			entranceRoom.getTraderSpawnLocation();
			traderSpawnRooms.Add(entranceRoom); */
			//



			if (traderSpawnRooms.Count > 0)
			{
				// Debug.Log($"the spawnpoint pool is: {string.Join(", ", traderSpawnRooms)}");

				// choose 1 random place to spawn it at
				traderSpawnRoom = traderSpawnRooms[rnd.Next(traderSpawnRooms.Count())];

				int traderSpawnOrderOnMap = traderSpawnRoom.orderOnMap;

				room_cavern rc = selectedRooms[traderSpawnOrderOnMap] as room_cavern;
				rc.hasTrader = true;

				//create trader
				npcTrader = Instantiate(traderPrefab, traderParent).GetComponent<npcTrader>();
				Debug.LogWarning($"spawned trader at room {traderSpawnOrderOnMap}");
				npcTrader.gameObject.SetActive(false);
			}
			else
			{
				Debug.LogWarning("viable room pool empty. trader will not appear");
				traderSpawnRoom = null;
			}
		}
		else
		{
			Debug.Log("trader will not appear (chance)");
			traderSpawnRoom = null; // otherwise dont spawn it
		}
	}
	void spawnTrader()
	{
		// Debug.Log("this room has the trader");
		npcTrader.transform.localPosition = traderSpawnRoom.chosenTraderSpawn.position;
		npcTrader.gameObject.SetActive(true);
	}
	void killTrader()
	{
		foreach (Transform child in traderParent.gameObject.GetComponentInChildren<Transform>())
		//! the thing is also considered a child so only delete the children on the children if that makes sense (no it does not. fucj the naming on this one)
		{
			Destroy(child.gameObject);
		}

		foreach (room_cavern room in selectedRooms)
		{
			room.hasTrader = false;
		}
	}

	void hideDarkness()
	{
		darknessOverlay.darknessOverlayNormal.gameObject.SetActive(false);
		darknessOverlay.darknessOverlayNV.gameObject.SetActive(false);
	}
	public void chooseDarkness()
	{
		// Debug.LogWarning("choosing darkness");
		// if (playerEquipment.checkEquipment(1)) // nv goggles
		// Debug.LogWarning("temp item lookup");
		// Debug.LogWarning("item lookup?");
		// if (inventory.equippedItems.Count > 0 && inventory.checkEquipment(inventory.inventoryItems
		// [8])) // nv goggles
		if (inventory.checkEquipment(inventory.inventoryContents.nightVisionGoggles))
		{
			chosenDarkness = darknessOverlay.darknessOverlayNV;
			// chosenDarkness = darknessOverlay.darknessOverlayNormal;
		}
		else
		{
			//* just gets rid of the warning. this stays. ==> 
			chosenDarkness = darknessOverlay.darknessOverlayNormal;
		}
		// Debug.Log("darkness chosen: " + darknessOverlay);
	}
	public enum darknessLevel { light, midLight, midDark, dark };
	void setDarknessChance()
	{
		// chooseDarkness();

		System.Random rnd = new System.Random();
		int n = rnd.Next(100);
		if (n <= 10)
		{
			darknessLvl = darknessLevel.light;
			darknessChance = 0;
			// Debug.Log("light shines through the dungeon (light)");
		}
		else if (n <= 20)
		{
			darknessLvl = darknessLevel.dark;
			darknessChance = 100;
			// Debug.Log("dungeon is drowning in darkness (dark)");
		}
		else if (n <= 50)
		{
			darknessLvl = darknessLevel.midDark;
			darknessChance = 69;
			// Debug.Log("its kinda dark in the dungeon (midDark)");
		}
		else
		{
			darknessLvl = darknessLevel.midLight;
			darknessChance = 21;
			// Debug.Log("its kinda light in the dungeon (midLight)");
		}
	}
	void chooseDarkRooms()
	{
		setDarknessChance();

		System.Random rnd = new System.Random();
		foreach (room_cavern room in selectedRooms)
		{
			room.isDark = false;
			if (room != entranceRoom || darknessLvl == darknessLevel.dark) // for every room except the entrance room (cant be dark there) (except for when dl is dark)
			{
				if (rnd.Next(100) <= darknessChance)
					room.isDark = true;
			}
		}
	}
	void toggleDarkness(room_cavern cr)
	{
		// Debug.Log(darknessOverlay);
		if (cr.isDark)
		{
			// darknessOverlay.gameObject.SetActive(true);
			chosenDarkness.gameObject.SetActive(true);
		}
		else
		{
			// darknessOverlay.gameObject.SetActive(false);
			chosenDarkness.gameObject.SetActive(false);
		}
		// Debug.Log($"chosen darkness: {chosenDarkness}");
	}

	void setLeftAndRightRoomNumbers()
	{
		if (currentRoomNr == 0) // room is first
		{
			// roomLeft = emptyRoom;
			roomRight = currentRoomNr + 1;
		}
		else if (currentRoomNr == selectedRooms.Length - 1) // room is last
		{
			roomLeft = currentRoomNr - 1;
			// roomRight = emptyRoom;
		}
		else // room is mid
		{
			roomLeft = currentRoomNr - 1;
			roomRight = currentRoomNr + 1;
		}
	}

	roomSO getRoom(leftRight lr)
	{
		switch (lr)
		{
			case leftRight.left:
				currentRoomNr--;
				return selectedRooms[roomLeft];
			case leftRight.right:
				currentRoomNr++;
				return selectedRooms[roomRight];
			case leftRight.entrance:
				return entranceRoom;
			default:
				Debug.LogError("wrong lr");
				return null;
		}
	}

	void placeDoorwayBlock()
	{
		if (currentRoomNr == 0)
		{
			Instantiate(blockL, roomParent);
		}
		if (currentRoomNr == selectedRooms.Length - 1)
		{
			Instantiate(blockR, roomParent);
		}
	}

	void destroyRoom()
	{
		foreach (Transform child in roomParent.gameObject.GetComponentInChildren<Transform>())
		//! the thing is also considered a child so only delete the children on the children if that makes sense (no it does not. fucj the naming on this one)
		{
			Destroy(child.gameObject); // destroy current room
		}
	}
	void destroyItems()
	{
		foreach (Transform child in itemParent.gameObject.GetComponentInChildren<Transform>())
		//! the thing is also considered a child so only delete the children on the children if that makes sense (no it does not. fucj the naming on this one)
		{
			Destroy(child.gameObject); // destroy current room
		}
	}

	void insEntranceR()
	{
		// destroyRoom();
		// Instantiate(entranceRoom.roomPrefab, roomParent);
		// getBlock();
		currentRoomNr = Array.IndexOf(selectedRooms, entranceRoom);
		roomNumberTMP.text = $"room {currentRoomNr} / {selectedRooms.Length - 1} (ENTRANCE)";

		changeRoom(leftRight.entrance);
	}
	public void changeRoom(leftRight isLeft)
	{
		setLeftAndRightRoomNumbers();
		destroyRoom();
		Instantiate(getRoom(isLeft).roomPrefab.transform, roomParent);
		roomNumberTMP.text = $"room {currentRoomNr} / {selectedRooms.Length - 1}";
		placeDoorwayBlock();

		if (selectedRooms[currentRoomNr] is room_cavern cr)
		{
			toggleDarkness(cr);

			// items
			destroyItems();
			cr.spawnItems(itemPrefab, itemParent);

			// trader
			if (cr.hasTrader)
				spawnTrader();
			else if (npcTrader != null)
				npcTrader.gameObject.SetActive(false);
		}
	}
}