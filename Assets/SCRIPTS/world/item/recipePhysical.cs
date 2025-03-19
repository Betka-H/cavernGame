using UnityEngine;

public class recipePhysical : MonoBehaviour
{
    // public Collider2D recipeDestroyCollider;
    // public Rigidbody2D rb;

    // chatgpt
    /* void Awake()
    {
        // Physics2D.simulationMode = SimulationMode2D.Script;
    } */
    /* void FixedUpdate()
    {
        if (Time.timeScale == 0)
        {
            rb.MovePosition(rb.position + Vector2.right * 5f * Time.unscaledDeltaTime);
        }
    } */
    void Update()
    {
        gameObject.transform.position = gameObject.transform.position + Vector3.down * Time.unscaledDeltaTime * 5;
        // Debug.LogWarning($"obj y: {gameObject.transform.localPosition.y}");
        if (gameObject.transform.position.y < 1.15)
            Destroy(gameObject);
        /* if (Time.timeScale == 0)
        {
            // rb.velocity = new Vector2(0f, -3.5f);
            // rb.MovePosition(rb.position + Vector2.down * 3.5f * Time.unscaledDeltaTime);
            gameObject.transform.position = gameObject.transform.position + Vector3.down * 21f * Time.unscaledDeltaTime;
            // Physics2D.Simulate(Time.unscaledDeltaTime); // Runs physics at timeScale 0
            // Physics2D.Simulate(0.02f);
        } */
    }

    /* void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == recipeDestroyCollider)
        {
            Destroy(gameObject);
        }
        Debug.LogWarning($"collsison with {other}");
        Destroy(gameObject);
    } */
}