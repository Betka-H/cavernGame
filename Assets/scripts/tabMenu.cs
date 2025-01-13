using UnityEngine;

public class tabMenu : MonoBehaviour
{
	public inventory inv;

	void Start()
	{
		gameObject.SetActive(false);
	}

	void OnEnable()
	{
		// inv.refreshItems();
	}
}
