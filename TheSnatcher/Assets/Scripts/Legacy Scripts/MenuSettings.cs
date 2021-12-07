using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MenuSettings : MonoBehaviour
{
    [SerializeField] private GameObject settings;
    [SerializeField] private AudioMixer audioMixer;

    public static MenuSettings menuSettings;

    public void Awake()
    {
        menuSettings = this;
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
        //  Debug.Log("Saving");
        GameStateManager.SaveGame();
    }
    public void Retry()
    {
        AudioManager.audioManager.PlayAudio("Option Button");
        GameStateManager.Restart();
    }
    public void Quit()
    {
        AudioManager.audioManager.PlayAudio("Option Button");
        //  Debug.Log("Quitting");
        GameStateManager.QuitGame();
    }
}
