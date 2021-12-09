using System.Collections.Generic;
using UnityEngine;

//Emily Chavez
[System.Serializable]
public class Dialogue
{
    public string characterName;

    [TextArea(3, 10)] //The bounds of the text box
    public List<string> sentences = new List<string>(); 
}
