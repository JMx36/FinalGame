using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public void Update()
    {
        if (currentHealth <= 0)
            Destroy(gameObject);
    }
}
