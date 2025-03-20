using UnityEngine;
using UnityEngine.Events;

public class button : MonoBehaviour
{
    public bool doDefaultSfx = true;
    [Header("event to do")][SerializeField] private UnityEvent buttonEvent;
    audioManager audioManager; // keep private

    void Start()
    {
        audioManager = FindObjectOfType<audioManager>();
    }

    void OnMouseEnter()
    {
        audioManager.playSfx(audioManager.uiSfxSource, audioManager.uiButtonMouseover);
    }

    void OnMouseDown()
    {
        if (doDefaultSfx)
            audioManager.playSfx(audioManager.uiSfxSource, audioManager.uiButtonClick);
        buttonEvent.Invoke();
    }
}
