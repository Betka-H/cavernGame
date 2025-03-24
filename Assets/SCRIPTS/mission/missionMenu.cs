using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class missionMenu : MonoBehaviour
{
    menuManager menuManager;
    public itemMenu requiredItemsMenu;

    public TMP_Text txtTitle;
    public TMP_Text txtDescription;

    public Transform m_solitaire;

    missionManager missionManager;

    void Awake()
    {
        menuManager = FindObjectOfType<menuManager>();
        missionManager = FindObjectOfType<missionManager>();

        displayMission(missionManager.allMissions[missionManager.currentMission]);
    }

    void OnEnable()
    {
        // Debug.Log($"cm: {missionManager.allMissions[missionManager.currentMission]}, {missionManager.currentMission}");
        displayMission(missionManager.allMissions[missionManager.currentMission]);

        if (m_solitaire != null)
        {
            if (!missionManager.checkCurrentMission(-1, -1)) // only out of tutorial
            {
                int rnd = Random.Range(0, 100);
                // Debug.LogWarning($"solitaire change was {rnd}");
                if (rnd > 90)
                    m_solitaire.gameObject.SetActive(true);
                else m_solitaire.gameObject.SetActive(false);
            }
            else m_solitaire.gameObject.SetActive(false); // always deactivate in tutorial
        }
    }

    public void displayMission(missionSO mission)
    {
        // Debug.Log($"new mission: {mission.missionID}, {mission.missionTitle}, {mission.missionDescription}");
        // Debug.Log($"displaying mission {mission.missionID} - {mission.name}");
        // txtTitle.text = mission.missionTitle;
        txtTitle.text = (mission.missionID + 1).ToString();
        txtDescription.text = mission.missionDescription;
        // requiredItemsMenu.refreshItems(requiredItemsMenu.regularSlots, menuManager.inventoryManager.missionInventory);
        requiredItemsMenu.refreshItems(requiredItemsMenu.regularSlots, menuManager.inventoryManager.missionInventory, missionManager.allMissions[missionManager.currentMission].requiredItems);
    }
}
