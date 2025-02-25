using TMPro;
using UnityEngine;

public enum tooltipKind { item, trader };

public class interactionTooltip : MonoBehaviour
{
	public TMP_Text textDisplay;

	public void showTooltip(KeyCode key, tooltipKind tk, Vector3 pos)
	{
		string action = "";
		switch (tk)
		{
			case tooltipKind.item:
				action = "pick up";
				break;
			case tooltipKind.trader:
				action = "trade";
				break;
		}

		textDisplay.text = $"press {key} to {action}";

		transform.localPosition = pos;
		// Debug.Log($"{transform.position.x}, {transform.position.y}, {transform.position.z}");

		gameObject.SetActive(true);
	}

	public void hideTooltip()
	{
		gameObject.SetActive(false);
	}
}
