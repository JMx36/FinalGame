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

    void Start()
    {   
        player = GameObject.FindWithTag("Player");
        levers = GameObject.FindObjectsOfType<Lever>();
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
               // Debug.Log(dist);
                if (dist < 2 && Input.GetKeyDown(KeyCode.E))
                {
                    if (lever.isOpened == false)
                    {
                        //Debug.Log("Lever opened");
                        lever.isOpened = true;
                        OpenPortal++;
                        //Debug.Log(OpenPortal);
                    }
                    else
                    {
                       //add sound effects
                    }                  
                }
                
            } 
            if (OpenPortal == levers.Length)
            {
                LeverManager.leverManager.TurnPortalOn();
                OpenPortal = 1;
            }
        }

    }

}
