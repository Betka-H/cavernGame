using System.Collections;
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
                        //? toggleLabMenuScreen();
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
        if (!anotherMenuOpen && !escMenu.gameObject.activeSelf && !callScreen.gameObject.activeSelf)
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
        // if (FindObjectOfType<roomController>().m_hasIntroducedTrader) // only if knows about the trader
        {  // if (menuOpen == null || menuOpen == openMenu.trader)
            if (!anotherMenuOpen && !escMenu.gameObject.activeSelf && !callScreen.gameObject.activeSelf) //?
                                                                                                         // if (!escMenu.gameObject.activeSelf) //?
            {
                // menuOpen = openMenu.trader;
                bool onOff = toggleMenu(traderMenu.gameObject);

                toggleCaveItemMenu(onOff);
                toggleTraderMenu(onOff);
                // anotherMenuOpen = onOff; //?
            }
        }
        // FindObjectOfType<announcerManager>().announceMessage("doesnt know trader yet", true);
    }
    public void toggleLabMenuScreen()
    {
        // if (menuOpen == null || menuOpen == openMenu.lab)
        // if (!escMenu.gameObject.activeSelf)
        if (!anotherMenuOpen && !escMenu.gameObject.activeSelf && !callScreen.gameObject.activeSelf)
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
        if (!escMenu.gameObject.activeSelf && !callScreen.gameObject.activeSelf)
        {
            bool onOff = toggleMenu(toolWorkstationMenu.gameObject);

            toggleLabItemMenu(onOff);
            toggleToolWorkstationMenu(onOff);

            anotherMenuOpen = onOff;
            // menuOpen = openMenu.craft;

            if (missionManager.checkCurrentMission(-1, -1)) // if in tutorial
            {
                // if 7th: should access room
                if (missionManager.checkCurrentMission(-1, 7)) //! testing
                {
                    callManager.startCall(getCurrentMission());
                }
                else if (missionManager.checkCurrentMission(-1, 8) || missionManager.checkCurrentMission(-1, 9) || missionManager.checkCurrentMission(-1, 10) || missionManager.checkCurrentMission(-1, 11) || missionManager.checkCurrentMission(-1, 12) || missionManager.checkCurrentMission(-1, 13) || missionManager.checkCurrentMission(-1, 14))
                // else if (missionManager.checkCurrentMission(-1, 8) || missionManager.checkCurrentMission(-1, 9))
                {
                    Debug.LogWarning($"in tutorial, not locked");
                } //! HELP
                else // if in tutorial, but not supposed to go in at the moment
                {
                    onOff = toggleMenu(toolWorkstationMenu.gameObject);
                    toggleLabItemMenu(onOff);
                    toggleToolWorkstationMenu(onOff);
                    anotherMenuOpen = onOff;

                    FindObjectOfType<announcerManager>().announceMessage($"this room is locked");
                }
            }
        }
    }
    public void toggleToggleEquipmentWorkstationMenu()
    {
        // if (menuOpen == null || menuOpen == openMenu.equip)
        // if (!anotherMenuOpen && !escMenu.gameObject.activeSelf)
        if (!escMenu.gameObject.activeSelf && !callScreen.gameObject.activeSelf)
        {
            bool onOff = toggleMenu(equipWorkstation.gameObject);

            if (onOff)
            {
                Debug.LogWarning($"gear?: v v v");
                inventoryManager.printInventory(inventoryManager.equippedItems);
            }

            // labItemMenu.itemDispType = itemDispType.scrap;
            toggleLabItemMenu(onOff);
            toggleEquipmentWorkstationMenu(onOff);

            // menuOpen = openMenu.equip;
            // Debug.Log("eq");
            anotherMenuOpen = onOff;


            if (missionManager.checkCurrentMission(-1, -1)) // if in tutorial
            {
                // if 10th or 11th: should access room
                if (missionManager.checkCurrentMission(-1, 10))
                {
                    Debug.LogWarning("advancing from 10th to 11th call");
                    missionManager.allMissions[missionManager.currentMission].currentCall++;
                }
                if (missionManager.checkCurrentMission(-1, 11)) //! DONT ELSE THIS
                {
                    callManager.startCall(getCurrentMission());
                }
                else if (missionManager.checkCurrentMission(-1, 12) || missionManager.checkCurrentMission(-1, 13) || missionManager.checkCurrentMission(-1, 14))
                // else if (missionManager.checkCurrentMission(-1, 12))
                {
                    Debug.LogWarning($"in tutorial, not locked");
                } //! HELP
                else // if in tutorial, but not supposed to go in at the moment
                {
                    onOff = toggleMenu(equipWorkstation.gameObject);
                    toggleLabItemMenu(onOff);
                    toggleEquipmentWorkstationMenu(onOff);
                    anotherMenuOpen = onOff;

                    FindObjectOfType<announcerManager>().announceMessage($"this room is locked");
                }
            }
        }
    }

    // bool hasTalkedBeforeFirstCave = true;
    public void toggleToggleMissionWorkstationMenu()
    {
        Debug.LogWarning($"trying to toggle mission menu");

        // if (menuOpen == null || menuOpen == openMenu.misison)
        // if (!anotherMenuOpen && !escMenu.gameObject.activeSelf)
        if (!escMenu.gameObject.activeSelf && !callScreen.gameObject.activeSelf)
        {
            // menuOpen = openMenu.misison;
            bool onOff = toggleMenu(missionMenu.gameObject);

            if (onOff)
                missionManager.checkRndMission();

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
            else if (missionManager.checkCurrentMission(-1, 4) || missionManager.checkCurrentMission(-1, 5)) // in case of death
            {
                onOff = toggleMenu(missionMenu.gameObject);
                toggleMissionWorkstationMenu(onOff);
                anotherMenuOpen = onOff;

                FindObjectOfType<announcerManager>().announceMessage($"go into the elevator!");
            }
            else if (missionManager.checkCurrentMission(-1, 6)) //! testing
            {
                missionManager.checkMissionItems(); //? why?
                if (gameController.isCalling) // if calling 6th
                {
                    // disable menu again
                    onOff = toggleMenu(missionMenu.gameObject);
                    toggleMissionWorkstationMenu(onOff);
                    anotherMenuOpen = onOff;

                    callManager.startCall(getCurrentMission());
                }
                else if (onOff) // if not calling and only on toggle on
                    FindObjectOfType<announcerManager>().announceMessage($"have you got everything?");

                // startNextMainMissionCall();
            }
            // else if (missionManager.checkCurrentMission(-1, 7) || missionManager.checkCurrentMission(-1, 8) || missionManager.checkCurrentMission(-1, 9))
            else if (missionManager.checkCurrentMission(-1, 8) || missionManager.checkCurrentMission(-1, 9))
            {
                onOff = toggleMenu(missionMenu.gameObject);
                toggleMissionWorkstationMenu(onOff);
                anotherMenuOpen = onOff;

                FindObjectOfType<announcerManager>().announceMessage($"go to the crafting lab!");
            }
            else if (missionManager.checkCurrentMission(-1, 10)) //! testing
            {
                /* Debug.LogWarning("advancing from 10th to 11th call");
                missionManager.allMissions[missionManager.currentMission].currentCall++; */
                onOff = toggleMenu(missionMenu.gameObject);
                toggleMissionWorkstationMenu(onOff);
                anotherMenuOpen = onOff;

                callManager.startCall(getCurrentMission());
            }
            else if (missionManager.checkCurrentMission(-1, 11) || missionManager.checkCurrentMission(-1, 12))
            {
                onOff = toggleMenu(missionMenu.gameObject);
                toggleMissionWorkstationMenu(onOff);
                anotherMenuOpen = onOff;

                FindObjectOfType<announcerManager>().announceMessage($"go to the locker room!");
            }
            else if (missionManager.checkCurrentMission(-1, 13)) //! testing
            {
                onOff = toggleMenu(missionMenu.gameObject);
                toggleMissionWorkstationMenu(onOff);
                anotherMenuOpen = onOff;
                if (inventoryManager.checkEquipment(inventoryManager.inventoryDefinitions.shoes))
                {
                    callManager.startCall(getCurrentMission());

                    StartCoroutine(showFirstMission());
                }
                else
                {
                    FindObjectOfType<announcerManager>().announceMessage($"equip shoes first");
                    // Debug.LogWarning("no shoes! equip shoes first");
                }
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
    IEnumerator showFirstMission()
    {
        while (gameController.isCalling)
        {
            Debug.Log($"calling: {gameController.isCalling}");
            yield return null;
        }

        toggleToggleMissionWorkstationMenu();
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
