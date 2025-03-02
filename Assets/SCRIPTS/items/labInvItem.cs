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
		/* menuManager.itemMenu.selectedItem = assignedItem;
		menuManager.itemMenu.showInfo();

		if (menuManager.traderMenu.gameObject.activeSelf)
		{
			menuManager.traderMenu.offer();
		} */
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
		if (it != null)
		{
			assignedItem = it;
			setSprite();
			assignedAmount = amount;
			setAmount();
			gameObject.SetActive(true);
		}
		else gameObject.SetActive(false);
	}

	void setSprite()
	{
		spriteRenderer.sprite = assignedItem.itemSprite;
	}
	void setAmount()
	{
		amountTextDisp.text = assignedAmount.ToString();
	}
}
