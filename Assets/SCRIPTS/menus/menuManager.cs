using UnityEngine;

public enum openMenu { lab, cave, esc, trader, misison, craft, equip, call };
// public enum menuScreen { gameplay, trading };
public class menuManager : MonoBehaviour
{
    public GameObject menuBg;
    public inventoryManager inventoryManager;
    public missionManager missionManager;
    public callManager callManager;
    public deathScreen deathScreen;

    public caveItemMenu caveItemMenu;
    public missionMenu missionMenu;
    public missionMenu ingameMissionMenu;
    public callScreen callScreen;
    public traderMenu traderMenu;
    public labItemMenu labItemMenu;
    public toolWorkstationMenu toolWorkstationMenu;
    public equipWorkstation equipWorkstation;
    public equipWorkstation ingameEquipWorkstation;
    public Transform escMenu;

    public itemInfoDisplay itemInfoDisplay;

    public gameController gameController;

    void Awake()
    {
        // menuOpen = null;
        // Debug.Log($"mo: {menuOpen}");
        // hideMenus();
    }

    bool anotherMenuOpen = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Debug.Log($"mo: {menuOpen}");
            // if (menuOpen == null || menuOpen == openMenu.item)
            // if (menuOpen == null || menuOpen == openMenu.lab || menuOpen == openMenu.cave)
            // if (!anotherMenuOpen)
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
                // anotherMenuOpen = false;
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
        // if (anotherMenuOpen && escMenu.gameObject.activeSelf) return;
        // Debug.LogError($"hiding menus");

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

        ingameMissionMenu.gameObject.SetActive(false);
        ingameEquipWorkstation.gameObject.SetActive(false);

        menuBg.SetActive(false);

        // anotherMenuOpen = null;
        anotherMenuOpen = false;
        Time.timeScale = 1;
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
        // toggleLabItemMenu(onOff);
    }

    public void toggleEscapeMenu()
    {
        // bool onOff = toggleMenu(escMenu.gameObject);
        // if (anotherMenuOpen == null || anotherMenuOpen == openMenu.esc)
        // if (anotherMenuOpen == null || anotherMenuOpen == openMenu.esc)
        {
            bool onOff = !escMenu.gameObject.activeSelf;
            // if (onOff) anotherMenuOpen = openMenu.esc;
            // else anotherMenuOpen = null;
            toggleEsc(onOff);
            if (!onOff && !anotherMenuOpen)
            {
                // menuOpen = null;
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = 0;
            }
            // Debug.Log("esc");
        }
        // toggleEsc(!escMenu.gameObject.activeSelf);
    }
    public void toggleCallScreen()
    {
        // if (!anotherMenuOpen && !escMenu.gameObject.activeSelf)
        // if (!escMenu.gameObject.activeSelf)
        {// menuOpen = openMenu.call;
         // bool onOff = toggleMenu(callScreen.gameObject);
            bool onOff = !callScreen.gameObject.activeSelf;
            if (!onOff)
                Time.timeScale = 1;
            else Time.timeScale = 0;

            // Debug.LogError($"toggling call screen {onOff} (true = on)");

            toggleCallMenu(onOff);
            anotherMenuOpen = onOff; //?
        }
    }
    public void toggleCaveGameplayMenuScreen()
    {
        // if (menuOpen == null || menuOpen == openMenu.cave)
        // if (!escMenu.gameObject.activeSelf)
        if (!anotherMenuOpen && !escMenu.gameObject.activeSelf)
        {
            // menuOpen = openMenu.cave;
            bool onOff = toggleMenu(caveItemMenu.gameObject);

            toggleCaveItemMenu(onOff);
            // toggleMissionMenu(onOff);
            // anotherMenuOpen = onOff;
            ingameMissionMenu.gameObject.SetActive(onOff);
            ingameEquipWorkstation.gameObject.SetActive(onOff);
        }
    }
    public void toggleTradingScreen()
    {
        // if (menuOpen == null || menuOpen == openMenu.trader)
        if (!anotherMenuOpen && !escMenu.gameObject.activeSelf) //?
        // if (!escMenu.gameObject.activeSelf) //?
        {
            // menuOpen = openMenu.trader;
            bool onOff = toggleMenu(traderMenu.gameObject);

            toggleCaveItemMenu(onOff);
            toggleTraderMenu(onOff);
            // anotherMenuOpen = onOff; //?
        }
    }
    public void toggleLabMenuScreen()
    {
        // if (menuOpen == null || menuOpen == openMenu.lab)
        // if (!escMenu.gameObject.activeSelf)
        if (!anotherMenuOpen && !escMenu.gameObject.activeSelf)
        {
            // menuOpen = openMenu.lab;
            bool onOff = toggleMenu(labItemMenu.gameObject);

            toggleLabItemMenu(onOff);
            // anotherMenuOpen = onOff;
        }
    }
    public void toggleToggletoolWorkstationMenuScreen()
    {
        // if (menuOpen == null || menuOpen == openMenu.craft)
        // if (!anotherMenuOpen && !escMenu.gameObject.activeSelf)
        if (!escMenu.gameObject.activeSelf)
        {
            bool onOff = toggleMenu(toolWorkstationMenu.gameObject);

            toggleLabItemMenu(onOff);
            toggleToolWorkstationMenu(onOff);

            anotherMenuOpen = onOff;
            // menuOpen = openMenu.craft;
        }
    }
    public void toggleToggleEquipmentWorkstationMenu()
    {
        // if (menuOpen == null || menuOpen == openMenu.equip)
        // if (!anotherMenuOpen && !escMenu.gameObject.activeSelf)
        if (!escMenu.gameObject.activeSelf)
        {
            bool onOff = toggleMenu(equipWorkstation.gameObject);

            // labItemMenu.itemDispType = itemDispType.scrap;
            toggleLabItemMenu(onOff);
            toggleEquipmentWorkstationMenu(onOff);

            // menuOpen = openMenu.equip;
            // Debug.Log("eq");
            anotherMenuOpen = onOff;
        }
    }

    // bool hasTalkedBeforeFirstCave = true;
    public void toggleToggleMissionWorkstationMenu()
    {
        // if (menuOpen == null || menuOpen == openMenu.misison)
        // if (!anotherMenuOpen && !escMenu.gameObject.activeSelf)
        if (!escMenu.gameObject.activeSelf)
        {
            // menuOpen = openMenu.misison;
            bool onOff = toggleMenu(missionMenu.gameObject);

            toggleMissionWorkstationMenu(onOff);
            anotherMenuOpen = onOff;

            if (onOff && missionManager.checkCurrentMission(-1, 3))
            {
                onOff = toggleMenu(missionMenu.gameObject);
                toggleMissionWorkstationMenu(onOff);
                anotherMenuOpen = onOff;

                callManager.startCall(getCurrentMission());
                // hasTalkedBeforeFirstCave = true;
            }
            /* if (onOff && !hasTalkedBeforeFirstCave)
            {
                onOff = toggleMenu(missionMenu.gameObject);
                toggleMissionWorkstationMenu(onOff);
                anotherMenuOpen = onOff;

                // callManager.startCall(getCurrentMission()); //?
            } */
        }
    }

    missionSO getCurrentMission()
    {
        return missionManager.allMissions[missionManager.currentMission];
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
            Time.timeScale = 1;
            // Debug.Log("ts 1");
        }
        else
        {
            Time.timeScale = 0;
            // Debug.Log("ts 0");
        }

        // Debug.Log($"menu opne: {anotherMenuOpen}");

        return onOff;
    }
}
