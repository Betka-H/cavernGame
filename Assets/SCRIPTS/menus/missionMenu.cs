using TMPro;
using UnityEngine;

public class missionMenu : MonoBehaviour
{
    public TMP_Text title;
    public TMP_Text main;

    public void setMission(string newTittle, string newMain)
    {
        title.text = newTittle;
        main.text = newMain;
    }
}
