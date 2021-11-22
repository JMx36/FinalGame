using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public Animator animator;
    private SpriteRenderer sprite; 

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
        sprite = GetComponent<SpriteRenderer>();
/*
        int checkpointID = PlayerPrefs.GetInt("Checkpoint"); // addded player instantiated it will get checkpoint
        Checkpoint[] checkpoints = FindObjectsOfType<Checkpoint>(); // added will get list of objects
        foreach (Checkpoint point in checkpoints) // added 
        {
            if (point.ID == checkpointID)
            {
                transform.position = point.transform.position; // added moves to whereever the checkpoint is at
            }
        }*/
       
        isJumping = false;
        currentHealth = initHealth;
    }

    // Update is called once per frame
    void Update()
    {
       animator.SetFloat("Speed",Mathf.Abs (moveHorizontal));
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        if (moveHorizontal > 0)
            sprite.flipX = false;
        else if (moveHorizontal < 0)
        {
            sprite.flipX = true;
        }
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

           animator.SetBool ("IsJumping", isJumping);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Surface")
        {
            isJumping = true;
            animator.SetBool("IsJumping", isJumping);
        }
    }
}
