using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Emily Chavez
public class RedSuitGuy : MonoBehaviour
{
    [SerializeField]
    private Animator deathAni;
    [SerializeField]
    private float distance;
   
    private Transform player;

    private bool close;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (Vector2.Distance(player.position, transform.position) < distance)
        {
            close = true;
        }
        else
            close = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "P_Bullet" && close) 
        {
            deathAni.SetBool("IfKilled", true);
            GameStateManager.DialogueScene(); //triggers ending dialogue scene to play right after bullet and death animation play 
        }         
    }
}
