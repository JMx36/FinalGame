using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuteEnemies : MonoBehaviour
{
    [SerializeField] private CuteEnemyObject enemy;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    private float speed;
    private float jumpForce;
    private int health;
    private int currentHealth;
    private int damage;
    private float maxSpeed;

    private Transform player; 

    private bool jump;
    private float direction;
   
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        speed = enemy.speed;
        maxSpeed = enemy.maxSpeed;
        jumpForce = enemy.jumpForce;
        health = enemy.health;
        damage = enemy.damage;
        //assigns health to currenthealth 
        currentHealth = health;
        
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        direction = Mathf.Sign(player.position.x - transform.position.x);

        if (direction > 0)
        {
            sprite.flipX = false;
            //Debug.Log("Facing right");
        }
        else
        {
            sprite.flipX = true;
            //Debug.Log("Facing left");
        }

        if (currentHealth <= 0)
        {
                Destroy(gameObject);      
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {       
        if (jump)
        {
           // Debug.Log(rb.velocity.x);
            rb.velocity = Vector2.up * jumpForce;
           // Debug.Log("Jumping");
            //Debug.Log(rb.velocity.y);
            jump = false;
        }

        if(rb.velocity.x <= maxSpeed)
            rb.AddForce(Vector2.right * direction * speed);
    }

    public void ReduceHealth()
    {
        currentHealth -= 30;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "P_Bullet")
        {
            ReduceHealth();
        }
        if(collision.gameObject.tag == "Player")
        {
            Player.player.ApplyDamage(damage);
        }
        if (collision.gameObject.tag == "Edge")
        {
            jump = true;
        }
    }
}
    