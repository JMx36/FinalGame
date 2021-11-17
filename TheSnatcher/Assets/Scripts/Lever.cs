using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public bool isOpened { get; set;}
    public GameObject player;
    float dist;
    // Start is called before the first frame update
    void Start()
    {
        isOpened = false;
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
            }
            else
            {
                //add sound effects
            }
        }
    }

}
