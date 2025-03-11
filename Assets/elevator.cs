using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevator : MonoBehaviour
{
    public Transform doorL;
    public Transform doorR;

    public Transform walls;

    audioManager audioManager;

    void Awake()
    {
        audioManager = FindObjectOfType<audioManager>();
        // openDoors();
    }

    public void openDoors()
    {
        audioManager.playSfx(audioManager.worldSfxSource, audioManager.elevatorMove, true);

        iTween.MoveBy(doorL.gameObject, iTween.Hash("x", -2, "speed", 0.5, "easeType", "easeInOutExpo", "delay", 4));
        iTween.MoveBy(doorR.gameObject, iTween.Hash("x", 2, "speed", 0.5, "easeType", "easeInOutExpo", "delay", 4));

        StartCoroutine("openWalls");
    }

    IEnumerator openWalls()
    {
        yield return new WaitForSeconds(3);
        audioManager.playSfx(audioManager.worldSfxSource, audioManager.elevatorDing, true);
        audioManager.playSfx(audioManager.worldSfxSource, audioManager.elevatorOpen, true);
        yield return new WaitForSeconds(3);
        walls.gameObject.SetActive(false);
    }
}
