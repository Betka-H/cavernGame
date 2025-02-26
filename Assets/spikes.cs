using UnityEngine;

public class spikes : MonoBehaviour
{
	private gameController gameController;

	void Start()
	{
		gameController = FindObjectOfType<gameController>();
	}

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
