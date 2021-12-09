using UnityEngine;

//Josh Castillo
public class InGameUI : MonoBehaviour
{
    [SerializeField]
    private GameObject[] lives;

    public static InGameUI inGameUI;

    public void Start()
    {
        inGameUI = this;
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

    //Displays the amount of lives at the beginning of each level
    public void Resume(int numberOfLives) 
    {
        numberOfLives--; //substracting by 1 to fit with the array index
        for(int i = lives.Length - 1; i > numberOfLives; i--)
        {
            lives[i].SetActive(false);
        }
    }
}
