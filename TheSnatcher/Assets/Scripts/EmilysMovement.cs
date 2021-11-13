using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmilysMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;

    [SerializeField]
    private float movingSpeed;

    [SerializeField]
    private float JumpForce;

    private bool isJumping;
    private float moveHorizontal;
    private float moveVertical;
  
       
    // Start is called before the first frame update
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();

        isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (moveHorizontal > 0.1f || moveHorizontal < -0.1f)
        {
            rb2d.AddForce(new Vector2(moveHorizontal * movingSpeed, 0f), ForceMode2D.Impulse);

        }
        if (!isJumping && moveVertical > 0.1f)
        {
            rb2d.AddForce(new Vector2(0f, moveVertical * JumpForce), ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Surface")
        {
            isJumping = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Surface")
        {
            isJumping = true;
        }
    }

}
