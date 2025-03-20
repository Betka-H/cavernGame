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
	[HideInInspector] public darknessLevel darknessLvl; // mainly for weather forecast

	private inventoryManager inventory;
	private gameController gameController;

	[HideInInspector] public gameController.level currentLevel;

	// enclosure
	[Space]
	public Transform roomParent;
	public Transform enclosureParent;
	public Transform doorBlockParent;
	[Header("doorway blocks")]
	public GameObject blockL;
	public GameObject blockR;
	[Header("doorway heights")]
	public GameObject enclosure_walls_lab;
	public GameObject enclosure_walls_space;
	public GameObject enclosure_walls_cave;

	missionManager missionManager;
	callManager callManager;

	// rooms
	[Header("rooms")]
	public int minCavernRoomsNr;
	public int maxCavernRoomsNr;
	[Header("rooms // cavern 0 is entry room, lab is already assorted: assign starting room at //choose lab entrance")]
	public roomSO[] labRooms;
	public roomSO[] spaceRooms;
	public room_cavern[] cavernRooms;
	private roomSO entranceRoom;
	[HideInInspector] public roomSO[] selectedRooms;

	private int currentRoomNr;
	private int roomLeft;
	private int roomRight;

	// trader
	[Header("trader")]
	[HideInInspector] public int traderSpawnChance;
	public Transform traderParent;
	public GameObject traderPrefab;
	private room_cavern traderSpawnRoom;
	private npcTrader npcTrader;

	// items
	[Header("items")]
	public Transform itemParent;
	public GameObject itemPrefab;
	private int itemSpawnRate;

	public SpriteRenderer colorBg;

	public bool hasMentionedJumping = true;

	void Start()
	{
		missionManager = FindObjectOfType<missionManager>();
		callManager = FindObjectOfType<callManager>();

		//! temp trader spawn value
		//! place trader into remote locations on maps - 5s trader call delay
		traderSpawnChance = 50;
		//! temp item spawn value
		itemSpawnRate = 50;

		//! temp room amount value
		// maxCavernRoomsNr = cavernRooms.Length;
		minCavernRoomsNr = 8;
		maxCavernRoomsNr = 15;

		darknessOverlay = FindObjectOfType<darknessOL>();
		inventory = FindObjectOfType<inventoryManager>();

		gameController = FindObjectOfType<gameController>();
	}

	// called at end of jump warn call
	public void m_resetPlayerBounciness()
	{
		Debug.LogError($"resetting bounce");
		getPlayerMovement().bounciness = 0;
	}
	// called at end of space call
	public void m_setPlayerBounciness()
	{
		Debug.LogError($"setting bounce");
		getPlayerMovement().bounciness = 7.5f;
	}
	// called at end of some tutorial call
	public void m_mentionJumping()
	{
		FindObjectOfType<announcerManager>().announceMessage($"mentioned jumping", true);
		hasMentionedJumping = true;
	}
	playerMovement getPlayerMovement()
	{
		return FindObjectOfType<playerMovement>();
	}

	public Transform spaceStuff;

	[HideInInspector] public bool labFirst;
	public void generateLevel(gameController.level lvl)
	{
		clearRoom();

		currentLevel = lvl;
		switch (currentLevel)
		{
			case gameController.level.lab:
				clearParent(enclosureParent);
				Instantiate(enclosure_walls_lab, enclosureParent);
				generateLab(labFirst);
				if (labFirst) labFirst = false;
				spaceStuff.gameObject.SetActive(false);
				break;
			case gameController.level.space:
				clearParent(enclosureParent);
				Instantiate(enclosure_walls_space, enclosureParent);
				spaceStuff.gameObject.SetActive(true);
				generateSpace();
				break;
			case gameController.level.cavern:
				spaceStuff.gameObject.SetActive(false);
				clearParent(enclosureParent);
				Instantiate(enclosure_walls_cave, enclosureParent);
				generateCavern();
				chooseDarkness();
				darknessOverlay.setPlayer();
				break;
		}

		insEntranceR();

		void generateSpace()
		{
			entranceRoom = spaceRooms[0];
			selectedRooms = spaceRooms;
		}
		void generateLab(bool first)
		{
			// choose lab entrance
			if (first)
				entranceRoom = labRooms[3]; // normal entrance
			else entranceRoom = labRooms[0]; // elevator entrance

			selectedRooms = labRooms;
		}
		// generates array of unique rooms in random order
		void generateCavern()
		{

			entranceRoom = cavernRooms[0];

			List<room_cavern> roomPool = cavernRooms.ToList();

			System.Random rndForRoom = new System.Random();
			int roomsNr = rndForRoom.Next(minCavernRoomsNr, maxCavernRoomsNr + 1); // choose a random amount of rooms (+1 to include maxRNr)
			selectedRooms = new room_cavern[roomsNr]; // new empty array for that amount

			for (int i = 0; i < roomsNr; i++)
			{
				room_cavern randomRoom = roomPool[rndForRoom.Next(roomPool.Count)]; // choose random room from all
				selectedRooms[i] = randomRoom; // add the room
				roomPool.Remove(randomRoom); // remove the room from the pool
			}

			// fix missing entrance
			if (!selectedRooms.Contains(entranceRoom)) // if the entrance room isnt there already
			{
				// Debug.LogWarning("the impostorrr");
				selectedRooms[rndForRoom.Next(selectedRooms.Length)] = entranceRoom; // insert entrance room instead of one random room
			}

			for (int i = 0; i < selectedRooms.Length; i++)
			{
				selectedRooms[i].orderOnMap = i;
			}

			setDarkRooms();

			setLootAndTheirSpawnLocations();
			chooseTraderSpawnLocation();
		}


		void setDarkRooms()
		{
			setDarknessLvl();

			System.Random rnd = new System.Random();
			foreach (room_cavern room in selectedRooms)
			{
				room.isDark = false;
				// for every room except the entrance room BECAUSE FUCK THE ELEVATOR DOORS
				if (room != entranceRoom)
					if (rnd.Next(100) <= darknessChance)
						room.isDark = true;
			}

			void setDarknessLvl()
			{
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
		}
		void chooseDarkness()
		{
			if (inventory.checkEquipment(inventory.inventoryDefinitions.nightVisionGoggles))
				chosenDarkness = darknessOverlay.darknessOverlayNightVis;
			else chosenDarkness = darknessOverlay.darknessOverlayNormal;
		}

		void setLootAndTheirSpawnLocations()
		{
			foreach (room_cavern room in selectedRooms)
			{
				room.setLoot(itemSpawnRate);
			}
		}
		void chooseTraderSpawnLocation()
		{
			killTrader();

			System.Random rnd = new System.Random();
			if (traderSpawnChance >= rnd.Next(100))
			{
				List<room_cavern> traderSpawnRoomPool = new List<room_cavern>();
				foreach (room_cavern room in selectedRooms) // will have list of all possible spawnpoints
				{
					if (room.isDark && room != entranceRoom) // only spawn trader in dark rooms! thats like its thing AND NOT THE ENTRANCE ROOM. ITS FORBIDDEN THERE. IT ATE THE ELEVATOR CABLES THE LAST TIME
					{
						room.setTraderSpawn();
						traderSpawnRoomPool.Add(room);
					}
				}

				//! for testing
				// forceEntranceTrader();

				if (traderSpawnRoomPool.Count > 0)
				{
					// choose 1 random place to spawn it at
					traderSpawnRoom = traderSpawnRoomPool[rnd.Next(traderSpawnRoomPool.Count)];
					int traderSpawnOrderOnMap = traderSpawnRoom.orderOnMap;

					room_cavern rc = selectedRooms[traderSpawnOrderOnMap] as room_cavern;
					rc.hasTrader = true;

					//create trader
					npcTrader = Instantiate(traderPrefab, traderParent).GetComponent<npcTrader>();
					npcTrader.gameObject.SetActive(false);
				}
				else traderSpawnRoom = null;

#pragma warning disable CS8321 // Local function is declared but never used
				void forceEntranceTrader()
				{
					if (entranceRoom is room_cavern traderEntrRoom)
					{
						traderSpawnRoomPool.Clear();
						traderEntrRoom.setTraderSpawn();
						traderSpawnRoomPool.Add(traderEntrRoom);
					}
				}
#pragma warning restore CS8321 // Local function is declared but never used
			}
			else traderSpawnRoom = null;

		}

		void insEntranceR()
		{
			currentRoomNr = Array.IndexOf(selectedRooms, entranceRoom);
			changeRoom(leftRight.entrance);
		}
	}

	public void clearRoom() //* might be called unnecessarily in gamectrl
	{
		clearParent(itemParent);
		killTrader();
		if (chosenDarkness != null)
			chosenDarkness.gameObject.SetActive(false);
	}

	public void killTrader()
	{
		clearParent(traderParent);

		if (selectedRooms != null)
			foreach (roomSO room in selectedRooms)
				if (room is room_cavern crRoom)
					crRoom.hasTrader = false;
	}

	public enum darknessLevel { light, midLight, midDark, dark };

	public bool m_hasIntroducedTrader = true;

	void clearParent(Transform parent)
	{
		foreach (Transform child in parent.gameObject.GetComponentInChildren<Transform>())
		{
			Destroy(child.gameObject); // destroy current room
		}
	}

	//* change room
	public void changeRoom(leftRight isLeft)
	{
		setLeftAndRightRooms();

		// spawn room
		clearParent(roomParent);
		Instantiate(getRoom().roomPrefab.transform, roomParent);
		// spawn door block
		clearParent(doorBlockParent);
		placeDoorwayBlock();

		switch (currentLevel)
		{
			case gameController.level.lab:
				roomNumberTMP.text = $"lab room {currentRoomNr} / {selectedRooms.Length - 1}";
				break;
			case gameController.level.cavern:
				roomNumberTMP.text = $"cavern room {currentRoomNr} / {selectedRooms.Length - 1} {logCavernRooms()}";
				break;
		}
		colorBg.color = selectedRooms[currentRoomNr].roomBgColor;

		if (selectedRooms[currentRoomNr] is room_cavern cr)
		{
			toggleDarkness(cr);

			// items
			clearParent(itemParent);
			cr.spawnItems(itemPrefab, itemParent);

			// trader
			if (cr.hasTrader)
				summonTrader();
			else if (npcTrader != null)
				npcTrader.gameObject.SetActive(false);
		}
		else if (chosenDarkness != null) chosenDarkness.gameObject.SetActive(false);


		if (missionManager.checkCurrentMission(-1, 5) && currentLevel == gameController.level.cavern && selectedRooms[currentRoomNr] != entranceRoom)
		{
			callManager.startCall(callManager.currentMainMission());
		}

		void setLeftAndRightRooms()
		{
			if (currentRoomNr == 0) // room is first
			{
				roomRight = currentRoomNr + 1;
			}
			else if (currentRoomNr == selectedRooms.Length - 1) // room is last
			{
				roomLeft = currentRoomNr - 1;
			}
			else // room is mid
			{
				roomLeft = currentRoomNr - 1;
				roomRight = currentRoomNr + 1;
			}
		}
		void placeDoorwayBlock()
		{
			GameObject selectedBlock = null;
			if (currentRoomNr == 0)
			{
				selectedBlock = blockL;
			}
			if (currentRoomNr == selectedRooms.Length - 1)
			{
				selectedBlock = blockR;
			}

			if (selectedBlock != null)
				Instantiate(selectedBlock, doorBlockParent);
		}

		string logCavernRooms()
		{
			int i = 0;
			string debug = "";
			foreach (room_cavern room in selectedRooms)
			{
				debug += $"room {room.orderOnMap}";
				if (room == entranceRoom)
					debug += " (ENTRANCE ROOM)";

				if (room.isDark == true)
				{
					debug += " (dark)";
				}
				if (room.hasTrader)
				{
					debug += " (TRADER)";
				}
				debug += ", ";
				i++;
			}
			return $" level rooms ({darknessLvl}):\n{debug}";
		}
		roomSO getRoom() // selects left or right room (i think)
		{
			switch (isLeft)
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
		void toggleDarkness(room_cavern cr)
		{
			if (cr.isDark)
			{
				chosenDarkness.gameObject.SetActive(true);
			}
			else
			{
				chosenDarkness.gameObject.SetActive(false);
			}
		}
		void summonTrader()
		{
			if (PlayerPrefs.GetInt("hasMetTrader", 0) == 0)
			{
				//* change this to trader collision instead
				PlayerPrefs.SetInt("hasMetTrader", 1);
				PlayerPrefs.Save();

				CancelInvoke(nameof(startTraderCall));
				missionManager.traderMission.currentCall = 0;

				Invoke(nameof(startTraderCall), 3.5f);

				m_hasIntroducedTrader = false;
			}
			else m_hasIntroducedTrader = true;


			npcTrader.transform.localPosition = traderSpawnRoom.chosenTraderSpawn.position;
			npcTrader.gameObject.SetActive(true);
		}
	}
	void startTraderCall()
	{
		m_hasIntroducedTrader = true;
		callManager.startCall(missionManager.traderMission);
	}
}