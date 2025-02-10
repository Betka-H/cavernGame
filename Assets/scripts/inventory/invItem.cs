using UnityEngine;

public class invItem : MonoBehaviour
{
	[HideInInspector]
	public item assignedItem;

	private itemMenu itemMenu;

	private SpriteRenderer spriteRenderer;
	// public BoxCollider2D boxCol;

	void Start()
	{
		itemMenu = FindObjectOfType<itemMenu>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void OnMouseDown()
	{
		setTootip();
	}

	void OnMouseEnter()
	{
		transform.localScale += new Vector3(0.25f, 0.25f, 0);
	}
	void OnMouseExit()
	{
		transform.localScale -= new Vector3(0.25f, 0.25f, 0);
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
