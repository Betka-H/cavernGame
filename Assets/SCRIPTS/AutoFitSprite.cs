using UnityEngine;

// chatgpt
[RequireComponent(typeof(SpriteRenderer))]
public class AutoFitSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public Vector2 targetSpriteSize = new Vector2(100, 100); // Set desired width & height in the Inspector

    Sprite lastSprite;
    void LateUpdate()
    {
        if (spriteRenderer != null && spriteRenderer.sprite != lastSprite)
        {
            lastSprite = spriteRenderer.sprite;
            AdjustScale();
        }
    }

    //// Update scale automatically if sprite changes in editor << straight up lies
    void OnValidate()
    {
        AdjustScale();
    }

    void AdjustScale()
    {
        if (spriteRenderer.sprite == null) return; // Avoid errors if no sprite is assigned

        Vector2 spriteSize = spriteRenderer.sprite.bounds.size; // Get sprite's original size

        float widthRatio = targetSpriteSize.x / spriteSize.x;
        float heightRatio = targetSpriteSize.y / spriteSize.y;
        float finalScale = Mathf.Min(widthRatio, heightRatio); // Pick the smaller ratio to maintain aspect ratio

        transform.localScale = new Vector3(finalScale, finalScale, 1);
    }
}