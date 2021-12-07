using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager m_Manager;
    // Player's lives
    [SerializeField]
    private int startingLives;
    public int currentLives { get; private set; } 

    // Varaibles meant for testing purposes
    public int TestingLevel; 
    public bool testing;

    //Bools to change InGameUI (lives) to either a resume or newGame state at the beginning of each level
    public bool resume { get; private set; }

    public bool newGame { get; private set; }

    //Game states
    public enum GAMESTATE
    {
        Menu,
        FirstLevel,
        SecondLevel,
        ThirdLevel,
        Paused,
        StartDialogue,
        EndDialogue,
        PlayerWon,        
        PlayerLost
    }

    //Allows other scripts to access the current state of the game but without being able to change its value
    public static GAMESTATE m_GameState { get; private set; }

    private void Awake()
    {
        if (m_Manager == null)
        {
            m_Manager = this;
            DontDestroyOnLoad(m_Manager);
        }
        else Destroy(this);

        if (PlayerPrefs.GetInt("Won") == (int)GAMESTATE.PlayerWon)
        {
            m_GameState = GAMESTATE.PlayerWon;
            Debug.Log("Player has won. Unlocking new option");
        }
        else 
        {
            m_GameState = GAMESTATE.Menu;
            m_Manager.currentLives = m_Manager.startingLives;
            Debug.Log("Player didnt win, so new button will not appeared. GameState: " + m_GameState.ToString() + ". Lives " + m_Manager.currentLives);
        }

        //overrides the default GAMESTATE mode
        if (TestingLevel > 0 && TestingLevel <= 3 && testing)
        {
            Debug.Log("Testing Level " + TestingLevel);
            m_GameState = (GAMESTATE)TestingLevel;           
            m_Manager.currentLives = m_Manager.startingLives;

            Debug.Log("Initial state: " + m_GameState.ToString());
           // Debug.Log(m_Manager.currentLives);
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
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log(m_GameState.ToString());
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
    public static void DialogueScene()
    {
        if(m_GameState == GAMESTATE.Menu)
        {
            m_GameState = GAMESTATE.StartDialogue;
            Debug.Log("DialogueScene GameState is " + m_GameState);
        }
        else if(m_GameState == GAMESTATE.ThirdLevel)
        {
            m_GameState = GAMESTATE.EndDialogue;
            Debug.Log(m_GameState);
        }

        SceneLoaderManager.m_SceneManager.LoadScene(); 
    }

    public static void FirstLevel()
    {
        m_GameState = GAMESTATE.FirstLevel;
        SceneLoaderManager.m_SceneManager.FirstLevel(); 
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

    public void SetResumeBool(bool value)
    {
        m_Manager.resume = value;
    }
    public void SetNewGameBool(bool value)
    {
        m_Manager.newGame = value;
    }

    /// <summary>
    /// Player progression, Restarts and Quitting
    /// </summary>
     
    //Resest Playerfebs and starts a new game
    public static void NewGame()
    {
      //Resets values to default ones
        m_Manager.newGame = true;

        m_GameState = GAMESTATE.Menu;

        m_Manager.currentLives = m_Manager.startingLives;

        DialogueScene();       

   //   InGameUI.inGameUI.NewGame();
        PlayerPrefs.SetInt("State", (int)m_GameState); //test whether this works
        PlayerPrefs.SetInt("Lives", m_Manager.currentLives);
        Debug.Log(" NewGame Log: Lives " + m_Manager.currentLives + ". Player state is " + m_GameState.ToString());
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
        if (PlayerPrefs.GetInt("State") >= 1 && PlayerPrefs.GetInt("State") <= 3)
        {
            m_GameState = (GAMESTATE)PlayerPrefs.GetInt("State");
            //  Debug.Log("Player is in state " + m_GameState.ToString());
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
            }

            if (m_GameState != GAMESTATE.Menu)
            {
                m_Manager.currentLives = PlayerPrefs.GetInt("Lives");
            }
        }
        else
            Debug.Log("There is no saving data to resume: Error when resume button clicked");
    }
    public static void QuitGame()
    {
        Application.Quit();
    }
}


