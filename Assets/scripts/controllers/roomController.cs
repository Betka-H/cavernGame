using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class roomController : MonoBehaviour
{
	private darknessOL darknessOverlay;
	private Transform chosenDarkness;
	public bool hasNightVision;
	private int darknessChance; // % out of 100
	[HideInInspector]
	public darknessLevel darknessLvl; // mainly for weather forecast

	private playerEquipment playerEquipment;

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
	public roomSO[] cavernRooms;
	private roomSO[] selectedRooms;
	private roomSO entranceRoom;

	[Space]
	[HideInInspector]
	public int traderSpawnChance;
	public Transform traderParent;
	public GameObject traderPrefab;
	private Transform traderSpawnPoint;
	private npcTrader npcTrader;

	private int currentRoom;
	private int roomLeft;
	private int roomRight;

	void Start()
	{
		traderSpawnChance = 100;

		//! temp value. revisit later
		maxCavernRoomsNr = cavernRooms.Length;
		entranceRoom = cavernRooms[0]; // determines entrance room

		darknessOverlay = FindObjectOfType<darknessOL>();
		playerEquipment = FindObjectOfType<playerEquipment>();
		// chooseDarkness();
	}

	public void generateLevel(gameController.level lvl)
	{
		switch (lvl)
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

		insEntranceR();
		getBlock();

		toggleDarkness();

		logRooms();
	}

	void logRooms()
	{
		int i = 0;
		string debug = "";
		foreach (roomSO room in selectedRooms)
		{
			debug += $"{i}: ";
			if (room == entranceRoom)
				debug += "ENTRANCE ROOM";
			else debug += room.name;

			if (room.isDark == true)
			{
				debug += " (dark)";
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
		// generates array of unique rooms in random order

		List<roomSO> roomPool = cavernRooms.ToList();

		System.Random rndForRoom = new System.Random();
		int roomsNr = rndForRoom.Next(minCavernRoomsNr, maxCavernRoomsNr + 1); // choose a random amount of rooms (+1 to include maxRNr)
		selectedRooms = new roomSO[roomsNr]; // new empty array for that amount

		for (int i = 0; i < roomsNr; i++)
		{
			roomSO randomRoom = roomPool[rndForRoom.Next(roomPool.Count())]; // choose random room from all
			randomRoom.roomPrefab.GetComponent<roomObj>().orderOnMap = i;
			selectedRooms[i] = randomRoom; // add the room
			roomPool.Remove(randomRoom); // remove the room from the pool
		}

		// fix missing entrance
		if (!selectedRooms.Contains(entranceRoom)) // if the entrance room isnt there already
			selectedRooms[rndForRoom.Next(selectedRooms.Length)] = entranceRoom; // insert entrance room instead of one random room

		chooseDarkRooms();
		chooseItemSpawnLocations();
		chooseTraderSpawnLocation();
	}

	void chooseItemSpawnLocations()
	{
		foreach (roomSO room in selectedRooms)
		{
			room.setItemSpawnLocations();
		}
	}
	private void chooseTraderSpawnLocation()
	{
		killTrader();

		System.Random rnd = new System.Random();
		if (traderSpawnChance >= rnd.Next(100))
		{
			// Debug.Log("attempting to spawn trader");
			List<Transform> traderSpawnpoints = new List<Transform>();
			foreach (roomSO room in selectedRooms) // will have list of all possible spawnpoints
			{
				if (room.isDark) // only spawn trader in dark rooms! thats like its thing
				{
					// rnd = new System.Random();
					traderSpawnpoints.Add(room.getTraderSpawnLocation());
					// Debug.Log($"adding point {traderSpawnpoints[traderSpawnpoints.Count - 1]} to trader spawn pool");
				}
			}

			if (traderSpawnpoints.Count > 0)
			{
				// rnd = new System.Random();
				// Debug.Log($"the spawnpoint pool is: {string.Join(", ", traderSpawnpoints)}");
				traderSpawnPoint = traderSpawnpoints[rnd.Next(traderSpawnpoints.Count())]; // choose 1 random place to spawn it at
				npcTrader = Instantiate(traderPrefab, traderParent).GetComponent<npcTrader>();
				// npcTrader.transform.SetParent(traderParent);
				// Debug.Log($"spawned trader at room {Array.IndexOf(selectedRooms, selectedRooms.ToList().Find(x => x.hasTrader == true))}");
				// Debug.Log($"spawned trader at room {traderSpawnPoint.parent.parent.name}");
				selectedRooms[traderSpawnPoint.parent.parent.GetComponent<roomObj>().orderOnMap].hasTrader = true;
				Debug.Log($"spawned trader at room {traderSpawnPoint.parent.parent.GetComponent<roomObj>().orderOnMap}");
				npcTrader.gameObject.SetActive(false);
			}
			else
			{
				Debug.Log("viable room pool empty. trader will not appear");
				traderSpawnPoint = null;
			}
		}
		else
		{
			Debug.Log("trader will not appear");
			traderSpawnPoint = null; // otherwise dont spawn it
		}
	}
	void spawnTrader()
	{
		if (selectedRooms[currentRoom].hasTrader)
		{
			Debug.Log("this room has the trader");
			npcTrader.transform.localPosition = traderSpawnPoint.position;
			npcTrader.gameObject.SetActive(true);
		}
		else if (npcTrader.gameObject.activeSelf)
		{
			npcTrader.gameObject.SetActive(false);
		}
	}
	void killTrader()
	{
		foreach (Transform child in traderParent.gameObject.GetComponentInChildren<Transform>())
		//! the thing is also considered a child so only delete the children on the children if that makes sense (no it does not. fucj the naming on this one)
		{
			Destroy(child.gameObject);
		}
	}

	void hideDarkness()
	{
		darknessOverlay.darknessOverlayNormal.gameObject.SetActive(false);
		darknessOverlay.darknessOverlayNV.gameObject.SetActive(false);
	}
	public void chooseDarkness()
	{
		if (playerEquipment.checkEquipment(1)) // nv goggles
		{
			chosenDarkness = darknessOverlay.darknessOverlayNV;
			// chosenDarkness = darknessOverlay.darknessOverlayNormal;
		}
		else
		{
			chosenDarkness = darknessOverlay.darknessOverlayNormal;
		}
		// Debug.Log("darkness chosen: " + darknessOverlay);
	}
	public enum darknessLevel { light, midLight, midDark, dark };
	void setDarkness()
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
		setDarkness();

		System.Random rnd = new System.Random();
		foreach (roomSO room in selectedRooms)
		{
			room.isDark = false;
			if (room != entranceRoom || darknessLvl == darknessLevel.dark) // for every room except the entrance room (cant be dark there) (except for when dl is dark)
			{
				if (rnd.Next(100) <= darknessChance)
					room.isDark = true;
			}
		}
	}
	void toggleDarkness()
	{
		// Debug.Log(darknessOverlay);
		if (selectedRooms[currentRoom].isDark)
		{
			darknessOverlay.gameObject.SetActive(true);
			chosenDarkness.gameObject.SetActive(true);
		}
		else
		{
			darknessOverlay.gameObject.SetActive(false);
			chosenDarkness.gameObject.SetActive(false);
		}
	}

	void insEntranceR()
	{
		destroyRoom();
		Instantiate(entranceRoom.roomPrefab, roomParent);
		currentRoom = Array.IndexOf(selectedRooms, entranceRoom);
	}

	void setLR()
	{
		if (currentRoom == 0) // room is first
		{
			// roomLeft = emptyRoom;
			roomRight = currentRoom + 1;
		}
		else if (currentRoom == selectedRooms.Length - 1) // room is last
		{
			roomLeft = currentRoom - 1;
			// roomRight = emptyRoom;
		}
		else // room is mid
		{
			roomLeft = currentRoom - 1;
			roomRight = currentRoom + 1;
		}
	}

	roomSO getRoom(bool lr)
	{
		switch (lr)
		{
			case true:
				currentRoom--;
				return selectedRooms[roomLeft];
			case false:
				currentRoom++;
				return selectedRooms[roomRight];
		}
	}

	void getBlock()
	{
		if (currentRoom == 0)
		{
			Instantiate(blockL, roomParent);
		}
		if (currentRoom == selectedRooms.Length - 1)
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

	public void changeRoom(bool isLeft)
	{
		setLR();
		destroyRoom();
		Instantiate(getRoom(isLeft).roomPrefab.transform, roomParent);
		getBlock();
		toggleDarkness();
		spawnTrader();
	}
}