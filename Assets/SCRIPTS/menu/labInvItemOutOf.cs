using TMPro;
using UnityEngine;

public class labInvItemOutOf : invItem
{
	public TMP_Text amountTextDisp;
	public SpriteRenderer bgSpriteRenderer;
	public Transform wholeThing;

	[HideInInspector] public int hasAmount;
	[HideInInspector] public int needsAmount;

	[Header("colors")]
	public Color normal;
	public Color hasEnough;
	public Color notEnough;

	new void OnMouseDown()
	{
		base.OnMouseDown();
	}
	public void assignItem(item it, int hasAmount, int neededAmount)
	{
		base.assignItem(it);
		amountTextDisp.text = $"{hasAmount} / {neededAmount}";

		wholeThing.gameObject.SetActive(true);
		if (neededAmount == 0)
			wholeThing.gameObject.SetActive(false);
		else if (hasAmount < neededAmount)
			bgSpriteRenderer.color = notEnough;
		else if (hasAmount >= neededAmount)
			bgSpriteRenderer.color = hasEnough;
	}
}
