using UnityEngine;

public class NPC : MonoBehaviour
{
    protected menuManager menuManager;
    public menuScreen menuScreenToOpen;

    void Start()
    {
        menuManager = FindObjectOfType<menuManager>();
    }
}