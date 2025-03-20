using System.Collections;
using TMPro;
using UnityEngine;

//! deprecated
public class cavernTimer : MonoBehaviour
{
    [Min(5)]
    public int initialTimeMin = 5;
    public int initialTimeMax = 8;

    public int caveInTimeMin = 5;
    public int caveInTimeMax = 8;

    roomController roomController;
    int roomsToCaveIn;

    public TMP_Text timerDisplayTxt;

    void Start()
    {
        roomController = FindObjectOfType<roomController>();

        StartCoroutine(startCaveTimer());
    }

    IEnumerator startCaveTimer()
    {
        int countdown;
        countdown = Random.Range(initialTimeMin, initialTimeMax);
        // Debug.Log($"started cave timer for {countdown}s");
        // FindObjectOfType<announcerManager>().announceMessage($"started cave timer for {countdown}s");

        // initial
        // yield return new WaitForSeconds(countdown);
        while (countdown > 0)
        {
            // Debug.Log($"Time left: {countdown:F2}s");
            yield return new WaitForSeconds(1);
            countdown -= 1;
            timerDisplayTxt.text = $"time left: {countdown}s";
        }
        endInitialTimer();

        roomsToCaveIn = roomController.selectedRooms.Length;
        for (int i = 0; i < roomsToCaveIn; i++)
        {
            countdown = Random.Range(caveInTimeMin, caveInTimeMax);
            yield return new WaitForSeconds(countdown);
        }
        // endInitialTimer();
    }

    void endInitialTimer()
    {
        //! Debug.Log("initial timer ended");
        //! Debug.Log("cue cave-ins");
        // FindObjectOfType<announcerManager>().announceMessage("timer finished :33", true);
        // StartCoroutine(StartRandomTimer());
    }
}

//! chatgpt
/* using System.Collections;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public float minTime = 5f; // Minimum countdown time
    public float maxTime = 15f; // Maximum countdown time

    void Start()
    {
        StartCoroutine(StartRandomTimer()); // Start the first timer
    }

    IEnumerator StartRandomTimer()
    {
        float countdown = Random.Range(minTime, maxTime); // Pick a random duration
        Debug.Log($"Timer started: {countdown} seconds");

        yield return new WaitForSeconds(countdown); // Wait for the countdown to finish

        TimerEnded(); // Call the function when time is up
    }

    void TimerEnded()
    {
        Debug.Log("Timer finished! Starting next timer...");
        StartCoroutine(StartRandomTimer()); // Restart a new timer
    }
} */
