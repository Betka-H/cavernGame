using UnityEngine;

public class menuCanvas : MonoBehaviour
{
    public Transform itemMenu;
    public Transform traderMenu;
    public Transform missionMenu;

    void Start()
    {
        hideMenus();
    }

    public void hideMenus()
    {
        toggleItemMenu(false);
        toggleMissionMenu(false);
        toggleTraderMenu(false);
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
        traderMenu.gameObject.SetActive(onOff);
    }

    public void showGamePlayMenu(bool onOff)
    {
        hideMenus();

        toggleItemMenu(onOff);
        toggleMissionMenu(onOff);
    }
    public void showTradingMenu(bool onOff)
    {
        hideMenus();

        toggleItemMenu(onOff);
        toggleTraderMenu(onOff);
    }
}
