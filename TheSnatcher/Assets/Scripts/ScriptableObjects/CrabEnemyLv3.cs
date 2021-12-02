using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CrabEnemyLv3", menuName = "Crabs")]
public class CrabEnemyLv3 : ScriptableObject
{
    public string Name;
    public GameObject Monster;
    public float speed;
    public int health;
    public int damage;
}
