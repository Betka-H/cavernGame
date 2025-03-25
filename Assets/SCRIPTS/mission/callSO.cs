using System.Collections.Generic;
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

    [System.Serializable]
    public struct endEventValue
    {
        public string objName;
        public string methodName;
    }

    public List<endEventValue> endEventValuesList;

    public void endCall()
    {
        if (endEventValuesList != null)
            foreach (var even in endEventValuesList)
            {
                GameObject gameObj = GameObject.Find(even.objName);
                gameObj.SendMessage(even.methodName);
            }
    }
}