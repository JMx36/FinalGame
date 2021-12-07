using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
   // Start is called before the first frame update

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (LeverManager.leverManager.leversComplete)
        {
            if (collision.tag == "Player")
            {
                //  Debug.Log("Teleporting Player"); 
                if (GameStateManager.m_GameState == GameStateManager.GAMESTATE.FirstLevel)
                {
                   GameStateManager.SecondLevel();
                    
                }
                else if (GameStateManager.m_GameState == GameStateManager.GAMESTATE.SecondLevel)
                {
                    GameStateManager.ThirdLevel();
                }
                else
                    Debug.Log("Level non existent. Current GameState level is " + GameStateManager.m_GameState.ToString());                    
            }
        }
    }
}
