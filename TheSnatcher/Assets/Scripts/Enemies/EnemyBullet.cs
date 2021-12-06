using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    private Transform player;
    private Vector2 target;
    private int damage;

    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
      
    }

    // Update is called once per frame
    public void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (transform.position.x == target.x && transform.position.y == target.y)
        {
           Destroy(gameObject);
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
          //  Debug.Log(damage);           
            Player.player.ApplyDamage(damage);
            Destroy(gameObject);
        }           
    }
    public void SetDamage(int enemyDamage)
    {
        damage = enemyDamage;
    }
}
