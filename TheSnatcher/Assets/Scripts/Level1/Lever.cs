using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Emily Chavez
public class Lever : MonoBehaviour
{
    public bool isOpened { get;  private set;} //made public so that the lever manager can access it

    private SpriteRenderer spriteRenderer; //spriteRender component for changing the sprite after it is flipped

    [SerializeField]
    private GameObject player;

    //floats for controlling the distance in which the player can activate the levers
    private float dist;
    [SerializeField]
    private float desiredDis;

    // Start is called before the first frame update
    void Start()
    {        
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        dist = Vector3.Distance(player.transform.position, transform.position);

        //testing purposes 
        //Debug.Log(dist); 

        if (dist < desiredDis && Input.GetKeyDown(KeyCode.E))
        {
            if (isOpened == false)
            {
                isOpened = true; //set to true so player can interact with it anymore

                spriteRenderer.sprite = LeverManager.leverManager.sprites[0]; //changes the sprite
 
                LeverManager.leverManager.CheckLevers(); 
            }
            
            else
            {
                //plays sound from the AudioManager
                AudioManager.audioManager.PlayAudio("Lever Done");
            }
        }
    }

}
