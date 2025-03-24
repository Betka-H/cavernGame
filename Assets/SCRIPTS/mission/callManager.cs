using System.Linq;
using UnityEngine;

public class callManager : MonoBehaviour
{
    public callScreen callScreen;
    public missionManager missionManager;
    public menuManager menuManager;
    public gameController gameController;
    public audioManager audioManager; // keep private

    void Awake()
    {
        gameController.isCalling = false;
    }

    missionSO selectedMission;
    public void startCall(missionSO newMission)
    {
        selectedMission = newMission;
        Debug.Log($"starting call for m: {selectedMission.name}, c: {selectedMission.currentCall}");
        menuManager.toggleCallScreen();
        if (selectedMission.calls[selectedMission.currentCall].messages.Length > 0)
        {
            gameController.isCalling = true;
            // audioManager.playMusic(musicLvl.call);

            currentMissionCall().currentMessage = 0;
            callScreen.talk(currentMissionCall());
        }
        else endCall(true); // empty call
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
        // Debug.Log($"ending call for m: {selectedMission.name}, end on all calls?: {selectedMission.endOnAllCalls}, call: {selectedMission.currentCall}, all calls: {selectedMission.calls.Length}");

        menuManager.toggleCallScreen();
        gameController.isCalling = false;

        //? audioManager.playMusic(audioManager.prevMusicLvl);

        if (selectedMission.currentCall + 1 < selectedMission.calls.Length)
        {
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
            selectedMission.currentCall++;
            selectedMission.endMission();
            if (missionManager.allMissions.Contains(selectedMission)) // if this is a main mission
                missionManager.newMission();
        }
    }

    public missionSO currentMainMission()
    {
        return missionManager.allMissions[missionManager.currentMission];
    }
    public callSO currentMissionCall()
    {
        return selectedMission.calls[selectedMission.currentCall];
    }
}