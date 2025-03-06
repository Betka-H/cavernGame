using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class gameController : MonoBehaviour
{
	[Header("player")]
	public GameObject playerParent;
	public Transform spawnPoint;
	public GameObject labPlayerPrefab;
	public GameObject cavePlayerPrefab;

	missionManager missionManager;
	private roomController roomController;

	menuManager menuManager;

	interactionTooltip interactionTooltip;

	public enum level { lab, cavern };

	void Start()
	{
		roomController = FindObjectOfType<roomController>();
		interactionTooltip = FindObjectOfType<interactionTooltip>(true);
		menuManager = FindObjectOfType<menuManager>();
		missionManager = FindObjectOfType<missionManager>();
		callManager = FindObjectOfType<callManager>();

		genAndSpawn(level.lab);

		clearMenus();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q))
		{
			switch (roomController.currentLevel)
			{
				case level.lab:
					toggleLabMenu();
					break;
				case level.cavern:
					toggleCavernMenu();
					break;
			}
		}
		if (Input.GetKeyDown(KeyCode.R))
		{
			menuManager.hideMenus();
			genAndSpawn(level.lab);
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			menuManager.toggleEscapeMenu(!menuManager.escMenu.gameObject.activeSelf);
		}
	}

	public void genAndSpawn(level lvl)
	{
		spawnPlayer(lvl);
		roomController.generateLevel(lvl);
	}
	void clearMenus()
	{
		menuManager.hideMenus();
		interactionTooltip.hideTooltip();
		deathScreen.SetActive(false);
	}

	GameObject playerPrefab;
	GameObject player;
	void spawnPlayer(level lvl)
	{
		destroyPlayer();
		switch (lvl)
		{
			case level.lab:
				playerPrefab = labPlayerPrefab;
				break;
			case level.cavern:
				playerPrefab = cavePlayerPrefab;
				break;
		}
		player = Instantiate(playerPrefab, playerParent.transform);
		player.transform.position = spawnPoint.position;
		player.GetComponent<playerMovement>().alive = true;
		player.GetComponent<playerMovement>().rb.velocity = Vector3.zero;

		roomController.player = player.GetComponent<Transform>();
	}
	void destroyPlayer()
	{
		foreach (Transform child in playerParent.gameObject.GetComponentsInChildren<Transform>())
		//! the thing is also considered a child so only delete the children on the children if that makes sense (no it does not. fucj the naming on this one)
		{
			if (child.CompareTag("player"))
			{
				Destroy(child.gameObject);
			}
		}
	}

	// public missionMenu missionMenu;
	void toggleCavernMenu()
	{
		// Debug.Log($"menu: {itemMenu.gameObject.activeSelf}");
		/* itemMenu.gameObject.SetActive(!itemMenu.gameObject.activeSelf);
		missionMenu.gameObject.SetActive(itemMenu.gameObject.activeSelf); */

		menuManager.toggleGameplayMenuScreen();

		/* switch (itemMenu.gameObject.activeSelf)
		{
			case true:
				gameState.State = gameState.gameStates.paused;
				break;
			case false:
				gameState.State = gameState.gameStates.playing;
				break;
		} */
		// Debug.LogWarning($"menu: {itemMenu.gameObject.activeSelf}");
	}
	void toggleLabMenu()
	{
		menuManager.toggleLabMenuScreen();
	}

	void OnApplicationQuit()
	{
		Debug.Log("quitting");
		todo();
	}

	void todo()
	{
		Debug.LogError("dont forget to upload to github!");
		Debug.LogWarning("=========================================================================");
		Debug.LogWarning("todo:");
		// Debug.LogWarning("have an unlockable npc which gives u a \"weather forecast\" for the next run depending on the darkness level");
		Debug.LogWarning("search temp");
	}

	public GameObject deathScreen;
	callManager callManager;
	// public callSO[] deathCalls;
	public missionSO deathMission;
	int currentDeathCall;
	public void death()
	{
		Debug.Log("death");
		// deathScreen.SetActive(true);

		/* if (deathCalls[currentDeathCall].currentMessage <= deathCalls[currentDeathCall].messages.Length)
		{
			// menuManager.callManager.callScreen.talk(deathCalls[currentDeathCall]);
			callManager.startCall(deathCalls[currentDeathCall]);
			// deathCalls[currentDeathCall].currentMessage++;
			currentDeathCall++;
		} */
		callManager.startCall(deathMission);

		// destroyPlayer();
		player.GetComponent<playerMovement>().alive = false;
		player.GetComponent<playerMovement>().rb.velocity = Vector3.zero;

		// lose inventories
		menuManager.inventoryManager.caveInventory.Clear();
		menuManager.inventoryManager.equippedItems.Clear();
		// menuManager.equipWorkstation.clearSlots();
	}

	public void leaveCavern()
	{
		Debug.Log("byyye deadly cavern");

		transferToLabAndMissionInventory();
		missionManager.checkMissionItems();

		roomController.clearRoom();
		genAndSpawn(level.lab);
	}
	public void transferToLabAndMissionInventory()
	{
		List<item> caveInv = menuManager.inventoryManager.caveInventory;
		menuManager.inventoryManager.labInventory.AddRange(caveInv);
		menuManager.inventoryManager.missionInventory.AddRange(caveInv);
		menuManager.inventoryManager.caveInventory.Clear();
	}
	public void enterCavern()
	{
		if (missionManager.currentMission == 0)
		{
			Debug.Log("helloooo deadly cavern");

			/* missionSO currentMission = menuManager.callManager.currentMainMission();
			if (currentMission.calls.Length > missionManager.allMissions[missionManager.currentMission].currentCall)
				menuManager.callManager.startMissionCall(); */
			missionSO currentMission = menuManager.callManager.currentMainMission();
			if (currentMission.calls.Length > missionManager.allMissions[missionManager.currentMission].currentCall)
				menuManager.callManager.startCall(currentMission);
		}

		genAndSpawn(level.cavern);
		if (menuManager.inventoryManager.checkEquipment(menuManager.inventoryManager.inventoryDefinitions.backpack))
			menuManager.caveItemMenu.hasAllSlots = true;
		else menuManager.caveItemMenu.hasAllSlots = false;
	}
}
