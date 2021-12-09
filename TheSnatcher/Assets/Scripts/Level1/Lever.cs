using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Emily Chavez
public class Lever : MonoBehaviour
{
    public bool isOpened { get;  private set;} 
    private SpriteRenderer spriteRenderer; 

    [SerializeField]
    private GameObject player;

    private  float dist;

    // Start is called before the first frame update
    void Start()
    {        
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        dist = Vector3.Distance(player.transform.position, transform.position);
        //Debug.Log(dist);
        if (dist < 2 && Input.GetKeyDown(KeyCode.E))
        {
            if (isOpened == false)
            {
                isOpened = true;
                spriteRenderer.sprite = LeverManager.leverManager.sprites[0];
 
                LeverManager.leverManager.CheckLevers();
            }
            
            else
            {
                AudioManager.audioManager.PlayAudio("Lever Done");
            }
        }
    }

}
