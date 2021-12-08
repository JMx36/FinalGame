using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionSettings : MonoBehaviour
{
    [SerializeField] private GameObject settings;
    [SerializeField] private AudioMixer audioMixer;

    public static OptionSettings optionSettings;

    public void Awake()
    {
        optionSettings = this;
    }
    public void Open()
    {
        GameStateManager.Pause();
        AudioManager.audioManager.PlayAudio("Option Button");
        Player.player.AllowMovement(false);
        settings.SetActive(true);
    }
    public void Close()
    {
        GameStateManager.Pause();
        AudioManager.audioManager.PlayAudio("Option Button");
        Player.player.AllowMovement(true);
        settings.SetActive(false);
    }
    public void AdjVolume(float volLevel)
    {
        // Debug.Log(volLevel);
        audioMixer.SetFloat("MasterVolume", volLevel);
    }
    public void Save()
    {        
        AudioManager.audioManager.PlayAudio("Option Button");
        Debug.Log("Saving");
        GameStateManager.SaveGame();
    }
    public void Retry()
    {
        AudioManager.audioManager.PlayAudio("Option Button");
        settings.SetActive(false);
        GameStateManager.Pause();
        GameStateManager.Restart();
    }
    public void Quit()
    {
        AudioManager.audioManager.PlayAudio("Option Button");
        //  Debug.Log("Quitting");
        GameStateManager.QuitGame();
    }
}