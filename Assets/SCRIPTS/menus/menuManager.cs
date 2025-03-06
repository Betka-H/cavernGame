using UnityEngine;

public enum menuScreen { gameplay, trading };
public class menuManager : MonoBehaviour
{
    public GameObject menuBg;
    public inventoryManager inventoryManager;
    public deathScreen deathScreen;

    public caveItemMenu caveItemMenu;
    public missionMenu missionMenu;
    public traderMenu traderMenu;
    public labItemMenu labItemMenu;
    public toolWorkstationMenu toolWorkstationMenu;
    public equipWorkstation equipWorkstation;
    public Transform escMenu;

    public itemInfoDisplay itemInfoDisplay;

    void Awake()
    {
        hideMenus();
    }

    public void hideMenus()
    {
        toggleItemMenu(false);
        toggleLabItemMenu(false);
        toggleitemInfoDisplay(false);
        toggleMissionMenu(false);
        toggleTraderMenu(false);
        toggleToolWorkstationMenu(false);
        toggleEquipmentWorkstationMenu(false);
        toggleMissionWorkstationMenu(false);
        deathScreen.gameObject.SetActive(false);
        escMenu.gameObject.SetActive(false);

        menuBg.SetActive(false);
    }

    public void toggleEscapeMenu(bool onOff)
    {
        escMenu.gameObject.SetActive(onOff);
    }
    void toggleItemMenu(bool onOff)
    {
        caveItemMenu.gameObject.SetActive(onOff);
        toggleitemInfoDisplay(onOff);
    }
    void toggleLabItemMenu(bool onOff)
    {
        labItemMenu.gameObject.SetActive(onOff);
        toggleitemInfoDisplay(onOff);
    }
    void toggleitemInfoDisplay(bool onOff)
    {
        itemInfoDisplay.gameObject.SetActive(onOff);
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
    void toggleEquipmentWorkstationMenu(bool onOff)
    {
        equipWorkstation.gameObject.SetActive(onOff);
    }
    void toggleMissionWorkstationMenu(bool onOff)
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
    public void toggleLabMenuScreen()
    {
        bool onOff = toggleMenu(labItemMenu.gameObject);

        // labItemMenu.itemDispType = itemDispType.all;
        toggleLabItemMenu(onOff);
    }
    public void toggleToggletoolWorkstationMenuScreen()
    {
        bool onOff = toggleMenu(toolWorkstationMenu.gameObject);

        // labItemMenu.itemDispType = itemDispType.scrap;
        toggleLabItemMenu(onOff);
        toggleToolWorkstationMenu(onOff);
    }
    public void toggleToggleEquipmentWorkstationMenu()
    {
        bool onOff = toggleMenu(equipWorkstation.gameObject);

        // labItemMenu.itemDispType = itemDispType.scrap;
        toggleLabItemMenu(onOff);
        toggleEquipmentWorkstationMenu(onOff);
    }
    public void toggleToggleMissionWorkstationMenu()
    {
        bool onOff = toggleMenu(missionMenu.gameObject);

        toggleMissionWorkstationMenu(onOff);
    }

    bool toggleMenu(GameObject menu)
    {
        bool onOff = !menu.activeSelf;
        hideMenus();
        menuBg.SetActive(onOff);
        return onOff;
    }
}
