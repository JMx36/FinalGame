using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
   // Start is called before the first frame update

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //  Debug.Log("Teleporting Player"); 
        if (collision.tag == "Player")
        {
            if (GameStateManager.m_GameState == GameStateManager.GAMESTATE.SecondLevel)
            {
                GameStateManager.ThirdLevel();
            }
            else if (GameStateManager.m_GameState == GameStateManager.GAMESTATE.FirstLevel && LeverManager.leverManager.leversComplete)
            {                
                    GameStateManager.SecondLevel();                     
            }
            else
                Debug.Log("Level non existent. Current GameState level is " + GameStateManager.m_GameState.ToString());
        }
    }
}
