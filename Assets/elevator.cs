using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class elevator : MonoBehaviour
{
    public Transform doorL;
    public Transform doorR;

    public Transform walls;

    audioManager audioManager;

    [HideInInspector] public bool isClosed;

    Vector2 doorLStartPos;
    Vector2 doorRStartPos;

    public bool isLab;
    public bool isFirst;

    void Awake()
    {
        Debug.LogWarning("door awake");
        audioManager = FindObjectOfType<audioManager>();

        doorLStartPos = doorL.position;
        doorRStartPos = doorR.position;

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
    }
    void Update()
    {
        // Debug.Log(isClosed);
    }

    public void openDoors(bool instant)
    {
        Debug.Log("opening doors");
        Debug.LogWarning($"is first: {isFirst}");
        CancelInvoke();
        doorL.position = doorLStartPos;
        doorR.position = doorRStartPos;

        if (instant)
        {
            // iTween.MoveBy(doorL.gameObject, iTween.Hash("x", -1.7, "speed", 1, "easeType", "easeInOutExpo"));
            // iTween.MoveBy(doorR.gameObject, iTween.Hash("x", 1.7, "speed", 1, "easeType", "easeInOutExpo"));

            doorL.position = doorLStartPos + new Vector2(-1.7f, 0);
            doorR.position = doorRStartPos + new Vector2(1.7f, 0);

            walls.gameObject.SetActive(false);
            isClosed = false;
        }
        else
        {
            Debug.Log("opening slowly");
            walls.gameObject.SetActive(true);
            audioManager.playSfx(audioManager.worldSfxSource, audioManager.elevatorMove, true);

            iTween.MoveBy(doorL.gameObject, iTween.Hash("x", -1.7, "speed", 0.5, "easeType", "easeInOutExpo", "delay", 4));
            iTween.MoveBy(doorR.gameObject, iTween.Hash("x", 1.7, "speed", 0.5, "easeType", "easeInOutExpo", "delay", 4));

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
        Debug.Log("closing doors");
        if (instant)
        {
            // iTween.MoveBy(doorL.gameObject, iTween.Hash("x", -1.7, "speed", 1, "easeType", "easeInOutExpo"));
            // iTween.MoveBy(doorR.gameObject, iTween.Hash("x", 1.7, "speed", 1, "easeType", "easeInOutExpo"));

            doorL.position = doorLStartPos;
            doorR.position = doorRStartPos;

            Debug.Log("instant!");

            walls.gameObject.SetActive(true);
            isClosed = true;
        }
        else
        {
            iTween.MoveBy(doorL.gameObject, iTween.Hash("x", 1.7, "speed", 0.5, "easeType", "easeInOutExpo", "delay", 4));
            iTween.MoveBy(doorR.gameObject, iTween.Hash("x", -1.7, "speed", 0.5, "easeType", "easeInOutExpo", "delay", 4));

            StartCoroutine("closeWalls");
        }
    }
    IEnumerator closeWalls()
    {
        yield return new WaitForSeconds(3);
        // audioManager.playSfx(audioManager.worldSfxSource, audioManager.elevatorDing, true);
        audioManager.playSfx(audioManager.worldSfxSource, audioManager.elevatorDoors, true);
        yield return new WaitForSeconds(3);
        walls.gameObject.SetActive(true);
        isClosed = true;
    }
}
