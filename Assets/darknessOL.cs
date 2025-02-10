using UnityEngine;

public class darknessOL : MonoBehaviour
{
	public Transform darknessOverlayNormal;
	public Transform darknessOverlayNV;

	Transform player;

	public void setPlayer(Transform player)
	{
		this.player = player;
	}

	void Update()
	{
		if (gameObject.activeSelf && player != null)
		{
			transform.position = player.position;
		}
	}

	/* void OnEnable()
	{
		Debug.Log(player);
	} */
}
