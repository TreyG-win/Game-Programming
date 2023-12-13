using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SettingsPopup : MonoBehaviour
{

    [SerializeField] AudioClip sound;

    //This allows the button to mute the audio
    public void OnSoundToggle()
    {
        Managers.Audio.soundMute = !Managers.Audio.soundMute;
        Managers.Audio.PlaySound(sound);
    }
    //Allows the audio to be adjusted by the slider
    public void OnSoundValue(float volume)
    {
        Managers.Audio.soundVolume = volume;
    }
    //Opens the popup
    public void Open()
    {
        gameObject.SetActive(true);
    }
    //Closes the popup
    public void Close()
    {
        gameObject.SetActive(false);
    }
    //After the player changes their name, it will appear in the terminal.
    public void OnSubmitName(string name)
    {
        Debug.Log(name);
    }
    //Allows the music to be muted by a toggle function
    public void OnMusicToggle()
    {
        Managers.Audio.musicMute = !Managers.Audio.musicMute;
        Managers.Audio.PlaySound(sound);
    }
    //Adjusts the volume of the music by a set value
    public void OnMusicValue(float volume)
    {
        Managers.Audio.musicVolume = volume;
    }
    //Allows music to be played with the buttons based on the number associated with the track
    public void OnPlayMusic(int selector)
    {
        Managers.Audio.PlaySound(sound);

        switch (selector)
        {
            case 1:
                Managers.Audio.PlayIntroMusic();
                break;
            case 2:
                Managers.Audio.PlayLevelMusic();
                break;
            default:
                Managers.Audio.StopMusic();
                break;

        }
    }
}
