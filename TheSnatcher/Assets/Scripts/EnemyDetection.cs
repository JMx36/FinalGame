using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    private GameObject enemy;

    private GameObject player;

    [SerializeField]
    private float enemyDistance = 5f;

    private float playerDistance;

    [SerializeField]
    private float enemyMovement = 0.01f;

    [SerializeField]
    private float enemyAttackRange = 7f;

    private float maxMovementLeft;

    private float maxMovementRight;

    private bool moveLeft;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        maxMovementLeft = transform.position.x - enemyDistance;
        maxMovementRight = transform.position.x + enemyDistance;

        // have enemy move certain distance over and over
        //hardcode amount? like 5f to left and right?
        /*enemy.transform.position*/
    }

    // Update is called once per frame
    void Update()
    {
        playerDistance = Vector3.Distance(player.transform.position, transform.position);

        if (playerDistance < enemyAttackRange)
        {
            if (player.transform.position.x < transform.position.x)
            {
                MoveEnemy(-1);
            }
            else
            {
                MoveEnemy(1);
            }
            //transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemyMovement);
        }
        else
        {
            if (moveLeft)
            {
                if (transform.position.x > maxMovementLeft)
                {
                    MoveEnemy(-1);
                }
                else
                {
                    moveLeft = false;
                }
            }
            else
            {
                if (transform.position.x < maxMovementRight)
                {
                    MoveEnemy(1) ;
                }
                else
                {
                    moveLeft = true;
                }
            }

        }
    }
    private void MoveEnemy(int direction)
    {
        transform.position = new Vector3(transform.position.x + (direction * enemyMovement), transform.position.y, transform.position.z);
    }   
}
