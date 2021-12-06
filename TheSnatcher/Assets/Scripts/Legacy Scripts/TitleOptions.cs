using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleOptions : MonoBehaviour
{
    public void NewGame()
    {
        GameStateManager.NewGame();
    }

    public void QuitGame()
    {
        GameStateManager.QuitGame();
    }

    public void Resume()
    {
        GameStateManager.Resume();
    }

    public void ReturnToMenu()
    {
        GameStateManager.MainMenu();
    }
}
