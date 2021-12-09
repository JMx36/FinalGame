using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Emily Chavez
[System.Serializable]
public class Dialogue
{
    public string characterName;

    [TextArea(3, 10)] //3 as minimum and  10 as maximum amount of text groups allowed
    public List<string> sentences = new List<string>(); 
}
