using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

public enum musicLvl { labRegular, caveRegular, caveEscape, death, mainMenu, call, walk, space }

// chatgpt
public class audioManager : MonoBehaviour
{
    public static audioManager instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource playerSfxSource;
    public AudioSource worldSfxSource;
    public AudioSource uiSfxSource;

    public Slider musicSlider;
    public Slider sfxSlider;

    private float musicVolume = 1f;
    private float sfxVolume = 1f;

    void Awake()
    {
        //! check Debug.LogWarning($"credit sound sources!!!");
        //! check Debug.LogWarning($"also delete unused cause these files are huuuuge");

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        loadVolumeSettings();
    }

    AudioClip[] selectedClips;
    [HideInInspector] public musicLvl prevMusicLvl;
    public void playMusic(musicLvl lvl)
    {
        switch (lvl)
        {
            case musicLvl.labRegular:
                selectedClips = labMusic;
                prevMusicLvl = musicLvl.labRegular;
                break;
            case musicLvl.caveRegular:
                selectedClips = caveMusicRegular;
                prevMusicLvl = musicLvl.caveRegular;
                break;
            case musicLvl.caveEscape: // unused
                selectedClips = caveMusicEscape;
                prevMusicLvl = musicLvl.caveEscape;
                break;
            case musicLvl.death:
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
            case musicLvl.walk: // deprecated
                selectedClips = footSteps;
                break;
            case musicLvl.space:
                selectedClips = spaceMusic;
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
    public AudioClip[] spaceMusic;
    public AudioClip[] itemPickup;

    int prevTrackNumber = -1;
    int trackNumber = -1;
    void playMusic()
    {
        musicSource.Stop();
        CancelInvoke("playMusic");

        // select random track
        if (selectedClips.Length > 0) // (but only if there are clips) (just in case)
        {
            if (selectedClips.Length == 1) // if there is just one song, play the one song
                trackNumber = 0;
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
            Invoke("playMusic", musicSource.clip.length); // when its done, play again //* sometimes ends song early?
        }
    }

    [Header("sfx clips")]
    public AudioClip[] footSteps;
    public AudioClip[] deathImpale;
    public AudioClip[] callBg;
    public AudioClip[] callAdvance;
    public AudioClip[] callEnd;
    public AudioClip[] uiButtonMouseover;
    public AudioClip[] uiButtonClick;
    public AudioClip[] elevatorMove;
    public AudioClip[] elevatorDing;
    public AudioClip[] elevatorDoors;

    bool canPlaySfx = true;
    public void playSfx(AudioSource source, AudioClip[] clips)
    {
        if (canPlaySfx && clips.Length > 0)
        {
            int rnd = UnityEngine.Random.Range(0, clips.Length);

            source.PlayOneShot(clips[rnd]);

            StartCoroutine(sfxCooldown());
        }
    }
    IEnumerator sfxCooldown()
    {
        canPlaySfx = false;
        yield return new WaitForSecondsRealtime(0.25f);
        canPlaySfx = true;
    }

    // called on slider change
    public void setMusicVolume(float volume)
    {
        musicVolume = volume;
        musicSource.volume = musicVolume;
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.Save();
    }

    // called on slider change
    public void setSFXVolume(float volume)
    {
        sfxVolume = volume;
        playerSfxSource.volume = sfxVolume;
        worldSfxSource.volume = sfxVolume;
        uiSfxSource.volume = sfxVolume;
        PlayerPrefs.SetFloat("SFXvolume", sfxVolume);
        PlayerPrefs.Save();
    }

    private void loadVolumeSettings()
    {
        musicVolume = PlayerPrefs.GetFloat("musicVolume", 0.75f);
        sfxVolume = PlayerPrefs.GetFloat("SFXvolume", 1f);

        // music
        musicSource.volume = musicVolume;
        // sfx
        playerSfxSource.volume = sfxVolume;
        worldSfxSource.volume = sfxVolume;
        uiSfxSource.volume = sfxVolume;

        if (musicSlider != null && sfxSlider != null)
        {
            musicSlider.value = musicVolume;
            sfxSlider.value = sfxVolume;
        }
    }
    public void saveVolumeSettings()
    {
        try
        {
            musicSlider = GameObject.Find("music slider").GetComponent<Slider>();
            sfxSlider = GameObject.Find("sfx slider").GetComponent<Slider>();
        }
        catch (System.Exception)
        {
            Debug.LogWarning($"found no sliders");
            // throw;
        }
        if (musicSlider != null && sfxSlider != null)
        {
            PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
            PlayerPrefs.SetFloat("SFXvolume", sfxSlider.value);
        }
    }

    void OnApplicationQuit()
    {
        saveVolumeSettings();
    }
}