using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Monsterslv1", menuName ="GreenMonsters" )]
public class MonsterLv1 : ScriptableObject 
{
    public string Name;
    public GameObject Monster;
    public float speed;
    public float health;
    public float damage;
}
 

