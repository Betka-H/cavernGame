using UnityEngine;

public class gameState : MonoBehaviour
{
	public enum gameStates { mainMenu, playing, paused };
	private static gameStates state;
	public static gameStates State
	{
		get
		{
			return state;
		}
		set
		{
			state = value;
			// Debug.Log($"game state: {state}");
		}
	}
}