using UnityEngine;

public class invItem : MonoBehaviour
{
	[HideInInspector]
	public item assignedItem;

	private itemMenu itemMenu;

	private SpriteRenderer spriteRenderer;
	// public BoxCollider2D boxCol;

	private Vector3 normalScaleTransform;

	void Awake()
	{
		itemMenu = FindObjectOfType<itemMenu>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		setNormalScale();
	}

	void OnMouseDown()
	{
		setTootip();
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

	void logScale()
	{
		Debug.Log($"scale: {normalScaleTransform.x}, {normalScaleTransform.y}");
	}

	void setNormalScale()
	{
		normalScaleTransform = transform.localScale;
		// logScale();
	}
	void scaleUp()
	{
		transform.localScale = normalScaleTransform + new Vector3(0.25f, 0.25f, 0);
	}
	void scaleNormal()
	{
		transform.localScale = normalScaleTransform;
		// logScale();
	}

	void setTootip()
	{
		// Debug.Log(assignedItem);
		itemMenu.showTooltip(assignedItem);
	}

	public void assignItem(item it)
	{
		// Debug.Log(it);
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
