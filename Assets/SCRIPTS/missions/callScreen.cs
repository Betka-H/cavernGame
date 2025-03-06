using TMPro;
using UnityEngine;

public class callScreen : MonoBehaviour
{
    public SpriteRenderer speakerSpriteRenderer;
    public TMP_Text txtSpeakerName;
    public TMP_Text txtMessage;

    callManager callManager;

    void Awake()
    {
        callManager = FindObjectOfType<callManager>();
    }

    public void talk(callSO caller)
    {
        gameObject.SetActive(true);
        Debug.Log($"trying to talk! ({caller.currentMessage})");
        int currentMsg = caller.currentMessage;

        speakerSpriteRenderer.sprite = caller.talkSprites[currentMsg];
        txtSpeakerName.text = caller.speakerNames[currentMsg];
        txtMessage.text = caller.messages[currentMsg];
    }
}