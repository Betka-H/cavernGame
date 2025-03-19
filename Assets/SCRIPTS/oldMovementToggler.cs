using TMPro;
using UnityEngine;

public class oldMovementToggler : MonoBehaviour
{
    string key = "oldMovement";

    void OnEnable()
    {
        changeBtnTitle();
    }

    public TMP_Text onOffTxt;
    void changeBtnTitle()
    {
        if (PlayerPrefs.GetInt(key, 0) == 0)
            onOffTxt.text = "OFF";
        else
            onOffTxt.text = "ON";
    }

    public void toggleOldMovement()
    {
        if (PlayerPrefs.GetInt(key, 0) == 0)
            PlayerPrefs.SetInt(key, 1);
        else PlayerPrefs.SetInt(key, 0);

        PlayerPrefs.Save();

        changeBtnTitle();
    }
}
