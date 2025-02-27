using System;
using UnityEditor.SearchService;
using UnityEngine;

public class gameController : MonoBehaviour
{
	[Header("player")]
	public GameObject playerParent;
	public Transform spawnPoint;
	public GameObject labPlayerPrefab;
	public GameObject cavePlayerPrefab;

	private roomController roomController;
	private itemMenu itemMenu;
	private inventory inventory;

	public enum level { lab, cavern };
	// level currentLevel;

	// public ScriptableObject item1;
	void Start()
	{
		roomController = FindObjectOfType<roomController>();
		// roomController.hasNightVision = false;
		itemMenu = FindObjectOfType<itemMenu>(true);
		inventory = FindObjectOfType<inventory>();

		// gameState.State = gameState.gameStates.playing;
		//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! lab should be first
		// currentLevel = level.cavern;
		genAndSpawn(level.lab);
		// genAndSpawn(level.cavern);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q))
		{
			toggleQMenu();
		}
		if (Input.GetKeyDown(KeyCode.R))
		{
			restart();
		}
	}

	void restart()
	{
		// genAndSpawn(level.lab);
		genAndSpawn(level.cavern);
		clearMenus();
		inventory.clearInventory();
	}

	public void genAndSpawn(level lvl)
	{
		// roomController.chooseDarkness();
		roomController.generateLevel(lvl);
		spawnPlayer(lvl);
	}
	public Transform[] menusToDisable;
	void clearMenus()
	{
		foreach (Transform menu in menusToDisable)
		{
			menu.gameObject.SetActive(false);
		}

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

	public missionMenu missionMenu;
	void toggleQMenu()
	{
		// Debug.Log($"menu: {itemMenu.gameObject.activeSelf}");
		itemMenu.gameObject.SetActive(!itemMenu.gameObject.activeSelf);
		missionMenu.gameObject.SetActive(itemMenu.gameObject.activeSelf);

		switch (itemMenu.gameObject.activeSelf)
		{
			case true:
				gameState.State = gameState.gameStates.paused;
				break;
			case false:
				gameState.State = gameState.gameStates.playing;
				break;
		}
		// Debug.LogWarning($"menu: {itemMenu.gameObject.activeSelf}");
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
		Debug.LogWarning("have an unlockable npc which gives u a \"weather forecast\" for the next run depending on the darkness level");
	}

	public GameObject deathScreen;
	public void death()
	{
		Debug.Log("death");
		deathScreen.SetActive(true);
		// destroyPlayer();
		player.GetComponent<playerMovement>().alive = false;
	}
}


/* 

using UnityEngine;

public class gameController : MonoBehaviour
{
	[Header("player")]
	public GameObject playerParent;
	public Transform spawnPoint;
	public GameObject labPlayerPrefab;
	public GameObject cavePlayerPrefab;

	[Header("room controller")]
	public roomController roomControl;

	[Header("inventory menu")]
	public tabMenu tabMenu;

	public enum level { lab, cavern };
	level currentLevel;

	void Start()
	{
		gameState.State = gameState.gameStates.playing;
		currentLevel = level.cavern; //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! lab should be first
		genAndSpawn();
	}

	void Update()
	{
		getPlayerInput();
	}

	void getPlayerInput()
	{
		// currentLevel = level.cavern;
		if (Input.GetKeyDown(KeyCode.R)) //! remove this lol
		{
			genAndSpawn();
		}
		if (Input.GetKeyDown(KeyCode.Q))
		{
			toggleTabMenu();
		}
		if (gameState.State == gameState.gameStates.paused)
		{
			Time.timeScale = 0;
		}
		else
			Time.timeScale = 1;
	}

	void genAndSpawn()
	{
		roomControl.generateLevel(currentLevel);
		spawnPlayer(currentLevel);
	}

	GameObject playerPrefab;
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
		GameObject player = Instantiate(playerPrefab, playerParent.transform);
		player.transform.position = spawnPoint.position;
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

	void toggleTabMenu()
	{
		tabMenu.gameObject.SetActive(!tabMenu.gameObject.activeSelf);
		switch (tabMenu.gameObject.activeSelf)
		{
			case true:
				gameState.State = gameState.gameStates.paused;
				break;
			case false:
				gameState.State = gameState.gameStates.playing;
				break;
		}
	}
}


 */
