using UnityEngine;

public class itemSlot : MonoBehaviour
{
	/* SpriteRenderer sr;
	[SerializeField]
	private bool isEnabled = true;
	private bool IsEnabled
	{
		get
		{
			return isEnabled;
		}
		set
		{
			isEnabled = value;
			toggleEnable(isEnabled);
			// Debug.Log($"item box: {isEnabled}");
		}
	}
	void OnValidate()
	{
		if (sr == null)
			sr = gameObject.GetComponent<SpriteRenderer>();

		// Trigger the IsEnabled setter when the value is changed in the Inspector
		IsEnabled = isEnabled;
	}

	Color cDefault = new Color(1f, 1f, 1f, 1f);
	Color cLightDark = new Color(0.78f, 0.78f, 0.78f, 1f);
	Color cDark = new Color(0.49f, 0.49f, 0.49f, 1f);

	void Start()
	{
		sr = gameObject.GetComponent<SpriteRenderer>();
		sr.color = Color.blue;
		toggleEnable(isEnabled);
	}

	public void toggleEnable(bool e)
	{
		if (e)
			sr.color = cDefault;
		else sr.color = cDark;
	}

	void OnMouseEnter()
	{
		sr.color = cLightDark;
	}
	void OnMouseExit()
	{
		sr.color = cDefault;
	} */
}
