using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverManager : MonoBehaviour
{
    public GameObject portalOn;
    public GameObject portalOff;
    private bool leversComplete;

    private Lever[] levers;
   
    public static LeverManager leverManager;

    // Start is called before the first frame update
    void Start()
    {      
        leverManager = this;
        leversComplete = false;
        levers = FindObjectsOfType<Lever>();
    }
    // Update is called once per frame
    private void Update()
    {
        leversComplete = true;
        foreach(Lever lever in levers)
        {
          //  Debug.Log("Before !  " + lever.isOpened + " and After !" + !lever.isOpened);
            if (!lever.isOpened) leversComplete = false; 
        }
        if (leversComplete)
        {
            TurnPortalOn();
            PortalScript.portal.Activate(true);
        }       
    }
    public void TurnPortalOn()
    {
            PortalScript.portal.Activate(true);
            portalOff.SetActive(false); //deactivates the Portal that is turned off
            portalOn.SetActive(true); //activates the Portal that is on
    }
    public void TurnPortalOff()
    {
            PortalScript.portal.Activate(false);
            portalOn.SetActive(false); //deactivates the Portal that is turned on
            portalOff.SetActive(true); //activates the Portal that is off        
    }
}
