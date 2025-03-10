using UnityEngine;
using UnityEngine.Events;

public class button : MonoBehaviour
{
    public bool doDefaultSfx = true;
    [Header("event to do")][SerializeField] private UnityEvent buttonEvent;
    audioManager audioManager;

    void Start()
    {
        audioManager = FindObjectOfType<audioManager>();
    }

    void OnMouseEnter()
    {
        audioManager.playSfx(audioManager.uiSfxSource, audioManager.uiButtonMouseover, true);
    }

    void OnMouseDown()
    {
        // Debug.Log("invoke?");
        if (doDefaultSfx)
            audioManager.playSfx(audioManager.uiSfxSource, audioManager.uiButtonClick, true);
        buttonEvent.Invoke();
    }
}
