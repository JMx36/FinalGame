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
    
    public static Player player; 

    // Start is called before the first frame update
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        isJumping = false;
        currentHealth = initHealth;
        player = this;
        alreadyOpened = false;
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(currentHealth);
        animator.SetFloat("Speed", Mathf.Abs (moveHorizontal));
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
    }

    private void FixedUpdate()
    {
        //Debug.Log(rb2d.velocity.x);

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
          //  Debug.Log("Landing");
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
            animator.SetBool("IsJumping", isJumping);
        }
    }
    public void ApplyDamage(int damage)
    {
        currentHealth -= damage;
        if  (currentHealth <= 0)
        {
          GameStateManager.m_Manager.LifeLost();
        }
    }

}
