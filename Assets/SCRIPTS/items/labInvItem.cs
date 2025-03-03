using TMPro;
using UnityEngine;

public class labInvItem : MonoBehaviour
{
	[HideInInspector] public item assignedItem;
	[HideInInspector] public int assignedAmount;

	menuManager menuManager;

	public SpriteRenderer spriteRenderer;
	public TMP_Text amountTextDisp;

	private Vector3 normalScaleTransform;

	void Awake()
	{
		menuManager = FindObjectOfType<menuManager>();
		setNormalScale();
	}

	void OnMouseDown()
	{
		select();
		if (menuManager.toolWorkstationMenu.gameObject.activeSelf)
		{
			if (assignedItem is scrap scrap)
				menuManager.toolWorkstationMenu.assignScrap(scrap);
		}
	}

	void select()
	{
		menuManager.labItemMenu.selectedItem = assignedItem;
		menuManager.labItemMenu.showInfo();
	}

	void OnEnable()
	{
		scaleNormal();
	}
	void OnDisable()
	{
		scaleNormal();
	}

	void OnMouseEnter()
	{
		scaleUp();
	}
	void OnMouseExit()
	{
		scaleNormal();
	}

	void setNormalScale()
	{
		normalScaleTransform = transform.localScale;
	}
	void scaleUp()
	{
		transform.localScale = normalScaleTransform + new Vector3(0.25f, 0.25f, 0);
	}
	void scaleNormal()
	{
		transform.localScale = normalScaleTransform;
	}

	public void assignItem(item it, int amount)
	{
		assignedItem = it;
		displaySprite();
		assignedAmount = amount;
		displayAmount();
		;
	}

	void displaySprite()
	{
		if (assignedItem != null)
			spriteRenderer.sprite = assignedItem.itemSprite;
		else
			spriteRenderer.sprite = null;
	}
	void displayAmount()
	{
		if (assignedItem != null)
			spriteRenderer.sprite = assignedItem.itemSprite;
		else
			spriteRenderer.sprite = null;
		amountTextDisp.text = assignedAmount.ToString();
	}
}
