using System.Collections;
using UnityEngine;

//* my least favorite script
//* inconsistent logic
public enum openMenu { lab, cave, esc, trader, misison, craft, equip, call };
public class menuManager : MonoBehaviour
{
    public inventoryManager inventoryManager;
    public missionManager missionManager;
    public callManager callManager;
    public gameController gameController;
    public announcerManager announcerManager;

    public labItemMenu labItemGrid;
    public caveItemGrid caveItemGrid;
    public itemInfoDisplay itemInfoDisplay;
    public missionMenu caveMissionMenu;
    public missionMenu labMissionMenu;
    public callScreen callScreen; //* have more types of call screens - for space, messages, direct speech...
    public traderMenu traderMenu;
    public toolWorkstationMenu craftingWorkstationMenu;
    public equipWorkstation equipWorkstationMenu;
    public equipWorkstation caveEquipWorkstationMenu;
    public Transform escMenu;
    public GameObject deathScreen;


    public GameObject menuBg;

    bool anotherMenuOpen = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            toggleEscapeMenu();
        else if (Input.GetKeyDown(KeyCode.Q))
            toggleInvMenu();
    }
    void toggleInvMenu()
    {
        if (!gameController.isDead && !anotherMenuOpen && !escMenu.gameObject.activeSelf && !callScreen.gameObject.activeSelf)
            switch (gameController.roomController.currentLevel)
            {
                case gameController.level.lab:
                    toggleLabMenuScreen();
                    break;
                case gameController.level.cavern:
                    toggleCaveGameplayMenuScreen();
                    break;
            }
    }

    public void hideMenus()
    {
        toggleCaveItemGridAndInfoDisplay(false);
        toggleLabItemGridAndInfoDisplay(false);

        caveMissionMenu.gameObject.SetActive(false);
        labMissionMenu.gameObject.SetActive(false);
        callScreen.gameObject.SetActive(false);

        craftingWorkstationMenu.gameObject.SetActive(false);
        equipWorkstationMenu.gameObject.SetActive(false);
        caveEquipWorkstationMenu.gameObject.SetActive(false);

        traderMenu.gameObject.SetActive(false);

        deathScreen.SetActive(false);
        escMenu.gameObject.SetActive(false);

        menuBg.SetActive(false);

        anotherMenuOpen = false;
        Time.timeScale = 1;
    }

    void toggleCaveItemGridAndInfoDisplay(bool onOff)
    {
        caveItemGrid.gameObject.SetActive(onOff);
        itemInfoDisplay.gameObject.SetActive(onOff);
    }
    void toggleLabItemGridAndInfoDisplay(bool onOff)
    {
        labItemGrid.gameObject.SetActive(onOff);
        itemInfoDisplay.gameObject.SetActive(onOff);
    }

    public void toggleEscapeMenu()
    {
        bool onOff = !escMenu.gameObject.activeSelf;
        escMenu.gameObject.SetActive(onOff);

        if (!onOff && !anotherMenuOpen)
            Time.timeScale = 1;
        else
            Time.timeScale = 0;
    }
    public void toggleCallScreen()
    {
        //?
        bool onOff = !callScreen.gameObject.activeSelf;
        if (!onOff)
            Time.timeScale = 1;
        else Time.timeScale = 0;
        //?

        callScreen.gameObject.SetActive(onOff);
        anotherMenuOpen = onOff;
    }
    public void toggleLabMenuScreen()
    {
        bool onOff = showMenuScreen(labItemGrid.gameObject);
        toggleLabItemGridAndInfoDisplay(onOff);
    }
    public void toggleCaveGameplayMenuScreen()
    {
        bool onOff = showMenuScreen(caveItemGrid.gameObject);

        toggleCaveItemGridAndInfoDisplay(onOff);
        caveMissionMenu.gameObject.SetActive(onOff);
        caveEquipWorkstationMenu.gameObject.SetActive(onOff);
    }
    public void toggleTradingScreen()
    {
        if (!anotherMenuOpen && !escMenu.gameObject.activeSelf && !callScreen.gameObject.activeSelf)
        //? conditions?
        {
            bool onOff = showMenuScreen(traderMenu.gameObject);

            toggleCaveItemGridAndInfoDisplay(onOff);
            traderMenu.gameObject.SetActive(onOff);
        }
    }
    public void toggleToolWorkstationMenuScreen()
    {
        if (!escMenu.gameObject.activeSelf && !callScreen.gameObject.activeSelf)
        //? conditions?
        {
            bool onOff = showMenuScreen(craftingWorkstationMenu.gameObject);

            toggleLabItemGridAndInfoDisplay(onOff);
            craftingWorkstationMenu.gameObject.SetActive(onOff);

            anotherMenuOpen = onOff;

            if (missionManager.checkCurrentMission(-1, -1))
            // if in tutorial
            {
                if (missionManager.checkCurrentMission(-1, 7))
                {
                    callManager.startCall(getCurrentMission());
                }
                else if (missionManager.checkCurrentMission(-1, 8) || missionManager.checkCurrentMission(-1, 9) || missionManager.checkCurrentMission(-1, 10) || missionManager.checkCurrentMission(-1, 11) || missionManager.checkCurrentMission(-1, 12) || missionManager.checkCurrentMission(-1, 13) || missionManager.checkCurrentMission(-1, 14))
                {
                    // Debug.LogWarning($"in tutorial, not locked");
                }
                else
                {
                    onOff = showMenuScreen(craftingWorkstationMenu.gameObject);
                    toggleLabItemGridAndInfoDisplay(onOff);
                    craftingWorkstationMenu.gameObject.SetActive(onOff);
                    anotherMenuOpen = onOff;

                    announcerManager.announceMessage($"this room is locked");
                }
            }
        }
    }
    public void toggleEquipmentWorkstationMenu()
    {
        if (!escMenu.gameObject.activeSelf && !callScreen.gameObject.activeSelf)
        //? conditions?
        {
            bool onOff = showMenuScreen(equipWorkstationMenu.gameObject);

            toggleLabItemGridAndInfoDisplay(onOff);
            equipWorkstationMenu.gameObject.SetActive(onOff);

            anotherMenuOpen = onOff;

            if (missionManager.checkCurrentMission(-1, -1))
            // if in tutorial
            {
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
                {
                    Debug.LogWarning($"in tutorial, not locked");
                }
                else
                {
                    onOff = showMenuScreen(equipWorkstationMenu.gameObject);
                    toggleLabItemGridAndInfoDisplay(onOff);
                    equipWorkstationMenu.gameObject.SetActive(onOff);
                    anotherMenuOpen = onOff;

                    announcerManager.announceMessage($"this room is locked");
                }
            }
        }
    }

    public void toggleMissionWorkstationMenu()
    {
        Debug.LogWarning($"trying to toggle mission menu");
        if (!escMenu.gameObject.activeSelf && !callScreen.gameObject.activeSelf)
        {
            bool onOff = showMenuScreen(labMissionMenu.gameObject);

            labMissionMenu.gameObject.SetActive(onOff);
            anotherMenuOpen = onOff;

            // mission calls
            if (onOff && missionManager.checkCurrentMission(-1, 3))
            {
                onOff = showMenuScreen(labMissionMenu.gameObject);
                labMissionMenu.gameObject.SetActive(onOff);
                anotherMenuOpen = onOff;

                callManager.startCall(getCurrentMission());
            }
            else if (missionManager.checkCurrentMission(-1, 4) || missionManager.checkCurrentMission(-1, 5))
            {
                onOff = showMenuScreen(labMissionMenu.gameObject);
                labMissionMenu.gameObject.SetActive(onOff);
                anotherMenuOpen = onOff;

                announcerManager.announceMessage($"go into the elevator!");
            }
            else if (missionManager.checkCurrentMission(-1, 6))
            {
                missionManager.checkMissionItems(); //? why?
                if (gameController.isCalling)
                {
                    // disable menu again
                    onOff = showMenuScreen(labMissionMenu.gameObject);
                    labMissionMenu.gameObject.SetActive(onOff);
                    anotherMenuOpen = onOff;

                    callManager.startCall(getCurrentMission());
                }
                else if (onOff)
                    announcerManager.announceMessage($"have you got everything?");
            }
            else if (missionManager.checkCurrentMission(-1, 8) || missionManager.checkCurrentMission(-1, 9))
            {
                onOff = showMenuScreen(labMissionMenu.gameObject);
                labMissionMenu.gameObject.SetActive(onOff);
                anotherMenuOpen = onOff;

                announcerManager.announceMessage($"go to the crafting lab!");
            }
            else if (missionManager.checkCurrentMission(-1, 10))
            {
                onOff = showMenuScreen(labMissionMenu.gameObject);
                labMissionMenu.gameObject.SetActive(onOff);
                anotherMenuOpen = onOff;

                callManager.startCall(getCurrentMission());
            }
            else if (missionManager.checkCurrentMission(-1, 11) || missionManager.checkCurrentMission(-1, 12))
            {
                onOff = showMenuScreen(labMissionMenu.gameObject);
                labMissionMenu.gameObject.SetActive(onOff);
                anotherMenuOpen = onOff;

                announcerManager.announceMessage($"go to the locker room!");
            }
            else if (missionManager.checkCurrentMission(-1, 13))
            {
                onOff = showMenuScreen(labMissionMenu.gameObject);
                labMissionMenu.gameObject.SetActive(onOff);
                anotherMenuOpen = onOff;
                if (inventoryManager.checkEquipment(inventoryManager.inventoryDefinitions.shoes))
                {
                    callManager.startCall(getCurrentMission());

                    StartCoroutine(showFirstMission());
                }
                else announcerManager.announceMessage($"equip shoes first");
            }
            else if (missionManager.checkCurrentMission(1001, -1))
            {
                onOff = showMenuScreen(labMissionMenu.gameObject);
                labMissionMenu.gameObject.SetActive(onOff);
                anotherMenuOpen = onOff;

                callManager.startCall(getCurrentMission());
            }
        }
    }
    IEnumerator showFirstMission()
    {
        while (gameController.isCalling)
            yield return null;

        toggleMissionWorkstationMenu();
    }

    missionSO getCurrentMission()
    {
        return missionManager.allMissions[missionManager.currentMission];
    }

    bool showMenuScreen(GameObject menu)
    {
        bool onOff = !menu.gameObject.activeSelf;
        hideMenus();
        menuBg.SetActive(onOff);

        if (!onOff)
            Time.timeScale = 1;
        else Time.timeScale = 0;

        return onOff;
    }
}
