using UnityEngine;

public class outsideMissionInfoToggler : MonoBehaviour
{
    outsideMissionInfo outsideMissionInfoObj;

    /* void Start()
    {
        OnEnable();
    } */

    void OnEnable()
    {
        // Debug.LogWarning($"ENABLING");
        outsideMissionInfoObj = FindObjectOfType<outsideMissionInfo>(true);
        outsideMissionInfoObj.show();
    }

    void OnDisable()
    {
        outsideMissionInfoObj.hide();
    }
}