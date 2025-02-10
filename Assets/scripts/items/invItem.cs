using UnityEngine;

public class invItem : MonoBehaviour
{
	public BoxCollider2D boxCol;
	[HideInInspector]
	public item item;

	public itemMenu itemMenu;

	SpriteRenderer spriteRenderer;

	void Start()
	{
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = item.itemSprite;
	}

	void OnMouseDown()
	{
		setTootip();
	}

	void setTootip()
	{
		// itemMenu.showTooltip(item.name, item.itemSprite, item.itemDescription);
	}
}
