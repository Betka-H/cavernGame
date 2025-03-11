using System.Collections;
using UnityEngine;

// chatgpt
public class announcerObj : MonoBehaviour
{
    // 50, 0.5, 3, 1.5
    public float speed;
    public float lifetime;
    public float fadeSpeed;
    public float dontFadeFor;

    private CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = gameObject.AddComponent<CanvasGroup>(); // Allows fading
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        float timer = 0f;

        // my part lol
        while (timer < dontFadeFor)
        {
            transform.localPosition += Vector3.up * speed * Time.unscaledDeltaTime; // Move up
            timer += Time.unscaledDeltaTime;
            yield return null;
        }
        timer = 0;

        while (timer < lifetime)
        {
            transform.localPosition += Vector3.up * speed * Time.unscaledDeltaTime; // Move up
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, timer / lifetime); // Fade out
            timer += Time.unscaledDeltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}