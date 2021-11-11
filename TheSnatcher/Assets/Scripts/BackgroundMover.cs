using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMover : MonoBehaviour
{
    private float zCord;
    public GameObject Camera;

    [SerializeField]
    public float movementAffect = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        zCord = transform.position.z;
        transform.position = new Vector3(Camera.transform.position.x, Camera.transform.position.y, zCord);
    }
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(Camera.transform.position.x, Camera.transform.position.y, transform.position.z);
        transform.position = transform.position * movementAffect;
    }
}
