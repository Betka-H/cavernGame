using UnityEngine.Events;
using TMPro;
using UnityEngine;

public enum tooltipKind { enter, exit, item, trader, toolWorkshop };

public class interactionTooltip : MonoBehaviour
{
	public TMP_Text textDisplay;

	[Header("interact functions")]
	[Header("trader event")][SerializeField] private UnityEvent traderEvent;
	[Header("cavern enter event")][SerializeField] private UnityEvent enterEvent;
	[Header("cavern exit event")][SerializeField] private UnityEvent exitEvent;
	[Header("tool workshop event")][SerializeField] private UnityEvent toolWorkshopEvent;

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
			case tooltipKind.enter:
				action = "enter the cavern";
				break;
			case tooltipKind.exit:
				action = "exit the cavern";
				break;
			case tooltipKind.toolWorkshop:
				action = "craft tools";
				break;
		}

		textDisplay.text = $"press {key} to {action}";

		transform.localPosition = pos;

		gameObject.SetActive(true);
	}

	public void action(tooltipKind tooltipKind)
	{
		switch (tooltipKind)
		{
			case tooltipKind.trader:
				traderEvent.Invoke();
				break;
			case tooltipKind.enter:
				enterEvent.Invoke();
				break;
			case tooltipKind.exit:
				exitEvent.Invoke();
				break;
			case tooltipKind.toolWorkshop:
				toolWorkshopEvent.Invoke();
				break;
		}
	}

	public void hideTooltip()
	{
		if (gameObject != null)
			gameObject.SetActive(false);
	}
}
