using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Natalie;
    public Vector3 offset;

    
    private void FixedUpdate()
    {
        transform.position = Natalie.position;
    }


}
