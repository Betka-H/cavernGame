using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevator : MonoBehaviour
{
    public Transform doorL;
    public Transform doorR;

    public Transform walls;

    void Start()
    {
        openDoors();
    }

    public void openDoors()
    {
        iTween.MoveBy(doorL.gameObject, iTween.Hash("x", -2, "speed", 0.5, "easeType", "easeInOutExpo", "delay", .3));
        iTween.MoveBy(doorR.gameObject, iTween.Hash("x", 2, "speed", 0.5, "easeType", "easeInOutExpo", "delay", .3));

        StartCoroutine("openWalls");
    }

    IEnumerator openWalls()
    {
        yield return new WaitForSeconds(3);
        walls.gameObject.SetActive(false);
    }
}
