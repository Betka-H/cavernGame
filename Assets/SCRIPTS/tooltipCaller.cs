using UnityEngine;

public class tooltipCaller : MonoBehaviour
{
	private interactionTooltip tooltipObj;
	public tooltipKind tooltipKind;
	public KeyCode keyToInteract;

	menuManager menuManager;

	public bool isEnabled;

	void Start()
	{
		tooltipObj = FindObjectOfType<interactionTooltip>(true);
		menuManager = FindObjectOfType<menuManager>();
		// Debug.Log($"tooltip obj is set to {tooltipObj}");
	}

	void Update()
	{
		if (isEnabled && Input.GetKeyDown(keyToInteract))
		{
			switch (tooltipKind)
			{
				case tooltipKind.trader:
					// Debug.Log("interacting with trader");
					// Debug.LogWarning("find better way to find tooltip interaction object");
					// Debug.Log("temporary");
					/* npcTrader traderNpc;
					traderNpc = FindObjectOfType<npcTrader>(true);
					traderNpc.toggleMenu(); */

					menuManager.toggleTradingScreen();
					break;
			}
			// hideTooltip();
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		// Debug.Log($"collision with {other}");
		if (other.gameObject.CompareTag("player"))
		{
			// tooltipObj.showTooltip(keyToInteract, tooltipKind, new Vector3(transform.localPosition.x, GetComponent<SpriteRenderer>().bounds.extents.y - 75, 0f));
			tooltipObj.showTooltip(keyToInteract, tooltipKind, new Vector3(transform.localPosition.x, transform.localPosition.y + 150, 0f));
			// tooltipObj.showTooltip(keyToInteract, tooltipKind, new Vector3(transform.localPosition.x, transform.localPosition.y, 0f));
			// Debug.Log("tooltip activated");
			// Debug.Log($"{tooltipObj.transform.position.x}, {tooltipObj.transform.position.y}, {tooltipObj.transform.position.z}");

			isEnabled = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		hideTooltip();
	}

	void hideTooltip()
	{
		Debug.Log("hiding tooltip");
		tooltipObj.hideTooltip();
		isEnabled = false;
	}

	/* void OnCollisionEnter2D(Collision2D other)
	{
		Debug.Log($"collision with {other}");
		if (other.gameObject.CompareTag("player"))
		{
			tooltipObj.showTooltip(keyToInteract, tooltipKind);
			Debug.Log("tooltip activated");
			if (Input.GetKeyDown(keyToInteract))
			{
				Debug.Log("interact action");
			}
		}
	} */
}
