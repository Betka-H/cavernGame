using System.Collections;
using UnityEngine;

// chatgpt helped with concept
public class announcerObj : MonoBehaviour
{
    // 50, 0.5, 3, 1.5
    public float speed;
    public float lifetime;
    public float fadeSpeed;
    public float dontFadeFor;

    public CanvasGroup canvasGroup;

    void Start()
    {
        StartCoroutine(animate());
    }

    IEnumerator animate()
    //* use itween for this?
    {
        float timer = 0f;

        // dontFadeFor added by me
        while (timer < dontFadeFor)
        {
            // move up
            transform.localPosition += Vector3.up * speed * Time.unscaledDeltaTime;

            timer += Time.unscaledDeltaTime;
            yield return null;
        }
        timer = 0;

        while (timer < lifetime)
        {
            // move up
            transform.localPosition += Vector3.up * speed * Time.unscaledDeltaTime;

            // fade
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, timer / lifetime);

            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        // when done moving and fading
        Destroy(gameObject);
    }
}