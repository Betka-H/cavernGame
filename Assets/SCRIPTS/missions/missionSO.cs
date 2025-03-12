using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "missionSO")]
public class missionSO : ScriptableObject
{
    // [Min(0)]
    public int missionID;
    public string missionTitle;
    [TextArea]
    public string missionDescription;

    public bool endOnAllItems;
    public List<item> requiredItems;
    public callSO[] calls;
    [HideInInspector] public int currentCall = 0;

    // public UnityEvent missionEndEvent;

    [System.Serializable]
    public struct endEventValue
    {
        public string objName;
        public string methodName;
    }

    public List<endEventValue> endEventValuesList;
}