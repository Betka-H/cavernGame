public class caveInvItem : invItem
{
	new void OnMouseDown()
	{
		base.OnMouseDown();

		if (menuManager.traderMenu.gameObject.activeSelf)
			menuManager.traderMenu.offer();
	}

	new public void assignItem(item it)
	{
		base.assignItem(it);
	}
}
