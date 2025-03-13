using TMPro;
using UnityEngine;

public class labInvItem : invItem
{
	[HideInInspector] public int assignedAmount;
	public TMP_Text amountTextDisp;

	new void OnMouseDown()
	{
		base.OnMouseDown();

		if (menuManager.toolWorkstationMenu.gameObject.activeSelf)
		{
			if (assignedItem is scrap scrap)
				menuManager.toolWorkstationMenu.assignScrap(scrap);
			else menuManager.toolWorkstationMenu.assignScrap(null);
		}
	}
	public void assignItem(item it, int amount)
	{
		base.assignItem(it);
		assignedAmount = amount;
		if (amount == 0)
		{
			amountTextDisp.text = "";
		}
		else amountTextDisp.text = assignedAmount.ToString();
	}
}
