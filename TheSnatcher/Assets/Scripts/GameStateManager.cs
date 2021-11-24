using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField]
    private List<string> m_level = new List<string>();
    [SerializeField]
    private string m_MainMenu;

    private static GameStateManager m_Manager;

    [SerializeField]
    private int startingLives;
    private int currentLives; // added current lives for NewGame and Pause methods below
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
      m_Manager.currentLives = m_Manager.startingLives; /// need to declare current lives and starting lives in a different playerhealth class? 
        SceneLoaderManager.m_SceneManager.LoadScene(); // to not start at anything but the starting checkpoint
        

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
        m_GameState = GAMESTATE.Playing;
        SceneLoaderManager.m_SceneManager.Restart();
    }
    public static void Resume()
    { 
        m_GameState = GAMESTATE.Playing;
        SceneLoaderManager.m_SceneManager.LoadScene();
    }

        public static void SaveGame() // Emily added body for PlayerPrefs save method
        {
        //add other items here

        Lever[] levers = FindObjectsOfType<Lever>();
            foreach (Lever lever in levers)
            {
                if (lever.isOpened)
                {
                    PlayerPrefs.SetInt(lever.name, 1);
                }
                else
                {
                    PlayerPrefs.SetInt(lever.name, 0);
                }
            }
   

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
    public static void LifeLost()
    {
        m_Manager.currentLives -= 1;
        if (m_Manager.currentLives <= 0)
        {
            GameStateManager.GameOver();
        }
        else
        {
            GameStateManager.Resume(); 
        } 
    }
}


