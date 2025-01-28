using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class itemList : MonoBehaviour
{
	public item[] items;
	public item item1;

	public Sprite spriteBucket;

	void Start()
	{
		foreach (item id in items)
		{

		}
		item1 = new item()
		{
			itemID = 0,
			itemName = "bucket",
			itemDescription = "wonderful thing",
			itemKind = kind.weapon,
			itemSprite = spriteBucket
		};
	}
}
