using TMPro;
using UnityEngine;

public class itemInfoDisplay : MonoBehaviour
{
    [HideInInspector] public item selectedItem;

    public SpriteRenderer invSprite_itemSprite;
    public TMP_Text invTxt_itemName;
    public TMP_Text invTxt_itemDescription;

    [Header("placeholders")]
    public Sprite placeholderItemSprite;
    public string placeholderItemName;
    [TextArea] public string placeholderItemDescription;

    void OnEnable()
    {
        // selectedItem = null;
        showInfo(null);
    }

    public void showInfo(item it)
    {
        selectedItem = it;

        void show(Sprite sprite, string name, string description)
        {
            invSprite_itemSprite.sprite = sprite;
            invTxt_itemName.SetText(name);
            invTxt_itemDescription.SetText(description);
        }

        if (selectedItem != null)
            show(selectedItem.itemSprite, selectedItem.itemName, selectedItem.itemDescription);
        else show(placeholderItemSprite, placeholderItemName, placeholderItemDescription);
    }
}