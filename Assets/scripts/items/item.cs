using UnityEngine;

public enum kind { armor, tool, weapon, cosmetic, gem, rock, bioMaterial };

[CreateAssetMenu(menuName = "item")]
public class item : ScriptableObject
{
	public int itemID;
	public string itemName;
	public Sprite itemSprite;
	public string description;
	public kind kind;
}

// old stuff - mainly item pickup (that doesnt belong here bruh)
/* 

// audioManager audioMan;
	// audioMan = FindObjectOfType<audioManager>();
	// audioMan.playSfx(audioMan.sfxItemPickUp); 

	public GameObject objPrefab;
	inventory inv;
	public GameObject pickupTooltip;

	void Start()
	{
		inv = FindObjectOfType<inventory>();
		pickupTooltip.SetActive(false);
	}

	bool isOn;
	void Update()
	{
		if (isOn && Input.GetKeyDown(KeyCode.E))
		{
			pickUp();
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		pickupTooltip.SetActive(true);
		isOn = true;
	}

	void OnTriggerExit2D(Collider2D other)
	{
		pickupTooltip.SetActive(false);
		isOn = false;
	}

	void pickUp()
	{
		inv.addItem(GetComponent<Sprite>());
		Destroy(gameObject);

		Debug.Log($"item {gameObject.name} picked up");
	}

 */
