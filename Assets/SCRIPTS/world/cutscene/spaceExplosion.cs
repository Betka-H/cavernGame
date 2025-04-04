using UnityEngine;

public class spaceExplosion : MonoBehaviour
{
    public gameController gameController;

    public void ignite()
    {
        // Debug.Log($"EXPLOSION START");
        gameObject.SetActive(false);
        Invoke(nameof(explode), 7f);
    }

    void explode()
    {
        gameObject.SetActive(true);
        // Debug.Log($"explosion");
        Invoke(nameof(endGame), 3.5f);
    }
    void endGame()
    {
        gameController.saveManager.clearPrefs();
        gameController.saveManager.loadSave();
        gameController.saveAndGoToMainMenu();
    }
}