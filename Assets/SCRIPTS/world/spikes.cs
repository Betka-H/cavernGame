using UnityEngine;

public class spikes : MonoBehaviour
{
	private gameController gameController;

	void Awake()
	{
		gameController = FindObjectOfType<gameController>();
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("player"))
		{
			Debug.LogError($"fuckinf died");
			// gameController.death(); //! disabled death for testing
		}
	}
}
