using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CuteEnemy", menuName = "CuteEnemy")]
public class CuteEnemyObject : ScriptableObject
{
    public string Name;
    public GameObject Enemy;
    public float speed;
    public float jumpForce;
    public float health;
    public float damage;
}
