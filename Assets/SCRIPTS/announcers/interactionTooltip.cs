using UnityEngine.Events;
using TMPro;
using UnityEngine;

public enum tooltipKind { enter, exit, item, trader, toolWorkshop, equipWorkshop, missionWorkshop };

public class interactionTooltip : MonoBehaviour
{
	public TMP_Text txtDisplay;

	bool isLocked;

	[Header("interaction events")]
	[Header("trader event")][SerializeField] private UnityEvent traderEvent;
	[Header("cavern enter event")][SerializeField] private UnityEvent enterEvent;
	[Header("cavern exit event")][SerializeField] private UnityEvent exitEvent;
	[Header("tool workshop event")][SerializeField] private UnityEvent toolWorkshopEvent;
	[Header("equip workshop event")][SerializeField] private UnityEvent equipWorkshopEvent;
	[Header("mission workshop event")][SerializeField] private UnityEvent missionWorkshopEvent;

	// called on tooltip caller enter
	public void showTooltip(KeyCode key, tooltipKind tk)
	{
		isLocked = false;
		string action = "";
		switch (tk)
		{
			case tooltipKind.item:
				action = "pick up item";
				break;
			case tooltipKind.trader:

				// only if knows about the trader
				if (FindObjectOfType<roomController>().m_hasIntroducedTrader)
					action = "trade";
				else isLocked = true;

				break;
			case tooltipKind.enter:
				action = "enter the cavern";
				break;
			case tooltipKind.exit:
				action = "exit the cavern";
				break;
			case tooltipKind.toolWorkshop:
				action = "enter crafting lab";
				break;
			case tooltipKind.equipWorkshop:
				action = "enter locker room";
				break;
			case tooltipKind.missionWorkshop:
				action = "see current mission";
				break;
		}

		if (!isLocked)
		{
			txtDisplay.text = $"press {key} to {action}";

			// isLocked = false;
			gameObject.SetActive(true);
		}
	}

	// called from tooltip caller interaction
	public void action(tooltipKind tooltipKind)
	{
		if (!isLocked)
			switch (tooltipKind)
			{
				case tooltipKind.item:
					// nothign
					break;
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
				case tooltipKind.equipWorkshop:
					equipWorkshopEvent.Invoke();
					break;
				case tooltipKind.missionWorkshop:
					missionWorkshopEvent.Invoke();
					break;
				default:
					Debug.LogError($"invalid tooltipkind");
					break;
			}
	}

	// called on tooltip caller exit & game controller menu clear
	public void hideTooltip()
	{
		if (this != null)
			gameObject.SetActive(false);
	}
}
