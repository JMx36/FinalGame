using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float speed = 5f;
    public Rigidbody2D player;
    Vector2 ShipMovement;
    // Update is called once per frame
    void Update()
    {
        ShipMovement.x = Input.GetAxisRaw("Horizontal");
        ShipMovement.y = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate()
    {
        player.MovePosition(player.position + ShipMovement * speed * Time.deltaTime);
    }
}


