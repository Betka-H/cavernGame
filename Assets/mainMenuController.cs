using UnityEngine;

public class mainMenuController : MonoBehaviour
{
    public Transform mainMenu;
    public Transform settingsMenu;
    public Transform creditsMenu;

    audioManager audioManager;

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
        // Debug.Log("deactivating all menus????");

        // activate chosen menu
        menu.gameObject.SetActive(true);
    }

    public void resetSave()
    {
        Debug.Log("try to not delete settings?");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}
