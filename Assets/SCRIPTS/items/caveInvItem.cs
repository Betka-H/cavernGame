using UnityEngine;

public class caveInvItem : MonoBehaviour
{
	[HideInInspector] public item assignedItem;

	menuManager menuManager;

	private SpriteRenderer spriteRenderer;

	private Vector3 normalScaleTransform;

	void Awake()
	{
		menuManager = FindObjectOfType<menuManager>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		setNormalScale();
	}

	void OnMouseDown()
	{
		menuManager.itemMenu.selectedItem = assignedItem;
		menuManager.itemMenu.showInfo();

		if (menuManager.traderMenu.gameObject.activeSelf)
		{
			menuManager.traderMenu.offer();
		}
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

	public void assignItem(item it)
	{
		if (it != null)
		{
			assignedItem = it;
			setSprite();
			gameObject.SetActive(true);
		}
		else gameObject.SetActive(false);

	}

	void setSprite()
	{
		spriteRenderer.sprite = assignedItem.itemSprite;
	}
}
