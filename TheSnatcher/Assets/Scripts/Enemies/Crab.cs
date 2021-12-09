using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Emily Chavez
public class Crab : MonoBehaviour
{
    [SerializeField]
    private Monsters monster;

    [SerializeField]
    private GameObject enemyBullet;

    private Transform player;

    [SerializeField]
    private int damageTaken;

    private int damage;
    private string enemyName;
    private int health;
    private int currentHealth;

    //This is to prevent the enemy from spamming bullets. It will shoot them every two seconds 
    private float timeToShoot;
    public float coolDown;

    //These values are for detecting player 
    private float distance;
    public float noticed;

    private void Awake()
    {
        damage = monster.damage;
        health = monster.health;
        enemyName = monster.Name;
        currentHealth = health;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    
    public void Update()
    {
        // To stop the crab from spamming bullet every frame 
        distance = Vector2.Distance(transform.position, player.position);
      
        if (timeToShoot < 0 && distance < noticed)
        {
            //Spawns a bullet and assings it to this Gameobject 
            GameObject enemyB = Instantiate(enemyBullet, transform.position, transform.rotation);
            
            //The EnemyBullet script componet is stored in bullet and then the damage is set
            EnemyBullet bullet = enemyB.GetComponent<EnemyBullet>();
            bullet.SetDamage(damage);
            timeToShoot = coolDown;
        }
        else
        {
            //reduces timeToShoot by TIme.dealTime each frame
            timeToShoot -= Time.deltaTime;
        }

        if (currentHealth <= 0)
            Destroy(gameObject);
    }
    //damage to crab after getting hit by a bullet
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
}

