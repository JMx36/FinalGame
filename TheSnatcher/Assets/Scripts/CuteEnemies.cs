using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuteEnemies : MonoBehaviour
{
    [SerializeField] private CuteEnemyObject enemy;
    private float speed;
    private float jumpForce;
    private float health;
    private float damage;
    private Transform player; 
   
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        speed = enemy.speed;
        jumpForce = enemy.jumpForce;
        health = enemy.health;
        damage = enemy.damage;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }
}
