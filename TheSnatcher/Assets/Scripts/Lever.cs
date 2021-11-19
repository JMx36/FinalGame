using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public bool isOpened { get; private set;}
    private SpriteRenderer spriteRenderer; 

    [SerializeField]
    private GameObject player;
    private  float dist;
    // Start is called before the first frame update
    void Start()
    {
        isOpened = false;
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
               // Debug.Log("Lever opened ");
                isOpened = true;
                spriteRenderer.sprite = LeverManager.leverManager.sprites[1];
            }
            else
            {
                //add sound effects
            }
        }
    }

}
