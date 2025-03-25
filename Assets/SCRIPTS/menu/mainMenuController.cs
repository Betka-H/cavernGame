using UnityEngine;

public class mainMenuController : MonoBehaviour
{
    public Transform mainMenu;
    public Transform settingsMenu;
    public Transform creditsMenu;

    audioManager audioManager; // keep private

    void Start()
    {
        audioManager = FindObjectOfType<audioManager>();

        openMenu(mainMenu);
        audioManager.playMusic(musicLvl.mainMenu);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            openMenu(mainMenu);
        }
    }

    public void openMenu(Transform menu)
    {
        // deactivate all
        mainMenu.gameObject.SetActive(false);
        settingsMenu.gameObject.SetActive(false);
        creditsMenu.gameObject.SetActive(false);

        // activate chosen menu
        menu.gameObject.SetActive(true);
    }

    public void resetSave()
    {
        PlayerPrefs.DeleteAll();
        FindObjectOfType<audioManager>().saveVolumeSettings();
        PlayerPrefs.Save();
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
