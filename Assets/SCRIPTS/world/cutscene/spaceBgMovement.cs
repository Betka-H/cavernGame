using UnityEngine;

public class spaceBgMovement : MonoBehaviour
{
    void Start()
    {
        iTween.MoveBy(gameObject, iTween.Hash("x", -5, "easeType", iTween.EaseType.linear, "time", 12));
    }
}