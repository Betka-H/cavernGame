using System.Collections;
using UnityEngine;

//* use itween for this?
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
        canvasGroup = gameObject.AddComponent<CanvasGroup>(); // canvas group allows fading
        StartCoroutine(animate());
    }

    IEnumerator animate()
    {
        float timer = 0f;

        // my part lol
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