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
        Player.player.AllowMovement(false);
        settings.SetActive(true);
    }
    public void Close()
    {
        Player.player.AllowMovement(true);
        settings.SetActive(false);
    }
    public void AdjVolume(float volLevel)
    {
        Debug.Log(volLevel);
        //audioMixer.SetFloat("MasterVolume", volLevel);
    }

    public void Save()
    {
        Debug.Log("Saving");
        GameStateManager.SaveGame();
    }

    public void Quit()
    {
        Debug.Log("Quitting");
        GameStateManager.QuitGame();
    }

}
