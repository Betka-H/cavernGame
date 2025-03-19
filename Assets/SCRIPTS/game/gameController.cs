using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour
{
	[Header("player")]
	Transform playerSpawnPoint;
	public Transform centerPlayerSpawnPoint;
	public Transform caveExitPlayerSpawnPoint;

	public GameObject playerParent;

	GameObject playerPrefab;
	public GameObject labPlayerPrefab;
	public GameObject cavePlayerPrefab;
	public GameObject spacePlayerPrefab;

	[HideInInspector] public bool oldMovement;

	[HideInInspector] public roomController roomController;

	public SpriteRenderer floorImg;



	missionManager missionManager;
	audioManager audioManager;
	announcerManager announcerManager;
	menuManager menuManager;
	callManager callManager;
	inventoryManager inventoryManager;
	saveManager saveManager;

	[HideInInspector] public bool isCalling;

	interactionTooltip interactionTooltip;


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

		announcerManager = FindObjectOfType<announcerManager>();

		roomController.labFirst = true;

		if (missionManager.checkCurrentMission(-1, 0))
		// if space call
		{
			if (PlayerPrefs.GetInt("HasStarted", 0) == 0)
			{
				//? why?
				missionManager.restartMissions();
				inventoryManager.resetInventories();
				//?

				PlayerPrefs.SetInt("HasStarted", 1);
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
		if (Input.GetKeyDown(KeyCode.R))
			if (isDead && !isCalling)
				respawn();

		// if player jumps in the lab
		if (checkJumpCallConditions())
		{
			Invoke(nameof(startJumpCall), 5f);
			m_hasWarnedJump = true; // prevents multiple triggers
		}
	}
	void respawn()
	{
		roomController.labFirst = true;
		genAndSpawn(level.lab);
		getElevator().isFirst = true;

		isDead = false;
	}

	bool checkJumpCallConditions()
	{
		if (roomController.currentLevel == level.lab
		  && getPlayerMovement().transform.GetComponent<Rigidbody2D>().velocity.y > 0
		  && !m_hasWarnedJump
		  && roomController.hasMentionedJumping)
			return true;
		else return false;
	}
	public bool m_hasWarnedJump = false;
	void startJumpCall()
	{
		if (roomController.currentLevel == level.lab)
		{
			Debug.LogError($"test jump call in cave");
			callManager.startCall(missionManager.jumpMission);

			PlayerPrefs.SetInt("jumpWarn", 1);
			PlayerPrefs.Save();
		}
		else m_hasWarnedJump = false;
	}

	playerMovement getPlayerMovement()
	{
		return FindObjectOfType<playerMovement>();
	}

	// called at end of intro call. spawns first lab
	public void m_endOfIntroCall()
	{
		Invoke(nameof(endIntroCall), 5f);
	}
	void endIntroCall()
	{
		roomController.labFirst = true;
		genAndSpawn(level.lab);
		getElevator().isFirst = true;
	}

	public void saveAndGoToMainMenu()
	{
		saveManager.save();
		SceneManager.LoadScene(0);
	}

	public void genAndSpawn(level lvl)
	{
		spawnPlayer();
		roomController.generateLevel(lvl);
		clearMenus();

		checkJumpWarn();

		// misc - play music, enable floor img
		switch (lvl)
		{
			case level.lab:
				audioManager.playMusic(musicLvl.labRegular);
				floorImg.enabled = false;
				break;
			case level.cavern:
				audioManager.playMusic(musicLvl.caveRegular);
				floorImg.enabled = true;
				break;
			case level.space:
				audioManager.playMusic(musicLvl.space);
				floorImg.enabled = false;
				break;
		}

		// missions
		if (missionManager.checkCurrentMission(-1, 0)) // space call
		{
			Invoke(nameof(startNextMainMissionCall), 5f);
		}
		else if (missionManager.checkCurrentMission(-1, 1)) // first tutorial call
		{
			FindObjectOfType<playerMovement>().speed = 3.5f;
			roomController.hasMentionedJumping = false;
			getElevator().isFirst = true;

			float callDelay = 8f;
			Invoke(nameof(startNextMainMissionCall), callDelay);
			callDelay += 2.5f;
			Invoke(nameof(startNextMainMissionCall), callDelay);
		}
		else if (missionManager.checkCurrentMission(0, 0)) // first real mission
		{
			getElevator().isFirst = true;
			getElevator().openDoors(true);
		}

		void checkJumpWarn()
		{
			if (lvl == level.lab)
				switch (PlayerPrefs.GetInt("jumpWarn", 0))
				{
					case 0:
						m_hasWarnedJump = false;
						getPlayerMovement().bounciness = 7.5f;
						break;
					case 1:
						m_hasWarnedJump = true;
						getPlayerMovement().bounciness = 0;
						break;
				}
		}
		void spawnPlayer()
		{
			destroyPlayer();
			switch (lvl)
			{
				case level.lab:
					playerPrefab = labPlayerPrefab;
					if (leavingCavern)
					{
						playerSpawnPoint = caveExitPlayerSpawnPoint;
						leavingCavern = false;
					}
					else playerSpawnPoint = centerPlayerSpawnPoint;
					break;
				case level.cavern:
					playerPrefab = cavePlayerPrefab;
					playerSpawnPoint = centerPlayerSpawnPoint;
					break;
				case level.space:
					playerPrefab = spacePlayerPrefab;
					playerSpawnPoint = centerPlayerSpawnPoint;
					break;
			}
			GameObject playerObj = Instantiate(playerPrefab, playerParent.transform);
			playerObj.transform.position = playerSpawnPoint.position;
			getPlayerMovement().isAlive = true;
			getPlayerMovement().rb.velocity = Vector3.zero;

			checkShoes();

			void destroyPlayer()
			{
				foreach (Transform child in playerParent.gameObject.GetComponentsInChildren<Transform>())
					if (child.CompareTag("player"))
						Destroy(child.gameObject);
			}
			void checkShoes()
			{
				if (roomController.currentLevel == level.cavern && inventoryManager.checkEquipment(inventoryManager.inventoryDefinitions.shoes))
					getPlayerMovement().speed += 2;
			}
		}
	}

	public void startNextMainMissionCall()
	{
		callManager.startCall(getCurrentMission());
	}

	// called at end of tmc 2
	public void m_resetPlayerSpeed()
	{
		getPlayerMovement().speed = getPlayerMovement().defaultSpeed;
	}
	// called at end of trader mission
	public void m_metTrader()
	{
		PlayerPrefs.SetInt("hasMetTrader", 1);
		PlayerPrefs.Save();
	}
	// called at the end of tutorial mission
	public void m_endTutorial()
	{
		announcerManager.announceMessage($"ending the tutorial!");

		if (!missionManager.checkCurrentMission(-1, -1))
		{
			Debug.Log($"skipping tutorial");
			roomController.labFirst = false;
			leavingCavern = true;
			genAndSpawn(level.lab);
			getElevator().isFirst = false;
			m_unlockCavern(); // cavern is locked from collecting all tutorial items
		}
		else Debug.Log($"is in tutorial. not moving");
		m_unlockCavern();
	}

	void clearMenus()
	{
		menuManager.hideMenus();
		interactionTooltip.hideTooltip();
	}
	missionSO getCurrentMission()
	{
		return missionManager.allMissions[missionManager.currentMission];
	}

	void OnApplicationQuit()
	{
		Debug.LogError($"add quit button!");
		Debug.Log("quitting app");
	}

	public GameObject deathScreen;
	public bool isDead;
	public void death()
	{
		if (!inventoryManager.checkEquipment(inventoryManager.inventoryDefinitions.harderHat))
		{
			announcerManager.announceMessage($"you died!");

			audioManager.playSfx(audioManager.worldSfxSource, audioManager.deathImpale);
			audioManager.playMusic(musicLvl.death);

			missionSO deathMission = missionManager.deathMission;
			if (deathMission.currentCall < deathMission.calls.Length)
				callManager.startCall(deathMission);
			else Debug.Log($"no death call remaining");

			deathScreen.SetActive(true);

			getPlayerMovement().isAlive = false;
			getPlayerMovement().rb.velocity = Vector3.zero;

			// lose inventories
			menuManager.inventoryManager.caveInventory.Clear();
			menuManager.inventoryManager.equippedItems.Clear();

			isDead = true;
		}
		else
		{
			announcerManager.announceMessage($"you lost the harder hat! be careful");
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
		leavingCavern = true;

		getElevator().closeDoors(false);

		StartCoroutine(waitForDoorsToCloseAndGoToLab());
		IEnumerator waitForDoorsToCloseAndGoToLab()
		{
			while (!getElevator().isClosed)
			{
				yield return null;
			}

			yield return new WaitForSeconds(3);

			transferInventory();
			//? missionManager.checkMissionItems(); //?

			audioManager.worldSfxSource.Stop();

			Debug.LogWarning($"test this");
			roomController.clearRoom(); //? why?
			genAndSpawn(level.lab);

			missionManager.checkMissionItems(); //?

			void transferInventory()
			{
				List<item> caveInv = menuManager.inventoryManager.caveInventory;
				List<item> requiredInv = missionManager.allMissions[missionManager.currentMission].requiredItems;

				foreach (item it in requiredInv)
				{
					int howMuchOfIt = requiredInv.Count(item => item == it);

					// add the item to the mission inv and remove it from the cave inv
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
		}
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

				yield return new WaitForSeconds(3);

				audioManager.worldSfxSource.Stop();

				Debug.LogWarning($"test this");
				roomController.clearRoom(); //? why?
				genAndSpawn(level.cavern);

				getElevator().isFirst = true;

				if (menuManager.inventoryManager.checkEquipment(menuManager.inventoryManager.inventoryDefinitions.backpack))
					menuManager.caveItemMenu.hasAllSlots = true;
				else menuManager.caveItemMenu.hasAllSlots = false;

				if (missionManager.checkCurrentMission(-1, 4))
					Invoke(nameof(startNextMainMissionCall), 5f);

				// chatgpt i guess
				StartCoroutine(WaitForCallToEndAndOpenDoors());
				IEnumerator WaitForCallToEndAndOpenDoors()
				{
					while (isCalling)
					{
						yield return null;
					}

					getElevator().openDoors(false);
				}
			}
		}
		else announcerManager.announceMessage($"cavern locked!");
	}
}
