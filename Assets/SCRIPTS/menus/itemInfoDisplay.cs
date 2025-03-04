using TMPro;
using UnityEngine;

public class itemInfoDisplay : MonoBehaviour
{
    public SpriteRenderer invSprite_itemSprite;
    public TMP_Text invTxt_itemName;
    public TMP_Text invTxt_itemDescription;

    [Header("placeholders")]
    public Sprite placeholderItemSprite;
    public string placeholderItemName;
    [TextArea] public string placeholderItemDescription;

    void OnEnable()
    {
        showInfo(null);
    }

    public void showInfo(item it)
    {
        void show(Sprite sprite, string name, string description)
        {
            invSprite_itemSprite.sprite = sprite;
            invTxt_itemName.SetText(name);
            invTxt_itemDescription.SetText(description);
        }

        if (it != null)
            show(it.itemSprite, it.itemName, it.itemDescription);
        else show(placeholderItemSprite, placeholderItemName, placeholderItemDescription);
    }
}