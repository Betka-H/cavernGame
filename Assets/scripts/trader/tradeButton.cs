using UnityEngine;

public class tradeButton : MonoBehaviour
{
	public traderMenu traderMenu;

	void OnMouseDown()
	{
		traderMenu.trade();
	}
}