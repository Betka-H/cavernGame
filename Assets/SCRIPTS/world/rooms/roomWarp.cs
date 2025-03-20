using UnityEngine;

public class roomWarp : MonoBehaviour
{
	public Transform destination; // other side of the screen the player will be moved to
	[Tooltip("left or right side of screen")]
	public leftRight lr;

	public roomController roomController;

	void OnTriggerEnter2D(Collider2D other)
	{
		//* check if player?
		warpTo();
		roomController.changeRoom(lr);

		void warpTo()
		{
			Vector3 otherPos = other.transform.position; // copy current position of other
			otherPos.x = destination.position.x; // change new position x to destination x
			other.transform.position = otherPos; // apply new position to other
		}
	}
}
