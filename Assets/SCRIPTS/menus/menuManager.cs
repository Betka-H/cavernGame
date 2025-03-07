using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UI;

public enum openMenu { lab, cave, esc, trader, misison, craft, equip, call };
// public enum menuScreen { gameplay, trading };
public class menuManager : MonoBehaviour
{
    public GameObject menuBg;
    public inventoryManager inventoryManager;
    public callManager callManager;
    public deathScreen deathScreen;

    public caveItemMenu caveItemMenu;
    public missionMenu missionMenu;
    public callScreen callScreen;
    public traderMenu traderMenu;
    public labItemMenu labItemMenu;
    public toolWorkstationMenu toolWorkstationMenu;
    public equipWorkstation equipWorkstation;
    public Transform escMenu;

    public itemInfoDisplay itemInfoDisplay;

    public gameController gameController;

    void Awake()
    {
        // menuOpen = null;
        // Debug.Log($"mo: {menuOpen}");
        hideMenus();
    }

    openMenu? menuOpen = null;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Debug.Log($"mo: {menuOpen}");
            // if (menuOpen == null || menuOpen == openMenu.item)
            // if (menuOpen == null || menuOpen == openMenu.lab || menuOpen == openMenu.cave)
            if (menuOpen == null)
            {
                switch (gameController.roomController.currentLevel)
                {
                    case gameController.level.lab:
                        toggleLabMenuScreen();
                        break;
                    case gameController.level.cavern:
                        toggleCaveGameplayMenuScreen();
                        break;
                }
                /* if (labItemMenu.gameObject.activeSelf || caveItemMenu.gameObject.activeSelf)
                    menuOpen = openMenu.item;
                else menuOpen = null; */
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // if (menuOpen == null || menuOpen == openMenu.esc)
            {
                toggleEscapeMenu();
            }
            // else hideMenus();
        }
    }

    public void hideMenus()
    {
        toggleCaveItemMenu(false);
        toggleLabItemMenu(false);
        toggleitemInfoDisplay(false);
        toggleMissionMenu(false);
        toggleCallMenu(false);
        toggleTraderMenu(false);
        toggleToolWorkstationMenu(false);
        toggleEquipmentWorkstationMenu(false);
        toggleMissionWorkstationMenu(false);
        deathScreen.gameObject.SetActive(false);
        toggleEsc(false);

        menuBg.SetActive(false);

        menuOpen = null;
    }
    // /menuOpen = openMenu.esc;

    void toggleEsc(bool onOff)
    {
        // if (onOff && menuOpen != openMenu.esc)
        {
            // hideMenus();
        }
        escMenu.gameObject.SetActive(onOff);
    }
    void toggleCaveItemMenu(bool onOff)
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
    void toggleCallMenu(bool onOff)
    {
        callScreen.gameObject.SetActive(onOff);
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

    public void toggleEscapeMenu()
    {
        // bool onOff = toggleMenu(escMenu.gameObject);
        if (menuOpen == null || menuOpen == openMenu.esc)
        {
            bool onOff = !escMenu.gameObject.activeSelf;
            if (onOff) menuOpen = openMenu.esc;
            else menuOpen = null;
            toggleEsc(onOff);
            Debug.Log("esc");
        }
        // toggleEsc(!escMenu.gameObject.activeSelf);
    }
    public void toggleCallScreen()
    {
        menuOpen = openMenu.call;
        bool onOff = toggleMenu(callScreen.gameObject);

        toggleCallMenu(onOff);
    }
    public void toggleCaveGameplayMenuScreen()
    {
        if (menuOpen == null || menuOpen == openMenu.cave)
        {
            menuOpen = openMenu.cave;
            bool onOff = toggleMenu(missionMenu.gameObject);

            toggleCaveItemMenu(onOff);
            toggleMissionMenu(onOff);
        }
    }
    public void toggleTradingScreen()
    {
        if (menuOpen == null || menuOpen == openMenu.trader)
        {
            menuOpen = openMenu.trader;
            bool onOff = toggleMenu(traderMenu.gameObject);

            toggleCaveItemMenu(onOff);
            toggleTraderMenu(onOff);
        }
    }
    public void toggleLabMenuScreen()
    {
        if (menuOpen == null || menuOpen == openMenu.lab)
        {
            menuOpen = openMenu.lab;
            bool onOff = toggleMenu(labItemMenu.gameObject);

            toggleLabItemMenu(onOff);
        }
    }
    public void toggleToggletoolWorkstationMenuScreen()
    {
        if (menuOpen == null || menuOpen == openMenu.craft)
        {
            bool onOff = toggleMenu(toolWorkstationMenu.gameObject);

            toggleLabItemMenu(onOff);
            toggleToolWorkstationMenu(onOff);

            menuOpen = openMenu.craft;
        }
    }
    public void toggleToggleEquipmentWorkstationMenu()
    {
        if (menuOpen == null || menuOpen == openMenu.equip)
        {
            bool onOff = toggleMenu(equipWorkstation.gameObject);

            // labItemMenu.itemDispType = itemDispType.scrap;
            toggleLabItemMenu(onOff);
            toggleEquipmentWorkstationMenu(onOff);

            menuOpen = openMenu.equip;
            Debug.Log("eq");
        }
    }
    public void toggleToggleMissionWorkstationMenu()
    {
        if (menuOpen == null || menuOpen == openMenu.misison)
        {
            menuOpen = openMenu.misison;
            bool onOff = toggleMenu(missionMenu.gameObject);

            toggleMissionWorkstationMenu(onOff);
        }
    }

    bool toggleMenu(GameObject menu)
    {
        bool onOff = !menu.gameObject.activeSelf;
        /* if (menuOpen != openMenu.esc)
        {
            hideMenus();
            menuBg.SetActive(onOff);
        }
        else
        {
            menuBg.SetActive(false);
        } */

        // if (menuOpen != openMenu.esc)
        hideMenus();
        menuBg.SetActive(onOff);

        if (!onOff)
        {
            // menuOpen = null;
        }

        Debug.Log($"menu opne: {menuOpen}");

        return onOff;
    }
}
