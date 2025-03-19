using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameStartController : MonoBehaviour
{
    int gameSceneIndex = 1;
    bool hasStarted;
    public TMP_Text startTxt;

    void OnEnable()
    {
        changeBtnTitle();
    }

    void changeBtnTitle()
    {
        loadStartPref();
        if (hasStarted)
            startTxt.text = "continue game";
        else startTxt.text = "start game";
    }

    /* void Start()
    {
        loadStartPref();
        if (hasStarted)
            startTxt.text = "continue game";
        else startTxt.text = "start game";
    } */

    public void startGame()
    {
        SceneManager.LoadScene(gameSceneIndex);
    }

    // chatgpt
    void loadStartPref()
    {
        hasStarted = PlayerPrefs.GetInt("HasStarted", 0) == 1; // Default to 0 (false)
    }
}
