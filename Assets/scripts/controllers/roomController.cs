using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class roomController : MonoBehaviour
{
	public Transform roomParent;
	public Transform enclosureParent;
	[Header("doorway blocks")]
	public GameObject blockL;
	public GameObject blockR;
	[Header("doorway height")]
	public GameObject enclosure_walls_lab;
	public GameObject enclosure_walls_cave;

	[Header("rooms // cavern 0 is entry room")]
	public GameObject[] labRooms;
	public GameObject[] cavernRooms;
	private GameObject[] selectedRooms;
	private int minRoomsNr = 3; // 3
	private int maxRoomsNr = 5; // 7
	private GameObject entranceRoom;

	private int currentRoom;
	private int roomLeft;
	private int roomRight;

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

		insEntranceR();
		getBlock();

		//* log
		string debug = "";
		foreach (GameObject obj in selectedRooms)
		{
			debug += obj.name + ", ";
		}
		Debug.Log("level rooms: " + debug);
	}

	void generateLab()
	{
		entranceRoom = labRooms[2];
		selectedRooms = labRooms;
	}
	void generateCavern()
	{
		// generates array of unique rooms in random order
		entranceRoom = cavernRooms[0];

		List<GameObject> roomPool = cavernRooms.ToList();

		System.Random rnd = new System.Random();
		int roomsNr = rnd.Next(minRoomsNr, maxRoomsNr + 1); // choose a random amount of rooms (+1 to include maxRNr)
		selectedRooms = new GameObject[roomsNr]; // new empty array for that amount

		for (int i = 0; i < roomsNr; i++)
		{
			GameObject randomRoom = roomPool[rnd.Next(roomPool.Count())]; // choose random room from all
			selectedRooms[i] = randomRoom; // add the room
			roomPool.Remove(randomRoom); // remove the room from the pool
		}

		if (!selectedRooms.Contains(entranceRoom)) // if the entrance room isnt there already
			selectedRooms[rnd.Next(selectedRooms.Length)] = entranceRoom; // insert entrance room instead of one random room
	}

	void insEntranceR()
	{
		destroyRoom();
		Instantiate(entranceRoom, roomParent);
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

	GameObject getRoom(bool lr)
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
		Instantiate(getRoom(isLeft).transform, roomParent);
		getBlock();
	}
}

/* 

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class roomController : MonoBehaviour
{
	public Transform roomParent;
	public Transform enclosureParent;
	[Header("doorway blocks")]
	public GameObject blockL;
	public GameObject blockR;
	[Header("doorway height")]
	public GameObject enclosure_walls_lab;
	public GameObject enclosure_walls_cave;

	[Header("rooms // cavern 0 is entry room")]
	public GameObject[] labRooms;
	public GameObject[] cavernRooms;
	private GameObject[] selectedRooms;
	private int minRoomsNr = 3; // 3
	private int maxRoomsNr = 5; // 7
	private GameObject entranceRoom;

	private int currentRoom;
	private int roomLeft;
	private int roomRight;

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

		destroyRoom();
		spawnRooms();
		insEntranceR();
		getBlock();

		//* log
		string debug = "";
		foreach (GameObject obj in selectedRooms)
		{
			debug += obj.name + ", ";
		}
		Debug.Log("level rooms: " + debug);
	}

	void generateLab()
	{
		entranceRoom = labRooms[2];
		selectedRooms = labRooms;
	}
	void generateCavern()
	{
		// generates array of unique rooms in random order
		entranceRoom = cavernRooms[0];

		List<GameObject> roomPool = cavernRooms.ToList();

		System.Random rnd = new System.Random();
		int roomsNr = rnd.Next(minRoomsNr, maxRoomsNr + 1); // choose a random amount of rooms (+1 to include maxRNr)
		selectedRooms = new GameObject[roomsNr]; // new empty array for that amount

		for (int i = 0; i < roomsNr; i++)
		{
			GameObject randomRoom = roomPool[rnd.Next(roomPool.Count())]; // choose random room from all
			selectedRooms[i] = randomRoom; // add the room
			roomPool.Remove(randomRoom); // remove the room from the pool
		}

		if (!selectedRooms.Contains(entranceRoom)) // if the entrance room isnt there already
			selectedRooms[rnd.Next(selectedRooms.Length)] = entranceRoom; // insert entrance room instead of one random room
	}

	void insEntranceR()
	{
		entranceRoom.SetActive(true);
		// Instantiate(entranceRoom, roomParent);
		// spawnRooms();
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

	int getRoom(bool lr)
	{
		switch (lr)
		{
			case true:
				currentRoom--;
				return roomLeft;
			case false:
				currentRoom++;
				return roomRight;
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
	void hideRoom()
	{
		foreach (Transform child in roomParent.gameObject.GetComponentInChildren<Transform>())
		//! the thing is also considered a child so only delete the children on the children if that makes sense (no it does not. fucj the naming on this one)
		{
			child.gameObject.SetActive(false);
		}
	}
	void spawnRooms()
	{
		for (int i = 0; i < selectedRooms.Length; i++)
		{
			Transform inst = Instantiate(selectedRooms[i].transform, roomParent);
			inst.gameObject.SetActive(false);
		}
	}

	public void changeRoom(bool isLeft)
	{
		Debug.Log("current room: " + currentRoom);
		setLR();
		hideRoom();
		selectedRooms[getRoom(isLeft)].gameObject.SetActive(true);
		//		getRoom(isLeft).gameObject.SetActive(true);
		getBlock();
	}
}


 */
