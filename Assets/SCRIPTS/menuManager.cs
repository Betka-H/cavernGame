using UnityEngine;

public enum menuScreen { gameplay, trading };
public class menuManager : MonoBehaviour
{
    public GameObject menuBg;

    deathScreen deathScreen;

    [HideInInspector]
    public itemMenu itemMenu;
    [HideInInspector]
    public missionMenu missionMenu;
    [HideInInspector]
    public traderMenu traderMenu;
    [HideInInspector]
    public labItemMenu labItemMenu;

    void Awake()
    {
        deathScreen = FindObjectOfType<deathScreen>(true);
        itemMenu = FindObjectOfType<itemMenu>(true);
        missionMenu = FindObjectOfType<missionMenu>(true);
        traderMenu = FindObjectOfType<traderMenu>(true);
        labItemMenu = FindObjectOfType<labItemMenu>(true);
        hideMenus();
    }

    public void hideMenus()
    {
        toggleItemMenu(false);
        toggleLabItemMenu(false);
        toggleMissionMenu(false);
        toggleTraderMenu(false);
        deathScreen.gameObject.SetActive(false);

        menuBg.SetActive(false);
    }

    void toggleItemMenu(bool onOff)
    {
        itemMenu.gameObject.SetActive(onOff);
    }
    void toggleLabItemMenu(bool onOff)
    {
        labItemMenu.gameObject.SetActive(onOff);
    }
    void toggleTraderMenu(bool onOff)
    {
        traderMenu.gameObject.SetActive(onOff);
    }
    void toggleMissionMenu(bool onOff)
    {
        missionMenu.gameObject.SetActive(onOff);
    }

    public void toggleGameplayMenuScreen()
    {
        bool onOff = toggleMenu(missionMenu.gameObject);

        toggleItemMenu(onOff);
        toggleMissionMenu(onOff);
    }
    public void toggleTradingScreen()
    {
        bool onOff = toggleMenu(traderMenu.gameObject);

        toggleItemMenu(onOff);
        toggleTraderMenu(onOff);
    }
    // temp name?
    public void toggleLabMenu()
    {
        bool onOff = !labItemMenu.gameObject.activeSelf;

        toggleLabItemMenu(onOff);
    }

    bool toggleMenu(GameObject menu)
    {
        bool onOff = !menu.activeSelf;
        hideMenus();
        menuBg.SetActive(onOff);
        return onOff;
    }
}
