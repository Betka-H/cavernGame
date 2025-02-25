using UnityEngine;

public class roomWarp : MonoBehaviour
{
	public Transform destination;
	public roomController rController;
	[Tooltip("is left")]
	public leftRight lr;

	void OnTriggerEnter2D(Collider2D other)
	{
		warp(other);
		changeRoom();
	}

	void warp(Collider2D other)
	{
		Vector3 current = other.transform.position;
		current.x = destination.position.x;
		other.transform.position = current;
	}

	void changeRoom()
	{
		rController.changeRoom(lr);
	}

	//* fade
	/* public SpriteRenderer black;
   void fade()
   {
	   StopCoroutine(Efade());
	   StartCoroutine(Efade());
   }
   IEnumerator Efade()
   {
	   black.color = new Color(0f, 0f, 0f, 100f); // solid
	   for (float i = 1; i >= 0; i -= Time.deltaTime * 1.5f)
	   {
		   if (i < 0.1f)
		   {
			   black.color = new Color(0f, 0f, 0f, 0f); // transparent
			   break;
		   }
		   black.color = new Color(0f, 0f, 0f, i);
		   yield return null;
	   }
   } */
}
