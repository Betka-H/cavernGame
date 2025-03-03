using UnityEngine;

public class gameController : MonoBehaviour
{
	[Header("player")]
	public GameObject playerParent;
	public Transform spawnPoint;
	public GameObject labPlayerPrefab;
	public GameObject cavePlayerPrefab;

	private roomController roomController;
	private inventoryManager inventory;

	menuManager menuManager;

	interactionTooltip interactionTooltip;

	public enum level { lab, cavern };

	void Start()
	{
		roomController = FindObjectOfType<roomController>();
		inventory = FindObjectOfType<inventoryManager>();
		interactionTooltip = FindObjectOfType<interactionTooltip>(true);
		menuManager = FindObjectOfType<menuManager>();

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
		//! temp
		if (Input.GetKeyDown(KeyCode.R))
		{
			respawnCavern();
		}
	}

	void respawnCavern()
	{
		genAndSpawn(level.cavern);
		clearMenus();
		inventory.caveInventory.Clear();
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
		Debug.LogWarning("have an unlockable npc which gives u a \"weather forecast\" for the next run depending on the darkness level");
	}

	public GameObject deathScreen;
	public void death()
	{
		Debug.Log("death");
		deathScreen.SetActive(true);
		// destroyPlayer();
		player.GetComponent<playerMovement>().alive = false;
		player.GetComponent<playerMovement>().rb.velocity = Vector3.zero;
	}

	public void leaveCavern()
	{
		Debug.Log("byyye deadly cavern");
		roomController.clearRoom();
		transferInventory();
		genAndSpawn(level.lab);
	}
	public void transferInventory()
	{
		/* foreach (item it in inventory.caveInventory)
        {
            inventory.addCaveItem(it);
        } // lmao what*/
		inventory.labInventory.AddRange(inventory.caveInventory);
		inventory.caveInventory.Clear();
	}
	public void enterCavern()
	{
		Debug.Log("helloooo deadly cavern");
		genAndSpawn(level.cavern);
	}
}
