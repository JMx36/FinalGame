using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject> { };
    [SerializeField] private float delayTime;
    [SerializeField] private float startTime;
    // Start is called before the first frame update
    void Awake()
    {
        InvokeRepeating("SpawnEnemies", startTime, delayTime);
    }
    private void FixedUpdate()
    {
        //Follow Player Script
    }

    public void SpawnEnemies()
    {
        int random = Random.Range(0, 10);
        if (random == 0) Instantiate(enemies[random], transform);
        if (random > 1 &&  random <= 4) Instantiate(enemies[1], transform);
        if (random > 4 && random <= 9) Instantiate(enemies[2], transform);
    }
}
