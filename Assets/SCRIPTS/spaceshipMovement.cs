using UnityEngine;

public class spaceshipMovement : MonoBehaviour
{
    void Start()
    {
        iTween.MoveBy(gameObject, iTween.Hash("x", 32, "easeType", iTween.EaseType.easeInSine, "time", 10));
    }
}
