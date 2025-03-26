using UnityEngine;

public class spriteSizeController : MonoBehaviour
{
    public void setFixedSize(SpriteRenderer spriteRenderer, float size)
    // chatgpt
    {
        if (spriteRenderer.sprite == null) return;

        Vector2 targetSize = new Vector2(size, size);

        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;
        Vector3 objectScale = spriteRenderer.transform.localScale;

        objectScale.x = targetSize.x / spriteSize.x;
        objectScale.y = targetSize.y / spriteSize.y;

        spriteRenderer.transform.localScale = objectScale;
    }
}
