using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLv1 : MonoBehaviour
{
    [SerializeField]
    private MonsterLv1 monsterLv1;

    [SerializeField]
    private int damageTaken;

    private int damage;
    private string enemyName;
    private int health;
    private int currentHealth; 
    private void Awake()
    {
        damage = monsterLv1.damage;
        health = monsterLv1.health;
        enemyName = monsterLv1.Name;
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
