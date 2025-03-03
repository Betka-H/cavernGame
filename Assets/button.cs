using UnityEngine;
using UnityEngine.Events;

public class button : MonoBehaviour
{
    [Header("event to do")][SerializeField] private UnityEvent buttonEvent;

    void OnMouseDown()
    {
        Debug.Log("invoke?");
        buttonEvent.Invoke();
    }
}
