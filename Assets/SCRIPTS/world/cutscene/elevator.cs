using System.Collections;
using UnityEngine;

public class elevator : MonoBehaviour
{
    public Transform doorL;
    public Transform doorR;

    Vector2 doorLStartPos;
    Vector2 doorRStartPos;

    public Transform walls;

    float moveAmount = 2f;

    audioManager audioManager; // keep private

    [HideInInspector] public bool isClosed;

    public bool isFirst; // opens at first, then stays open
    public bool isMainEntrance;
    public bool isLab; // used to check exit point

    bool isAwake;
    void Awake()
    {
        audioManager = FindObjectOfType<audioManager>();

        doorLStartPos = doorL.position;
        doorRStartPos = doorR.position;

        isAwake = true;
    }
    void Start()
    {
        if (isMainEntrance)
        // inverts default behaviour
        {
            if (isFirst)
            {
                isFirst = false;
                openDoors(false);
            }
            else closeDoors(true);
        }
        else
        {
            if (!isFirst)
            {
                walls.gameObject.SetActive(false);
                openDoors(true);
            }
            else
            {
                openDoors(false);

                GameObject exitObj;
                if (isLab)
                    exitObj = GameObject.Find("lab exit point");
                else exitObj = GameObject.Find("cavern exit point");

                exitObj.GetComponent<tooltipCaller>().isExitPoint = true;
            }
        }
    }

    public void openDoors(bool instant)
    {
        if (!isAwake) Awake();

        resetDoorsToClosed();

        if (instant)
        {
            doorL.position = doorLStartPos + new Vector2(-moveAmount, 0);
            doorR.position = doorRStartPos + new Vector2(moveAmount, 0);

            walls.gameObject.SetActive(false);
            isClosed = false;
        }
        else
        {
            audioManager.playSfx(audioManager.worldSfxSource, audioManager.elevatorMove);
            // FindObjectOfType<announcerManager>().announceMessage($"opening elevator doors", true);

            iTween.MoveBy(doorL.gameObject, iTween.Hash("x", -moveAmount, "speed", 0.5, "easeType", "easeInOutExpo", "delay", 4));
            iTween.MoveBy(doorR.gameObject, iTween.Hash("x", moveAmount, "speed", 0.5, "easeType", "easeInOutExpo", "delay", 4));

            StartCoroutine(openWalls());
        }
    }
    IEnumerator openWalls()
    {
        yield return new WaitForSeconds(3);

        audioManager.playSfx(audioManager.worldSfxSource, audioManager.elevatorDing);
        audioManager.playSfx(audioManager.worldSfxSource, audioManager.elevatorDoors);

        yield return new WaitForSeconds(3.5f);

        walls.gameObject.SetActive(false);
        isClosed = false;
    }

    public void closeDoors(bool instant)
    {
        if (!isAwake) Awake();

        if (!isClosed)
            if (instant)
                resetDoorsToClosed();
            else
            {
                audioManager.playSfx(audioManager.worldSfxSource, audioManager.elevatorDoors);

                iTween.MoveBy(doorL.gameObject, iTween.Hash("x", moveAmount, "speed", 0.5, "easeType", "easeInOutExpo"));
                iTween.MoveBy(doorR.gameObject, iTween.Hash("x", -moveAmount, "speed", 0.5, "easeType", "easeInOutExpo"));

                // FindObjectOfType<announcerManager>().announceMessage($"closing elevator doors", true);

                StartCoroutine(closeWalls());
            }
    }
    IEnumerator closeWalls()
    {
        walls.gameObject.SetActive(true);

        yield return new WaitForSeconds(5);
        audioManager.playSfx(audioManager.worldSfxSource, audioManager.elevatorMove);
        isClosed = true;
    }

    void resetDoorsToClosed()
    {
        StopAllCoroutines();

        doorL.position = doorLStartPos;
        doorR.position = doorRStartPos;

        walls.gameObject.SetActive(true);

        isClosed = true;
    }
}
