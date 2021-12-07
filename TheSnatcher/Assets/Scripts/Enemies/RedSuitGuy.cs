using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSuitGuy : MonoBehaviour
{
    [SerializeField]
    private Animator deathAni;    
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "P_Bullet")
        {
            deathAni.SetBool("IfKilled", true);
        }

        GameStateManager.DialogueScene();
    }



}
