using UnityEngine;

public class invItem : MonoBehaviour
{
    [HideInInspector] public item assignedItem;

    protected menuManager menuManager;

    public SpriteRenderer spriteRenderer;

    void Awake()
    {
        menuManager = FindObjectOfType<menuManager>();
        setNormalScale();
    }

    void OnEnable()
    {
        scaleNormal();
    }

    void OnMouseEnter()
    {
        scaleUp();
    }
    void OnMouseExit()
    {
        scaleNormal();
    }

    private Vector3 normalScaleTransform;
    void setNormalScale()
    {
        normalScaleTransform = transform.localScale;
    }
    void scaleUp()
    {
        transform.localScale = normalScaleTransform + new Vector3(0.25f, 0.25f, 0);
    }
    void scaleNormal()
    {
        transform.localScale = normalScaleTransform;
    }

    protected void OnMouseDown()
    {
        showInfo();
    }

    void showInfo()
    {
        // display item info in menu
        // menuManager.itemInfoDisplay.selectedItem = assignedItem;
        menuManager.itemInfoDisplay.showInfo(assignedItem);
    }

    protected void assignItem(item it)
    {
        if (it != null)
        {
            assignedItem = it;
            spriteRenderer.sprite = assignedItem.itemSprite;
            gameObject.SetActive(true);
        }
        else gameObject.SetActive(false); // makes it unclickable
    }
}