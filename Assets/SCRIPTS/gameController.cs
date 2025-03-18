using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour
{
	[Header("player")]
	public GameObject playerParent;
	Transform playerSpawnPoint;
	public Transform centerPlayerSpawnPoint;
	public Transform caveExitPlayerSpawnPoint;

	public GameObject labPlayerPrefab;
	public GameObject cavePlayerPrefab;
	public GameObject spacePlayerPrefab;

	missionManager missionManager;
	[HideInInspector] public roomController roomController;

	public SpriteRenderer floorImg;

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

		//! tmp, use this>>>>
		if (missionManager.checkCurrentMission(-1, 0)) // if first call is tutorial space call
		{
			if (PlayerPrefs.GetInt("HasStarted", 0) == 0)
			{
				resetEvent(); //! testing
				PlayerPrefs.SetInt("HasStarted", 1); // Save that the game has started
				PlayerPrefs.Save();
			}

			genAndSpawn(level.space);
		}
		else
		{
			roomController.labFirst = true;
			genAndSpawn(level.lab);
			getElevator().isFirst = true;
		}
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
				roomController.labFirst = true;
				genAndSpawn(level.lab);
				getElevator().isFirst = true;
				isDead = false;
			}
		}
		/* if (Input.GetKeyDown(KeyCode.Escape))
		{
			menuManager.toggleEscapeMenu(!menuManager.escMenu.gameObject.activeSelf);
		} */

		if (roomController.currentLevel == level.lab && getPlayerMovement().transform.GetComponent<Rigidbody2D>().velocity.y > 0) // if player jumps in the lab
		{
			if (!m_hasWarnedJump && roomController.hasMentionedJumping)
			{
				// Debug.LogError("should call jump");
				Invoke("startJumpCall", 5f);
				m_hasWarnedJump = true; // here to prevent multiple triggers
			}
		}
	}
	playerMovement getPlayerMovement()
	{
		return FindObjectOfType<playerMovement>();
	}

	public bool m_hasWarnedJump = false;
	public void startJumpCall()
	{
		if (roomController.currentLevel == level.lab) // but only if theyre still in the lab
		{
			// Debug.LogWarning("still in lab");
			callManager.startCall(missionManager.jumpMission);

			PlayerPrefs.SetInt("jumpWarn", 1);
			PlayerPrefs.Save();
			// m_hasWarnedJump = true;
		}
		else m_hasWarnedJump = false;
	}

	public void m_endOfIntroCall()
	{
		Invoke("endIntroCall", 5f);
	}
	public void endIntroCall()
	{
		Debug.LogError($"end of intro call");
		roomController.labFirst = true;
		genAndSpawn(level.lab);
		getElevator().isFirst = true;
	}

	public void saveAndGoToMainMenu()
	{
		saveManager.save();
		SceneManager.LoadScene(0);
	}

	public void resetEvent() // called by ????? < lets try at start from new game
	{
		// genAndSpawn(level.lab);
		Debug.LogWarning($"reset event: restarting missions");
		missionManager.restartMissions();
		inventoryManager.resetInventories();
		// saveAndGoToMainMenu();
	}

	public void genAndSpawn(level lvl)
	{
		Debug.LogWarning($"gear?: v v v");
		inventoryManager.printInventory(inventoryManager.equippedItems);

		switch (lvl)
		{
			case level.lab:
				audioManager.playMusic(musicLvl.labRegular);
				floorImg.enabled = false;
				if (leavingCavern)
				{
					playerSpawnPoint = caveExitPlayerSpawnPoint;
					leavingCavern = false;
				}
				else playerSpawnPoint = centerPlayerSpawnPoint;
				break;
			case level.cavern:
				playerSpawnPoint = centerPlayerSpawnPoint;
				audioManager.playMusic(musicLvl.caveRegular);
				floorImg.enabled = true;
				break;
			case level.space:
				playerSpawnPoint = centerPlayerSpawnPoint;
				audioManager.playMusic(musicLvl.space);
				floorImg.enabled = false;
				break;
		}

		spawnPlayer(lvl);
		roomController.generateLevel(lvl);
		clearMenus();

		if (lvl == level.lab)
			switch (PlayerPrefs.GetInt("jumpWarn", 0))
			{
				case 0:
					Debug.LogError("has warned jump false");
					m_hasWarnedJump = false;
					getPlayerMovement().bounciness = 7.5f;
					break;
				case 1:
					Debug.LogError("has warned jump true");
					m_hasWarnedJump = true;
					getPlayerMovement().bounciness = 0;
					break;
			}

		// if (lvl == level.space)
		if (missionManager.checkCurrentMission(-1, 0))
		{
			Debug.LogWarning("invoking space call");
			// callManager.startCall(getCurrentMission());
			Invoke("startNextMainMissionCall", 5f); // space call delay 5f
		}
		else if (missionManager.checkCurrentMission(-1, 1))
		{
			Debug.LogWarning("gennspawn: starting 1st tutorial call");
			FindObjectOfType<playerMovement>().speed = 3.5f;
			roomController.hasMentionedJumping = false;
			getElevator().isFirst = true; // door opens only on first and then stays closed

			// StartCoroutine(delayAction(() => callManager.startCall(getCurrentMission()), 21f));
			// StartCoroutine(delayAction(() => callManager.startCall(getCurrentMission()), 3f));

			float callDelay = 8f; //! run this
								  // float callDelay = 0.5f; //! delete this
			Invoke("startNextMainMissionCall", callDelay);
			callDelay += 2.5f; //! run this
							   // callDelay += 0.5f; //! delete this
			Invoke("startNextMainMissionCall", callDelay);
		}
		else if (missionManager.checkCurrentMission(0, 0))
		{
			// Debug.LogWarning("BYEAH");

			//? roomController.clearRoom(); /?! what
			getElevator().isFirst = true;
			getElevator().openDoors(true);
		}
	}
	public void startNextMainMissionCall()
	{
		callManager.startCall(getCurrentMission());
	}
	public void m_resetPlayerSpeed()
	{
		FindObjectOfType<playerMovement>().speed = FindObjectOfType<playerMovement>().defaultSpeed;
	}
	public void m_metTrader() // called at end of trader mission
	{
		// FindObjectOfType<announcerManager>().announceMessage($"has met trader", true);
		PlayerPrefs.SetInt("hasMetTrader", 1);
		PlayerPrefs.Save();
	}

	public void m_endTutorial() // called at the end of tutorial mission
	{
		/* FindObjectOfType<announcerManager>().announceMessage($"skipping the tutorial! and checking cm+cc");
		missionManager.checkCurrentMission(0, -1);
		 */
		FindObjectOfType<announcerManager>().announceMessage($"ending the tutorial!");

		if (!missionManager.checkCurrentMission(-1, -1))
		{
			Debug.Log($"skipping tutorial");
			roomController.labFirst = false;
			leavingCavern = true;
			genAndSpawn(level.lab);
			getElevator().isFirst = false;
			m_unlockCavern(); // cavern is locked from collecting all tutorial items
		}
		else Debug.Log($"is in -1st mission. not moving");
		// getElevator().openDoors(true);
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

			audioManager.playSfx(audioManager.worldSfxSource, audioManager.deathImpale);
			audioManager.playMusic(musicLvl.death);

			/* if (deathCalls[currentDeathCall].currentMessage <= deathCalls[currentDeathCall].messages.Length)
			{
				// menuManager.callManager.callScreen.talk(deathCalls[currentDeathCall]);
				callManager.startCall(deathCalls[currentDeathCall]);
				// deathCalls[currentDeathCall].currentMessage++;
				currentDeathCall++;
			} */
			missionSO deathMission = missionManager.deathMission;
			if (deathMission.currentCall < deathMission.calls.Length)
				callManager.startCall(deathMission);
			else Debug.LogWarning($"no death call remaining");

			deathScreen.SetActive(true);

			// destroyPlayer();
			player.GetComponent<playerMovement>().alive = false;
			player.GetComponent<playerMovement>().rb.velocity = Vector3.zero;

			// lose inventories
			menuManager.inventoryManager.caveInventory.Clear();
			menuManager.inventoryManager.equippedItems.Clear();
			// menuManager.equipWorkstation.clearSlots();

			isDead = true;
			// roomController.labFirst = true;
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
	bool leavingCavern;
	public void leaveCavern()
	{
		Debug.Log("byyye deadly cavern");

		leavingCavern = true;

		getElevator().closeDoors(false);
		StartCoroutine(waitForDoorsToCloseAndGoToLab());
	}
	IEnumerator waitForDoorsToCloseAndGoToLab()
	{
		while (!getElevator().isClosed)
		{
			yield return null;
		}

		// playerSpawnPoint = caveExitPlayerSpawnPoint;

		// getElevator().openDoors(false);

		Debug.LogWarning("wait for 3!!!");
		yield return new WaitForSeconds(3); // keep this line
											// yield return new WaitForSeconds(0.5f); //! delete this line

		transferToLabAndMissionInventory();
		missionManager.checkMissionItems();

		audioManager.worldSfxSource.Stop();
		roomController.clearRoom();
		// roomController.labFirst = true;
		genAndSpawn(level.lab);
		// getElevator().isFirst = true;
		Debug.LogWarning("door is first");

		// getElevator().isFirst = true;
		// Debug.Log("new doors!!");
		// getElevator().closeDoors(true);
		// getElevator().openDoors(false);

		/* GameObject exitObj = GameObject.Find("lab exit point");
		exitObj.GetComponent<tooltipCaller>().disable();
		Debug.Log($"eo: {exitObj}"); */
	}
	public void transferToLabAndMissionInventory()
	{
		List<item> caveInv = menuManager.inventoryManager.caveInventory;
		List<item> requiredInv = missionManager.allMissions[missionManager.currentMission].requiredItems;

		foreach (item it in requiredInv) // for all current mission items
		{
			int howMuchOfIt = requiredInv.Count(item => item == it);

			// add the item to the mission inv and remove it from the cave inv
			//! removes items even if quota is complete (bad)
			if (caveInv.Contains(it) && menuManager.inventoryManager.missionInventory.Count(item => item == it) < howMuchOfIt)
			{
				menuManager.inventoryManager.missionInventory.Add(it);
				caveInv.Remove(it);
			}
		}

		// add the rest of cave inv to lab inv
		menuManager.inventoryManager.labInventory.AddRange(caveInv);

		menuManager.inventoryManager.caveInventory.Clear();
	}

	bool cavernLocked = false; // defaultly open
	public void m_lockCavern()
	{
		// called at end of space call. locks the cavern cause tutorial
		// also called when collecting all items for the tutorial mission
		cavernLocked = true;
	}
	public void m_unlockCavern()
	{
		// called at end of 3rd(?) tutorial call (when u finish talking to MG at his outpost before entering the elevator for the first time)
		// also called at end of tutorial lol
		cavernLocked = false;
	}

	public void enterCavern()
	{
		if (!cavernLocked)
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
				if (missionManager.checkCurrentMission(-1, 4))
				{
					Invoke("startNextMainMissionCall", 5f); //! run this
															// Invoke("startNextMainMissionCall", 0.5f); //! delete this
				}

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
		else
		{
			// FindObjectOfType<announcerManager>().announceMessage($"cavern locked! go talk to MG first");
			FindObjectOfType<announcerManager>().announceMessage($"cavern locked!");

			/* //? if (missionManager.checkCurrentMission(-1, 4))
			{
				Invoke("startNextMainMissionCall", 5f);
			} */
		}
	}
}
