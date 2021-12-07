using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderManager : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    public Animator transition;
    public float transitionTime = 1f;
    public static SceneLoaderManager m_SceneManager;
    public enum Scene
    {
        MainMenu,
        LevelOne,
        LevelTwo,
        LevelThree,
        WinScreen,
        GameOverScreen,
    }
    public void Awake()
    {
        m_SceneManager = this;
    }
    public void LoadMainMenu()
    {
        StartCoroutine(LoadLevel(0, Scene.MainMenu.ToString()));
    }
    public void LoadWin()
    {
        StartCoroutine(LoadLevel(0, Scene.WinScreen.ToString()));
    }
    public void Restart()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex, ""));
    }
    public void LoadGameOver()
    {
        StartCoroutine(LoadLevel(0, Scene.GameOverScreen.ToString()));
    }
    public void LoadScene()
    {
        //Debug.Log("Number of scenes " + SceneManager.sceneCountInBuildSettings);
        if(SceneManager.sceneCountInBuildSettings - 1 < SceneManager.GetActiveScene().buildIndex + 1)
        {
            Debug.Log("Build index out of range");
        }
        else 
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1, ""));
    }
    public void FirstLevel()
    {
        StartCoroutine(LoadLevel(0, Scene.LevelOne.ToString()));
    }
    public void SecondLevel()
    {
        StartCoroutine(LoadLevel(0, Scene.LevelTwo.ToString()));
    }
    public void ThirdLevel()
    {
        StartCoroutine(LoadLevel(0, Scene.LevelThree.ToString()));
    }


    IEnumerator LoadLevel(int levelIndex, string scene) //animation 
    {
        transition.SetTrigger("End");

        yield return new WaitForSeconds(transitionTime);

        if (levelIndex != 0)
            SceneManager.LoadScene(levelIndex);
        else
            SceneManager.LoadScene(scene);
    }
}
