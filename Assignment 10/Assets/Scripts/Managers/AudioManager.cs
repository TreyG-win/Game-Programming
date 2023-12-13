using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour, IGameManager
{

    [SerializeField] AudioSource soundSource;

    [SerializeField] AudioSource music1Source;
    [SerializeField] AudioSource music2Source;

    //Allows the user to enter the name of a track to add
    [SerializeField] string introBGMusic;
    [SerializeField] string levelBGMusic;

    private AudioSource activeMusic;
    private AudioSource inactiveMusic;

    public float crossFadeRate = 1.5f;
    private bool crossFading;

    private float _musicVolume;
    public float musicVolume {
        get { 
            return _musicVolume; 
        } 
        set {
            _musicVolume = value;

            if (music1Source != null && !crossFading)
            {
                music1Source.volume = _musicVolume;
                music2Source.volume = _musicVolume;
            }
        }
    }


    public ManagerStatus status { get; private set; }

    //Starts the sound manager
    public void Startup ()
    {

        Debug.Log("Audio manager starting...");

        status = ManagerStatus.Started;

        music1Source.ignoreListenerVolume = true;
        music2Source.ignoreListenerVolume = true;
        music1Source.ignoreListenerPause = true;
        music2Source.ignoreListenerPause = true;

        soundVolume = 1.0f;
        musicVolume = 1.0f;
        activeMusic = music1Source;
        inactiveMusic = music2Source;


    }

    //Adjusts the volume of the overall sound
    public float soundVolume
    {
        get { return AudioListener.volume; }
        set { AudioListener.volume = value; }
    }
    //Allows the audio to stop if the game is paused
    public bool soundMute
    {
        get { return AudioListener.pause; }
        set { AudioListener.pause = value; }
    }
    //Controls the audio that does not have a source
    public void PlaySound(AudioClip clip)
    {
        soundSource.PlayOneShot(clip);
    }
    //Loads the intro music from the Resources folder
    public void PlayIntroMusic()
    {
        PlayMusic(Resources.Load($"Music/{introBGMusic}") as AudioClip);
    }
    //Loads the level music from the Resources folder
    public void PlayLevelMusic()
    {
        PlayMusic(Resources.Load($"Music/{levelBGMusic}") as AudioClip);
    }
    //Plays the tracks that are selected through AudioSource.clip
    private void PlayMusic(AudioClip clip)
    {
        if (crossFading)
        {
            return;
        }
        StartCoroutine(CrossFadeMusic(clip));
    }
    //Stops the music
    public void StopMusic()
    {
        activeMusic.Stop();
        inactiveMusic.Stop();
    }

    //Allows the music to be muted with a toggle
    public bool musicMute
    {
        get
        {
            if (music1Source != null)
            {
                return music1Source.mute;
            }
            return false;
        }
        set
        {
            if (music1Source != null)
            {
                music1Source.mute = value;
                music2Source.mute = value;
            }
        }
    }
    //Allows the music to properly fade between each other instead of jarringly switch
    private IEnumerator CrossFadeMusic(AudioClip clip)
    {
        crossFading = true;

        inactiveMusic.clip = clip;
        inactiveMusic.volume = 0;
        inactiveMusic.Play();

        float scaledRate = crossFadeRate * musicVolume;
        while (activeMusic.volume > 0)
        {
            activeMusic.volume -= scaledRate * Time.deltaTime;
            inactiveMusic.volume += scaledRate * Time.deltaTime;

            yield return null;
        }

        AudioSource temp = activeMusic;
        activeMusic = inactiveMusic;
        activeMusic.volume = musicVolume;

        inactiveMusic = temp;
        inactiveMusic.Stop();

        crossFading = false;
    }




}
