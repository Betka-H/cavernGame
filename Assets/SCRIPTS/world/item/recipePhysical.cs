using UnityEngine;

//* i like this one
public class recipePhysical : MonoBehaviour
{
    void Update()
    {
        gameObject.transform.position = gameObject.transform.position + Vector3.down * Time.unscaledDeltaTime * 5;
        if (gameObject.transform.position.y < 1.15)
            Destroy(gameObject);
    }
}