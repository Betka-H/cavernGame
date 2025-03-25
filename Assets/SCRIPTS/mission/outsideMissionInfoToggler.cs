using UnityEngine;

public class outsideMissionInfoToggler : MonoBehaviour
{
    outsideMissionInfo outsideMissionInfoObj;

void OnEnable()
    {
        outsideMissionInfoObj = FindObjectOfType<outsideMissionInfo>(true);
        outsideMissionInfoObj.show();
    }

    void OnDisable()
    {
        outsideMissionInfoObj.hide();
    }
}