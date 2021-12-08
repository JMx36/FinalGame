using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuteEnemies : MonoBehaviour
{
    //The unique scriptable object of each bear
    [SerializeField] private CuteEnemyObject enemy;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    //variables of the bear 
    private float speed;
    private float jumpForce;
    private int health;
    private int currentHealth;
    private int damage;
    private float maxSpeed;

    //player transform 
    private Transform player;

    //fixed value as it won't vary by the type of bear and it will not be able to be access through the inspector as it spawns it during playing time
    private float distance = 50f;
    
    //boolean that allows jumping
    private bool jump;

    //the direction the bear is facing
    private float direction;
   
    //Assigns deault values to the scriptableObject values
    void Awake()
    {
        //gets the player transform for the bear to go towards the player and distance check
        player = GameObject.FindGameObjectWithTag("Player").transform;
        speed = enemy.speed;
        maxSpeed = enemy.maxSpeed;
        jumpForce = enemy.jumpForce;
        health = enemy.health;
        damage = enemy.damage;
        //assigns health to currenthealth 
        currentHealth = health;
        
        //gets rigibody and sprite of the gameObject as it spawns
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //if statement to destroy bears that are too far from the player 
        if (Vector2.Distance(player.position, transform.position) > distance)
        {
            Destroy(gameObject);
        }
        direction = Mathf.Sign(player.position.x - transform.position.x);

        //flips the sprite so that it faces towards the player
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
            rb.velocity = Vector2.up * jumpForce;
            jump = false;
        }

        //max velocity check
        if(rb.velocity.x <= maxSpeed)
            rb.AddForce(Vector2.right * direction * speed);
    }

    public void ReduceHealth()
    {
        currentHealth -= 30;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "P_Bullet")
        {
            ReduceHealth();
        }
        if(collision.tag == "Player")
        {
            Player.player.ApplyDamage(damage);
            //setting velocity to zero to allow the player to out run the bear that just hit him
            rb.velocity = new Vector2(0, 0);
        }
        if(collision.tag == "spike")
        {
            ReduceHealth();
        }
        if (collision.tag == "Edge")
        {
            jump = true;
        }
    }
}
    