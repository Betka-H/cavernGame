using System.Collections;
using System;
using UnityEngine;

public enum musicLvl { labRegular, caveRegular, caveEscape, death }

// chatgpt
public class audioManager : MonoBehaviour
{
    public static audioManager instance;

    [Header("Audio Sources")]
    public AudioSource sfxSource;   // For sound effects
    public AudioSource musicSource; // For background music

    private float sfxVolume = 1f;
    private float musicVolume = 1f;

    void Awake()
    {
        Debug.LogError($"credit sound sources!!!");
        Debug.LogError($"also delete unused cause these files are huuuuge");

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object across scenes //? yeah okay
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        LoadVolumeSettings();
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
    public void playMusic(musicLvl lvl)
    {
        switch (lvl)
        {
            case musicLvl.labRegular:
                // playMusic(labMusic);
                selectedClips = labMusic;
                break;
            case musicLvl.caveRegular:
                // playMusic(caveMusicRegular);
                selectedClips = caveMusicRegular;
                break;
            case musicLvl.caveEscape:
                // playMusic(caveMusicEscape);
                selectedClips = caveMusicEscape;
                break;
            case musicLvl.death:
                // playMusic(caveMusicEscape);
                selectedClips = deathMusic;
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
    int prevTrackNumber = -1;
    int trackNumber = -1;
    void playMusic()
    {
        musicSource.Stop();

        // select random track
        if (selectedClips.Length < 2) // if there is just one song, play the one song
        {
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

    // 🔊 Play a sound effect
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip, sfxVolume);
    }

    // 🎚 Set music volume
    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        musicSource.volume = musicVolume;
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
    }

    // 🎚 Set SFX volume
    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        sfxSource.volume = sfxVolume;
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
    }

    // 💾 Load saved volume settings
    private void LoadVolumeSettings()
    {
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        musicSource.volume = musicVolume;
        sfxSource.volume = sfxVolume;
    }
}
/* //? play from other
AudioManager.instance.PlaySFX(mySoundEffect);
AudioManager.instance.PlayMusic(myBackgroundTrack);

  */
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