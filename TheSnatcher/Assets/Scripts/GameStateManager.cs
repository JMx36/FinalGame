using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager m_Manager;

    [SerializeField]
    private int startingLives;
    private int currentLives; // added current lives for NewGame and Pause methods below

    public int TestingLevel; //meant for testing GameState

    private bool resume;


    public enum GAMESTATE
    {
        Menu,
        FirstLevel,
        SecondLevel,
        ThirdLevel,
        Paused,      
        PlayerWon,        
        PlayerLost
    }

    public static GAMESTATE m_GameState { get; private set; }

    private void Awake()
    {
        if (m_Manager == null)
        {
            m_Manager = this;
            DontDestroyOnLoad(m_Manager);
        }
        else Destroy(this);
        
        //if statement to check if player saved progress or won the game on a previous play through
        //still have to test this
        if(PlayerPrefs.GetInt("Won") == 5)
        {
            m_GameState = GAMESTATE.PlayerWon;
            Debug.Log("Player has won.Unlocking new option");
        }
        else if(PlayerPrefs.GetInt("State") >= 1 && PlayerPrefs.GetInt("State") <= 3)
        {
            m_GameState = (GAMESTATE)PlayerPrefs.GetInt("State");
            Debug.Log("Player is in state " + m_GameState.ToString());
        }
        else
        {
            m_GameState = GAMESTATE.Menu;
            m_Manager.currentLives = m_Manager.startingLives;
            Debug.Log("Player didnt save. State:" + m_GameState.ToString() + "Lives " + m_Manager.currentLives);
        }
        
        //overrides the "else" above
        if (TestingLevel != 0 && TestingLevel <= 3 && !m_Manager.resume)
        {
            m_GameState = (GAMESTATE)TestingLevel;
            Debug.Log("Initial state: " + m_GameState.ToString());
            m_Manager.currentLives = m_Manager.startingLives;
            Debug.Log(m_Manager.currentLives);
        }

        else if (m_Manager.resume)
        {
            InGameUI.inGameUI.Resume(m_Manager.currentLives);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }
        if (Input.GetKeyDown(KeyCode.R)) //restart button
        {
            // Restart();
            Resume();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            MainMenu();
        }
    }
    /// <summary>
    /// Game States of the player. Useful to know on what level the player was before quitting game (only applicable if the player saves). 
    /// </summary>

    public static void MainMenu()
    {
        m_GameState = GAMESTATE.Menu;
        SceneLoaderManager.m_SceneManager.LoadMainMenu();
    }

    public static void FirstLevel()
    {
        m_GameState = GAMESTATE.FirstLevel;
        SceneLoaderManager.m_SceneManager.LoadScene(); 
    }         

    public static void SecondLevel()
    {
        m_GameState = GAMESTATE.SecondLevel;
        SceneLoaderManager.m_SceneManager.SecondLevel();
    }

    public static void ThirdLevel()
    {
        m_GameState = GAMESTATE.SecondLevel;
        SceneLoaderManager.m_SceneManager.ThirdLevel();
    }
    public static void PlayerWins() //Call this when the player wins to unlock ability to choose levels. Still have to test this
    {
        m_GameState = GAMESTATE.PlayerWon;
        SceneLoaderManager.m_SceneManager.LoadWin();
        PlayerPrefs.SetInt("Won", (int)m_GameState);
    }
    public static void GameOver()
    {
        m_GameState = GAMESTATE.PlayerLost;
        SceneLoaderManager.m_SceneManager.LoadGameOver();
    }

    /// <summary>
    /// Interactions with UI and lives 
    /// </summary>
    
    public static void Pause() //still have to fix this
    {       
        if (m_GameState != GAMESTATE.Paused)
        {
            //create a provisional GameState to save the GameState of the game when paused
            m_GameState = GAMESTATE.Paused;
            Time.timeScale = 0;
        }
        else
        {
            m_GameState = GAMESTATE.FirstLevel; //chagned Playing to FirstLevel
            Time.timeScale = 1;  
        }
    }
    public void LifeLost()
    {
       // Debug.Log(m_Manager.currentLives);
        if (m_Manager.currentLives > 1)
        {
         //   Debug.Log("Losing life");
            m_Manager.currentLives -= 1;
            InGameUI.inGameUI.OnLoseLife();
        }
        else
        {
            //for Quality of Live purposes, so that it takes the last life image away.  
            InGameUI.inGameUI.OnLoseLife(); 
            //
            GameOver();
        }
    }

    /// <summary>
    /// Player progression, Restarts and Quitting
    /// </summary>
     
    //Resest Playerfebs and starts a new game
    public static void NewGame()
    {
        FirstLevel();
        m_Manager.currentLives = m_Manager.startingLives;
        InGameUI.inGameUI.NewGame();
        PlayerPrefs.SetInt("State", (int)m_GameState);
        PlayerPrefs.SetInt("Lives", m_Manager.currentLives);
        Debug.Log("Lives " + m_Manager.currentLives + ". Player state is " + m_GameState.ToString());
    }

    public static void SaveGame() //saves the lives and level of the player when run
    {
        PlayerPrefs.SetInt("State", (int)m_GameState);
        PlayerPrefs.SetInt("Lives", m_Manager.currentLives);
    }
    public void LeverSave() //maybe have this return a value if all complete;
    {
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
    public static void Restart() //Restarts the level the player is on
    {
        // Debug.Log(m_GameState.ToString());
        if (m_GameState == GAMESTATE.PlayerLost)
        {
            MainMenu();
            m_Manager.currentLives = m_Manager.startingLives; //Resets the amount of lives to its intial value every time the player Restarts       
        }
        else
        {
            SceneLoaderManager.m_SceneManager.Restart();
        }
    }

    //Resumes to the last level of the player and assigns the corresponding lives if saved
    public static void Resume()
    {
        m_Manager.resume = true;
        switch (m_GameState)
        {
            case GAMESTATE.FirstLevel:
                FirstLevel();
              //  m_Manager.currentLives = PlayerPrefs.GetInt("Lives");
                break;
            case GAMESTATE.SecondLevel:
                SecondLevel();
              //  m_Manager.currentLives = PlayerPrefs.GetInt("Lives");
                break;
            case GAMESTATE.ThirdLevel:
                ThirdLevel();
              //  m_Manager.currentLives = PlayerPrefs.GetInt("Lives");
                break;
            default:
                MainMenu(); //Maybe have a display box that says the player did not save?
                Debug.Log("Level not saved");
                break;
        }
        if(m_GameState != GAMESTATE.Menu)
        {
            m_Manager.currentLives = PlayerPrefs.GetInt("Lives");          
        }
    }
    public static void QuitGame()
    {
        Application.Quit();
    }
}


