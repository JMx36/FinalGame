using UnityEngine;
using UnityEngine.SceneManagement; // added

public class Player : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public Animator animator;
    private SpriteRenderer sprite;

    [SerializeField] 
    private float movingSpeed;
    [SerializeField] 
     private float jumpForce;
    [SerializeField] 
    private float maxSpeed;
    [SerializeField] 
    private int initHealth;

    private int currentHealth;
    
    private bool isJumping;

    private float moveHorizontal;

    private float currentXvelocity;

    private bool alreadyOpened;

    private bool allowMovement;
    
    public static Player player; 

    void Awake()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        isJumping = false;
        currentHealth = initHealth;
        player = this;
        alreadyOpened = false;
        allowMovement = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (allowMovement)
        {
            animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));
            moveHorizontal = Input.GetAxisRaw("Horizontal");

            if (moveHorizontal > 0)
                sprite.flipX = false;

            else if (moveHorizontal < 0)
            {
                sprite.flipX = true;
            }

            if (Input.GetAxisRaw("Jump") > 0 && !isJumping)
            {
                //  Debug.Log("Jumping");                    
                rb2d.velocity = new Vector2(currentXvelocity, jumpForce);
            }
        }
       

        //If statements to open and close options
        if (Input.GetKeyDown(KeyCode.Escape) && !alreadyOpened)
        {
            MenuSettings.menuSettings.Open();
            alreadyOpened = true;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && alreadyOpened)
        {
            MenuSettings.menuSettings.Close();
            alreadyOpened = false;
        }


        //Testing

        if (Mathf.Abs(rb2d.velocity.y) > 0)
        {
            //Debug.Log(rb2d.velocity.y);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ApplyDamage(100);
        }
    }

    private void FixedUpdate()
    {
      //  Debug.Log(rb2d.velocity.x);

        if (rb2d.velocity.sqrMagnitude <= maxSpeed)
        {
            rb2d.AddForce(new Vector2(moveHorizontal, 0f) * movingSpeed);
            
        }
        currentXvelocity = rb2d.velocity.x;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Surface")
        {
          // Debug.Log("Landing");
            isJumping = false;
          //this is to keep the velocity of the character constant after landing
            rb2d.velocity = new Vector2(currentXvelocity, 0);
            
            animator.SetBool ("IsJumping", isJumping);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Surface")
        {
            isJumping = true;
            animator.SetFloat("Speed", Mathf.Abs(rb2d.velocity.y));
            animator.SetBool("IsJumping", isJumping);
        }
    }
    public void ApplyDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
          GameStateManager.m_Manager.LifeLost();
          currentHealth = initHealth;
          Debug.Log(currentHealth);
        }
    }
    public void AllowMovement(bool allow)
    {
        allowMovement = allow;
    }
    public bool GetMovement()
    {
        return allowMovement;
    }
}