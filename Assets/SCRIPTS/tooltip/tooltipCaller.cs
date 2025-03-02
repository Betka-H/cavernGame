using UnityEngine;

public class tooltipCaller : MonoBehaviour
{
	interactionTooltip tooltipObj;
	public tooltipKind tooltipKind;
	public KeyCode keyToInteract;

	bool isEnabled;

	void Awake()
	{
		tooltipObj = FindObjectOfType<interactionTooltip>(true);
	}

	void Update()
	{
		if (isEnabled && Input.GetKeyDown(keyToInteract))
		{
			tooltipObj.action(tooltipKind);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("player"))
		{
			tooltipObj.showTooltip(keyToInteract, tooltipKind, new Vector3(transform.localPosition.x, transform.localPosition.y + 150, 0f));

			isEnabled = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		hideTooltip();
	}

	void hideTooltip()
	{
		tooltipObj.hideTooltip();
		isEnabled = false;
	}
}
