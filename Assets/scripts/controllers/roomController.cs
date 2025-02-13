using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class roomController : MonoBehaviour
{
	public TMP_Text roomNumberTMP;

	private darknessOL darknessOverlay;
	private Transform chosenDarkness;
	private int darknessChance; // % out of 100
	[HideInInspector]
	public darknessLevel darknessLvl; // mainly for weather forecast
	public bool hasNightVision;

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
	// private Transform traderSpawnPoint;
	private roomSO traderSpawnRoom;
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
		Debug.Log("====================================================================");

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

		toggleDarkness();

		// logRooms();
	}

	void logRooms()
	{
		int i = 0;
		string debug = "";
		foreach (roomSO room in selectedRooms)
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
		//!===============================================================
		// clear console :bwomp:

		// generates array of unique rooms in random order

		List<roomSO> roomPool = cavernRooms.ToList();

		System.Random rndForRoom = new System.Random();
		int roomsNr = rndForRoom.Next(minCavernRoomsNr, maxCavernRoomsNr + 1); // choose a random amount of rooms (+1 to include maxRNr)
		selectedRooms = new roomSO[roomsNr]; // new empty array for that amount

		for (int i = 0; i < roomsNr; i++)
		{
			roomSO randomRoom = roomPool[rndForRoom.Next(roomPool.Count())]; // choose random room from all
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

		logRooms();

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
			Debug.Log("trader will appear (chance)");
			// Debug.Log("attempting to spawn trader");
			List<roomSO> traderSpawnRooms = new List<roomSO>();
			foreach (roomSO room in selectedRooms) // will have list of all possible spawnpoints
			{
				if (room.isDark) // only spawn trader in dark rooms! thats like its thing AND NOT THE ENTRANCE ROOM. ITS FORBIDDEN THERE. IT ATE THE ELEVATOR CABLES THE LAST TIME
				{
					// rnd = new System.Random();
					room.getTraderSpawnLocation();
					traderSpawnRooms.Add(room);
					// Debug.Log($"adding point {traderSpawnpoints[traderSpawnpoints.Count - 1]} to trader spawn pool");
				}
			}

			if (traderSpawnRooms.Count > 0)
			{
				// Debug.Log($"the spawnpoint pool is: {string.Join(", ", traderSpawnpoints)}");

				// choose 1 random place to spawn it at
				traderSpawnRoom = traderSpawnRooms[rnd.Next(traderSpawnRooms.Count())];

				int traderSpawnOrderOnMap = traderSpawnRoom.orderOnMap;
				selectedRooms[traderSpawnOrderOnMap].hasTrader = true;

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
		Debug.Log("this room has the trader");
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

		foreach (roomSO room in selectedRooms)
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
		getBlock();
		roomNumberTMP.text = $"room {currentRoom} / {selectedRooms.Length - 1} (ENTRANCE)";
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
		roomNumberTMP.text = $"room {currentRoom} / {selectedRooms.Length - 1}";
		getBlock();
		toggleDarkness();
		if (selectedRooms[currentRoom].hasTrader)
			spawnTrader();
		else if (npcTrader != null)
			npcTrader.gameObject.SetActive(false);
	}
}