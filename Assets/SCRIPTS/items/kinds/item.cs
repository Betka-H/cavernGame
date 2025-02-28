using UnityEngine;

public class item : ScriptableObject
{
	[Header("item")]
	public string itemName;
	[TextArea]
	public string itemDescription;
	public Sprite itemSprite;
}

// audioManager audioMan;
// audioMan = FindObjectOfType<audioManager>();
// audioMan.playSfx(audioMan.sfxItemPickUp);
