using UnityEngine;

//? all items. further divided into loot and gear
public enum itemKind { empty, loot, gear };
public class item : ScriptableObject
{
	[Header("item")]
	// [Range(1, 100)]
	// public int itemID;
	public string itemName;
	[TextArea]
	public string itemDescription;
	public Sprite itemSprite;
}

//*=========================================================================
//? EMPTY: empty item
public class empty : item
{
	[HideInInspector]
	public itemKind itemKind = itemKind.empty;

	[Header("empty")]
	public int TMP_emptyBehavior;
}

//? LOOT: shit you can find in the cave
public enum lootKind { mineral, biomaterial, scrap };
public class loot : item
{
	[HideInInspector]
	public itemKind itemKind = itemKind.loot;

	[Header("loot")]
	[Tooltip("the larger the number, the more common the item is")]
	[Range(1, 100)]
	public int rarity;
}

//? GEAR: equipable. cannot be found as loot in the cave
public enum equipSlot { headEquip, handEquip, bodyEquip }

public class gear : item
{
	[HideInInspector]
	public itemKind itemKind = itemKind.gear;

	[Header("gear")]
	[Range(1, 5)]
	public int level;
}

/* //*-------------------------------------
//? minerals
public enum gemColor { red, orange, yellow, green, blue, purple, pink, clear }
[CreateAssetMenu(menuName = "itemL_Mineral")]
public class mineral : loot
{
	[HideInInspector]
	public lootKind lootKind = lootKind.mineral;

	[Header("mineral")]
	[Tooltip("red gem - ruby, orange gem - , yellow gem - , green gem - emerald, blue gem - , purple gem - amethyst, pink gem - , clear gem - diamond")]
	public gemColor gemColor;
}

//? biomaterials
public enum biomatKind { soil, plant, liquid, animal }
[CreateAssetMenu(menuName = "itemL_BioMat")]
public class bioMat : loot
{
	[HideInInspector]
	public lootKind lootKind = lootKind.biomaterial;

	[Header("biomaterial")]
	public biomatKind biomatKind;
}

//? scraps
public enum scrapKind { dirt, plant, liquid, animal }
[CreateAssetMenu(menuName = "itemL_Scrap")]
public class scrap : loot
{
	[HideInInspector]
	public lootKind lootKind = lootKind.scrap;

	[Header("scrap")]
	public scrapKind scrapKind;
}

//*=========================================================================
//? equipable. cannot be found as loot in the cave
public enum equipSlot { headEquip, handEquip, bodyEquip }
public class gear : item
{
	[HideInInspector]
	public itemKind itemKind = itemKind.gear;

	[Header("gear")]
	[Range(1, 5)]
	public int level;
}

//*-------------------------------------
//? head equip
public enum headEqKind { flashlight, NVgoggles };
[CreateAssetMenu(menuName = "itemE_HeadEq")]
public class headEquip : gear
{
	[HideInInspector]
	public equipSlot equipSlot = equipSlot.headEquip;

	[Header("head equip")]
	public headEqKind headEqKind;
}

//? hand equip
public enum handEqKind { pickaxe, sledgehammer, vial };
[CreateAssetMenu(menuName = "itemE_HandEq")]
public class handEquip : gear
{
	[HideInInspector]
	public equipSlot equipSlot = equipSlot.handEquip;

	[Header("hand equip")]
	public handEqKind handEqKind;
}

//? body equip
public enum bodyEqKind { hazmatSuit };
[CreateAssetMenu(menuName = "itemE_BodyEq")]
public class bodyEquip : gear
{
	[HideInInspector]
	public equipSlot equipSlot = equipSlot.bodyEquip;

	[Header("body equip")]
	public bodyEqKind bodyEqKind;
}

//*=========================================================================
 */

/* 
// old stuff - mainly item pickup (that doesnt belong here bruh)

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
