using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "callSO")]
public class callSO : ScriptableObject
{
    [Header("all assorted! in the order they will appear")]
    public Sprite[] talkSprites;
    public string[] speakerNames;
    [TextArea]
    public string[] messages;
    [HideInInspector] public int currentMessage = 0;
    public UnityEvent callEndEvent;
}