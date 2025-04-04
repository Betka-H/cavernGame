using UnityEngine;
using TMPro;
using System.Collections;

// chatgpt
public class announcerManager : MonoBehaviour
{
    public GameObject announcerPrefab;
    public Transform announcerParent;

    Color defaultColor = Color.yellow;
    Color debugColor = Color.red;

    GameObject newAnnouncer;
    // base announcement
    public void announceMessage(string message)
    {
        Debug.Log($"announcer: \"{message}\"");

        if (canAnnounce)
        // when cooldown is up
        {
            // make an announcement
            newAnnouncer = Instantiate(announcerPrefab, announcerParent);
            newAnnouncer.transform.localPosition = announcerParent.localPosition;
            newAnnouncer.GetComponent<TextMeshProUGUI>().text = message;
            newAnnouncer.GetComponent<TextMeshProUGUI>().color = defaultColor;

            // start cooldown
            StartCoroutine(announcerCooldown());
        }
        else newAnnouncer = null;
    }
    // overload - changes color
    public void announceMessage(string message, bool debug)
    { //* color speech white
        // make an announcement
        announceMessage(message);

        // color text
        if (newAnnouncer != null)
            if (debug)
                newAnnouncer.GetComponent<TextMeshProUGUI>().color = debugColor;
            else newAnnouncer.GetComponent<TextMeshProUGUI>().color = defaultColor;
    }

    // chatgpt helped with concept
    bool canAnnounce = true;
    float cooldown = 0.65f;
    IEnumerator announcerCooldown()
    {
        canAnnounce = false;
        yield return new WaitForSecondsRealtime(cooldown);
        canAnnounce = true;
    }
}
