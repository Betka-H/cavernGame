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
		currentLevel = level.lab; //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! lab should be first
		genAndSpawn();
	}

	void Update()
	{
		if (gameState.State != gameState.gameStates.paused)
		{
			Time.timeScale = 1;
			currentLevel = level.cavern;
			if (Input.GetKeyDown(KeyCode.Q))
			{
				toggleTabMenu();
			}
		}
		else
		{
			Time.timeScale = 0;
		}
	}

	public void genAndSpawn()
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

	void OnApplicationQuit()
	{
		Debug.Log("quitting");
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
