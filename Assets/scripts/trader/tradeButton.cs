using UnityEngine;

public class tradeButton : MonoBehaviour
{
	public menuManager menuManager;

	void Start()
	{
		menuManager = FindObjectOfType<menuManager>();
	}

	void OnMouseDown()
	{
		menuManager.traderMenu.trade();
	}
}