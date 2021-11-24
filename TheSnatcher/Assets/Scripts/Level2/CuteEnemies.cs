using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuteEnemies : MonoBehaviour
{
    [SerializeField] private CuteEnemyObject enemy;
    private float speed;
    private float jumpForce;
    private float health;
    private float currentHealth;
    private float damage;
    private Transform player; 
   
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        speed = enemy.speed;
        jumpForce = enemy.jumpForce;
        health = enemy.health;
        damage = enemy.damage;
        //assigns health to currenthealth 
        currentHealth = health;
    }

    private void Update()
    {
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    public void ReduceHealth()
    {
        currentHealth -= 30;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "P_Bullet")
        {
            currentHealth -= 30;
        }
    }
}
    