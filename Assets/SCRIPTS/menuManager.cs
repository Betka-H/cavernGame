using UnityEngine;

public enum menuScreen { gameplay, trading };
public class menuManager : MonoBehaviour
{
    public GameObject menuBg;

    deathScreen deathScreen;

    [HideInInspector] public itemMenu itemMenu;
    [HideInInspector] public missionMenu missionMenu;
    [HideInInspector] public traderMenu traderMenu;
    [HideInInspector] public labItemMenu labItemMenu;
    [HideInInspector] public toolWorkstationMenu toolWorkstationMenu;

    void Awake()
    {
        deathScreen = FindObjectOfType<deathScreen>(true);
        itemMenu = FindObjectOfType<itemMenu>(true);
        missionMenu = FindObjectOfType<missionMenu>(true);
        traderMenu = FindObjectOfType<traderMenu>(true);
        labItemMenu = FindObjectOfType<labItemMenu>(true);
        toolWorkstationMenu = FindObjectOfType<toolWorkstationMenu>(true);
        hideMenus();
    }

    public void hideMenus()
    {
        toggleItemMenu(false);
        toggleLabItemMenu(false);
        toggleMissionMenu(false);
        toggleTraderMenu(false);
        toggleToolWorkstationMenu(false);
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
    void toggleToolWorkstationMenu(bool onOff)
    {
        toolWorkstationMenu.gameObject.SetActive(onOff);
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
    public void toggleLabMenuScreen()
    {
        bool onOff = toggleMenu(labItemMenu.gameObject);

        labItemMenu.itemDispType = itemDispType.all;
        toggleLabItemMenu(onOff);
    }
    public void toggleToggletoolWorkstationMenuScreen()
    {
        bool onOff = toggleMenu(toolWorkstationMenu.gameObject);

        labItemMenu.itemDispType = itemDispType.scrap;
        toggleLabItemMenu(onOff);
        toggleToolWorkstationMenu(onOff);
    }

    bool toggleMenu(GameObject menu)
    {
        bool onOff = !menu.activeSelf;
        hideMenus();
        menuBg.SetActive(onOff);
        return onOff;
    }
}
