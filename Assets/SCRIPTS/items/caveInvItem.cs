using UnityEngine;

public class caveInvItem : invItem
{
	new void OnMouseDown()
	{
		base.OnMouseDown();

		Debug.LogWarning("weird code alert");
		if (menuManager.traderMenu.gameObject.activeSelf)
		{
			menuManager.traderMenu.offer();
		}
	}

	new public void assignItem(item it)
	{
		base.assignItem(it);
	}
}
