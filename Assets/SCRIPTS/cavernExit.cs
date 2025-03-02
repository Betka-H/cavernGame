using UnityEngine;

public class cavernExit : MonoBehaviour
{
    inventoryManager inventory;

    void Awake()
    {
        inventory = FindObjectOfType<inventoryManager>();
    }

    
}
