using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "missionSO")]
public class missionSO : ScriptableObject
{
    // [Min(0)]
    public int missionID;
    public string missionTitle; // obsolete
    [TextArea] public string missionDescription; // obsolete

    public bool endOnAllItems;
    public bool endOnAllCalls;
    public List<item> requiredItems;
    public callSO[] calls;
    [HideInInspector] public int currentCall = 0;

    [System.Serializable]
    public struct endEventValue
    {
        public string objName;
        public string methodName;
    }

    public List<endEventValue> endEventValuesList;

    public void endMission()
    {
        foreach (var endEvent in endEventValuesList)
            GameObject.Find(endEvent.objName).SendMessage(endEvent.methodName);
    }
}