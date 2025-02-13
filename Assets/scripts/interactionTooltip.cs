using TMPro;
using UnityEngine;

public enum tooltipKind { item, trader };

public class interactionTooltip : MonoBehaviour
{
	public TMP_Text text;

	public void setTooltipText(tooltipKind tk)
	{
		string key;
		string msg = "press [";
		switch (tk)
		{
			case tooltipKind.item:
				msg += "E";
				break;
			case tooltipKind.trader:
				msg += "E";
				break;
		}
		msg += "] to ";
	}
}
