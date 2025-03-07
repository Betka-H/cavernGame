using UnityEngine;

public class NPC : MonoBehaviour
{
    protected menuManager menuManager;
    // public menuScreen menuScreenToOpen;

    void Awake()
    {
        menuManager = FindObjectOfType<menuManager>();
    }
}