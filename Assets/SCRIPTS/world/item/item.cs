using UnityEngine;

public class item : ScriptableObject
{
	[Header("item")]
	[Min(0)]
	public float itemId;
	public string itemName;
	[TextArea]
	public string itemDescription;
	public Sprite itemSprite;
}
