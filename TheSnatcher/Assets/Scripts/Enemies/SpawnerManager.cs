using System.Collections.Generic;
using UnityEngine;
//Josh Castillo

public class SpawnerManager : MonoBehaviour
{
    [SerializeField]    
    private List<GameObject> enemies = new List<GameObject> { }; //List of the bear enemies
    [SerializeField] private float delayTime;
    [SerializeField] private float startTime;

    //stops the spawner from initiating again
    private bool pass = true;
    private void Update()
    {
        if (Player.player.GetMovement() && pass)
        {
            pass = false;
            Debug.Log("Invoking in 2");
            InvokeRepeating("SpawnEnemies", startTime, delayTime);          
        }
    }
    public void SpawnEnemies()
    {
        //there is a random chance of spawning a type of bear. Brow are the most common, green medium, and red rare
        int random = Random.Range(0, 10);
        if (random == 0) Instantiate(enemies[random], transform);
        if (random > 1 &&  random <= 4) Instantiate(enemies[1], transform);
        if (random > 4 && random <= 9) Instantiate(enemies[2], transform);
    }
}
