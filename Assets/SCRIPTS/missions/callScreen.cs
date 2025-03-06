using System.Linq;
using TMPro;
using UnityEngine;

public class callScreen : MonoBehaviour
{
    public SpriteRenderer speakerSpriteRenderer;
    public TMP_Text txtSpeakerName;
    public TMP_Text txtMessage;

    menuManager menuManager;
    callManager callManager;

    void Awake()
    {
        menuManager = FindObjectOfType<menuManager>();
        callManager = FindObjectOfType<callManager>();
    }

    public void talk(callSO call)
    {
        // gameObject.SetActive(true);
        // if (call.messages.Length > 0)
        {
            Debug.Log($"trying to talk! ({call.currentMessage})");
            int currentMsg = call.currentMessage;

            speakerSpriteRenderer.sprite = call.talkSprites[currentMsg];
            txtSpeakerName.text = call.speakerNames[currentMsg];
            txtMessage.text = call.messages[currentMsg];
        }
        // else Debug.Log("empty call!");
    }
}