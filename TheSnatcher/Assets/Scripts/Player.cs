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
    private float moveVertical;
    
    public static Player player; 

    // Start is called before the first frame update
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        isJumping = false;
        currentHealth = initHealth;
        player = this;
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
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            Debug.Log("Jumping");
            rb2d.velocity = Vector2.up * jumpForce;
        }
    }

    private void FixedUpdate()
    {
        if (rb2d.velocity.sqrMagnitude <= maxSpeed)
        {
            rb2d.AddForce(new Vector2(moveHorizontal, 0f) * movingSpeed);
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

    public void ApplyDamage(int damage)
    {
        currentHealth -= damage;
        if  (currentHealth <= 0)
        {
          GameStateManager.m_Manager.LifeLost();
        }
    }

}
