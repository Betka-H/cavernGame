using System.Linq;
using UnityEngine;

public class callManager : MonoBehaviour
{
    callScreen callScreen;
    missionManager missionManager;
    menuManager menuManager;

    void Awake()
    {
        callScreen = FindObjectOfType<callScreen>();
        missionManager = FindObjectOfType<missionManager>();
        menuManager = FindObjectOfType<menuManager>();
    }

    public void startCall()
    {
        Debug.Log("starting call!");

        //! temp?
        // currentMission().currentCall = 0;
        currentCall().currentMessage = 0;
        callScreen.talk(currentCall());
    }
    public void advanceCall()
    {
        Debug.Log("advancing call.");

        // Debug.Log($"cm: {currentCall().currentMessage}++, msgs.l: {currentCall().messages.Length}");
        if (currentCall().currentMessage + 1 < currentCall().messages.Length)
        {
            currentCall().currentMessage++;
            callScreen.talk(currentCall());
        }
        else
        {
            Debug.Log($"no more messages!");
            endCall();
        }
    }
    void endCall()
    {
        Debug.Log($"ending call.");
        if (currentMission().currentCall < currentMission().calls.Length)
        {
            currentMission().currentCall++;
        }
        menuManager.toggleCallScreen();
    }

    public missionSO currentMission()
    {
        return missionManager.allMissions[missionManager.currentMission];
    }
    public callSO currentCall()
    {
        return currentMission().calls[currentMission().currentCall];
    }
}