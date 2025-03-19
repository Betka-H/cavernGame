using UnityEngine;

public class darknessOL : MonoBehaviour
{
	public Transform darknessOverlayNormal;
	public Transform darknessOverlayNightVis;

	Transform player;

	public void setPlayer()
	{
		player = FindObjectOfType<playerMovement>().transform;
	}

	//* check for player when null instead?
	void Update()
	{
		if ((darknessOverlayNormal.gameObject.activeSelf || darknessOverlayNightVis.gameObject.activeSelf) && player != null)
		{
			transform.position = player.position;
		}
	}
}
