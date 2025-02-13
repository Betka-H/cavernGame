using UnityEngine;

public class player : MonoBehaviour
{
	darknessOL darknessOL;
	public bool hasNightVision;


	void Start()
	{
		darknessOL = FindObjectOfType<darknessOL>(true);
		darknessOL.setPlayer(this.transform);
	}

	public void setNightVision()
	{

	}
}
