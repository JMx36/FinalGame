using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Josh Castillo

public class ParallaxBackground : MonoBehaviour
{
    //allows us to have a parrallax effect in both axis
    [SerializeField]
    private Vector2 parallaxEffectMultplier;

    [SerializeField] 
    private float maximumDif;
    private float totalDelta;

    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
        totalDelta = 0;
    }
    private void LateUpdate()
    {       
        //gets the difference in displacement and moves the gameobject accordinly times the parallax multiplier 
        Vector3 deltaMovemenet = cameraTransform.position - lastCameraPosition;
        totalDelta += deltaMovemenet.x;
        transform.position += new Vector3(deltaMovemenet.x * parallaxEffectMultplier.x, deltaMovemenet.y * parallaxEffectMultplier.y, transform.position.z);
        lastCameraPosition = cameraTransform.position;
        //Debug.Log(totalDelta + " " + maximumDif);
        if(Mathf.Abs(totalDelta) > maximumDif)
        {
            float offset = (cameraTransform.position.x - transform.position.x) % maximumDif;
            transform.position = new Vector3(cameraTransform.position.x + offset, transform.position.y, transform.position.z);
            totalDelta = 0;
        } 
    }
}
