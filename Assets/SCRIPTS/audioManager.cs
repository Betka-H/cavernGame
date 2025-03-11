using System.Collections;
using System;
using UnityEngine;

public enum musicLvl { labRegular, caveRegular, caveEscape, death, mainMenu, call, walk }

// chatgpt
public class audioManager : MonoBehaviour
{
    public static audioManager instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource playerSfxSource;
    public AudioSource worldSfxSource;
    public AudioSource uiSfxSource;

    private float musicVolume = 1f;
    private float sfxVolume = 1f;

    void Awake()
    {
        //! check Debug.LogWarning($"credit sound sources!!!");
        //! check Debug.LogWarning($"also delete unused cause these files are huuuuge");

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object across scenes //? yeah okay
            // Debug.Log("ddol!");
        }
        else
        {
            Destroy(gameObject);
            Debug.LogError("destroyed audioman");
            return;
        }

        //! LoadVolumeSettings();
    }

    AudioClip[] selectedClips;
    [HideInInspector] public musicLvl prevMusicLvl;
    public void playMusic(musicLvl lvl)
    {
        // Debug.Log($"trying to play {lvl}");
        switch (lvl)
        {
            case musicLvl.labRegular:
                // playMusic(labMusic);
                selectedClips = labMusic;
                prevMusicLvl = musicLvl.labRegular;
                break;
            case musicLvl.caveRegular:
                // playMusic(caveMusicRegular);
                selectedClips = caveMusicRegular;
                prevMusicLvl = musicLvl.caveRegular;
                break;
            case musicLvl.caveEscape:
                // playMusic(caveMusicEscape);
                selectedClips = caveMusicEscape;
                prevMusicLvl = musicLvl.caveEscape;
                break;
            case musicLvl.death:
                // playMusic(caveMusicEscape);
                selectedClips = deathMusic;
                prevMusicLvl = musicLvl.death;
                break;
            case musicLvl.mainMenu:
                selectedClips = mainMenuMusic;
                prevMusicLvl = musicLvl.mainMenu;
                break;
            case musicLvl.call:
                selectedClips = callBg;
                break;
            case musicLvl.walk:
                selectedClips = footSteps;
                break;
            default:
                Debug.LogError("music selection error, playing fallback");
                selectedClips = labMusic;
                break;
        }
        playMusic();
    }

    [Header("music clips")]
    public AudioClip[] labMusic;
    public AudioClip[] caveMusicRegular;
    public AudioClip[] caveMusicEscape;
    public AudioClip[] deathMusic;
    public AudioClip[] mainMenuMusic;
    public AudioClip[] itemPickup;

    int prevTrackNumber = -1;
    int trackNumber = -1;
    void playMusic()
    {
        musicSource.Stop();
        CancelInvoke("playMusic");

        // select random track
        if (selectedClips.Length > 0) // (but only if there are clips lol) (just in case)
        {
            if (selectedClips.Length == 1) // if there is just one song, play the one song
            {
                // Debug.Log("yeah happening");
                trackNumber = 0;
            }
            else // otherwise check for repeats
            {
                while (trackNumber == prevTrackNumber)
                {
                    trackNumber = UnityEngine.Random.Range(0, selectedClips.Length);
                    if (trackNumber == prevTrackNumber)
                    {
                        // Debug.Log("skipping track " + trackNumber);
                    }
                }
            }

            // define clip
            musicSource.clip = selectedClips[trackNumber]; // select the clip
            prevTrackNumber = Array.IndexOf(selectedClips, musicSource.clip); // set clip as previous track

            // play + repeat
            musicSource.Play();
            Invoke("playMusic", musicSource.clip.length); // when its done, play again

            // log
            // Debug.LogWarning("playing music track " + trackNumber + ": " + musicSource.clip.name);
            // FindObjectOfType<announcerManager>().announceMessage($"playing music track {trackNumber}: {musicSource.clip.name}");
        }
    }

    [Header("sfx clips")]
    public AudioClip[] footSteps;
    public AudioClip[] deathImpale;
    public AudioClip[] callBg; // actually just one. but idk easier to input into existing music looping system
    public AudioClip[] callAdvance;
    public AudioClip[] callEnd;
    public AudioClip[] uiButtonMouseover;
    public AudioClip[] uiButtonClick;
    public AudioClip[] elevatorMove;
    public AudioClip[] elevatorDing;
    public AudioClip[] elevatorDoors;

    public void playSfx(AudioSource source, AudioClip[] clips, bool allowOverlap)
    {
        if ((!source.isPlaying || allowOverlap) && clips.Length > 0)
        {
            int rnd = UnityEngine.Random.Range(0, clips.Length);

            source.PlayOneShot(clips[rnd]);

            // Debug.LogWarning("playing sound effect: " + clips[rnd].name);
        }
        // else Debug.LogError($"no clip assigned or banning multiple");
    }

    // 🎚 Set music volume
    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        musicSource.volume = musicVolume;
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
    }

    // 🎚 Set SFX volume
    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        playerSfxSource.volume = sfxVolume;
        worldSfxSource.volume = sfxVolume;
        uiSfxSource.volume = sfxVolume;
        PlayerPrefs.SetFloat("SFXvolume", sfxVolume);
    }

    // 💾 Load saved volume settings
    private void LoadVolumeSettings()
    {
        musicVolume = PlayerPrefs.GetFloat("musicVolume", 1f);
        sfxVolume = PlayerPrefs.GetFloat("SFXvolume", 1f);

        // music
        musicSource.volume = musicVolume;
        // sfx
        playerSfxSource.volume = sfxVolume;
        worldSfxSource.volume = sfxVolume;
        uiSfxSource.volume = sfxVolume;
    }
}
/* //? for sliders
public void OnMusicSliderChange(float value)
{
    AudioManager.instance.SetMusicVolume(value);
}

public void OnSFXSliderChange(float value)
{
    AudioManager.instance.SetSFXVolume(value);
}

  */