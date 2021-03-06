using UnityEngine;
//Josh Castillo
public class GameStateManager : MonoBehaviour
{
    public static GameStateManager m_Manager;
    // Player's starting lives
    [SerializeField]
    private int startingLives;
    public int currentLives { get; private set; }

    //Varaibles meant for testing purposes
    [SerializeField]
    private int TestingLevel; 
    [SerializeField]
    private bool testing;

    //Bool for testing purposes
    public bool resume { get; private set; }

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

        m_Manager.currentLives = m_Manager.startingLives; //Assigning lives 

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

            Debug.Log("Initial state: " + m_GameState.ToString() + ".Lives " + m_Manager.currentLives);
        }

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
          //  Debug.Log("DialogueScene GameState is " + m_GameState);
        }
        else if(m_GameState == GAMESTATE.ThirdLevel)
        {
            m_GameState = GAMESTATE.EndDialogue;
          //  Debug.Log("DialogueScene GameState is " + m_GameState);
        }

        SceneLoaderManager.m_SceneManager.LoadScene(); 
    }

    public static void FirstLevel()
    {
        m_GameState = GAMESTATE.FirstLevel;
        SceneLoaderManager.m_SceneManager.FirstLevel();
        SaveGame();
    }         

    public static void SecondLevel()
    {
        m_GameState = GAMESTATE.SecondLevel;
        SceneLoaderManager.m_SceneManager.SecondLevel();
        SaveGame();
    }

    public static void ThirdLevel()
    {
        m_GameState = GAMESTATE.ThirdLevel;
        SceneLoaderManager.m_SceneManager.ThirdLevel();
        SaveGame();
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

    /// <summary>
    /// Player progression, Restarts and Quitting
    /// </summary>
     
    //Loads the Game from the Beginning
    public static void NewGame()
    {
        m_Manager.currentLives = m_Manager.startingLives;

        DialogueScene();       

        Debug.Log(" NewGame Log: Lives " + m_Manager.currentLives + ". Player state is " + m_GameState.ToString());
    }

    public static void SaveGame() //saves the lives and level of the player when called
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

            m_Manager.currentLives = PlayerPrefs.GetInt("Lives");

            m_Manager.resume = true;

            LoadByState();

            Debug.Log("Player is in state " + m_GameState.ToString() + ". Lives = " + m_Manager.currentLives);
        }
        else
            Debug.Log("There is no saving data to resume: Error when resume button clicked");
    }

    //Restarts the level where the player was last on and the lives
    public static void Restart()
    {
        m_GameState = (GAMESTATE)PlayerPrefs.GetInt("State");

        LoadByState();

        m_Manager.currentLives = m_Manager.startingLives;
    }

    //Loads a level based on the GameState
    public static void LoadByState()
    {
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
    }
    public static void QuitGame()
    {
        Application.Quit();
    }
}


