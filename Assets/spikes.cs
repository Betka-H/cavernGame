using UnityEngine;

public class spikes : MonoBehaviour
{
	public gameController gameController;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("player"))
		{
			die();
		}
	}

	void die()
	{
		gameController.death();
	}
}
