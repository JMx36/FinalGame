using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleOptions : MonoBehaviour
{
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
}
