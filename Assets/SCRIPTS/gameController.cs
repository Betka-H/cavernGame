using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour
{
	[Header("player")]
	public GameObject playerParent;
	public Transform playerSpawnPoint;
	public GameObject labPlayerPrefab;
	public GameObject cavePlayerPrefab;
	public GameObject spacePlayerPrefab;

	missionManager missionManager;
	[HideInInspector] public roomController roomController;

	audioManager audioManager;

	menuManager menuManager;
	callManager callManager;

	[HideInInspector] public bool isCalling;

	interactionTooltip interactionTooltip;

	inventoryManager inventoryManager;
	saveManager saveManager;

	public enum level { lab, cavern, space };

	void Start()
	{
		saveManager = FindObjectOfType<saveManager>();
		roomController = FindObjectOfType<roomController>();
		interactionTooltip = FindObjectOfType<interactionTooltip>(true);
		menuManager = FindObjectOfType<menuManager>();
		missionManager = FindObjectOfType<missionManager>();
		callManager = FindObjectOfType<callManager>();
		inventoryManager = FindObjectOfType<inventoryManager>();
		audioManager = FindObjectOfType<audioManager>();

		roomController.labFirst = true;

		// clearMenus();

		//! tmp, use this>>>>
		/* if (PlayerPrefs.GetInt("HasStarted", 0) == 0)
		{
			PlayerPrefs.SetInt("HasStarted", 1); // Save that the game has started
			PlayerPrefs.Save();

			genAndSpawn(level.space);
		}
		else genAndSpawn(level.lab); */
		genAndSpawn(level.space); //! delet this line

		// clearMenus();
	}

	void Update()
	{
		/* if (Input.GetKeyDown(KeyCode.Q))
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
		} */
		if (Input.GetKeyDown(KeyCode.R))
		{
			Debug.Log($"dead: {isDead}, calling: {isCalling}");
			if (isDead && !isCalling)
			{
				// menuManager.hideMenus();
				genAndSpawn(level.lab);
			}
		}
		/* if (Input.GetKeyDown(KeyCode.Escape))
		{
			menuManager.toggleEscapeMenu(!menuManager.escMenu.gameObject.activeSelf);
		} */
	}

	public void endOfIntroCall()
	{
		// Debug.LogError($"end of intro call");
		genAndSpawn(level.lab);
	}

	public void saveAndGoToMainMenu()
	{
		saveManager.save();
		SceneManager.LoadScene(0);
	}

	public void resetEvent()
	{
		// genAndSpawn(level.lab);
		missionManager.restartMissions();
		inventoryManager.resetInventories();
		saveAndGoToMainMenu();
	}

	public void genAndSpawn(level lvl)
	{
		switch (lvl)
		{
			case level.lab:
				audioManager.playMusic(musicLvl.labRegular);
				break;
			case level.cavern:
				audioManager.playMusic(musicLvl.caveRegular);
				break;
			case level.space:
				audioManager.playMusic(musicLvl.space);
				break;
		}

		spawnPlayer(lvl);
		roomController.generateLevel(lvl);
		clearMenus();

		// if (lvl == level.space)
		if (missionManager.checkCurrentMission(-1, 0))
		{
			callManager.startCall(getCurrentMission());
		}
		else if (missionManager.checkCurrentMission(-1, 1))
		{
			getElevator().isFirst = true; // door opens only on first and then stays closed

			// StartCoroutine(delayAction(() => callManager.startCall(getCurrentMission()), 21f));
			// StartCoroutine(delayAction(() => callManager.startCall(getCurrentMission()), 3f));

			float callDelay = 8f;
			Invoke("startNextMainMissionCall", callDelay);
			callDelay += 2.5f;
			Invoke("startNextMainMissionCall", callDelay);
		}
	}
	public void startNextMainMissionCall()
	{
		callManager.startCall(getCurrentMission());
	}

	/* // chatgpt
	float logger;
	IEnumerator delayAction(Action action, float delay)
	{
		logger += Time.deltaTime;
		Debug.Log($"{logger}");
		yield return new WaitForSeconds(delay);
		action?.Invoke();
	} */


	void clearMenus()
	{
		menuManager.hideMenus();
		interactionTooltip.hideTooltip();
	}
	missionSO getCurrentMission()
	{
		return missionManager.allMissions[missionManager.currentMission];
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
			case level.space:
				playerPrefab = spacePlayerPrefab;
				break;
		}
		player = Instantiate(playerPrefab, playerParent.transform);
		player.transform.position = playerSpawnPoint.position;
		player.GetComponent<playerMovement>().alive = true;
		player.GetComponent<playerMovement>().rb.velocity = Vector3.zero;

		roomController.player = player.GetComponent<Transform>();

		checkShoes();
		// Debug.LogWarning("shoes???");
	}
	void checkShoes()
	{
		if (roomController.currentLevel == level.cavern && inventoryManager.checkEquipment(inventoryManager.inventoryDefinitions.shoes))
		{
			Debug.Log("speeeed");
			player.GetComponent<playerMovement>().speed += 2;
		}
	}
	void destroyPlayer()
	{
		foreach (Transform child in playerParent.gameObject.GetComponentsInChildren<Transform>())
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

		menuManager.toggleCaveGameplayMenuScreen();

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
		/* Debug.LogError("dont forget to upload to github!");
		Debug.LogWarning("=========================================================================");
		Debug.LogWarning("todo:");
		// Debug.LogWarning("have an unlockable npc which gives u a \"weather forecast\" for the next run depending on the darkness level");
		Debug.LogWarning("search temp"); */
	}

	public GameObject deathScreen;
	bool isDead;
	public void death()
	{
		if (!inventoryManager.checkEquipment(inventoryManager.inventoryDefinitions.harderHat))
		{
			// Debug.Log("death");
			FindObjectOfType<announcerManager>().announceMessage($"you died!");

			audioManager.playSfx(audioManager.worldSfxSource, audioManager.deathImpale, true);
			audioManager.playMusic(musicLvl.death);

			/* if (deathCalls[currentDeathCall].currentMessage <= deathCalls[currentDeathCall].messages.Length)
			{
				// menuManager.callManager.callScreen.talk(deathCalls[currentDeathCall]);
				callManager.startCall(deathCalls[currentDeathCall]);
				// deathCalls[currentDeathCall].currentMessage++;
				currentDeathCall++;
			} */
			missionSO deathMission = missionManager.deathMission;
			if (deathMission.calls.Length > deathMission.currentCall)
				callManager.startCall(deathMission);

			deathScreen.SetActive(true);

			// destroyPlayer();
			player.GetComponent<playerMovement>().alive = false;
			player.GetComponent<playerMovement>().rb.velocity = Vector3.zero;

			// lose inventories
			menuManager.inventoryManager.caveInventory.Clear();
			menuManager.inventoryManager.equippedItems.Clear();
			// menuManager.equipWorkstation.clearSlots();

			isDead = true;
			roomController.labFirst = true;
		}
		else
		{
			// Debug.Log("has hat! bye bye hat");
			FindObjectOfType<announcerManager>().announceMessage($"you lost the harder hat! be careful");
			inventoryManager.removeItem(inventoryManager.inventoryDefinitions.harderHat, inventoryManager.equippedItems);
		}
	}

	public void testMethod()
	{
		Debug.LogWarning("test");
	}

	elevator getElevator()
	{
		return FindObjectOfType<elevator>();
	}
	public void leaveCavern()
	{
		Debug.Log("byyye deadly cavern");

		getElevator().closeDoors(false);
		StartCoroutine(waitForDoorsToCloseAndGoToLab());
	}
	IEnumerator waitForDoorsToCloseAndGoToLab()
	{
		while (!getElevator().isClosed)
		{
			yield return null;
		}

		// getElevator().openDoors(false);

		Debug.LogWarning("wait for 3!!!");
		yield return new WaitForSeconds(3); // keep this line
											// yield return new WaitForSeconds(0.5f); //! delete this line

		transferToLabAndMissionInventory();
		missionManager.checkMissionItems();

		audioManager.worldSfxSource.Stop();
		roomController.clearRoom();
		genAndSpawn(level.lab);

		getElevator().isFirst = true;
		// Debug.Log("new doors!!");
		// getElevator().closeDoors(true);
		getElevator().openDoors(false);

		/* GameObject exitObj = GameObject.Find("lab exit point");
		exitObj.GetComponent<tooltipCaller>().disable();
		Debug.Log($"eo: {exitObj}"); */
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
		Debug.Log("helloooo deadly cavern");

		getElevator().closeDoors(false);
		StartCoroutine(waitForDoorsToCloseAndGoToCave());

		IEnumerator waitForDoorsToCloseAndGoToCave()
		{
			while (!getElevator().isClosed)
			{
				yield return null;
			}

			// getElevator().openDoors(false);

			Debug.LogWarning("wait for 3!!!");
			yield return new WaitForSeconds(3); // keep this line
												// yield return new WaitForSeconds(0.5f); //! delete this line

			// transferToLabAndMissionInventory();
			// missionManager.checkMissionItems();

			roomController.clearRoom();
			genAndSpawn(level.cavern);
			audioManager.worldSfxSource.Stop();

			// genAndSpawn(level.cavern);
			getElevator().isFirst = true;

			if (menuManager.inventoryManager.checkEquipment(menuManager.inventoryManager.inventoryDefinitions.backpack))
				menuManager.caveItemMenu.hasAllSlots = true;
			else menuManager.caveItemMenu.hasAllSlots = false;

			/* GameObject exitObj = GameObject.Find("cavern exit point");
			exitObj.GetComponent<tooltipCaller>().disable();
			Debug.Log($"eo: {exitObj}"); */

			// if (missionManager.currentMission == 0)
			// if (getCurrentMission().missionID == 0)
			if (missionManager.checkCurrentMission(0, -1))
			{

				/* missionSO currentMission = menuManager.callManager.currentMainMission();
				if (currentMission.calls.Length > currentMission.currentCall) // to prevent looping the last message
					menuManager.callManager.startCall(currentMission); */

				callManager.startCall(getCurrentMission());
			}
			/* else
				getElevator().openDoors(false); */

			StartCoroutine(WaitForCallToEndAndOpenDoors());

			// chatgpt i guess
			IEnumerator WaitForCallToEndAndOpenDoors()
			{
				// FindObjectOfType<announcerManager>().announceMessage($"door opening?????");
				while (isCalling)
				{
					yield return null;
				}

				Debug.Log($"not calling anymore");
				// FindObjectOfType<announcerManager>().announceMessage($"door opening?");
				getElevator().openDoors(false);
				// Debug.LogWarning("should open now");
			}
		}
	}
}
