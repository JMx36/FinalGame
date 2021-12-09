using UnityEngine;
using UnityEngine.Audio;
//Josh Castillo

public class OptionSettings : MonoBehaviour
{
    [SerializeField] private GameObject settings;
    [SerializeField] private AudioMixer audioMixer;

    private bool paused;

    public static OptionSettings optionSettings;

    public void Awake()
    {
        optionSettings = this;
    }
    public void Open()
    {
        optionSettings.Pause();
        AudioManager.audioManager.PlayAudio("Option Button");
        Player.player.AllowMovement(false);
        settings.SetActive(true);
    }
    public void Close()
    {
        optionSettings.Pause();
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
        optionSettings.Pause();
        GameStateManager.Restart();
    }
    public void Quit()
    {
        AudioManager.audioManager.PlayAudio("Option Button");
        //  Debug.Log("Quitting");
        GameStateManager.QuitGame();
    }
   
    public void Pause()
    {
        if (!optionSettings.paused)
        {
            Debug.Log("pausing");
            Time.timeScale = 0;
            optionSettings.paused = true;
        }
        else
        {
            Debug.Log("unpausing");
            Time.timeScale = 1;
            optionSettings.paused = false;
        }
    }
}
