using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameStartController : MonoBehaviour
{
    bool hasStarted;

    void OnEnable()
    {
        changeBtnTitle();
    }
    public TMP_Text startTxt;
    void changeBtnTitle()
    {
        loadStartPref();
        if (hasStarted)
            startTxt.text = "Continue game";
        else startTxt.text = "New game";
    }

    int gameSceneIndex = 1;
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
