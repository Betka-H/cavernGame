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

    /* void Update()
    { // chatgpt
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("UI")))
        {
            Debug.Log("Clicked UI Element: " + hit.collider.name);
        }
    } */

    void OnMouseEnter()
    {
        // audioManager.playSfx(audioManager.uiSfxSource, audioManager.uiButtonMouseover, true);
        audioManager.playSfx(audioManager.uiSfxSource, audioManager.uiButtonMouseover);
    }

    void OnMouseDown()
    {
        // Debug.Log("invoke btn event?");
        if (doDefaultSfx)
        {
            // audioManager.playSfx(audioManager.uiSfxSource, audioManager.uiButtonClick, true);
            audioManager.playSfx(audioManager.uiSfxSource, audioManager.uiButtonClick);
        }
        buttonEvent.Invoke();
    }
}
