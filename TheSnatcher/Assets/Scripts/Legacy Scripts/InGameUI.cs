using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    [SerializeField]
    private GameObject[] lives;

    public static InGameUI inGameUI;

    public void Start()
    {
        inGameUI = this;
        // Debug.Log("InGameUI starting");
        if (GameStateManager.m_Manager.resume)
        {
            Debug.Log("Resuming lives");
            inGameUI.Resume(GameStateManager.m_Manager.currentLives);
            GameStateManager.m_Manager.SetResumeBool(false);
        }
        else if (GameStateManager.m_Manager.newGame)
        {
            Debug.Log("Reseting Lives for New Game");
            inGameUI.NewGame();
            GameStateManager.m_Manager.SetNewGameBool(false);
        }
        else
            inGameUI.Resume(GameStateManager.m_Manager.currentLives);
    }
    public void OnLoseLife()
    {
        int lose = 1;
        for(int i = lives.Length - 1; i >= 0; i--)
        {
            if (lives[i].activeInHierarchy && lose > 0)
            {
                //Debug.Log("Taking away a life");
                lives[i].SetActive(false);
                lose--;
            }             
        }
    }
    public void Resume(int numberOfLives) //I have to test this
    {
        numberOfLives--; //substracting by 1 to fit with the array index
        for(int i = lives.Length - 1; i > numberOfLives; i--)
        {
            lives[i].SetActive(false);
        }
    }
    //resets the amount of lives to show in the screen
    public void NewGame()
    {
        foreach(GameObject g in lives)
        {
            if (!g.activeInHierarchy)
            {
                g.SetActive(true);
            }
        }
    }
}
