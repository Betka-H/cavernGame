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

	[HideInInspector] public Transform player;

	public SpriteRenderer colorBg;

	bool hasMentionedJumping;

	void Start()
	{
		missionManager = FindObjectOfType<missionManager>();
		callManager = FindObjectOfType<callManager>();

		//! temp trader spawn value
		//! place trader into remote locations on maps - 5s trader call delay
		traderSpawnChance = 100;
		//! temp item spawn value
		itemSpawnRate = 100;

		//! temp room amount value
		// maxCavernRoomsNr = cavernRooms.Length;
		minCavernRoomsNr = 5;
		maxCavernRoomsNr = 8;

		darknessOverlay = FindObjectOfType<darknessOL>();
		inventory = FindObjectOfType<inventoryManager>();

		switch (PlayerPrefs.GetInt("jumpWarn", 0))
		{
			case 0:
				m_hasWarnedJump = false;
				break;
			case 1:
				m_hasWarnedJump = true;
				getPlayerMovement().bounciness = 0;
				break;
		}

		gameController = FindObjectOfType<gameController>();
	}

	bool m_hasWarnedJump = false;
	void Update()
	{
		if (!m_hasWarnedJump && hasMentionedJumping)
		{
			if (currentLevel == gameController.level.lab && getPlayerMovement().transform.GetComponent<Rigidbody2D>().velocity.y > 0)
			{
				Invoke("startJumpCall", 5f);
				m_hasWarnedJump = true;
			}
		}
	}
	public void startJumpCall()
	{
		if (currentLevel == gameController.level.lab) // but only if theyre still in the lab
		{
			Debug.LogWarning("still in lab");
			callManager.startCall(missionManager.jumpMission);

			getPlayerMovement().bounciness = 0;
			PlayerPrefs.SetInt("jumpWarn", 1);
		}
		else m_hasWarnedJump = true;
	}
	public void m_mentionJumping()
	{
		FindObjectOfType<announcerManager>().announceMessage($"mentioned jumping", true);
		hasMentionedJumping = true;
	}
	playerMovement getPlayerMovement()
	{
		return FindObjectOfType<playerMovement>();
	}

	[HideInInspector] public bool labFirst;
	public void generateLevel(gameController.level lvl)
	{
		Debug.LogWarning($"=============================... generating new level ({lvl})...=======================================");
		clearRoom();

		currentLevel = lvl;
		switch (currentLevel)
		{
			case gameController.level.lab:
				clearParent(enclosureParent);
				Instantiate(enclosure_walls_lab, enclosureParent);
				generateLab(labFirst);
				if (labFirst) labFirst = false;
				break;
			case gameController.level.space:
				clearParent(enclosureParent);
				Instantiate(enclosure_walls_space, enclosureParent);
				generateSpace();
				break;
			case gameController.level.cavern:
				clearParent(enclosureParent);
				Instantiate(enclosure_walls_cave, enclosureParent);
				generateCavern();
				chooseDarkness();
				darknessOverlay.setPlayer();
				break;
		}

		insEntranceR();
	}

	public void chooseDarkness()
	{
		if (inventory.checkEquipment(inventory.inventoryDefinitions.nightVisionGoggles))
		{
			chosenDarkness = darknessOverlay.darknessOverlayNV;
		}
		else
		{
			chosenDarkness = darknessOverlay.darknessOverlayNormal;
		}
		// Debug.Log($"darkness chosen: {darknessOverlay}");
	}

	public void clearRoom()
	{
		clearParent(itemParent);
		killTrader();
		if (chosenDarkness != null)
			chosenDarkness.gameObject.SetActive(false);
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
			// else debug += room.name;

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
		// Debug.Log($"({darknessLvl}) level rooms: {debug}");
		return $" level rooms ({darknessLvl}):\n{debug}";
	}

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
	//* generate cavern
	void generateCavern()
	{
		// generates array of unique rooms in random order

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

		chooseItemSpawnLocations();
		chooseTraderSpawnLocation();

		// Debug.Log(logCavernRooms());
	}

	void setDarkRooms()
	{
		setDarknessChance();

		System.Random rnd = new System.Random();
		foreach (room_cavern room in selectedRooms)
		{
			room.isDark = false;
			// if (room != entranceRoom || darknessLvl == darknessLevel.dark) // for every room except the entrance room (cant be dark there) (except for when dl is dark)
			if (room != entranceRoom) // for every room except the entrance room BECAUSE FUCK THE ELEVATOR DOORS
			{
				if (rnd.Next(100) <= darknessChance)
					room.isDark = true;
			}
		}
	}
	void setDarknessChance()
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

	void chooseItemSpawnLocations()
	{
		foreach (room_cavern room in selectedRooms)
		{
			room.setLoot(itemSpawnRate);
		}
	}

	private void chooseTraderSpawnLocation()
	{
		killTrader();

		System.Random rnd = new System.Random();
		if (traderSpawnChance >= rnd.Next(100))
		{
			// Debug.Log("trader will appear (chance)");
			List<room_cavern> traderSpawnRoomPool = new List<room_cavern>();
			foreach (room_cavern room in selectedRooms) // will have list of all possible spawnpoints
			{
				if (room.isDark && room != entranceRoom) // only spawn trader in dark rooms! thats like its thing AND NOT THE ENTRANCE ROOM. ITS FORBIDDEN THERE. IT ATE THE ELEVATOR CABLES THE LAST TIME
				{
					room.setTraderSpawn();
					traderSpawnRoomPool.Add(room);
				}
			}


			//
			//! TEMP FOR TESTING. forces trader to spawn in entrance room
			Debug.LogWarning("trader should spawn elsewhere");
			if (entranceRoom is room_cavern traderEntrRoom)
			{
				traderSpawnRoomPool.Clear();
				traderEntrRoom.setTraderSpawn();
				traderSpawnRoomPool.Add(traderEntrRoom);
			}
			//



			if (traderSpawnRoomPool.Count > 0)
			{
				// choose 1 random place to spawn it at
				traderSpawnRoom = traderSpawnRoomPool[rnd.Next(traderSpawnRoomPool.Count)];
				int traderSpawnOrderOnMap = traderSpawnRoom.orderOnMap;

				room_cavern rc = selectedRooms[traderSpawnOrderOnMap] as room_cavern;
				rc.hasTrader = true;

				//create trader
				npcTrader = Instantiate(traderPrefab, traderParent).GetComponent<npcTrader>();
				// Debug.Log($"spawned trader at room {traderSpawnOrderOnMap}");
				npcTrader.gameObject.SetActive(false);
			}
			else
			{
				// Debug.LogWarning("viable room pool empty. trader will not appear");
				traderSpawnRoom = null;
			}
		}
		else
		{
			// Debug.Log("trader will not appear (chance)");
			traderSpawnRoom = null; // otherwise dont spawn it
		}
	}
	void killTrader()
	{
		FindObjectOfType<announcerManager>().announceMessage($"murdering trader", true);

		clearParent(traderParent);

		/* foreach (room_cavern room in selectedRooms)
		{
			room.hasTrader = false;
		} */
		if (selectedRooms != null)
			foreach (roomSO room in selectedRooms)
				if (room is room_cavern crRoom)
					crRoom.hasTrader = false;
	}
	void summonTrader()
	{
		if (PlayerPrefs.GetInt("hasMetTrader", 0) == 0)
		{
			PlayerPrefs.SetInt("hasMetTrader", 1);

			CancelInvoke("startTraderCall");
			Invoke("startTraderCall", 3.5f);
			m_hasIntroducedTrader = false;
		}

		Debug.LogWarning("summoning trader");
		npcTrader.transform.localPosition = traderSpawnRoom.chosenTraderSpawn.position;
		npcTrader.gameObject.SetActive(true);
	}
	public bool m_hasIntroducedTrader;
	public void startTraderCall()
	{
		m_hasIntroducedTrader = true;
		callManager.startCall(missionManager.traderMission);
	}

	public enum darknessLevel { light, midLight, midDark, dark };
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

	void setLeftAndRightRoomNumbers()
	{
		if (currentRoomNr == 0) // room is first
		{
			roomRight = currentRoomNr + 1;
			// roomLeft = emptyRoom;
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

	void clearParent(Transform parent)
	{
		foreach (Transform child in parent.gameObject.GetComponentInChildren<Transform>())
		{
			Destroy(child.gameObject); // destroy current room
		}
	}

	void insEntranceR()
	{
		currentRoomNr = Array.IndexOf(selectedRooms, entranceRoom);
		changeRoom(leftRight.entrance);
		// roomNumberTMP.text = $"room {currentRoomNr} / {selectedRooms.Length - 1} (ENTRANCE)";
	}
	//* change room
	public void changeRoom(leftRight isLeft)
	{
		setLeftAndRightRoomNumbers();
		clearParent(roomParent);
		clearParent(doorBlockParent);
		Instantiate(getRoom(isLeft).roomPrefab.transform, roomParent);
		switch (currentLevel)
		{
			case gameController.level.lab:
				roomNumberTMP.text = $"lab room {currentRoomNr} / {selectedRooms.Length - 1}";
				break;
			case gameController.level.cavern:
				roomNumberTMP.text = $"cavern room {currentRoomNr} / {selectedRooms.Length - 1} {logCavernRooms()}";
				break;
		}
		placeDoorwayBlock();

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
	}
}