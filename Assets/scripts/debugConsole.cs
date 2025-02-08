using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class debugConsole : MonoBehaviour
{
	private Dictionary<string, System.Action> commands;

	public Transform consoleObj;
	public TMP_Text announcerField;
	public TMP_InputField inputField;

	bool consoleOpen;

	string playerInput;
	List<string> inputHistory = new List<string>();
	int historyLocation = 0;

	public gameController gameCtrl;

	System.Random rnd = new System.Random();

	void Start()
	{
		// Debug.LogWarning("console should not be active at start"); //!
		// toggleConsole(true); //!

		loadCommands();
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.X) && Input.GetKeyDown(KeyCode.C))
		{
			toggleConsole(consoleOpen);
		}
		if (consoleOpen)
		{
			// Debug.Log($"command history: {string.Join(", ", inputHistory)}");
			if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				moveHistory(1); // up
			}
			else if (Input.GetKeyDown(KeyCode.DownArrow))
			{
				moveHistory(-1); // down
			}

			if (Input.GetKeyDown(KeyCode.Return)) // NOT KeypadEnter ffs
			{
				submit();
			}
		}
	}

	void toggleConsole(bool on)
	{
		consoleObj.gameObject.SetActive(on);
		if (on)
		{
			gameState.State = gameState.gameStates.paused;
			focus();
		}
		else
		{
			clearConsole();
			gameState.State = gameState.gameStates.playing;
		}
		consoleOpen = !consoleOpen;
	}

	void moveHistory(int up)
	{
		if (up > 0 ? historyLocation < inputHistory.Count : historyLocation > 1)
		{
			Debug.Log(up > 0 ? "loading prev command" : "loading next command");
			Debug.Log($"ihc: {inputHistory.Count}; hl: {historyLocation}");
			historyLocation += up;
			inputField.text = inputHistory[inputHistory.Count - historyLocation];
		}
	}

	void runCommand()
	{
		if (commands.TryGetValue(playerInput, out var commandAction)) // chatgpt
		{
			commandAction();
		}
		else
		{
			Debug.Log($"invalid command: {playerInput}");
		}
	}

	void listCommands()
	{
		Debug.Log("available commands: " + string.Join(", ", commands.Keys));
	}

	void loadCommands() // chatgpt
	{
		commands = new Dictionary<string, System.Action>
		{
			/* { "help", listCommands },
			{ "listinv", () => inventory.printInventory() },
			{ "clrinv", () => inventory.clearInventory() },
			{ "saveinv", () => inventory.saveInventory() },
			{ "rnditem", () => inventory.addItem(inventory.itemList[rnd.Next(inventory.itemList.Length)]) },
			{ "clrhst", clearHistory },
			{ "newlvl", () => gameCtrl.genAndSpawn() } */
		};
	}

	void clearHistory() //*
	{
		inputHistory.Clear();
		historyLocation = 0;
	}

	public void submit() //*
	{
		playerInput = inputField.text;
		Debug.Log($"command entered: {playerInput}");
		runCommand();
		inputHistory.Add(playerInput);

		historyLocation = 0;
		clearConsole();
		focus();
	}

	void focus() //*
	{
		inputField.ActivateInputField();
		inputField.Select();
	}

	void clearConsole() //*
	{
		inputField.text = "";
	}
}
