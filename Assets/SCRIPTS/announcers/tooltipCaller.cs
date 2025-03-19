using UnityEngine;

public class tooltipCaller : MonoBehaviour
{
	interactionTooltip tooltipObj;
	public tooltipKind tooltipKind;
	public KeyCode keyToInteract;

	public bool onlyActivateOncePerEnter;
	[HideInInspector] public bool isExitPoint;

	bool isEnabled;

	void Awake()
	{
		tooltipObj = FindObjectOfType<interactionTooltip>(true);
		keyToInteract = KeyCode.E;
	}

	void Update()
	{
		if (isEnabled && Input.GetKeyDown(keyToInteract))
		{
			if (isItem())
				GetComponent<worldItem>().pickUp();
			else tooltipObj.action(tooltipKind);

			if (onlyActivateOncePerEnter)
				OnTriggerExit2D(null);
		}
	}
	bool isItem()
	{
		if (GetComponent<worldItem>() != null)
			return true;
		else return false;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (isExitPoint) isExitPoint = false;
		else if (other.gameObject.CompareTag("player"))
		{
			tooltipObj.showTooltip(keyToInteract, tooltipKind, new Vector3(transform.localPosition.x, transform.localPosition.y + 150, 0f));

			isEnabled = true;
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		isEnabled = false;
		tooltipObj.hideTooltip();
	}
}
