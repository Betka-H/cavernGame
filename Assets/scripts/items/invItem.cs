using UnityEngine;

public class invItem : MonoBehaviour
{
	public BoxCollider2D boxCol;
	[HideInInspector]
	public item item;

	SpriteRenderer spriteRenderer;

	void Start()
	{
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = item.itemSprite;
	}

	void OnMouseEnter()
	{
		tooltipToggle(1);
	}
	void OnMouseExit()
	{
		tooltipToggle(0);
	}

	void tooltipToggle(int onOff)
	{
		switch (onOff)
		{
			case 0:
				tooltipDisable();
				break;
			case 1:
				tooltipEnable();
				break;
			default:
				Debug.Log("invalid input");
				break;
		}
	}

	void tooltipEnable()
	{

	}
	void tooltipDisable()
	{

	}
}
