using UnityEngine;
//Josh Castillo

public class TitleOptions : MonoBehaviour
{
    [SerializeField]
    private GameObject newOption;

    [SerializeField]
    private Animator buttonAnim;

    private bool opened;

    public static TitleOptions titleOptions;
    public void Awake()
    {
        titleOptions = this;

        opened = false;
    }
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
    public void NewOption()
    {
        newOption.SetActive(true);
    }

    public void LevelButtonClicked()
    {
        if (!titleOptions.opened)
        {
           // Debug.Log("Appearing");
            buttonAnim.SetTrigger("Appear");
            titleOptions.opened = true;
        }
        else
        {
            //Debug.Log("Disappeaering");
            buttonAnim.SetTrigger("Appear");
            titleOptions.opened = false;
        }
    }
    public void LoadFirtLevel()
    {
        GameStateManager.FirstLevel();
    }
    public void LoadSecondLevel()
    {
        GameStateManager.SecondLevel();
    }
    public void LoadThirdLevel()
    {
        GameStateManager.ThirdLevel();
    }
}
