using System.Linq;
using TMPro;
using UnityEngine;

public class callScreen : MonoBehaviour
{
    public SpriteRenderer speakerSpriteRenderer;
    public TMP_Text txtSpeakerName;
    public TMP_Text txtMessage;

    public Transform skipTutorialBtn;

    menuManager menuManager;
    callManager callManager;
    missionManager missionManager;

    void Awake()
    {
        menuManager = FindObjectOfType<menuManager>();
        callManager = FindObjectOfType<callManager>();
        missionManager = FindObjectOfType<missionManager>();
    }

    void OnEnable()
    {
        // if (missionManager.currentMission == 0) // if in tutorial
        if (missionManager.checkCurrentMission(-1, 0)) // if in tutorial
        {
            skipTutorialBtn.gameObject.SetActive(true);
            // spaceStuff.gameObject.SetActive(true);
        }
        else
        {
            skipTutorialBtn.gameObject.SetActive(false);
            // spaceStuff.gameObject.SetActive(false);
        }
    }

    public void talk(callSO call)
    {
        // gameObject.SetActive(true);
        // if (call.messages.Length > 0)
        if (missionManager.checkCurrentMission(-1, 0)) // if in space call
        {
            // gameObject.SetActive(false);
            Debug.Log($"spaaaace!!1!1");
        }
        // else
        {
            // Debug.Log($"trying to talk! ({call.currentMessage})");
            int currentMsg = call.currentMessage;

            speakerSpriteRenderer.sprite = call.talkSprites[currentMsg];
            txtSpeakerName.text = call.speakerNames[currentMsg];
            txtMessage.text = call.messages[currentMsg];

            gameObject.SetActive(true);
        }
        // else Debug.Log("empty call!");
    }
}