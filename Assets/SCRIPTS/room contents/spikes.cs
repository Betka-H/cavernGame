using UnityEngine;

public class spikes : MonoBehaviour
{
	private gameController gameController;
	callManager callManager;
	callSO deathCall;

	void Awake()
	{
		gameController = FindObjectOfType<gameController>();
		callManager = FindObjectOfType<callManager>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		/* if (other.gameObject.CompareTag("player"))
		{
			die();
		} */
	}
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("player"))
		{
			//! die();
		}
	}

	void die()
	{
		// Debug.Log("patch notes 3.14: removed death");
		gameController.death();
	}
}
