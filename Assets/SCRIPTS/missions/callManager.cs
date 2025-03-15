using System.Linq;
using UnityEngine;

public class callManager : MonoBehaviour
{
    [HideInInspector] public callScreen callScreen;
    missionManager missionManager;
    menuManager menuManager;
    gameController gameController;
    audioManager audioManager;

    void Awake()
    {
        gameController = FindObjectOfType<gameController>();
        callScreen = FindObjectOfType<callScreen>();
        missionManager = FindObjectOfType<missionManager>();
        menuManager = FindObjectOfType<menuManager>();
        audioManager = FindObjectOfType<audioManager>();

        gameController.isCalling = false;
    }

    missionSO selectedMission;
    public void startCall(missionSO newMission)
    {
        try
        {
            FindObjectOfType<playerMovement>().GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        catch (System.Exception)
        {

            // throw;
        }

        selectedMission = newMission;
        Debug.Log($"starting call for m: {selectedMission.name}, c: {selectedMission.currentCall}");
        menuManager.toggleCallScreen();
        // Debug.Log($"cml: {selectedMission.calls[selectedMission.currentCall].messages.Length}");
        if (selectedMission.calls[selectedMission.currentCall].messages.Length > 0)
        {
            // Debug.Log("starting call!");

            gameController.isCalling = true;
            //? audioManager.playMusic(musicLvl.call);

            currentMissionCall().currentMessage = 0;
            callScreen.talk(currentMissionCall());
        }
        else
        {
            // Debug.Log("empty call!");
            endCall(true);
        }
    }
    public void advanceCall()
    {
        // Debug.Log("advancing call.");

        if (currentMissionCall().currentMessage + 1 < currentMissionCall().messages.Length)
        {
            audioManager.playSfx(audioManager.worldSfxSource, audioManager.callAdvance);

            currentMissionCall().currentMessage++;
            callScreen.talk(currentMissionCall());
        }
        else
        {
            // Debug.Log($"no more messages in call!");

            audioManager.playSfx(audioManager.worldSfxSource, audioManager.callEnd); // here, otherwise plays end call even in empty calls
            endCall(true);
        }
    }
    public void endCall(bool advance)
    {
        Debug.Log($"ending call for m: {selectedMission.name}, eoac?: {selectedMission.endOnAllCalls}, call: {selectedMission.currentCall}, all calls: {selectedMission.calls.Length}");

        menuManager.toggleCallScreen();
        gameController.isCalling = false;

        //? audioManager.playMusic(audioManager.prevMusicLvl);

        Debug.LogWarning($"{selectedMission.currentCall + 1} < {selectedMission.calls.Length}?");
        if (selectedMission.currentCall + 1 < selectedMission.calls.Length)
        {
            // runCallEndEvent();

            // Debug.Log("setting next call");
            selectedMission.currentCall++;
            Debug.Log($"next call is {selectedMission.calls[selectedMission.currentCall]} in mission {selectedMission.name}");

            if (advance)
            {
                // Debug.LogWarning("ending call");
                Debug.Log($"ending event for m: {selectedMission.name}, c: {selectedMission.currentCall}");
                selectedMission.calls[selectedMission.currentCall - 1].endCall();
            }
        }
        else if (selectedMission.endOnAllCalls)
        {
            selectedMission.calls[selectedMission.currentCall].endCall();
            selectedMission.endMission();
            if (missionManager.allMissions.Contains(selectedMission))
                missionManager.newMission();
        }

        // if this is the last call, end the mission
        // if (selectedMission.endOnAllCalls) //! my god.....
        /* if (selectedMission.endOnAllCalls && selectedMission.currentCall == selectedMission.calls.Length)
        {
            selectedMission.endMission();
        } */
    }

    /* void runCallEndEvent()
    {
        int currentCallID = selectedMission.currentCall - 1;
        var endEventList = selectedMission.calls[currentCallID].endEventValuesList;
        if (endEventList != null)
            foreach (var even in endEventList)
            {
                // Debug.Log("found event");
                GameObject gameObj = GameObject.Find(even.objName);
                gameObj.SendMessage(even.methodName);
            }
 */
    // else Debug.LogError($"no call end event");

    /* if (selectedMission.calls[selectedMission.currentCall].endEventValuesList != null)
        foreach (var even in selectedMission.calls[selectedMission.currentCall].endEventValuesList)
        {
            // Debug.Log("found event");
            GameObject gameObj = GameObject.Find(even.objName);
            gameObj.SendMessage(even.methodName);
        }
    // else Debug.LogError($"no call end event"); */
    /*  } */

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