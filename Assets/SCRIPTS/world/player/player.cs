using UnityEngine;

public class player : MonoBehaviour
{
	darknessOL darknessOL;

	void Start()
	{
		Debug.LogError($"whats this for?"); //?
		darknessOL = FindObjectOfType<darknessOL>(true);
		// darknessOL.setPlayer(this.transform);
	}
}
