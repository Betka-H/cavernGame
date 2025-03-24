using TMPro;
using UnityEngine;

public class outsideMissionInfo : MonoBehaviour
{
    public TMP_Text missionNumberDisp;
    public itemMenu itemGrid;

    public missionManager missionManager;
    public inventoryManager inventoryManager;

    //! keep disabled in inspector

    public void show()
    {
        missionManager.checkRndMission();
        missionNumberDisp.text = missionManager.currentMission.ToString();
        itemGrid.wakeUp();
        itemGrid.refreshItems(itemGrid.regularSlots, inventoryManager.missionInventory, missionManager.allMissions[missionManager.currentMission].requiredItems);
        gameObject.SetActive(true);
    }

    public void hide()
    {
        gameObject.SetActive(false);
    }
}
