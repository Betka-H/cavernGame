using UnityEngine;

public class darknessOL : MonoBehaviour
{
	public Transform darknessOverlayNormal;
	public Transform darknessOverlayNightVis;

	Transform player;

	public void setPlayer()
	{
		player = FindObjectOfType<playerMovement>().transform;
		// Debug.Log($"darkness player set to {player}");
	}

	void Update()
	{
		if ((darknessOverlayNormal.gameObject.activeSelf || darknessOverlayNightVis.gameObject.activeSelf) && player != null)
		{
			transform.position = player.position;
			// Debug.Log($"pos: {transform.position}");
		}
	}
}
