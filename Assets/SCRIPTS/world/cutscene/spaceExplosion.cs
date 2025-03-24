using UnityEngine;

public class spaceExplosion : MonoBehaviour
{
    public gameController gameController;

    /*  void OnEnable()
     {
         gameObject.SetActive(false);
         if (gameController.missionManager.checkCurrentMission(1001, -1))
         // if in last mission
         {
             ignite();
         }
     } */

    public void ignite()
    {
        Debug.LogWarning($"EXPLOSION START");
        gameObject.SetActive(false);
        Invoke(nameof(kaboom), 7f);
    }

    void kaboom()
    {
        gameObject.SetActive(true);
        Debug.LogError($"explosion");
    }
}