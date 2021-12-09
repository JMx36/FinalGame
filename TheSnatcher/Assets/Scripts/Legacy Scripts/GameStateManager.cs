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

    //bool for pausing game while in Options
    private bool paused;

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
        else
        {
            Destroy(this);
        }

        m_GameState = GAMESTATE.Menu; //Default GameState

        if (!m_Manager.resume)
        {
            m_Manager.currentLives = m_Manager.startingLives; //Assigning lives }
            m_Manager.resume = false;
        }

        //overrides the default GAMESTATE mode
        if (TestingLevel > 0 && TestingLevel <= 3 && testing)
        {
            Debug.Log("Testing Level " + TestingLevel);
            m_GameState = (GAMESTATE)TestingLevel;
            if (!m_Manager.resume)
            {
                m_Manager.currentLives = m_Manager.startingLives;
                m_Manager.resume = false;
            }
         

            Debug.Log("Initial state: " + m_GameState.ToString());
            Debug.Log("Live " + m_Manager.currentLives);
           // Debug.Log(m_Manager.currentLives);
        }

        paused = false;

        if (PlayerPrefs.GetInt("Won") == (int)GAMESTATE.PlayerWon && m_GameState == GAMESTATE.Menu)
        {
            Debug.Log("Player has won. Unlocking new option");
            TitleOptions.titleOptions.NewOption();
        }
        else
        {
            Debug.Log("Player didnt win, so new button will not appeared. GameState: " + m_GameState.ToString() + ". Lives " + m_Manager.currentLives);
        }

        Debug.Log("GameState: " + m_GameState.ToString() + ". Lives " + m_Manager.currentLives);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) //restart button
        {
            Restart();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            MainMenu();
        }
        if (Input.anyKeyDown && m_GameState == GAMESTATE.PlayerWon)
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
            Debug.Log("DialogueScene GameState is " + m_GameState);
        }

        SceneLoaderManager.m_SceneManager.LoadScene(); 
    }

    public static void FirstLevel()
    {
        m_GameState = GAMESTATE.FirstLevel;
        SceneLoaderManager.m_SceneManager.FirstLevel();
        PlayerPrefs.SetInt("State", (int)m_GameState);
        PlayerPrefs.SetInt("Lives", m_Manager.currentLives);
    }         

    public static void SecondLevel()
    {
        m_GameState = GAMESTATE.SecondLevel;
        SceneLoaderManager.m_SceneManager.SecondLevel();
        PlayerPrefs.SetInt("State", (int)m_GameState);
        PlayerPrefs.SetInt("Lives", m_Manager.currentLives);       
    }

    public static void ThirdLevel()
    {
        m_GameState = GAMESTATE.ThirdLevel;
        SceneLoaderManager.m_SceneManager.ThirdLevel();
        PlayerPrefs.SetInt("State", (int)m_GameState);
        PlayerPrefs.SetInt("Lives", m_Manager.currentLives);
    }

    public static void PlayerWins()
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
        if (!m_Manager.paused)
        {
            Debug.Log("pausing");
            Time.timeScale = 0;
            m_Manager.paused = true;
        }
        else 
        {
            Debug.Log("unpausing");
            Time.timeScale = 1;
            m_Manager.paused = false;
        }
    }
    public void LifeLost()
    {
        AudioManager.audioManager.PlayAudio("Lost Life");
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
            //Loads GameOver scene
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
        
        //Resets PlayerPrefs Lives and State to default values of Menu and 3 lives respectively 
        PlayerPrefs.SetInt("State", (int)m_GameState); 
        PlayerPrefs.SetInt("Lives", m_Manager.currentLives);

        Debug.Log(" NewGame Log: Lives " + m_Manager.currentLives + ". Player state is " + m_GameState.ToString());
    }

    public static void SaveGame() //saves the lives and level of the player when run
    {
        PlayerPrefs.SetInt("State", (int)m_GameState);
        PlayerPrefs.SetInt("Lives", m_Manager.currentLives);
    }
    //Resumes to the last level of the player and assigns the corresponding lives if saved. Also works as a restart method. 
    public static void Resume()
    {
        //values between 1 and 3 used in if as it corresponds to the value of the leves in the enum
        if (PlayerPrefs.GetInt("State") >= 1 && PlayerPrefs.GetInt("State") <= 3)
        {
            m_GameState = (GAMESTATE)PlayerPrefs.GetInt("State");
            Debug.Log("Player is in state " + m_GameState.ToString());
            m_Manager.resume = true;
            Debug.Log(m_Manager.resume);

            switch (m_GameState)
            {
                case GAMESTATE.FirstLevel:
                    FirstLevel();
                    break;
                case GAMESTATE.SecondLevel:
                    SecondLevel();
                    break;
                case GAMESTATE.ThirdLevel:
                    ThirdLevel();
                    break;
            }

            m_Manager.currentLives = PlayerPrefs.GetInt("Lives");
            Debug.Log(m_Manager.currentLives);
        }
        else
            Debug.Log("There is no saving data to resume: Error when resume button clicked");
    }
    public static void Restart()
    {
        m_GameState = (GAMESTATE)PlayerPrefs.GetInt("State");
        switch (m_GameState)
        {
            case GAMESTATE.FirstLevel:
                FirstLevel();
                break;
            case GAMESTATE.SecondLevel:
                SecondLevel();
                break;
            case GAMESTATE.ThirdLevel:
                ThirdLevel();
                break;
        }
        m_Manager.currentLives = m_Manager.startingLives;
    }
    public static void QuitGame()
    {
        Application.Quit();
    }
}


