using UnityEngine;

[CreateAssetMenu(menuName = "randomMissionSO")]
public class randomMissionSO : missionSO
{
    public int howManyItems;
    [HideInInspector] public bool hasBeenAssigned;
}