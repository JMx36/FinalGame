using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleOptions : MonoBehaviour
{
    [SerializeField]
    private GameObject newOption;

    public static TitleOptions titleOptions;
    public void Start()
    {
        titleOptions = this;
    }
    public void NewGame()
    {
        AudioManager.audioManager.PlayAudio("Button Sound");
        GameStateManager.NewGame();
    }

    public void QuitGame()
    {
        AudioManager.audioManager.PlayAudio("Button Sound");
        GameStateManager.QuitGame();
    }

    public void Resume()
    {
        AudioManager.audioManager.PlayAudio("Button Sound");
        GameStateManager.Resume();
    }
    public void Retry()
    {
        AudioManager.audioManager.PlayAudio("Button Sound");
        GameStateManager.Resume();
    }

    public void ReturnToMenu()
    {
        AudioManager.audioManager.PlayAudio("Button Sound");
        GameStateManager.MainMenu();
    }
    public void NewOption()
    {
        newOption.SetActive(true);
    }

    public void LoadFirtLevel()
    {
        GameStateManager.FirstLevel();
    }
    public void LoadSecondLevel()
    {
        GameStateManager.SecondLevel();
    }
    public void LoadThirdLevel()
    {
        GameStateManager.ThirdLevel();
    }
}
