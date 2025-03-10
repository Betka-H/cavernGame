using System.Linq;
using UnityEngine;

public class callManager : MonoBehaviour
{
    [HideInInspector] public callScreen callScreen;
    missionManager missionManager;
    menuManager menuManager;
    gameController gameController;

    void Awake()
    {
        gameController = FindObjectOfType<gameController>();
        callScreen = FindObjectOfType<callScreen>();
        missionManager = FindObjectOfType<missionManager>();
        menuManager = FindObjectOfType<menuManager>();
    }

    missionSO selectedMission;
    public void startCall(missionSO newMission)
    {
        selectedMission = newMission;
        menuManager.toggleCallScreen();
        Debug.Log($"cml: {selectedMission.calls[selectedMission.currentCall].messages.Length}");
        if (selectedMission.calls[selectedMission.currentCall].messages.Length > 0)
        {
            Debug.Log("starting call!");

            currentMissionCall().currentMessage = 0;
            callScreen.talk(currentMissionCall());
        }
        else
        {
            Debug.Log("empty call!");
            endCall();
        }
        gameController.isCalling = true;
    }
    public void advanceCall()
    {
        Debug.Log("advancing call.");

        if (currentMissionCall().currentMessage + 1 < currentMissionCall().messages.Length)
        {
            currentMissionCall().currentMessage++;
            callScreen.talk(currentMissionCall());
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
        //! temp
        // currentMissionCall().callEndEvent.Invoke();
        if (selectedMission.currentCall < selectedMission.calls.Length)
        {
            Debug.Log("setting next call");
            selectedMission.currentCall++;
            // Debug.Log($"next m is {selectedMission.calls[selectedMission.currentCall]}");
        }
        menuManager.toggleCallScreen();
        gameController.isCalling = false;
    }

    /* public void startMissionCall()
    {
        Debug.Log("starting mission call!");

        menuManager.toggleCallScreen();
        currentMissionCall().currentMessage = 0;
        callScreen.talk(currentMissionCall());
    }
    public void advanceMissionCall()
    {
        Debug.Log("advancing mission call.");

        // Debug.Log($"cm: {currentCall().currentMessage}++, msgs.l: {currentCall().messages.Length}");
        if (currentMissionCall().currentMessage + 1 < currentMissionCall().messages.Length)
        {
            currentMissionCall().currentMessage++;
            callScreen.talk(currentMissionCall());
        }
        else
        {
            Debug.Log($"no more mission messages!");
            endMissionCall();
        }
    }
    void endMissionCall()
    {
        Debug.Log($"ending mission call.");
        if (currentMainMission().currentCall < currentMainMission().calls.Length)
        {
            currentMainMission().currentCall++;
        }
        menuManager.toggleCallScreen();
    } */

    public missionSO currentMainMission()
    {
        return missionManager.allMissions[missionManager.currentMission];
    }
    public callSO currentMissionCall()
    {
        // return currentMission().calls[currentMission().currentCall];
        return selectedMission.calls[selectedMission.currentCall];
    }
}