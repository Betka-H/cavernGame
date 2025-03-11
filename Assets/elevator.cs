using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class elevator : MonoBehaviour
{
    public Transform doorL;
    public Transform doorR;

    float moveAmount = 1.7f;

    public Transform walls;

    audioManager audioManager;

    [HideInInspector] public bool isClosed;

    Vector2 doorLStartPos;
    Vector2 doorRStartPos;

    public bool isLab;
    public bool isFirst;

    bool isAwake;
    void Awake()
    {
        audioManager = FindObjectOfType<audioManager>();

        doorLStartPos = doorL.position;
        doorRStartPos = doorR.position;
        isAwake = true;
        // walls.gameObject.SetActive(true);
        // isClosed = true;

        // if (isLab || !isClosed)
        // if (isLab)
        // if (isLab || isClosed)
    }
    void Start()
    {
        if (!isFirst)
        {
            walls.gameObject.SetActive(false);
            openDoors(true);
        }
        else
        {
            GameObject exitObj;
            if (isLab)
                exitObj = GameObject.Find("lab exit point");
            else exitObj = GameObject.Find("cavern exit point");

            exitObj.GetComponent<tooltipCaller>().isExitPoint = true;
            Debug.Log($"eo: {exitObj}");
        }
    }

    //chatgpt
    float logTimer = 0f; // Tracks elapsed time
    void Update()
    {
        /* logTimer += Time.deltaTime; // Accumulate time

        if (logTimer >= 1f) // If 1 second has passed
        {
            Debug.Log(isClosed);
            logTimer = 0f; // Reset timer
        } */
    }//chgpt

    public void openDoors(bool instant)
    {
        if (!isAwake) Awake();

        resetDoorsToClosed();

        Debug.Log("opening doors");
        // Debug.LogWarning($"is first: {isFirst}");
        // CancelInvoke();
        // closeDoors(true);

        if (instant)
        {
            doorL.position = doorLStartPos + new Vector2(-moveAmount, 0);
            doorR.position = doorRStartPos + new Vector2(moveAmount, 0);

            walls.gameObject.SetActive(false);
            isClosed = false;
        }
        else
        {
            // walls.gameObject.SetActive(true);
            audioManager.playSfx(audioManager.worldSfxSource, audioManager.elevatorMove, true);

            iTween.MoveBy(doorL.gameObject, iTween.Hash("x", -moveAmount, "speed", 0.5, "easeType", "easeInOutExpo", "delay", 4));
            iTween.MoveBy(doorR.gameObject, iTween.Hash("x", moveAmount, "speed", 0.5, "easeType", "easeInOutExpo", "delay", 4));

            StartCoroutine("openWalls");
        }
    }
    IEnumerator openWalls()
    {
        yield return new WaitForSeconds(3);
        audioManager.playSfx(audioManager.worldSfxSource, audioManager.elevatorDing, true);
        audioManager.playSfx(audioManager.worldSfxSource, audioManager.elevatorDoors, true);

        yield return new WaitForSeconds(3.5f);
        walls.gameObject.SetActive(false);
        isClosed = false;
    }

    public void closeDoors(bool instant)
    {
        if (!isAwake) Awake();

        Debug.Log("closing doors");
        openDoors(true);

        walls.gameObject.SetActive(true);
        if (instant)
        {
            resetDoorsToClosed();

            // walls.gameObject.SetActive(true);
            isClosed = true;
        }
        else
        {
            audioManager.playSfx(audioManager.worldSfxSource, audioManager.elevatorDoors, true);
            iTween.MoveBy(doorL.gameObject, iTween.Hash("x", moveAmount, "speed", 0.5, "easeType", "easeInOutExpo"));
            iTween.MoveBy(doorR.gameObject, iTween.Hash("x", -moveAmount, "speed", 0.5, "easeType", "easeInOutExpo"));

            StartCoroutine("closeWalls");
            // walls.gameObject.SetActive(true);
            // isClosed = true;
        }
    }
    IEnumerator closeWalls()
    {
        yield return new WaitForSeconds(5);
        // audioManager.playSfx(audioManager.worldSfxSource, audioManager.elevatorDing, true);
        audioManager.playSfx(audioManager.worldSfxSource, audioManager.elevatorMove, true);
        // Debug.Log("closign doors");
        // yield return new WaitForSeconds(3);
        isClosed = true;
    }

    void resetDoorsToClosed()
    {
        Debug.Log("resetting doors to closed");

        StopAllCoroutines();
        doorL.position = doorLStartPos;
        doorR.position = doorRStartPos;

        walls.gameObject.SetActive(true);
        isClosed = true;
    }
}
