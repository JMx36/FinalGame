using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//coded by Emily Elizabeth Chavez
public class EnemyDetection : MonoBehaviour
{
    private GameObject enemy;

    private GameObject player;

    private Rigidbody2D rb;

    [SerializeField]
    private float enemyDistance = 5f;

    private float playerDistance;

    [SerializeField]
    private float enemyMovement = 0.01f;

    [SerializeField]
    private float enemyAttackRange;

    private float maxMovementLeft;

    private float maxMovementRight;

    private bool moveLeft;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        // enemy travels left to right on a set path 
        maxMovementLeft = transform.position.x - enemyDistance; 
        maxMovementRight = transform.position.x + enemyDistance;
    }

    void Update()
    {
        playerDistance = Vector3.Distance(player.transform.position, transform.position);

        if (playerDistance < enemyAttackRange) // for when the player is close enough in the enemy's range
        {
            if (player.transform.position.x < transform.position.x)
            {
                MoveEnemy(-1); //makes enemy detect and follow player to the left 
            }
            else
            {
                MoveEnemy(1); //makes enemy detect and follow player to the right
            }
        }
        else
        {
            if (moveLeft)
            {
                if (transform.position.x > maxMovementLeft)
                {
                    MoveEnemy(-1); //moves left
                }
                else
                {
                    moveLeft = false; // moves right
                }
            }
            else
            {
                if (transform.position.x < maxMovementRight)
                {
                    MoveEnemy(1) ; //moves right
                }
                else
                {
                    moveLeft = true; //moves left
                }
            }

        }
    }
    private void MoveEnemy(int direction) 
    {
        rb.MovePosition(rb.position +  new Vector2(direction, 0) * enemyMovement * Time.fixedDeltaTime);
    }
}
