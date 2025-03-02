using UnityEngine;

public class player : MonoBehaviour
{
	darknessOL darknessOL;

	void Start()
	{
		darknessOL = FindObjectOfType<darknessOL>(true);
		// darknessOL.setPlayer(this.transform);
	}
}
