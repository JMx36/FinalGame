using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CuteEnemy", menuName = "CuteEnemy")]
public class CuteEnemyObject : ScriptableObject
{
    public string Name;
    public GameObject Enemy; //ToDo: change this to sprite once the spirtes are added to the folder
    public float speed;
    public float jumpForce;
    public int health;
    public int damage;
    public float maxSpeed;
    public float unSpawnDistance;
}
