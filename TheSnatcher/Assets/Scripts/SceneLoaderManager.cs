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
        Win_Screen,
        GameOver,
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
        StartCoroutine(LoadLevel(0, Scene.Win_Screen.ToString()));
    }
    public void Restart()
    {
        StartCoroutine(LoadLevel(0, Scene.LevelOne.ToString()));
    }

    public void LoadGameOver()
    {
        StartCoroutine(LoadLevel(0, Scene.GameOver.ToString()));
    }
    public void LoadScene()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1, ""));
    }
    IEnumerator LoadLevel(int levelIndex, string scene)
    {
        transition.SetTrigger("End");

        yield return new WaitForSeconds(transitionTime);

        if (levelIndex != 0)
            SceneManager.LoadScene(levelIndex);
        else
            SceneManager.LoadScene(scene);
    }
}
