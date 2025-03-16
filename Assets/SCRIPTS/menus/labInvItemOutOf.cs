using TMPro;
using UnityEngine;

public class labInvItemOutOf : invItem
{
	[HideInInspector] public int hasAmount;
	[HideInInspector] public int needsAmount;
	public TMP_Text amountTextDisp;
	public SpriteRenderer bgSpriteRenderer;
	public Color normal;
	public Color hasEnough;
	public Color notEnough;
	public Transform wholeThing;

	new void OnMouseDown()
	{
		base.OnMouseDown();

		/* if (menuManager.toolWorkstationMenu.gameObject.activeSelf)
		{
			if (assignedItem is scrap scrap)
				menuManager.toolWorkstationMenu.assignScrap(scrap);
			else menuManager.toolWorkstationMenu.assignScrap(null);
		} */
	}
	public void assignItem(item it, int hasAmount, int neededAmount)
	{
		base.assignItem(it);
		// assignedAmount = amount;
		// amountTextDisp.text = assignedAmount.ToString();
		amountTextDisp.text = $"{hasAmount} / {neededAmount}";

		wholeThing.gameObject.SetActive(true);
		if (neededAmount == 0)
		{
			/* amountTextDisp.text = "";
			bgSpriteRenderer.color = normal; */
			wholeThing.gameObject.SetActive(false);
		}
		else if (hasAmount < neededAmount)
			bgSpriteRenderer.color = notEnough;
		else if (hasAmount >= neededAmount)
			bgSpriteRenderer.color = hasEnough;
	}
}
