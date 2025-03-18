using TMPro;
using UnityEngine;

public class outsideMissionInfo : MonoBehaviour
{
    public TMP_Text missionNumberDisp;
    public itemMenu itemGrid;

    public missionManager missionManager;
    public inventoryManager inventoryManager;

    void Start()
    {
        gameObject.SetActive(true);
    }

    public void show()
    {
        missionNumberDisp.text = missionManager.currentMission.ToString();
        // Debug.LogWarning($"ig: {itemGrid}, rs: {itemGrid.regularSlots.Length}, im: {inventoryManager}, imm: {inventoryManager.missionInventory.Count}, rqi: {missionManager.allMissions[missionManager.currentMission].requiredItems.Count}");
        itemGrid.refreshItems(itemGrid.regularSlots, inventoryManager.missionInventory, missionManager.allMissions[missionManager.currentMission].requiredItems);
        gameObject.SetActive(true);
    }

    public void hide()
    {
        gameObject.SetActive(false);
    }
}
