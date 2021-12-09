using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Emily Chavez 
[CreateAssetMenu (fileName = "Monsterslv1", menuName ="GreenMonsters" )]
//scriptable object for enemies in level 1 and 3
public class Monsters : ScriptableObject 
{
    public string Name;
    public GameObject Monster;
    public float speed;
    public int health;
    public int damage;
}
 

