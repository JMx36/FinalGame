using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverManager : MonoBehaviour
{
    public GameObject portalOn;
    public GameObject portalOff;
    public bool leversComplete { get; private set; }

    private Lever[] levers;
    public List<Sprite> sprites = new List<Sprite> { };
   
    public static LeverManager leverManager;

    // Start is called before the first frame update
    void Start()
    {      
        leverManager = this;
        leversComplete = false;
        levers = FindObjectsOfType<Lever>();
    }
    public void CheckLevers()
    {
        leversComplete = true;
        foreach (Lever lever in levers)
        {
            if (!lever.isOpened) leversComplete = false;
        }

        if (leversComplete)
        {
            TurnPortalOn();
        }
    }
    public void TurnPortalOn()
    {
            portalOff.SetActive(false); //deactivates the Portal that is turned off
            portalOn.SetActive(true); //activates the Portal that is on
    }
    public void TurnPortalOff()
    {
            portalOn.SetActive(false); //deactivates the Portal that is turned on
            portalOff.SetActive(true); //activates the Portal that is off        
    }
}
