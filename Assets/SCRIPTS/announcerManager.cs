using UnityEngine;
using TMPro;
using System.Collections;

// chatgpt
public class announcerManager : MonoBehaviour
{
    public GameObject announcerPrefab;
    public Transform announcerParent;

    /* float logTimer = 0f;
    void Update()
    {
        logTimer += Time.deltaTime;

        if (logTimer >= 3f) // seconds
        {
            FindObjectOfType<announcerManager>().ShowMessage("Hello World!", announcerParent.localPosition);
            logTimer = 0f;
        }
    } */

    GameObject newAnnouncer;
    // float lastAnnounced = 0;
    bool canAnnounce = true;
    /* void Update()
    {
        lastAnnounced += Time.unscaledDeltaTime;
        Debug.Log($"la: {lastAnnounced}, dt: {Time.unscaledDeltaTime}");
    } */

    public void announceMessage(string message)
    {
        Debug.Log($"announcer: \"{message}\"");

        // if (lastAnnounced < 0.5f)
        if (canAnnounce)
        {
            newAnnouncer = Instantiate(announcerPrefab, announcerParent);
            newAnnouncer.transform.localPosition = announcerParent.localPosition;
            newAnnouncer.GetComponent<TextMeshProUGUI>().text = message;


            // yellow
            newAnnouncer.GetComponent<TextMeshProUGUI>().color = Color.yellow;

            // announcer reset
            // lastAnnounced = 0;
            canAnnounce = false;
            StartCoroutine(announcerCooldown());
        }
        else
        {
            // Debug.Log($"cant announce more");
            newAnnouncer = null;
        }
    }
    public void announceMessage(string message, bool debug)
    {
        announceMessage(message);

        if (newAnnouncer != null)
            if (debug)
                newAnnouncer.GetComponent<TextMeshProUGUI>().color = Color.red;
            else
                newAnnouncer.GetComponent<TextMeshProUGUI>().color = Color.yellow;
    }
    // FindObjectOfType<announcerManager>().announceMessage("amongus");

    // chatgpt
    IEnumerator announcerCooldown()
    {
        yield return new WaitForSecondsRealtime(0.65f);
        canAnnounce = true;
    }
}
