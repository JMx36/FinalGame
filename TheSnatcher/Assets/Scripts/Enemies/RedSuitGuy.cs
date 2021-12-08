using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Emily Chavez
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

        GameStateManager.DialogueScene(); //triggers ending dialogue scene to play right after bullet and death animation play 
    }



}
