using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    private static bool ActivePortal;

    public static PortalScript portal;

        
    // Start is called before the first frame update
    void Start()
    {
        ActivePortal = false;
        portal = this;
    }


    public void Activate(bool state)
    {
        ActivePortal = state;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (ActivePortal)
        {
            if (collision.tag == "Player")
            {
                Debug.Log("Teleporting Player");
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
