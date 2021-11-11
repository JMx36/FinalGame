using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    [SerializeField]
    private List<string> m_level = new List<string>();
    [SerializeField]
    private string m_MainMenu;

    private static GameStateManager m_Manager;

    enum GAMESTATE
    {
        Menu,
        Playing,
        Paused,
        GameOver,
        PlayerWon
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }
        if (Input.GetKeyDown(KeyCode.R)) //restart button
        {
            Restart();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            MainMenu();
        }
    }

    private static GAMESTATE m_GameState;

    private void Awake()
    {
        if (m_Manager == null)
        {
            m_Manager = this;
            DontDestroyOnLoad(m_Manager);
        }
        else Destroy(this);
    }

    public static void NewGame()
    {
        m_GameState = GAMESTATE.Playing;
        SceneLoaderManager.m_SceneManager.LoadScene();
    }

    public static void MainMenu()
    {
        m_GameState = GAMESTATE.Menu;
        SceneLoaderManager.m_SceneManager.LoadMainMenu();
    }

    public static void Pause()
    {
        if (m_GameState == GAMESTATE.Playing)
        {
            m_GameState = GAMESTATE.Paused;
            Time.timeScale = 0;
        }
        else
        {
            m_GameState = GAMESTATE.Playing;
            Time.timeScale = 1;
        }
    }
    public static void Restart()
    {
        SceneLoaderManager.m_SceneManager.Restart();
    }
    public static void PlayerWins()
    {
        m_GameState = GAMESTATE.PlayerWon;
        SceneLoaderManager.m_SceneManager.LoadWin();
    }
    public static void GameOver()
    {
        m_GameState = GAMESTATE.GameOver;
        SceneLoaderManager.m_SceneManager.LoadGameOver();
    }
    public static void QuitGame()
    {
        Application.Quit();
    }
}

