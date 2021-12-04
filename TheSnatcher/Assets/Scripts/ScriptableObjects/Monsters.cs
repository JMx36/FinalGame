using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Monsterslv1", menuName ="GreenMonsters" )]
public class Monsters : ScriptableObject 
{
    public string Name;
    public GameObject Monster;
    public float speed;
    public int health;
    public int damage;
}
 

