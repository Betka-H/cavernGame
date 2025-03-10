using System.Collections;
using System;
using UnityEngine;

public enum musicLvl { labRegular, caveRegular, caveEscape, death, mainMenu, call, walk }

// chatgpt
public class audioManager : MonoBehaviour
{
    public static audioManager instance;

    [Header("Audio Sources")]
    public AudioSource musicSource; // For background music
    public AudioSource playerSfxSource;   // For sound effects
    public AudioSource environmentSfxSource;   // For sound effects

    private float musicVolume = 1f;
    private float sfxVolume = 1f;

    void Awake()
    {
        Debug.LogWarning($"credit sound sources!!!");
        Debug.LogWarning($"also delete unused cause these files are huuuuge");

        /* if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object across scenes //? yeah okay
        }
        else
        {
            Destroy(gameObject);
            return;
        } */

        //! LoadVolumeSettings();
    }

    void Start()
    {
        /* if (defaultMusic != null)
        {
            PlayMusic(defaultMusic);
        } */

        // playMusic();
    }

    /* // 🎵 Play background music
    public void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip == clip) return; // Avoid restarting the same track
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    } */

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
            Debug.LogWarning("playing music track " + trackNumber + ": " + musicSource.clip.name);
        }
    }

    [Header("sfx clips")]
    public AudioClip[] footSteps;
    public AudioClip[] deathImpale;
    public AudioClip[] callBg; // actually just one. but idk easier to input into existing music looping system
    public AudioClip[] callAdvance;
    public AudioClip[] callEnd;

    /* public void playPlayerSfx(AudioClip clip)
    {
        if (!sfxSource.isPlaying)
        {
            sfxSource.PlayOneShot(clip);
            // sfxSource.clip = clip;
            // sfxSource.Play();

            Debug.LogWarning("playing sound effect: " + clip.name);
        }
    } */
    public void playSfx(AudioSource source, AudioClip[] clips)
    {
        if (!source.isPlaying && clips.Length > 0)
        {
            int rnd = UnityEngine.Random.Range(0, clips.Length);

            source.PlayOneShot(clips[rnd]);

            Debug.LogWarning("playing sound effect: " + clips[rnd].name);
        }
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
        environmentSfxSource.volume = sfxVolume;
        PlayerPrefs.SetFloat("SFXvolume", sfxVolume);
    }

    // 💾 Load saved volume settings
    private void LoadVolumeSettings()
    {
        musicVolume = PlayerPrefs.GetFloat("musicVolume", 1f);
        sfxVolume = PlayerPrefs.GetFloat("SFXvolume", 1f);

        musicSource.volume = musicVolume;
        playerSfxSource.volume = sfxVolume;
        environmentSfxSource.volume = sfxVolume;
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