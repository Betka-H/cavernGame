using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class missionMenu : MonoBehaviour
{
    public TMP_Text title;
    public TMP_Text main;

    public void setMission(string title, string main)
    {
        this.title.text = title;
        this.main.text = main;
    }

    void OnEnable()
    {
        updateMissionText();
    }

    void updateMissionText()
    {

    }
}
