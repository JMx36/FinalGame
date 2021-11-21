using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb2d;

    [SerializeField] private float movingSpeed;
    [SerializeField] private float JumpForce;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float initHealth;


    private bool isJumping;

    private float moveHorizontal;
    private float moveVertical;
    private float currentHealth;




    // Start is called before the first frame update
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();

        isJumping = false;

        currentHealth = initHealth;
    }

    // Update is called once per frame
    void Update()
    {

        moveHorizontal = Input.GetAxisRaw("Horizontal");
        //  Debug.Log(rb2d.velocity.sqrMagnitude);


        moveVertical = Input.GetAxisRaw("Vertical");

    }

    private void FixedUpdate()
    {
        if (rb2d.velocity.sqrMagnitude <= maxSpeed)
        {
            rb2d.AddForce(new Vector2(moveHorizontal, 0f) * movingSpeed);
        }
        if (!isJumping)
        {
            rb2d.AddForce(new Vector2(0f, moveVertical) * JumpForce);
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
