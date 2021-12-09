using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Emily Chavez
public class Octopus : MonoBehaviour
{
    [SerializeField]
    private Monsters monster;

    [SerializeField]
    private int damageTaken;

    private int damage;
    private string enemyName;
    private int health;
    private int currentHealth; 

    private void Awake()
    {
        damage = monster.damage;
        health = monster.health;
        enemyName = monster.Name;
        currentHealth = health;
    }
    //damge to octopus after getting hit by player's bullet
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "P_Bullet")
        {
            currentHealth -= damageTaken;
        }

        if (collision.gameObject.tag == "Player")
        {
            Player.player.ApplyDamage(damage);
        }

    }
    //when octopus is killed
    public void Update()
    {
        if (currentHealth <= 0)
            Destroy(gameObject);
    }
}
