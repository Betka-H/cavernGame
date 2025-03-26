using System.Linq;
using TMPro;
using UnityEngine;

public class callScreen : MonoBehaviour
{
    public SpriteRenderer speakerSpriteRenderer;
    public TMP_Text txtSpeakerName;
    public TMP_Text txtMessage;

    public Transform skipTutorialBtn;

    public missionManager missionManager;

    void OnEnable()
    {
        if (missionManager.checkCurrentMission(-1, 0))
        // if in tutorial
        {
            skipTutorialBtn.gameObject.SetActive(true);
        }
        else skipTutorialBtn.gameObject.SetActive(false);
    }

    public void talk(callSO call)
    {
        int currentMsg = call.currentMessage;

        speakerSpriteRenderer.sprite = call.talkSprites[currentMsg];
        // FindObjectOfType<spriteSizeController>().setFixedSize(speakerSpriteRenderer, 100); // chatgpt

        txtSpeakerName.text = call.speakerNames[currentMsg];
        txtMessage.text = call.messages[currentMsg];

        gameObject.SetActive(true);
    }
}