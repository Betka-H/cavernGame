using UnityEngine;

public enum menuScreen { gameplay, trading };
public class menuManager : MonoBehaviour
{
    deathScreen deathScreen;

    itemMenu itemMenu;
    missionMenu missionMenu;
    traderMenu traderMenu;

    void Awake()
    {
        deathScreen = FindObjectOfType<deathScreen>(true);
        itemMenu = FindObjectOfType<itemMenu>(true);
        missionMenu = FindObjectOfType<missionMenu>(true);
        traderMenu = FindObjectOfType<traderMenu>(true);
        hideMenus();
    }

    public void hideMenus()
    {
        toggleItemMenu(false);
        toggleMissionMenu(false);
        toggleTraderMenu(false);
        deathScreen.gameObject.SetActive(false);
    }

    void toggleItemMenu(bool onOff)
    {
        itemMenu.gameObject.SetActive(onOff);
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
        bool onOff = !missionMenu.gameObject.activeSelf;
        hideMenus();

        toggleItemMenu(onOff);
        toggleMissionMenu(onOff);
    }
    public void toggleTradingScreen()
    {
        bool onOff = !traderMenu.gameObject.activeSelf;
        hideMenus();

        toggleItemMenu(onOff);
        toggleTraderMenu(onOff);
    }
}
