using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public bool isOpened { get;  set;} // took off private set and changed to public
    private SpriteRenderer spriteRenderer; 

    [SerializeField]
    private GameObject player;

   /* [SerializeField]
    private int leverNum; //must be greater than 0 */

    private  float dist;
    private LeverManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponentInParent<LeverManager>();
       int levervalue = PlayerPrefs.GetInt(this.name);
        if (levervalue == 0)
        {
            isOpened = false;
            
        }
        else
        {
            isOpened = true;
        }
        
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
                /*PlayerPrefs.SetInt(name, leverNum);*/
                manager.CheckLevers();
            }
            
            else
            {
                //add sound effects
            }
        }
    }

}
