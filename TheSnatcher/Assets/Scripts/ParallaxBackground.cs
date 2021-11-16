using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField]
    private float parallaxEffectMultplier;

    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
    }
    private void Update()
    {
        Vector3 deltaMovemenet = cameraTransform.position - lastCameraPosition;
        
        transform.position += deltaMovemenet * parallaxEffectMultplier;
        lastCameraPosition = cameraTransform.position;
    }
}
