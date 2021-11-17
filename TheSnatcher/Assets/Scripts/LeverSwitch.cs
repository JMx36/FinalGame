using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverSwitch : MonoBehaviour
{
    [SerializeField]
    private GameObject portal;

    private GameObject player;

    private float dist;

    private Lever[] levers;

    private int OpenPortal;

   /* private SpriteRenderer change;
    private Sprite PortalOffSprite, OnPortalSprite;
*/
    [SerializeField] private List<Sprite> portals = new List<Sprite> { };
    // Start is called before the first frame update
    void Start()
    {   
        player = GameObject.FindWithTag("Player");
        levers = GameObject.FindObjectsOfType<Lever>();
      /*  change = GetComponent<SpriteRenderer>();
        PortalOffSprite = Resources.Load<Sprite>("PortalOff");
        PortalOffSprite = Resources.Load<Sprite>("PortalOff");
        change.sprite = PortalOffSprite;
*/
        /* Debug.Log(player.name);*/
        //levers in array FindGameObjects.FindWithTag

    }
    //collsion on key
    // Update is called once per frame
    void Update()
    {
        if (player && levers.Length > 0)
        {
            foreach (Lever lever in levers)
            {
                dist = Vector3.Distance(player.transform.position, lever.transform.position);
               /* Debug.Log(dist);*/
                if (dist < 2 && Input.GetKeyDown(KeyCode.E))
                {
                    if (lever.IsOpened == false)
                    {
                        Debug.Log("Lever opened");
                        lever.IsOpened = true;
                        OpenPortal++;
                        Debug.Log(OpenPortal);
                    }
                    else
                    {
                        //add sound effect
                    }                  
                }
      
            } 
            if (OpenPortal == levers.Length)
            {
                PortalScript.portal.Activate(true);
                portal.SetActive(false);
               // portal.transform.Rotate(0f, 80f, 0f);
                OpenPortal = 1;
            }
        }

    }
    //array for levers and foreach loop in the update function
   // calculate 

    /*private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Collided");
        if (Input.GetKey(KeyCode.E) && collision.tag == "Player")
        {
            if (isSwitched == false) 
            {
                Debug.Log("Lever pressed");
                portal.transform.Rotate(0f, 80f, 0f);
                isSwitched = true;
            }
            else
            {
                Debug.Log("Lever pressed");
                portal.transform.Rotate(0f, -80f, 0f);
                isSwitched = false;
            }*/
    // let player know if its switched
    // }
    // check to subtract the distance between player and the gameobject in update if not a trigger 
    //}
}
