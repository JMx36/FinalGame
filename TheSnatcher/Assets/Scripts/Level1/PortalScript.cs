using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
   // Start is called before the first frame update

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //  Debug.Log("Teleporting Player"); 
        if (collision.tag == "Player" && !collision.isTrigger)
        {
            if(LeverManager.leverManager == null && GameStateManager.m_GameState == GameStateManager.GAMESTATE.SecondLevel)
            {
              //  Debug.Log("Loading third level");
                GameStateManager.ThirdLevel();
                collision.gameObject.SetActive(false);
            }
            else if (GameStateManager.m_GameState == GameStateManager.GAMESTATE.FirstLevel && LeverManager.leverManager.leversComplete)
            {
               // Debug.Log("Loading Second level");
                    GameStateManager.SecondLevel();
                collision.gameObject.SetActive(false);
            }
            else
                Debug.Log("Level non existent. Current GameState level is " + GameStateManager.m_GameState.ToString());
        }
    }
}
