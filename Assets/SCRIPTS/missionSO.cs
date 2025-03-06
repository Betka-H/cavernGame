using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "missionSO")]
public class missionSO : ScriptableObject
{
    [Min(0)]
    public int missionID;
    public string missionTitle;
    [TextArea]
    public string missionDescription;

    public List<item> requiredItems;
    public callSO[] calls;
}